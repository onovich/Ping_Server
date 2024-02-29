using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ping.Server {

    public class Entry {

        static void Main(string[] args) {

            var entry = new ServerMain();
            float targetFrameRate = 60f;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            float currentTime = (float)stopWatch.Elapsed.TotalSeconds;
            float lastTime = currentTime;

            float targetDt = 1.0f / targetFrameRate;
            float restTime = 0.0f;
            float fixedDt = 0.02f;

            // ResetInput
            entry.ResetInput();

            // ProcessInput
            entry.ProcessInput();

            while (true) {

                currentTime = (float)stopWatch.Elapsed.TotalSeconds;

                float dt = currentTime - lastTime;
                restTime += dt;

                // PreTick
                entry.PreTick(dt);

                // FixedTick
                if (restTime <= fixedDt) {
                    entry.FixedTick(restTime);
                    restTime = 0;
                } else {
                    while (restTime >= fixedDt) {
                        entry.FixedTick(fixedDt);
                        restTime -= fixedDt;
                    }
                }

                // LateTick
                entry.LateTick(dt);

                // Sleep
                lastTime = currentTime;
                currentTime = (float)stopWatch.Elapsed.TotalSeconds;
                dt = currentTime - lastTime;

                var sleepTime = (int)((targetDt - dt) * 1000);
                if (sleepTime > 0) {
                    Thread.Sleep(sleepTime);
                }

            }

        }

    }

}