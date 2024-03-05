namespace Ping.Server {

    public class PaddleFSMComponent {

        public PaddleFSMStatus Status { get; private set; }

        public bool Moving_isEntering { get; set; }

        public PaddleFSMComponent() { }

        public void Moving_Enter() {
            Status = PaddleFSMStatus.Moving;
            Moving_isEntering = true;
        }

    }

}