using System.Net.Sockets;
using Ping.Protocol;
using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public static class LoginBusiness {

        public static void Enter(LoginBusinessContext ctx) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            fsmCom.WaitForJoin_Enter();
        }

        public static void Init(LoginBusinessContext ctx) { }

        public static void PreTickFSM(LoginBusinessContext ctx, float dt) {

            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            var status = fsmCom.Status;

            if (status == LoginFSMStatus.None) {
                return;
            }
            if (status == LoginFSMStatus.WaitForJoin) {
                Tick_WaitForJoin(ctx, dt);
            }
            if (status == LoginFSMStatus.WaitForStart) {
                Tick_WaitForStart(ctx, dt);
            }
            if (status == LoginFSMStatus.LoginDone) {
                Tick_LoginDone(ctx, dt);
            }
        }

        public static void Tick_WaitForJoin(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.WaitForJoin_isEntering) {
                fsmCom.WaitForJoin_isEntering = false;
                return;
            }

            var client1 = ctx.reqInfraContext.ClientState_GetByPlayerIndex(0);
            var client2 = ctx.reqInfraContext.ClientState_GetByPlayerIndex(1);
            if (client1 == null || client2 == null) {
                return;
            }
            if (!client1.isJoinReady || !client2.isJoinReady) {
                return;
            }

            RequestJoinRoomDomain.Send_JoinRoomBroadRes(ctx.reqInfraContext);
            fsmCom.WaitForStart_Enter();
        }

        public static void Tick_WaitForStart(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.WaitForStart_isEntering) {
                fsmCom.WaitForStart_isEntering = false;
                return;
            }

            var client1 = ctx.reqInfraContext.ClientState_GetByPlayerIndex(0);
            var client2 = ctx.reqInfraContext.ClientState_GetByPlayerIndex(1);
            if (client1 == null || client2 == null) {
                PLog.LogError("player0 or player1 is null");
            }
            if (!client1.isStartReady || !client2.isStartReady) {
                return;
            }

            RequestGameStartDomain.Send_GameStartBroadRes(ctx.reqInfraContext);
            fsmCom.LoginDone_Enter();
        }

        public static void Tick_LoginDone(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.LoginDone_isEntering) {
                fsmCom.LoginDone_isEntering = false;
                var evt = ctx.evt;
                evt.LoginDone();
                return;
            }
        }

        public static void Exit(LoginBusinessContext ctx) {
        }

        public static void ExitLogin(LoginBusinessContext ctx) {
            Exit(ctx);
        }

        // On Net Req
        public static void On_ConnectReq(LoginBusinessContext ctx, ClientStateEntity clientState) {
            RequestConnectDomain.SendConnectResAsync(ctx.reqInfraContext, clientState);
        }

        public static void On_ConnectResError(LoginBusinessContext ctx, string error) {
            PLog.LogError(error);
        }

        public static void On_JoinRoomReq(LoginBusinessContext ctx, JoinRoomReqMessage msg, ClientStateEntity clientState) {
            var userName = msg.userName;
            clientState.userName = userName;
            clientState.isJoinReady = true;
            clientState.isStartReady = false;
            PLog.Log("On_JoinRoomReq: " + userName + " Is Join Ready");

            var player = new PlayerEntity();
            player.SetPlayerIndex(clientState.playerIndex);
            player.SetUserName(userName);
            ctx.Player_Add(player);
        }

        public static void On_GameStartReq(LoginBusinessContext ctx, GameStartReqMessage msg, ClientStateEntity clientState) {
            clientState.isStartReady = true;
        }

        public static void TearDown(LoginBusinessContext ctx) {
            ExitLogin(ctx);
        }

    }

}