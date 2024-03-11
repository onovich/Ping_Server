using System;

namespace MortiseFrame.Pulse {

    public class PhysicalEventCenter {

        public PhysicalEventCenter() { }

        public Action<RigidbodyEntity, RigidbodyEntity> OnCollisionEnterHandle;
        public void OnCollisionEnter(RigidbodyEntity a, RigidbodyEntity b) {
            OnCollisionEnterHandle?.Invoke(a, b);
        }

        public Action<RigidbodyEntity, RigidbodyEntity> OnCollisionStayHandle;
        public void OnCollisionStay(RigidbodyEntity a, RigidbodyEntity b) {
            OnCollisionStayHandle?.Invoke(a, b);
        }

        public Action<RigidbodyEntity, RigidbodyEntity> OnCollisionExitHandle;
        public void OnCollisionExit(RigidbodyEntity a, RigidbodyEntity b) {
            OnCollisionExitHandle?.Invoke(a, b);
        }

        public Action<RigidbodyEntity, RigidbodyEntity> OnTriggerEnterHandle;
        public void OnTriggerEnter(RigidbodyEntity a, RigidbodyEntity b) {
            OnTriggerEnterHandle?.Invoke(a, b);
        }

        public Action<RigidbodyEntity, RigidbodyEntity> OnTriggerStayHandle;
        public void OnTriggerStay(RigidbodyEntity a, RigidbodyEntity b) {
            OnTriggerStayHandle?.Invoke(a, b);
        }

        public Action<RigidbodyEntity, RigidbodyEntity> OnTriggerExitHandle;
        public void OnTriggerExit(RigidbodyEntity a, RigidbodyEntity b) {
            OnTriggerExitHandle?.Invoke(a, b);
        }

        public void Clear() {
            OnCollisionEnterHandle = null;
            OnCollisionStayHandle = null;
            OnCollisionExitHandle = null;
            OnTriggerEnterHandle = null;
            OnTriggerStayHandle = null;
            OnTriggerExitHandle = null;
        }

    }

}