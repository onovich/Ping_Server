using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ping.Server {

    public class Entry {

        static void Main(string[] args) {

            var entry = new ServerMain();
            var targetFrameRate = 60d;

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var currentTime = stopWatch.Elapsed.TotalSeconds;
            var lastTime = currentTime;

            var targetDt = 1.0d / targetFrameRate;
            var restTime = 0.0d;
            var fixedDt = 0.02d;

            // ResetInput
            entry.ResetInput();

            // ProcessInput
            entry.ProcessInput();

            while (true) {

                currentTime = stopWatch.Elapsed.TotalSeconds;

                var dt = currentTime - lastTime;
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
                currentTime = stopWatch.Elapsed.TotalSeconds;
                dt = currentTime - lastTime;

                var sleepTime = (int)((targetDt - dt) * 1000);
                if (sleepTime > 0) {
                    Thread.Sleep(sleepTime);
                }

            }

        }

    }

}