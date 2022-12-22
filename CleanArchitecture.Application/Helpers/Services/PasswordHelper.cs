﻿using System.Security.Cryptography;
using System.Text;
using CleanArchitecture.Application.Helpers.Interfaces;

namespace CleanArchitecture.Application.Helpers.Services;

public class PasswordHelper : IPasswordHelper
{
    public string EncodePasswordMd5(string pass) //Encrypt using MD5   
    {
        byte[] originalBytes;
        byte[] encodedBytes;
        MD5 md5;
        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
        md5 = new MD5CryptoServiceProvider();
        originalBytes = Encoding.Default.GetBytes(pass);
        encodedBytes = md5.ComputeHash(originalBytes);
        //Convert encoded bytes back to a 'readable' string   
        return BitConverter.ToString(encodedBytes);
    }
}
