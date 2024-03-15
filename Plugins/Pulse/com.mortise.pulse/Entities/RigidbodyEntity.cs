using MortiseFrame.Abacus;

namespace MortiseFrame.Pulse {

    public class RigidbodyEntity {

        // Base Info
        uint id;
        public uint ID => id;

        // Holder
        int holderType;
        public int HolderType => holderType;

        int holderID;
        public int HolderID => holderID;

        // Layer
        uint layer;
        public uint Layer => layer;

        // Transform
        TFComponent transform;
        public TFComponent Transform => transform;

        // Shape
        IShape shape;
        public IShape Shape => shape;

        // Material
        float restitution;
        public float Restitution => restitution;

        // Trigger
        bool isTrigger;
        public bool IsTrigger => isTrigger;

        // Static
        bool isStatic;
        public bool IsStatic => isStatic;

        // Velocity
        FVector2 velocity;
        public FVector2 Velocity => velocity;

        // Mass
        float mass;
        public float Mass => mass;

        public RigidbodyEntity(FVector2 pos, IShape shape) {
            this.id = IDService.PickRigidbodyID();
            this.transform = new TFComponent(pos);
            this.shape = shape;
            this.velocity = FVector2.zero;
            this.mass = 0;
            this.isTrigger = false;
            this.isStatic = false;
            this.layer = 0;
            this.holderType = 0;
            this.holderID = 0;
        }

        // ID
        public void SetID(uint value) => id = value;

        // Trigger
        public void SetIsTrigger(bool value) => isTrigger = value;

        // Static 
        public void SetIsStatic(bool value) => isStatic = value;

        // Velocity
        public void SetVelocity(FVector2 value) => velocity = value;

        // Gravity
        public void SetMass(float value) => mass = value;

        // Holder
        public void SetHolder(int type, int id) {
            holderType = type;
            holderID = id;
        }

        // Layer
        public void SetLayer(uint value) => layer = value;

        // Material
        public void SetRestitution(float value) => restitution = value;

        // Transform
        public void SetPos(FVector2 pos) {
            transform.SetPos(pos);
        }

        public void SetRadAngle(float radAngle) {
            transform.SetRadAngle(radAngle);
        }

    }

}