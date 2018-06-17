using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WD.Common.Utility
{
    public class SecurityUtility
    {
        #region private
        private static string ASEPrivateKey = "02-25867890";
        private static string ASEPublicKey = "gss@mail.gss.com.tw";
        #endregion

        #region 加密
        /// <summary>
        ///  加密
        /// </summary>
        /// <param name="string2EnCrypt">加密字串</param>
        /// <returns></returns>
        public static string AESEnCrypt(string string2EnCrypt)
        {
            //預設用公司電話當私鑰
            return AESEnCrypt(string2EnCrypt, ASEPrivateKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="string2EnCrypt">加密字串</param>
        /// <param name="privateKey">鑰匙</param>
        /// <returns></returns>
        public static string AESEnCrypt(string string2EnCrypt, string privateKey)
        {
            RijndaelManaged AES;
            MD5CryptoServiceProvider MD5;

            AES = new RijndaelManaged();
            MD5 = new MD5CryptoServiceProvider();
            byte[] plainTextData = Encoding.Unicode.GetBytes(string2EnCrypt);
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(privateKey));
            //公鑰
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes(ASEPublicKey));
            ICryptoTransform transform = AES.CreateEncryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(plainTextData, 0, plainTextData.Length);
            return Convert.ToBase64String(outputData);
        }
        #endregion

        #region 解密
        /// <summary>
        /// 處理URL帶入加密參數字元錯誤問題
        /// </summary>
        /// <param name="string2DeCrypt">解密字串</param>
        /// <returns></returns>
        public static string AESDecrypt(string string2DeCrypt)
        {
            //處理傳遞參數會發生 "+" 會被取代掉 *kent
            string2DeCrypt = string2DeCrypt.Replace(" ", "+");
            return AESDecrypt(Convert.FromBase64String(string2DeCrypt), ASEPrivateKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="string2DeCrypt">解密字串</param>
        /// <param name="privateKey">鑰匙</param>
        /// <returns></returns>
        public static string AESDecrypt(string string2DeCrypt, string privateKey)
        {
            return AESDecrypt(Convert.FromBase64String(string2DeCrypt), privateKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="dnCrypt">解密字串(byte[])</param>
        /// <param name="privateKey">鑰匙</param>
        /// <returns></returns>
        private static string AESDecrypt(byte[] dnCrypt, string privateKey)
        {
            RijndaelManaged AES;
            MD5CryptoServiceProvider MD5;

            AES = new RijndaelManaged();
            MD5 = new MD5CryptoServiceProvider();
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(privateKey));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes(ASEPublicKey));
            ICryptoTransform transform = AES.CreateDecryptor(keyData, IVData);
            byte[] outputData = transform.TransformFinalBlock(dnCrypt, 0, dnCrypt.Length);
            return Encoding.Unicode.GetString(outputData);
        }
        #endregion

    }
}
