using Ping.Protocol;
using Ping.Server.Requests;

namespace Ping.Server.Business.Login {

    public class LoginBusiness {

        public static void Enter(LoginBusinessContext ctx) {
        }

        public static void PreTick(LoginBusinessContext ctx, float dt) {
            OnNetEvent(ctx, dt);
        }

        static void OnNetEvent(LoginBusinessContext ctx, float dt) {
            RequestInfra.Tick_Login(ctx.reqContext, dt);
        }

        public static void Exit(LoginBusinessContext ctx) {
        }

        public static void ExitLogin(LoginBusinessContext ctx) {
            Exit(ctx);
        }

        // On Net Req
        public static void OnNetReqConnect(LoginBusinessContext ctx) {
        }

        public static void OnNetReqLogin(LoginBusinessContext ctx, JoinRoomReqMessage msg) {
        }

        public static void TearDown(LoginBusinessContext ctx) {
            ExitLogin(ctx);
        }

    }

}