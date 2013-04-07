using System;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;

namespace PizzaMasterEmporium.Framework.Security.Aes
{
    public class SimpleAes
    {
        // Change these keys
        private readonly byte[] _key = { 6, 135, 150, 204, 31, 0, 78, 158, 206, 79, 112, 21, 32, 114, 186, 178, 44, 249, 158, 175, 52, 240, 48, 5, 84, 210, 71, 63, 150, 254, 60, 17 };
        private readonly byte[] _vector = { 153, 53, 156, 246, 26, 6, 113, 218, 131, 236, 159, 169, 146, 134, 17, 53 };


        private readonly ICryptoTransform _encryptorTransform;
        private readonly ICryptoTransform _decryptorTransform;
        private readonly System.Text.UTF8Encoding _utfEncoder;

        public SimpleAes()
        {
            //This is our encryption method
            var rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            _encryptorTransform = rm.CreateEncryptor(_key, _vector);
            _decryptorTransform = rm.CreateDecryptor(_key, _vector);

            //Used to translate bytes to text and vice versa
            _utfEncoder = new System.Text.UTF8Encoding();
        }

        /// -------------- Two Utility Methods (not used but may be useful) -----------
        /// Generates an encryption key.
        static private byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            var rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        /// Generates a unique encryption vector
        static private byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            var rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }


        /// ----------- The commonly used methods ------------------------------    
        /// Encrypt some text and return a string suitable for passing in a URL.
        public string EncryptString(string textValue)
        {
            if (textValue == null) return null;
            return ByteArrToString(Encrypt(textValue));
        }

        /// Encrypt some text and return an encrypted byte array.
        private byte[] Encrypt(string textValue)
        {
            if (textValue == null) return null;
            if (textValue.Length == 0) return new byte[0];

            //Translates our text value into a byte array.
            var bytes = _utfEncoder.GetBytes(textValue);

            //Used to stream the data in and out of the CryptoStream.
            var memoryStream = new MemoryStream();

            /*
         * We will have to write the unencrypted bytes to the stream,
         * then read the encrypted result back from the stream.
         */
            #region Write the decrypted value to the encryption stream
            var cs = new CryptoStream(memoryStream, _encryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            var encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        public string DecryptString(string encryptedString)
        {
            if (encryptedString == null) return null;
            if (encryptedString.Length == 0) return string.Empty;
            return Decrypt(StrToByteArray(encryptedString));
        }

        /// Decryption when working with byte arrays.    
        private string Decrypt(byte[] encryptedValue)
        {
            #region Write the encrypted value to the decryption stream
            var encryptedStream = new MemoryStream();
            var decryptStream = new CryptoStream(encryptedStream, _decryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(encryptedValue, 0, encryptedValue.Length);
            decryptStream.FlushFinalBlock();
            #endregion

            #region Read the decrypted value from the stream.
            encryptedStream.Position = 0;
            var decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();
            #endregion
            return _utfEncoder.GetString(decryptedBytes);
        }

        // Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        // System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        // return encoding.GetBytes(str);
        // However, this results in character values that cannot be passed in a URL.  So, instead, I just
        // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        private byte[] StrToByteArray(string str)
        {
            if (str.Length == 0) return new byte[0];

            var byteArr = new byte[str.Length / 3];
            var i = 0;
            var j = 0;
            do
            {
                byte val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        // Same comment as above. Normally the conversion would use an ASCII
        // encoding in the other direction:
        //   System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //   return enc.GetString(byteArr);    
        private string ByteArrToString(byte[] byteArr)
        {
            if (byteArr.Length == 0) return string.Empty;
            var tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                var val = byteArr[i];
                if (val < 10)
                    tempStr += "00" + val.ToString(CultureInfo.InvariantCulture);
                else if (val < 100)
                    tempStr += "0" + val.ToString(CultureInfo.InvariantCulture);
                else
                    tempStr += val.ToString(CultureInfo.InvariantCulture);
            }
            return tempStr;
        }
    }
}