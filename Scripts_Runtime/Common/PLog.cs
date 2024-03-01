using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ping.Server {

    public static class PLog {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(string msg) {
            Console.WriteLine(msg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogWarning(string msg) {
            Console.WriteLine($"WARNING: {msg}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogError(string msg) {
            Console.WriteLine($"ERROR: {msg}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogAssert(bool condition, string msg) {
            if (!condition) {
                Console.WriteLine($"ASSERTION FAILED: {msg}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogAssertWithoutMsg(bool condition) {
            if (!condition) {
                Console.WriteLine("ASSERTION FAILED");
            }
        }

    }

}
