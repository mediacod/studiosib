using MediaStudioService.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MediaStudioService.Core.Classes
{
    public static class FormatValidator
    {
        public static bool IsAudioFormat(IFormFile audio)
        {
            var fileExtensionn = Path.GetExtension(audio.FileName);
            return Enum.IsDefined(typeof(AudioTypes), fileExtensionn.Substring(1));
        }

        public static bool IsImageFormat(IFormFile Image)
        {
            var fileExtensionn = Path.GetExtension(Image.FileName);
            return Enum.IsDefined(typeof(ImageTypes), fileExtensionn.Substring(1));
        }
    }
}
