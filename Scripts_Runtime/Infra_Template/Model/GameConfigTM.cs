using MortiseFrame.Abacus;
using MortiseFrame.LitIO;

namespace Ping.Server {

    public struct GameConfigTM {

        // Wall
        public FVector2 wall0Pos;
        public FVector2 wall0Size;
        public FVector2 wall1Pos;
        public FVector2 wall1Size;

        // Gate
        public FVector2 gate1Pos;
        public FVector2 gate1Size;
        public FVector2 gate2Pos;
        public FVector2 gate2Size;

        // Ball
        public float ballMoveSpeed;
        public float ballMoveSpeedMax;
        public float ballRadius;
        public float ballSpawnAngleRange;

        // Paddle
        public FVector2 player1PaddleSpawnPos;
        public FVector2 player2PaddleSpawnPos;
        public float paddleMoveSpeed;
        public float paddleMoveSpeedMax;
        public FVector2 paddleSize;

        // Constraint
        public FVector2 constraint1Pos;
        public FVector2 constraint1Size;
        public FVector2 constraint2Pos;
        public FVector2 constraint2Size;

        public void FromBytes(byte[] src, ref int offset) {

            wall0Pos = ByteReader.Read<FVector2>(src, ref offset);
            wall0Size = ByteReader.Read<FVector2>(src, ref offset);
            wall1Pos = ByteReader.Read<FVector2>(src, ref offset);
            wall1Size = ByteReader.Read<FVector2>(src, ref offset);

            gate1Pos = ByteReader.Read<FVector2>(src, ref offset);
            gate1Size = ByteReader.Read<FVector2>(src, ref offset);
            gate2Pos = ByteReader.Read<FVector2>(src, ref offset);
            gate2Size = ByteReader.Read<FVector2>(src, ref offset);

            ballMoveSpeed = ByteReader.Read<float>(src, ref offset);
            ballMoveSpeedMax = ByteReader.Read<float>(src, ref offset);
            ballRadius = ByteReader.Read<float>(src, ref offset);
            ballSpawnAngleRange = ByteReader.Read<float>(src, ref offset);

            player1PaddleSpawnPos = ByteReader.Read<FVector2>(src, ref offset);
            player2PaddleSpawnPos = ByteReader.Read<FVector2>(src, ref offset);
            paddleMoveSpeed = ByteReader.Read<float>(src, ref offset);
            paddleMoveSpeedMax = ByteReader.Read<float>(src, ref offset);
            paddleSize = ByteReader.Read<FVector2>(src, ref offset);

            constraint1Pos = ByteReader.Read<FVector2>(src, ref offset);
            constraint1Size = ByteReader.Read<FVector2>(src, ref offset);
            constraint2Pos = ByteReader.Read<FVector2>(src, ref offset);
            constraint2Size = ByteReader.Read<FVector2>(src, ref offset);

        }

    }

}