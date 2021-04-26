using System;

namespace DECH.Enterprise.Services.Customers.Engines.Security
{
    public class CryptoItem
    {
        public byte[] EncryptedArray { get; set; }
        public byte[] IVArray { get; set; }
        public byte[] KeyArray { get; set; }

        public string EncryptedData => Convert.ToBase64String(EncryptedArray);
        public string IV => Convert.ToBase64String(IVArray);
        public string Key => Convert.ToBase64String(KeyArray);

    }

}
