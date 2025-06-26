using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManagerMVC.Services
{
    public interface IAesCryptoService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}