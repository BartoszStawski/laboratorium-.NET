using System.Diagnostics;
using System.Security.Cryptography;

namespace SymmetricEncryptionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Performance Measurement of Symmetric Encryption Algorithms");

            string[] algorithms = {
                "AES (CSP) 128bit",
                "AES (CSP) 256bit",
                "AES Managed 128bit",
                "AES Managed 256bit",
                "RijndaelManaged 128bit",
                "RijndaelManaged 256bit",
                "DES 56bit",
                "3DES 168bit"
            };

            Console.WriteLine("| {0,-20} | {1,-20} | {2,-20} | {3,-20} |",
                "Algorithm", "Seconds per Block", "Bytes per Second (RAM)", "Bytes per Second (HDD)");
            Console.WriteLine(new string('-', 94));

            foreach (string algorithm in algorithms)
            {
                try
                {
                    double secondsPerBlock = MeasureTimePerBlock(algorithm);
                    long ramBytesPerSecond = MeasureSpeed(algorithm, "RAM");
                    long hddBytesPerSecond = MeasureSpeed(algorithm, "HDD");

                    Console.WriteLine("| {0,-20} | {1,-20:F2} | {2,-20} | {3,-20} |",
                        algorithm, secondsPerBlock, ramBytesPerSecond + " B/s", hddBytesPerSecond + " B/s");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }
            }
        }

        static double MeasureTimePerBlock(string algorithm)
        {
            using (SymmetricAlgorithm symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                int blockSizeInBytes = symmetricAlgorithm.BlockSize / 8;
                int inputBlockSize = 1024 * blockSizeInBytes; // 1 KB
                byte[] inputBlock = new byte[inputBlockSize];

                Stopwatch stopwatch = Stopwatch.StartNew();

                // Encryption
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputBlock, 0, inputBlock.Length);
                    }
                }

                stopwatch.Stop();
                return stopwatch.Elapsed.TotalSeconds;
            }
        }

        static long MeasureSpeed(string algorithm, string providerType)
        {
            const int MegaByte = 1024 * 1024;
            const int BufferSize = 4 * MegaByte; // 4 MB buffer size

            using (SymmetricAlgorithm symmetricAlgorithm = GetSymmetricAlgorithm(algorithm))
            {
                int blockSizeInBytes = symmetricAlgorithm.BlockSize / 8;
                int inputBlockSize = 1024 * blockSizeInBytes; // 1 KB
                byte[] inputBlock = new byte[inputBlockSize];

                Stopwatch stopwatch = Stopwatch.StartNew();

                // Encryption
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        for (int i = 0; i < BufferSize / inputBlockSize; i++)
                        {
                            cryptoStream.Write(inputBlock, 0, inputBlock.Length);
                        }
                    }
                }

                stopwatch.Stop();
                double elapsedTimeInSeconds = stopwatch.Elapsed.TotalSeconds;
                long bytesPerSecond = (long)(BufferSize / elapsedTimeInSeconds);

                return bytesPerSecond;
            }
        }

        static SymmetricAlgorithm GetSymmetricAlgorithm(string algorithm)
        {
            return algorithm switch
            {
                "AES (CSP) 128bit" => new AesCryptoServiceProvider { KeySize = 128 },
                "AES (CSP) 256bit" => new AesCryptoServiceProvider { KeySize = 256 },
                "AES Managed 128bit" => new AesManaged { KeySize = 128 },
                "AES Managed 256bit" => new AesManaged { KeySize = 256 },
                "RijndaelManaged 128bit" => new RijndaelManaged { KeySize = 128 },
                "RijndaelManaged 256bit" => new RijndaelManaged { KeySize = 256 },
                "DES 56bit" => new DESCryptoServiceProvider(),
                "3DES 168bit" => new TripleDESCryptoServiceProvider(),
                _ => throw new ArgumentException($"Unknown algorithm specified: {algorithm}")
            };
        }
    }
}
