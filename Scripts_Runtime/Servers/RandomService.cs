using System;
using MortiseFrame.Abacus;

namespace Ping.Server {

    public class RandomService {

        System.Random random;
        public int seed;
        public int seedTimes;

        public RandomService(int seed, int seedTimes) {
            this.seed = seed;
            random = new System.Random(seed);
            for (int i = 0; i < seedTimes; i++) {
                random.Next();
            }
        }

        public FVector2 GetRandomDirection(FVector2 baseDir, float angleRange) {

            float randomAngle = (float)(random.NextDouble() * angleRange - angleRange / 2);
            float radian = randomAngle * FMath.Deg2Rad;
            float cos = FMath.Cos(radian);
            float sin = FMath.Sin(radian);
            FVector2 newDir = new FVector2(
                baseDir.x * cos - baseDir.y * sin,
                baseDir.x * sin + baseDir.y * cos
            );

            return newDir.normalized;
        }

    }

}