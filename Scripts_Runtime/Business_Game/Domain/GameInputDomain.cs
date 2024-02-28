namespace Ping.Server.Business.Game {

    public class GameInputDomain {

        public static void Player_BakeInput(GameBusinessContext ctx, float dt) {
            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.ProcessInput(ctx.mainCamera, dt);
        }

        public static void Owner_BakeInput(GameBusinessContext ctx, PaddleEntity owner) {
            InputEntity inputEntity = ctx.inputEntity;
            owner.Input_SetMoveAxis(inputEntity.Move_GetAxis());
        }

    }

}