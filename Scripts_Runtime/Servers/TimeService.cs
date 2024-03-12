namespace Ping.Server {

    public class TimeService {

        float timestamp;

        public TimeService() {
            timestamp = 0;
        }

        public float GetTimestamp() {
            return timestamp;
        }

        public void Update(float deltaTime) {
            timestamp += deltaTime;
        }

        public void Reset() {
            timestamp = 0;
        }

    }

}