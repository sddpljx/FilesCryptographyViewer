using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilesCryptographyViewer
{
    public class FilesCryptography
    {
        public static string GetFileMD5(string filePath)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider mD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                byte[] byteMD5 = mD5.ComputeHash(fileStream);
                fileStream.Close();

                for (int i = 0; i < byteMD5.Length; i++)
                {
                    stringBuilder.Append(byteMD5[i].ToString("x2"));
                }
            }
            catch (System.Exception ex)
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show(ex.ToString(), "Warnning!", btn);
                //ShowDialog.ShowMessage("读取数据失败！", "Warnning!", btn);
                //throw;
            }
            return stringBuilder.ToString();
        }

        public static string GetFileSHA1(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                System.Security.Cryptography.SHA1CryptoServiceProvider sHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                byte[] byteSHA1 = sHA1.ComputeHash(fileStream);
                fileStream.Close();
                for (int i = 0; i < byteSHA1.Length; i++)
                {
                    stringBuilder.Append(byteSHA1[i].ToString("x2"));
                }
            }
            catch (System.Exception ex)
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show(ex.ToString(), "Warnning!", btn);
            }
            return stringBuilder.ToString();
        }

        public static string GetFileSHA256(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                System.Security.Cryptography.SHA256CryptoServiceProvider sHA256 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                byte[] byteSHA1 = sHA256.ComputeHash(fileStream);
                fileStream.Close();
                for (int i = 0; i < byteSHA1.Length; i++)
                {
                    stringBuilder.Append(byteSHA1[i].ToString("x2"));
                }
            }
            catch (System.Exception ex)
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show(ex.ToString(), "Warnning!", btn);
            }
            return stringBuilder.ToString();
        }
    }
}
