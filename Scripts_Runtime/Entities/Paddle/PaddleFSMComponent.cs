namespace Ping.Server {

    public class PaddleFSMComponent {

        public PaddleFSMStatus status;

        public bool moving_isEntering;

        public PaddleFSMComponent() { }

        public void Moving_Enter() {
            status = PaddleFSMStatus.Moving;
            moving_isEntering = true;
        }

    }

}