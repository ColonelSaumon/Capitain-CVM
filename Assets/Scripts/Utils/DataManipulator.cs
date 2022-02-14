using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
/// Permet la manipulation des données pour l'encryptage
/// </summary>
public sealed class DataManipulator
{

    private byte[] data_key;
    private byte[] data_pkey;

    public DataManipulator() {
        string key = "A#NaOqBC";
        data_key = Encoding.UTF8
            .GetBytes(key);
        data_pkey = Encoding.UTF8
            .GetBytes("f06a78bd586d05323e2d9b620743d3b1");
    }

    public void Encrypt(string plainText, string path)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            ICryptoTransform encrypto = des.CreateEncryptor(data_key, data_pkey);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encrypto, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        byte[] cypto = Encoding.UTF8.GetBytes(plainText);
                        csEncrypt.Write(cypto, 0, cypto.Length);
                        csEncrypt.FlushFinalBlock();
                        streamWriter.Write(Encoding.UTF8.GetString(msEncrypt.ToArray()));
                    }
                }
            }
        }
    }

    public PlayerData Decrypt(string path)
    {
        string json = string.Empty;
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            ICryptoTransform encrypto = des.CreateEncryptor(data_key, data_pkey);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encrypto, CryptoStreamMode.Write))
                {
                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        byte[] inputDecrypt = Encoding.UTF8.GetBytes(streamReader.ReadToEnd());
                        csEncrypt.Write(inputDecrypt, 0, inputDecrypt.Length);
                        csEncrypt.FlushFinalBlock();
                        json = Encoding.UTF8.GetString(msEncrypt.ToArray());
                    }
                }
            }
        }

        return PlayerDataJson.ReadJson(json);
    }
}
