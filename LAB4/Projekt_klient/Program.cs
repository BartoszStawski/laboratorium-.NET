using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class FileMonitorService
{
    private static readonly string FolderPath = @"C:\Windows\System32";
    private static readonly string EncryptionKey = "your-encryption-key";
    private static readonly string ServerAddress = "127.0.0.1";
    private static readonly int ServerPort = 5000;

    public static async Task Main(string[] args)
    {
        while (true)
        {
            try
            {
                var checksums = CalculateChecksums(FolderPath);
                string checksumsJson = JsonConvert.SerializeObject(checksums);
                Console.WriteLine($"Checksums data: {checksumsJson}");

                var encryptedData = EncryptData(checksumsJson);
                await SendDataToServer(encryptedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromHours(1)); // Co godzinę
        }
    }

    private static Dictionary<string, string> CalculateChecksums(string folderPath)
    {
        var files = Directory.GetFiles(folderPath);
        var checksums = new Dictionary<string, string>();

        Parallel.ForEach(files, file =>
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    var hash = md5.ComputeHash(stream);
                    var hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    lock (checksums)
                    {
                        checksums[file] = hashString;
                    }
                }
            }
        });

        return checksums;
    }

    private static string EncryptData(string plainText)
    {
        using (var aes = Aes.Create())
        {
            var key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
            aes.Key = key;
            aes.IV = new byte[16]; // Zero initialization vector
            aes.Padding = PaddingMode.PKCS7;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }

                string encryptedText = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine($"Encrypted data: {encryptedText}");
                return encryptedText;
            }
        }
    }

    private static async Task SendDataToServer(string encryptedData)
    {
        using (var client = new TcpClient(ServerAddress, ServerPort))
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(encryptedData);

            await stream.WriteAsync(data, 0, data.Length);
        }
    }
}
