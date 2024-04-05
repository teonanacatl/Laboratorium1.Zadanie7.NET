using System.Diagnostics;

namespace Laboratorium1.Zadanie7.NET
{
    class Program
    {
        static async Task Main()
        {
            string sourceFilePath = @"C:\source.bin";
            string destinationFilePath = @"C:\destination.bin";

            if (File.Exists(sourceFilePath))
            {
                Console.WriteLine($"Plik źródłowy '{sourceFilePath}' już istnieje.");
                return;
            }

            await GenerateLargeFile(sourceFilePath, 300);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await using var sourceStream = new FileStream(sourceFilePath, FileMode.Open);
            await using var destinationStream = new FileStream(destinationFilePath, FileMode.Create);
            await sourceStream.CopyToAsync(destinationStream);

            stopwatch.Stop();
            Console.WriteLine($"Czas trwania: {stopwatch.ElapsedMilliseconds} ms");
        }

        static async Task GenerateLargeFile(string filePath, int sizeInMegabytes)
        {
            byte[] data = new byte[1024 * 1024];
            Buffer.BlockCopy(new byte[1024 * 1024], 0, data, 0, data.Length);

            await using var fileStream = File.Create(filePath);
            for (int i = 0; i < sizeInMegabytes; i++)
            {
                await fileStream.WriteAsync(data, 0, data.Length);
            }
        }
    }
}