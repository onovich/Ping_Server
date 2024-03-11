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
        public FVector2 gate0Pos;
        public FVector2 gate0Size;
        public FVector2 gate1Pos;
        public FVector2 gate1Size;

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

            ByteWriter.Write<FVector2>(dst, wall0Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, wall0Size, ref offset);
            ByteWriter.Write<FVector2>(dst, wall1Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, wall1Size, ref offset);

            ByteWriter.Write<FVector2>(dst, gate0Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, gate0Size, ref offset);
            ByteWriter.Write<FVector2>(dst, gate1Pos, ref offset);
            ByteWriter.Write<FVector2>(dst, gate1Size, ref offset);

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

            wall0Pos = ByteReader.Read<FVector2>(src, ref offset);
            wall0Size = ByteReader.Read<FVector2>(src, ref offset);
            wall1Pos = ByteReader.Read<FVector2>(src, ref offset);
            wall1Size = ByteReader.Read<FVector2>(src, ref offset);

            gate0Pos = ByteReader.Read<FVector2>(src, ref offset);
            gate0Size = ByteReader.Read<FVector2>(src, ref offset);
            gate1Pos = ByteReader.Read<FVector2>(src, ref offset);
            gate1Size = ByteReader.Read<FVector2>(src, ref offset);

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