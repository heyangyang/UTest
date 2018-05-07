using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class MD5Helper
{
    static MD5 md5 = new MD5CryptoServiceProvider();
    static SHA1 sha1 = new SHA1CryptoServiceProvider();

    static public string MD5_Hash(byte[] bytes_md5_in)
    {
        byte[] bytes_md5_out = md5.ComputeHash(bytes_md5_in);
        md5.Clear();
        string hashedValue = "";
        for (int i = 0; i < bytes_md5_out.Length; i++)
        {
            hashedValue += bytes_md5_out[i].ToString("x2");
        }
        return hashedValue.ToUpper();
    }
    static public string MD5_Hash(string str_md5_in)
    {
        byte[] bytes_md5_in = UTF8Encoding.Default.GetBytes(str_md5_in);
        return MD5_Hash(bytes_md5_in);
    }


    static public string SHA1_Hash(byte[] bytes_sha1_in)
    {
        byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
        sha1.Clear();
        string hashedValue = "";
        for (int i = 0; i < bytes_sha1_out.Length; i++)
        {
            hashedValue += bytes_sha1_out[i].ToString("x2");
        }
        return hashedValue.ToUpper();
    }
    //SHA1
    static public string SHA1_Hash(string str_sha1_in)
    {
        byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
        return SHA1_Hash(bytes_sha1_in);
    }

    public static string GetSHA512HashFromString(string strData)
    {
        byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
        try
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            byte[] retVal = sha512.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetSHA512HashFromString() fail,error:" + ex.Message);
        }
    }
}