using MediaStudioService.Core.Enums;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace MediaStudioService.Services.Audio
{
    internal static class CoverManager
    {
        public static MemoryStream GetResizingCover(IFormFile formFile, BucketTypes bucketTypes)
        {
            var verticalRes = GetVerticalResolution(bucketTypes);
            var horizontalRes = GetHorizontalResolution(bucketTypes);

            using var coverImage = Image.Load(formFile.OpenReadStream(), out IImageFormat format);
            coverImage.Mutate(imageProccesingContext => imageProccesingContext.Resize(verticalRes, horizontalRes));

            MemoryStream coverStream = new MemoryStream();

            var imageEncoder = coverImage.GetConfiguration().ImageFormatsManager.FindEncoder(format);
            coverImage.Save(coverStream, imageEncoder);
            coverStream.Position = 0;
            return coverStream;
        }

        private static int GetVerticalResolution(BucketTypes bucketTypes)
            => bucketTypes switch
            {
                BucketTypes.albumcoverlarge => (int)CoverResolution.LargeVertical,
                BucketTypes.albumcovermedium => (int)CoverResolution.MediumeVertical,
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(bucketTypes)),
            };

        private static int GetHorizontalResolution(BucketTypes bucketTypes)
            => bucketTypes switch
            {
                BucketTypes.albumcoverlarge => (int)CoverResolution.LargeHorizontal,
                BucketTypes.albumcovermedium => (int)CoverResolution.MediumHorizontal,
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(bucketTypes)),
            };
    }
}
