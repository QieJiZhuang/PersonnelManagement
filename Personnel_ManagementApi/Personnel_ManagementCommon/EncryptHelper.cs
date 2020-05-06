using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LandMarkApply.Common
{
    public class EncryptHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="bytesEncoding">编码</param>
        /// <returns></returns>
        public static string MD5Encrypt(string plaintext, Encoding bytesEncoding)
        {
            byte[] sourceBytes = bytesEncoding.GetBytes(plaintext);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashedBytes = md5.ComputeHash(sourceBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        public static string MD5Encrypt(string plaintext)
        {
            return MD5Encrypt(plaintext, Encoding.UTF8);
        }


        #region 字符串加密
        private const string DefaultDESKey = "loogn789";
        /// <summary>
        /// 利用DES加密算法加密字符串（可解密）
        /// </summary>
        /// <param name="plaintext">被加密的字符串</param>
        /// <param name="key">秘钥（只支持8字节的秘钥）</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptString(string plaintext, string key = DefaultDESKey)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(key);
            byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] resultBytes = des.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(resultBytes);
        }

        /// <summary>
        /// 利用DES解密算法解密密文（可解密）
        /// </summary>
        /// <param name="ciphertext">被解密的字符串</param>
        /// <param name="key">秘钥（只支持8字节的秘钥，和前面的加密密文相同）</param>
        /// <returns>返回被解密的字符串</returns>
        public static string DecryptString(string ciphertext, string key = DefaultDESKey)
        {
            DES des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = Encoding.UTF8.GetBytes(key);
            byte[] bytes = Convert.FromBase64String(ciphertext);
            byte[] resultBytes = des.CreateDecryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(resultBytes);
        }
        #endregion
    }
}
