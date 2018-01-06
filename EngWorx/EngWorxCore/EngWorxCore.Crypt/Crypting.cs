using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Crypt
{
    public class CryptingUtility
    {
        private static byte[] myRijndaelIV = { 13, 92, 91, 93, 16, 92, 67, 48, 169, 83, 135, 120, 49, 201, 120, 123 };

        public static string doCrypt(string original, string rijndaelKey)
        {
            byte[] myRijndaelKey = formatKey(rijndaelKey);
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                // Encrypt the string to an array of bytes. 
                byte[] encrypted = CryptingUtility.EncryptStringToBytes(original, myRijndaelKey, myRijndaelIV);

                StringBuilder s = new StringBuilder();
                foreach (byte item in encrypted)
                {
                    s.Append(item.ToString("X2"));
                }
                return s.ToString();
            }
        }

        public static string doDecrypt(string original, string rijndaelKey)
        {
            byte[] encrypted = Helper.StringToByteArray(original);
            byte[] myRijndaelKey = formatKey(rijndaelKey);
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                // Decrypt the bytes to a string. 
                string decrypted = DecryptStringFromBytes(encrypted, myRijndaelKey, myRijndaelIV);
                return decrypted;
            }
        }

        private static byte[] formatKey(string rijndaelKey)
        {
            int stringLenght = (Helper.KEY_LENGHT_BIT / 8) * 2;
            string preparedKey = rijndaelKey.PadRight(stringLenght, ' ');
            string hashedKey = Helper.ComputeHashSHA256(preparedKey).Replace("-", "");
            byte[] myRijndaelKey = Helper.StringToByteArray(hashedKey);
            return myRijndaelKey;
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform. 
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream. 
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream. 
            return encrypted;
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform. 
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string. 
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            // rimpiazzo i caratteri "fine stringa"
            return plaintext.Replace("\0", String.Empty);
        }
    }
}
