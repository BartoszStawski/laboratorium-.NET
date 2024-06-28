using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class SecurityServer
{
    private static readonly int Port = 5000;
    private static readonly string EncryptionKey = "your-encryption-key"; // Użyj silnego klucza szyfrowania
    private static readonly string LogFilePath = "security_log.txt";
    private static Dictionary<string, string> previousChecksums = new Dictionary<string, string>();

    public static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, Port);
        server.Start();
        Console.WriteLine("Security Server started...");

        while (true)
        {
            try
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string encryptedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Encrypted data received: {encryptedData}");

                string decryptedData = DecryptData(encryptedData);
                var checksums = JsonConvert.DeserializeObject<Dictionary<string, string>>(decryptedData);

                CompareChecksums(checksums);

                client.Close();
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Cryptographic error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static string DecryptData(string encryptedData)
    {
        try
        {
            using (var aes = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(EncryptionKey.PadRight(32).Substring(0, 32));
                aes.Key = key;
                aes.IV = new byte[16]; // Zero initialization vector
                aes.Padding = PaddingMode.PKCS7;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream(Convert.FromBase64String(encryptedData)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            string decryptedText = sr.ReadToEnd();
                            Console.WriteLine($"Decrypted data: {decryptedText}");
                            return decryptedText;
                        }
                    }
                }
            }
        }
        catch (CryptographicException ex)
        {
            Console.WriteLine($"Cryptographic error: {ex.Message}");
            throw;
        }
    }

    private static void CompareChecksums(Dictionary<string, string> currentChecksums)
    {
        foreach (var file in currentChecksums)
        {
            if (previousChecksums.ContainsKey(file.Key))
            {
                if (previousChecksums[file.Key] != file.Value)
                {
                    LogDiscrepancy(file.Key);
                }
            }
        }

        previousChecksums = new Dictionary<string, string>(currentChecksums);
    }

    private static void LogDiscrepancy(string filePath)
    {
        string logMessage = $"Discrepancy detected in file: {filePath} at {DateTime.Now}";
        File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
        Console.WriteLine(logMessage);
    }
}
