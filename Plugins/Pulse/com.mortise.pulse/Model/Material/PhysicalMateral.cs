namespace MortiseFrame.Pulse {

    public class PhysicalMaterial {

        // 动态摩擦力
        float dynamicFriction;
        public float DynamicFriction => dynamicFriction;

        // 弹性
        float restitution;
        public float Restitution => restitution;

        public PhysicalMaterial() {
            dynamicFriction = 0f;
            restitution = 1f;
        }

        public void SetDynamicFriction(float dynamicFriction) {
            this.dynamicFriction = dynamicFriction;
        }

        public void SetRestitution(float restitution) {
            this.restitution = restitution;
        }

    }

}