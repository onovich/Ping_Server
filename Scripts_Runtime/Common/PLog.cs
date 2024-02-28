using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ping.Server {

    public static class PLog {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(string msg) {
            Trace.WriteLine(msg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogWarning(string msg) {
            Trace.WriteLine($"WARNING: {msg}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogError(string msg) {
            Trace.TraceError(msg);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogAssert(bool condition, string msg) {
            if (!condition) {
                Trace.Fail(msg);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogAssertWithoutMsg(bool condition) {
            if (!condition) {
                Trace.Fail("Assertion failed!");
            }
        }

    }

}
