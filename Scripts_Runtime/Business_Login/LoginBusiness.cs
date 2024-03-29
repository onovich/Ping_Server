using System.Net.Sockets;
using Ping.Protocol;
using Ping.Server.Requests;
using MortiseFrame.Rill;

namespace Ping.Server.Business.Login {

    public static class LoginBusiness {

        public static void Enter(LoginBusinessContext ctx) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            fsmCom.WaitForJoin_Enter();
            RequestInfra.Start(ctx.reqInfraContext);
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

            if (!ctx.reqInfraContext.UserStatus_IsJoinReady(1) || !ctx.reqInfraContext.UserStatus_IsJoinReady(2)) {
                return;
            }

            PLog.Log("All Players Are Ready");
            var name1 = ctx.reqInfraContext.GetUserName(1);
            var name2 = ctx.reqInfraContext.GetUserName(2);
            RequestInfra.SendJoinRoomBroad(ctx.reqInfraContext, name1, name2);
            fsmCom.WaitForStart_Enter();
        }

        public static void Tick_WaitForStart(LoginBusinessContext ctx, float dt) {
            var fsmCom = ctx.loginEntity.FSM_GetComponent();
            if (fsmCom.WaitForStart_isEntering) {
                fsmCom.WaitForStart_isEntering = false;
                return;
            }

            if (!ctx.reqInfraContext.UserStatus_IsStartReady(1) || !ctx.reqInfraContext.UserStatus_IsStartReady(2)) {
                return;
            }

            RequestInfra.SendGameStartBroad(ctx.reqInfraContext);
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
        public static void On_ConnectReq(LoginBusinessContext ctx, ConnectionEntity conn) {
            RequestInfra.SendConnectRes(ctx.reqInfraContext, conn);
        }

        public static void On_ConnectResError(LoginBusinessContext ctx, string error) {
            PLog.LogError(error);
        }

        public static void On_JoinRoomReq(LoginBusinessContext ctx, JoinRoomReqMessage msg, ConnectionEntity conn) {
            var userName = msg.userName;
            ctx.reqInfraContext.AddUserName((byte)conn.ConnectionIndex, userName);
            ctx.reqInfraContext.UserStatus_SetJoinReady((byte)conn.ConnectionIndex);
            PLog.Log("On_JoinRoomReq:" + userName + " Is Join Ready");

            var player = new PlayerEntity();
            player.SetPlayerIndex(conn.ConnectionIndex);
            player.SetUserName(userName);
            ctx.Player_Add(player);
        }

        public static void On_GameStartReq(LoginBusinessContext ctx, GameStartReqMessage msg, ConnectionEntity conn) {
            ctx.reqInfraContext.UserStatus_SetStartReady((byte)conn.ConnectionIndex);
        }

        public static void TearDown(LoginBusinessContext ctx) {
            ExitLogin(ctx);
        }

    }

}