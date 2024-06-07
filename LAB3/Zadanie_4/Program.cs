using System.Security.Cryptography;

class RSATool
{
    private static string publicKey;
    private static string privateKey;

    public static void Main(string[] args)
    {
        GenerateKeys();

        string inputFilePath = "input.txt";
        string encryptedFilePath = "encrypted.txt";
        string decryptedFilePath = "decrypted.txt";

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Error: The input file does not exist: {inputFilePath}");
            return;
        }

        try
        {
            Console.WriteLine($"Attempting to encrypt file: {inputFilePath}");
            EncryptFile(inputFilePath, encryptedFilePath);
            Console.WriteLine("Encryption successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Encryption failed: {ex.Message}");
            return;
        }

        if (!File.Exists(encryptedFilePath))
        {
            Console.WriteLine($"Error: The encrypted file was not created: {encryptedFilePath}");
            return;
        }

        try
        {
            Console.WriteLine($"Attempting to decrypt file: {encryptedFilePath}");
            DecryptFile(encryptedFilePath, decryptedFilePath);
            Console.WriteLine("Decryption successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Decryption failed: {ex.Message}");
        }
    }

    private static void GenerateKeys()
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            publicKey = rsa.ToXmlString(false);
            privateKey = rsa.ToXmlString(true);
        }
    }

    private static void EncryptFile(string inputFilePath, string encryptedFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException($"Input file not found: {inputFilePath}");
        }

        byte[] dataToEncrypt = File.ReadAllBytes(inputFilePath);

        if (dataToEncrypt.Length > 214)
        {
            throw new ArgumentException("Input file is too large to encrypt with RSA directly. The maximum size is 214 bytes.");
        }

        byte[] encryptedData;

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            encryptedData = rsa.Encrypt(dataToEncrypt, true);
        }

        File.WriteAllBytes(encryptedFilePath, encryptedData);
    }

    private static void DecryptFile(string encryptedFilePath, string decryptedFilePath)
    {
        if (!File.Exists(encryptedFilePath))
        {
            throw new FileNotFoundException($"Encrypted file not found: {encryptedFilePath}");
        }

        byte[] dataToDecrypt = File.ReadAllBytes(encryptedFilePath);
        byte[] decryptedData;

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            decryptedData = rsa.Decrypt(dataToDecrypt, true);
        }

        File.WriteAllBytes(decryptedFilePath, decryptedData);
    }
}
