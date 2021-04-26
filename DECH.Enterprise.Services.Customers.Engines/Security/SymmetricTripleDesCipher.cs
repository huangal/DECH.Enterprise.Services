using System;
using System.Security.Cryptography;
using System.Text;

namespace DECH.Enterprise.Services.Customers.Engines.Security
{
    public class SymmetricTripleDesCipher
    {
        public CryptoItem Encrypt(string text, string secretKey)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(secretKey);////Convert.FromBase64String(secretKey);
            byte[] dataArray = Encoding.UTF8.GetBytes(text);

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider() { Key = keyArray })
            {

                TripleDesCipher cipher = new TripleDesCipher(tdes.Key, tdes.IV);

                byte[] encryptedArray = cipher.TripleDesEncrypt(dataArray);

                return new CryptoItem { EncryptedArray = encryptedArray, IVArray = tdes.IV, KeyArray = tdes.Key };
            }
        }



        public string Decrypt(string encryptedText, string IV, string secretKey)
        {
            byte[] IVArray = Convert.FromBase64String(IV);
            byte[] keyArray = Encoding.UTF8.GetBytes(secretKey);////Convert.FromBase64String(secretKey);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider() { Key = keyArray, IV = IVArray })
            {

                TripleDesCipher cipher = new TripleDesCipher(tdes.Key, tdes.IV);

                byte[] decryptedArray = cipher.TripleDesDecrypt(encryptedBytes);

                return Encoding.UTF8.GetString(decryptedArray);
            }

        }

    }
}
