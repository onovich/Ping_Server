namespace Ping.Server {

    public class GameEntity {

        public RandomService random;
        public TimeService time;
        public GameFSMComponent FsmCom { get; private set; }

        public int Turn { get; private set; }

        public GameEntity() {
            FsmCom = new GameFSMComponent();
            time = new TimeService();
        }

        public GameFSMComponent FSM_GetComponent() {
            return FsmCom;
        }

        public void Time_Update(float deltaTime) {
            time.Update(deltaTime);
        }

        public void Time_Reset() {
            time.Reset();
        }

        public float Time_Get() {
            return time.GetTimestamp();
        }

        public void Turn_Inc() {
            Turn++;
        }

        public void Turn_Reset() {
            Turn = 0;
        }

    }

}