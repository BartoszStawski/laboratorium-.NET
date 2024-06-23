using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class ChatClient
{
    private TcpClient _client;
    private NetworkStream _stream;
    private const int BufferSize = 1024;
    private byte[] _key = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte key
    private byte[] _iv = Encoding.UTF8.GetBytes("1234567890123456");  // 16-byte IV

    public ChatClient(string ipAddress, int port)
    {
        _client = new TcpClient();
        _client.Connect(ipAddress, port);  // Połączenie klienta z serwerem
        _stream = _client.GetStream();
    }

    public void Start()
    {
        Console.WriteLine("Connected to server...");
        Task.Run(() => ReceiveMessagesAsync());
        SendMessages();
    }

    private async Task ReceiveMessagesAsync()
    {
        var buffer = new byte[BufferSize];

        while (true)
        {
            var byteCount = await _stream.ReadAsync(buffer, 0, buffer.Length);
            if (byteCount == 0) break;

            var decryptedMessage = Decrypt(buffer, byteCount);
            Console.WriteLine($"Received: {decryptedMessage}");
        }
    }

    private void SendMessages()
    {
        while (true)
        {
            var message = Console.ReadLine();
            var encryptedMessage = Encrypt(message);
            _stream.Write(encryptedMessage, 0, encryptedMessage.Length);
        }
    }

    private string Decrypt(byte[] data, int count)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var decryptedData = decryptor.TransformFinalBlock(data, 0, count);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }

    private byte[] Encrypt(string message)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var data = Encoding.UTF8.GetBytes(message);
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }

    public static void Main(string[] args)
    {
        var client = new ChatClient("127.0.0.1", 65001);
        client.Start();
    }
}
