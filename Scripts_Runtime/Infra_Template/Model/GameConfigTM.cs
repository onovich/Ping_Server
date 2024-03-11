using MortiseFrame.Abacus;
using MortiseFrame.LitIO;

namespace Ping.Server {

    public struct GameConfigTM {

        // Field
        public FVector2 fieldBoundMax;
        public FVector2 fieldBoundMin;

        // Wall
        public FVector2 wall0Start;
        public FVector2 wall0End;
        public FVector2 wall1Start;
        public FVector2 wall1End;

        // Gate
        public FVector2 gate0Start;
        public FVector2 gate0End;
        public FVector2 gate1Start;
        public FVector2 gate1End;

        // Ball
        public float ballMoveSpeed;
        public float ballMoveSpeedMax;
        public float ballRadius;
        public float ballSpawnAngleRange;

        // Paddle
        public FVector2 player0PaddleSpawnPos;
        public FVector2 player1PaddleSpawnPos;
        public float paddleMoveSpeed;
        public float paddleMoveSpeedMax;
        public FVector2 paddleSize;

        public void WriteTo(byte[] dst, ref int offset) {

            ByteWriter.Write<FVector2>(dst, fieldBoundMax, ref offset);
            ByteWriter.Write<FVector2>(dst, fieldBoundMin, ref offset);

            ByteWriter.Write<FVector2>(dst, wall0Start, ref offset);
            ByteWriter.Write<FVector2>(dst, wall0End, ref offset);
            ByteWriter.Write<FVector2>(dst, wall1Start, ref offset);
            ByteWriter.Write<FVector2>(dst, wall1End, ref offset);

            ByteWriter.Write<FVector2>(dst, gate0Start, ref offset);
            ByteWriter.Write<FVector2>(dst, gate0End, ref offset);
            ByteWriter.Write<FVector2>(dst, gate1Start, ref offset);
            ByteWriter.Write<FVector2>(dst, gate1End, ref offset);

            ByteWriter.Write<float>(dst, ballMoveSpeed, ref offset);
            ByteWriter.Write<float>(dst, ballMoveSpeedMax, ref offset);
            ByteWriter.Write<float>(dst, ballRadius, ref offset);
            ByteWriter.Write<float>(dst, ballSpawnAngleRange, ref offset);

            ByteWriter.Write<FVector2>(dst, player0PaddleSpawnPos, ref offset);
            ByteWriter.Write<FVector2>(dst, player1PaddleSpawnPos, ref offset);
            ByteWriter.Write<float>(dst, paddleMoveSpeed, ref offset);
            ByteWriter.Write<float>(dst, paddleMoveSpeedMax, ref offset);
            ByteWriter.Write<FVector2>(dst, paddleSize, ref offset);

        }

        public void FromBytes(byte[] src, ref int offset) {

            fieldBoundMax = ByteReader.Read<FVector2>(src, ref offset);
            fieldBoundMin = ByteReader.Read<FVector2>(src, ref offset);

            wall0Start = ByteReader.Read<FVector2>(src, ref offset);
            wall0End = ByteReader.Read<FVector2>(src, ref offset);
            wall1Start = ByteReader.Read<FVector2>(src, ref offset);
            wall1End = ByteReader.Read<FVector2>(src, ref offset);

            gate0Start = ByteReader.Read<FVector2>(src, ref offset);
            gate0End = ByteReader.Read<FVector2>(src, ref offset);
            gate1Start = ByteReader.Read<FVector2>(src, ref offset);
            gate1End = ByteReader.Read<FVector2>(src, ref offset);

            ballMoveSpeed = ByteReader.Read<float>(src, ref offset);
            ballMoveSpeedMax = ByteReader.Read<float>(src, ref offset);
            ballRadius = ByteReader.Read<float>(src, ref offset);
            ballSpawnAngleRange = ByteReader.Read<float>(src, ref offset);

            player0PaddleSpawnPos = ByteReader.Read<FVector2>(src, ref offset);
            player1PaddleSpawnPos = ByteReader.Read<FVector2>(src, ref offset);
            paddleMoveSpeed = ByteReader.Read<float>(src, ref offset);
            paddleMoveSpeedMax = ByteReader.Read<float>(src, ref offset);
            paddleSize = ByteReader.Read<FVector2>(src, ref offset);

        }

    }

}