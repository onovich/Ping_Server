namespace Ping.Server {

    public class GameEntity {

        public RandomService random;
        GameFSMComponent fsmComponent;

        int localOwnerPlayerID;

        int turn;
        float time;

        public GameEntity() {
            fsmComponent = new GameFSMComponent();
            FSM_EnterNotInGame();
        }

        public GameFSMStatus GetStatus() {
            return fsmComponent.status;
        }

        public void FSM_EnterGaming() {
            fsmComponent.Gaming_Enter();
        }

        public void FSM_EnterNotInGame() {
            fsmComponent.status = GameFSMStatus.NotInGame;
        }

        public GameFSMStatus FSM_GetStatus() {
            return fsmComponent.status;
        }

        public int GetTurn() {
            return turn;
        }

        public void IncTurn() {
            turn++;
        }

        public void ResetTurn() {
            turn = 0;
        }

        public void SetLocalOwnerPlayerID(int id) {
            localOwnerPlayerID = id;
        }

        public int GetLocalOwnerPlayerID() {
            return localOwnerPlayerID;
        }

        public void SetTime(float t) {
            time = t;
        }

        public float GetTime() {
            return time;
        }

    }

}