using System;
using System.Collections.Generic;
using System.IO;

namespace UtilityDelta.EncryptedImages
{
    public interface ISaveImages
    {
        IEnumerable<ImageSaveResult> FromPath(string path, string encryptionKey, string destinationPath, Guid imageId, IEnumerable<Size> sizes);
        IEnumerable<ImageSaveResult> FromStream(Stream source, string encryptionKey, string destinationPath, Guid imageId, IEnumerable<Size> sizes);
    }
}