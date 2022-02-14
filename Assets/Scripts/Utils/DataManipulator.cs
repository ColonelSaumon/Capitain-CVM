using System.Security.Cryptography;
using System.Text;
using System;

/// <summary>
/// Permet la manipulation des données pour l'encryptage
/// </summary>
public sealed class DataManipulator
{

    private string key = "XCtmd6zUnPVgT9dW6vkuyNRbxsjvQqpM";
    private string iv = "QyEgUUI9OyLmUA8a";

    /// <summary>
    /// Permet d'encrypter une chaîne de caractère
    /// </summary>
    /// <param name="inputData">Chaîne à encrypter</param>
    /// <returns>Chaîne encrypter</returns>
    public string Encrypt(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    /// <summary>
    /// Permet de décrypter une chaîne de caractère
    /// </summary>
    /// <param name="inputData">Chaîne à décrypter</param>
    /// <returns>Chaîne décrypter</returns>
    public string Decrypt(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(key);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = Convert.FromBase64String(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateDecryptor();

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }
}
