using System;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using PasswordManagerMVC.Services;
using System.Collections.Specialized;

public class AesCryptoService : IAesCryptoService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AesCryptoService()
    {
        var encryptionSettings = (NameValueCollection)ConfigurationManager.GetSection("encryptionSettings");

        _key = Convert.FromBase64String(encryptionSettings["EncryptionKey"]);
        _iv = Convert.FromBase64String(encryptionSettings["EncryptionIV"]);

        if (_key.Length != 32) throw new ArgumentException("Key 256-bit (32 byte) olmalı");
        if (_iv.Length != 16) throw new ArgumentException("IV 128-bit (16 byte) olmalı");
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        try
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key ?? throw new CryptographicException("Şifreleme anahtarı bulunamadı");
                aesAlg.IV = _iv ?? throw new CryptographicException("Başlangıç vektörü (IV) bulunamadı");

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        catch (CryptographicException ex)
        {
            throw new CryptographicException("Şifreleme işlemi başarısız", ex);
        }
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return string.Empty;

        try
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key ?? throw new CryptographicException("Şifreleme anahtarı bulunamadı");
                aesAlg.IV = _iv ?? throw new CryptographicException("Başlangıç vektörü (IV) bulunamadı");

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
        catch (CryptographicException ex)
        {
            throw new CryptographicException("Şifre çözme işlemi başarısız", ex);
        }
        catch (FormatException ex)
        {
            throw new CryptographicException("Geçersiz şifreli metin formatı", ex);
        }
    }
}