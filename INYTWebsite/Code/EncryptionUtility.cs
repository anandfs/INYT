using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class EncryptionUtility
    {
        public static string password = "inyttest";

        /// <summary>
        /// Encrypt the data
        /// </summary>
        /// <param name="input">String to encrypt</param>
        /// <param name="password">The password.</param>
        /// <param name="uriFriendly">if set to <c>true</c> produce URI friendly output.</param>
        /// <returns>
        /// Encrypted string
        /// </returns>
        public static string Encrypt(string input, bool uriFriendly = false)
        {
            if (password.Length < 8)
            {
                password = (password + "zzzzzzzz").Substring(0, 8);
            }
            byte[] utfData = Encoding.UTF8.GetBytes(input);
            byte[] saltBytes = Encoding.UTF8.GetBytes(password);
            string encryptedString;
            using (var aes = new AesManaged())
            {
                var rfc = new Rfc2898DeriveBytes(password, saltBytes);

                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                using (var encryptTransform = aes.CreateEncryptor())
                {
                    using (var encryptedStream = new MemoryStream())
                    {
                        using (var encryptor =
                            new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                        {
                            encryptor.Write(utfData, 0, utfData.Length);
                            encryptor.Flush();
                            encryptor.Close();

                            byte[] encryptBytes = encryptedStream.ToArray();
                            encryptedString = Convert.ToBase64String(encryptBytes);
                        }
                    }
                }
            }
            if (uriFriendly)
            {
                encryptedString = encryptedString.Replace("+", "-").Replace("/", "_");
            }
            return encryptedString;
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="input">Input string in base 64 format</param>
        /// <param name="password">The password.</param>
        /// <param name="uriFriendly">if set to <c>true</c> produce URI friendly output.</param>
        /// <returns>
        /// Decrypted string
        /// </returns>
        public static string Decrypt(string input, bool uriFriendly = false)
        {
            if (password.Length < 8)
            {
                password = (password + "zzzzzzzz").Substring(0, 8);
            }
            if (uriFriendly)
            {
                input = input.Replace("-", "+").Replace("_", "/");
            }
            byte[] encryptedBytes = Convert.FromBase64String(input);
            byte[] saltBytes = Encoding.UTF8.GetBytes(password);
            string decryptedString;
            using (var aes = new AesManaged())
            {
                var rfc = new Rfc2898DeriveBytes(password, saltBytes);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);

                using (var decryptTransform = aes.CreateDecryptor())
                {
                    using (var decryptedStream = new MemoryStream())
                    {
                        var decryptor =
                            new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);
                        decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
                        decryptor.Flush();
                        decryptor.Close();

                        byte[] decryptBytes = decryptedStream.ToArray();
                        decryptedString =
                            Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
                    }
                }
            }

            return decryptedString;
        }
    }
}
