using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography;

namespace MediaStudioService.Service.ResourceService
{
    public static class IFormFileManager
    {
        public static string GetHash(IFormFile file)
        {
            // get stream from file then convert it to a MemoryStream
            MemoryStream stream = new MemoryStream();
            file.OpenReadStream().CopyTo(stream);

            byte[] bytes = MD5.Create().ComputeHash(stream.ToArray());
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
        }

        public static string GetHash(MemoryStream stream)
        {
            byte[] bytes = MD5.Create().ComputeHash(stream.ToArray());
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
        }

        public static string GetFullName(IFormFile file)
        {
            return GetHash(file) + Path.GetExtension(file.FileName);
        }
    }
}
