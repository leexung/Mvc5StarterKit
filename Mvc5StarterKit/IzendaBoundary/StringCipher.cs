using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Mvc5StarterKit.IzendaBoundary
{
    public static class StringCipher
    {
        private static readonly AesCryptoServiceProvider Crypto = new AesCryptoServiceProvider();

        public static string Encrypt(string raw, string key)
        {
            byte[] inBlock = Encoding.UTF8.GetBytes(raw);
            ICryptoTransform xfrm = Crypto.CreateEncryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);
            return Convert.ToBase64String(outBlock);
        }

        public static string Decrypt(string encrypted, string key)
        {
            byte[] inBytes = Convert.FromBase64String(encrypted);
            ICryptoTransform xfrm = Crypto.CreateDecryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return Encoding.UTF8.GetString(outBlock);
        }
    }
}