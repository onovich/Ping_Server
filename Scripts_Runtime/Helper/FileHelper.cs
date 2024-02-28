using System;
using System.IO;
using System.Threading.Tasks;

namespace Ping.Server {

    public static class FileHelper {

        public static async Task SaveBytesAsync(string path, byte[] data, int len) {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true)) {
                await stream.WriteAsync(data, 0, len);
            }
        }

        public static async Task<byte[]> LoadBytesAsync(string path) {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true)) {
                var buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public static bool Exists(string path) {
            return File.Exists(path);
        }

        public static async Task WriteFileToPersistentAsync(string filename, byte[] data) {
            string path = Path.Combine(GetPersistentDir(), filename);
            await SaveBytesAsync(path, data, data.Length);
        }

        public static void CreateDirIfNotExist(string dir) {
            string path = Path.Combine(GetPersistentDir(), dir);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

        public static byte[] ReadFileFromPersistent(string filename) {
            string path = Path.Combine(GetPersistentDir(), filename);
            if (File.Exists(path)) {
                return File.ReadAllBytes(path);
            } else {
                Console.WriteLine($"File not found: {path}");
                return null;
            }
        }

        public static string GetPersistentDir() {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}