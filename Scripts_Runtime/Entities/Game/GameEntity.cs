namespace Ping.Server {

    public class GameEntity {

        public RandomService random;
        public GameFSMComponent FsmCom { get; private set; }

        public int Turn { get; private set; }
        public float Time { get; private set; }

        public GameEntity() {
            FsmCom = new GameFSMComponent();
        }

        public GameFSMComponent FSM_GetComponent() {
            return FsmCom;
        }

        public void Turn_Inc() {
            Turn++;
        }

        public void Turn_Reset() {
            Turn = 0;
        }

        public void Time_Set(float t) {
            Time = t;
        }

    }

}