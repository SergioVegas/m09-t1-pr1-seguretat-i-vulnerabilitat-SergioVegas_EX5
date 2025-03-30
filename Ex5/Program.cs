using System.Security.Cryptography;
using System.Text;

namespace Ex5
{
    public class Program
    {
        // Variables per emmagatzemar dades en memòria
        private static string storedUsername = "";
        private static string storedHashedPassword = "";
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        private static void Main(string[] args)
        {
            // Generar claus RSA al iniciar el programa
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }

            bool exit = false;
            while (!exit)
            {
               Console.WriteLine( Messages.Menu);

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        VerifyUser();
                        break;
                    case "3":
                        EncryptDecryptWithRSA();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine(Messages.InvalidOption);
                        break;
                }
            }
        }

        public static void RegisterUser()
        {
            Console.Write(Messages.UserName);
            string username = Console.ReadLine();

            Console.Write(Messages.UserPassword);
            string password = Console.ReadLine();

            // Encriptar la contrasenya amb SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                storedUsername = username;
                storedHashedPassword = hashedPassword;

                Console.WriteLine(Messages.UserCorrect);
                Console.WriteLine($"Hash SHA256 de la contrasenya: {hashedPassword}");
            }
        }

        public static void VerifyUser()
        {
            if (string.IsNullOrEmpty(storedUsername))
            {
                Console.WriteLine("No hi ha cap usuari registrat. Primer fes el registre.");
                return;
            }

            Console.Write(Messages.UserName);
            string username = Console.ReadLine();

            Console.Write(Messages.UserPassword);
            string password = Console.ReadLine();

            // Calcular hash de la contrasenya introduïda
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                if (username == storedUsername && hashedPassword == storedHashedPassword)
                {
                    Console.WriteLine(Messages.CorrectData);
                }
                else
                {
                    Console.WriteLine(Messages.IncorrectData);
                }
            }
        }

        public static void EncryptDecryptWithRSA()
        {
            Console.Write("Introdueix el text a encriptar: ");
            string originalText = Console.ReadLine();

            if (string.IsNullOrEmpty(originalText))
            {
                Console.WriteLine("El text no pot estar buit.");
                return;
            }

            try
            {
                // Encriptar amb la clau pública
                byte[] encryptedData;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(publicKey);
                    byte[] dataToEncrypt = Encoding.UTF8.GetBytes(originalText);
                    encryptedData = rsa.Encrypt(dataToEncrypt, false);
                }

                Console.WriteLine($"\nText encriptat: {Convert.ToBase64String(encryptedData)}");

                // Desencriptar amb la clau privada
                string decryptedText;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(privateKey);
                    byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                    decryptedText = Encoding.UTF8.GetString(decryptedData);
                }

                Console.WriteLine($"Text desencriptat: {decryptedText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
