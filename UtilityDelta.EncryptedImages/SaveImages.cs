using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using UtilityDelta.Encryption;

namespace UtilityDelta.EncryptedImages
{
    public class SaveImages : ISaveImages
    {
        public IEnumerable<ImageSaveResult> FromPath(string path, string encryptionKey, string destinationPath,
            Guid imageId, IEnumerable<Size> sizes)
        {
            using (var source = File.OpenRead(path))
            {
                return FromStream(source, encryptionKey, destinationPath, imageId, sizes);
            }
        }

        public IEnumerable<ImageSaveResult> FromStream(Stream source, string encryptionKey, string destinationPath,
            Guid imageId, IEnumerable<Size> sizes)
        {
            var result = new List<ImageSaveResult>();
            if (sizes == null) return result;

            if (!string.IsNullOrEmpty(destinationPath) && !Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            var symmetricEncryption = new SymmetricEncryptionWithKnownKey(encryptionKey);

            foreach (var size in sizes)
            {
                var path = destinationPath + "\\" + size.ImageSizePrefix + "-" + imageId;

                if (size.MaxWidth == null && size.MaxHeight == null)
                {
                    using (var fileStream = File.OpenWrite(path))
                    using (var cryptStream = symmetricEncryption.GetEncryptStream(fileStream))
                    {
                        source.CopyTo(cryptStream);
                        source.Position = 0;
                    }

                    continue;
                }

                using (var image = Image.Load(source, out var format))
                {
                    var properHeight = ScaleCalc.Downsize(image.Height, image.Width, size.MaxHeight, size.MaxWidth);
                    image.Mutate(x => x.Resize(properHeight.Width, properHeight.Height));


                    using (var fileStream = File.OpenWrite(path))
                    using (var cryptStream = symmetricEncryption.GetEncryptStream(fileStream))
                    {
                        image.Save(cryptStream, format);
                    }

                    result.Add(new ImageSaveResult
                    {
                        Height = properHeight.Height,
                        Width = properHeight.Width,
                        ImageSizePrefix = size.ImageSizePrefix,
                        Path = path
                    });
                }
            }

            return result;
        }
    }
}