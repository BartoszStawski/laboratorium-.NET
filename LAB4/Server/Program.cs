using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class ChatServer
{
    private TcpListener _listener;
    private List<TcpClient> _clients = new List<TcpClient>();
    private const int BufferSize = 1024;
    private byte[] _key = Encoding.UTF8.GetBytes("1234567890123456"); // 16-byte key
    private byte[] _iv = Encoding.UTF8.GetBytes("1234567890123456");  // 16-byte IV

    public ChatServer(string ipAddress, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
    }

    public void Start()
    {
        _listener.Start();
        Console.WriteLine($"Server started on 127.0.0.1:65001");
        AcceptClientsAsync();
    }

    private async Task AcceptClientsAsync()
    {
        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _clients.Add(client);
            Console.WriteLine("Client connected...");
            HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        var stream = client.GetStream();
        var buffer = new byte[BufferSize];

        while (true)
        {
            try
            {
                var byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (byteCount == 0) break;

                var decryptedMessage = Decrypt(buffer, byteCount);
                Console.WriteLine($"Received: {decryptedMessage}");

                foreach (var otherClient in _clients)
                {
                    if (otherClient != client)
                    {
                        var encryptedMessage = Encrypt(decryptedMessage);
                        await otherClient.GetStream().WriteAsync(encryptedMessage, 0, encryptedMessage.Length);
                    }
                }
            }
            catch
            {
                break;
            }
        }

        _clients.Remove(client);
        client.Close();
        Console.WriteLine("Client disconnected...");
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
        var server = new ChatServer("127.0.0.1", 5001);
        server.Start();
    }
}
