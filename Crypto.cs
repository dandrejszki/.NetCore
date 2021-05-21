using System;
using System.Collections.Generic;
//using System.Linq;
//using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
//using System.Xml.Linq;
//using static System.Convert;

namespace testEnv
{
  public class Crypto
  {

    private readonly string _sspKey;
    private readonly byte[] key;
    private readonly byte[] IV;

    public Crypto(string sspKey)
    {
      _sspKey = sspKey;
      key = Encoding.UTF8.GetBytes(sspKey);
      IV = key[0..16];
    }

    public string encrypt(string textToEncode)
    {
      byte[] bytesToEncode = Encoding.UTF8.GetBytes(textToEncode);
      String returnValue;
      using (AesManaged aesAlg = new AesManaged())
      {
        aesAlg.Key = key;
        aesAlg.IV = IV;
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        byte[] encryptedData = encryptor.TransformFinalBlock(bytesToEncode, 0, bytesToEncode.Length);
        returnValue = System.Convert.ToBase64String(encryptedData);
      }
      return returnValue;
    }

    public string decrypt(string textToDecrypt)
    {
      String returnValue;
      byte[] chipertext = Convert.FromBase64String(textToDecrypt);
      using (AesManaged aesAlg = new AesManaged())
      {
        aesAlg.Key = key;
        aesAlg.IV = IV;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        byte[] originaltext = decryptor.TransformFinalBlock(chipertext, 0, chipertext.Length);
        returnValue = Encoding.UTF8.GetString(originaltext);
      }
      return returnValue;
    }

  }

}