using System.Net.Sockets;
using Ping.Protocol;
using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public static class LoginBusiness {

        public static void Enter(LoginBusinessContext ctx) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            fsmCom.WaitForJoin_Enter();
        }

        public static void PreTickFSM(LoginBusinessContext ctx, float dt) {
            Tick_Any(ctx, dt);

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

        static void Tick_Any(LoginBusinessContext ctx, float dt) {
            OnNetEvent(ctx, dt);
        }

        public static void Tick_WaitForJoin(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.waitForJoin_isEntering) {
                fsmCom.waitForJoin_isEntering = false;
                return;
            }

            var client1 = ctx.reqContext.ClientState_GetByPlayerID(1);
            var client2 = ctx.reqContext.ClientState_GetByPlayerID(2);
            if (client1 == null || client2 == null) {
                PLog.LogError("Player1 or Player2 is null");
            }
            if (!client1.isJoinReady || !client2.isJoinReady) {
                return;
            }

            RequestJoinRoomDomain.Send_JoinRoomBroadRes(ctx.reqContext);
            fsmCom.WaitForStart_Enter();
        }

        public static void Tick_WaitForStart(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.waitForStart_isEntering) {
                fsmCom.waitForStart_isEntering = false;
                return;
            }

            var client1 = ctx.reqContext.ClientState_GetByPlayerID(1);
            var client2 = ctx.reqContext.ClientState_GetByPlayerID(2);
            if (client1 == null || client2 == null) {
                PLog.LogError("Player1 or Player2 is null");
            }
            if (!client1.isStartReady || !client2.isStartReady) {
                return;
            }

            RequestGameStartDomain.Send_GameStartBroadRes(ctx.reqContext);
            fsmCom.LoginDone_Enter();
        }

        public static void Tick_LoginDone(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.loginDone_isEntering) {
                fsmCom.loginDone_isEntering = false;
                var evt = ctx.evt;
                evt.LoginDone();
                return;
            }
        }

        static void OnNetEvent(LoginBusinessContext ctx, float dt) {
            RequestInfra.Tick_OnLogin(ctx.reqContext, dt);
        }

        public static void Exit(LoginBusinessContext ctx) {
        }

        public static void ExitLogin(LoginBusinessContext ctx) {
            Exit(ctx);
        }

        // On Net Req
        public static async void On_ConnectReq(LoginBusinessContext ctx, Socket clientfd) {
            await RequestConnectDomain.SendConnectResAsync(ctx.reqContext, clientfd);
        }

        public static void On_ConnectResError(LoginBusinessContext ctx, string error) {
            PLog.LogError(error);
        }

        public static void On_JoinRoomReq(LoginBusinessContext ctx, JoinRoomReqMessage msg, ClientStateEntity clientState) {
            var userName = msg.userName;
            var id = ctx.PickPlayerID();

            clientState.userName = userName;
            clientState.playerID = id;
            clientState.isJoinReady = true;
            clientState.isStartReady = false;
        }

        public static void On_GameStartReq(LoginBusinessContext ctx, GameStartReqMessage msg, ClientStateEntity clientState) {
            clientState.isStartReady = true;
        }

        public static void TearDown(LoginBusinessContext ctx) {
            ExitLogin(ctx);
        }

    }

}