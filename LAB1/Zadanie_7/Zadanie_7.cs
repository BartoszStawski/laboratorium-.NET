using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string sourceFileName = "large_file.txt";
        string destinationFileName = "copied_file.txt";
        long fileSizeInMB = 300;
        long fileSizeInBytes = fileSizeInMB * 1024 * 1024;

        // Generowanie pliku
        GenerateLargeFile(sourceFileName, fileSizeInBytes);
        Console.WriteLine($"Plik {sourceFileName} o wielkości {fileSizeInMB} MB został wygenerowany.");

        // Kopiowanie przy użyciu FileStream
        Console.WriteLine("\nKopiowanie przy użyciu FileStream:");
        MeasureExecutionTime(() => CopyUsingFileStream(sourceFileName, destinationFileName));

        // Kopiowanie przy użyciu File.Copy
        Console.WriteLine("\nKopiowanie przy użyciu File.Copy:");
        MeasureExecutionTime(() => File.Copy(sourceFileName, destinationFileName, true)); // Ustawienie overwrite na true

        // Kopiowanie przy użyciu File.ReadAllBytes i File.WriteAllBytes
        Console.WriteLine("\nKopiowanie przy użyciu File.ReadAllBytes i File.WriteAllBytes:");
        MeasureExecutionTime(() => CopyUsingReadAllBytesAndWriteAllBytes(sourceFileName, destinationFileName));

        // Kopiowanie przy użyciu BinaryReader i BinaryWriter
        Console.WriteLine("\nKopiowanie przy użyciu BinaryReader i BinaryWriter:");
        MeasureExecutionTime(() => CopyUsingBinaryReaderAndBinaryWriter(sourceFileName, destinationFileName));
    }

    static void MeasureExecutionTime(Action action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        Console.WriteLine($"Czas wykonania: {stopwatch.ElapsedMilliseconds} ms");
    }

    static void GenerateLargeFile(string fileName, long fileSizeInBytes)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            byte[] buffer = new byte[1024 * 1024];
            Random rand = new Random();
            long bytesWritten = 0;
            while (bytesWritten < fileSizeInBytes)
            {
                rand.NextBytes(buffer);
                int bytesToWrite = (int)Math.Min(buffer.Length, fileSizeInBytes - bytesWritten);
                writer.Write(buffer, 0, bytesToWrite);
                bytesWritten += bytesToWrite;
            }
        }
    }

    static void CopyUsingFileStream(string sourceFileName, string destinationFileName)
    {
        using (FileStream sourceStream = new FileStream(sourceFileName, FileMode.Open))
        using (FileStream destinationStream = new FileStream(destinationFileName, FileMode.Create))
        {
            sourceStream.CopyTo(destinationStream);
        }
    }

    static void CopyUsingReadAllBytesAndWriteAllBytes(string sourceFileName, string destinationFileName)
    {
        byte[] data = File.ReadAllBytes(sourceFileName);
        File.WriteAllBytes(destinationFileName, data);
    }

    static void CopyUsingBinaryReaderAndBinaryWriter(string sourceFileName, string destinationFileName)
    {
        using (BinaryReader reader = new BinaryReader(File.OpenRead(sourceFileName)))
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(destinationFileName)))
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                writer.Write(buffer, 0, bytesRead);
            }
        }
    }
}
