using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UtilityDelta.Encryption;
using Xunit;

namespace UtilityDelta.EncryptedImages.Test
{
    public class TestSaveImages
    {
        private static Stream ExtractResource(string filename)
        {
            var a = Assembly.GetExecutingAssembly();
            return a.GetManifestResourceStream(filename);
        }

        private bool CompareStreams(Stream a, Stream b)
        {
            if (a == null &&
                b == null)
                return true;
            if (a == null ||
                b == null)
                throw new ArgumentNullException(
                    a == null ? "a" : "b");

            if (a.Length < b.Length)
                return false;
            if (a.Length > b.Length)
                return false;

            for (var i = 0; i < a.Length; i++)
            {
                var aByte = a.ReadByte();
                var bByte = b.ReadByte();
                if (aByte.CompareTo(bByte) != 0)
                    return false;
            }

            return true;
        }

        [Fact]
        public void Test1()
        {
            var service = new SaveImages();
            var imageGuid = Guid.NewGuid();
            var origStream = ExtractResource("UtilityDelta.EncryptedImages.Test.image.jpg");
            service.FromStream(origStream, "test-key", "subfolder", imageGuid, new List<Size>
            {
                new Size("medium", 1000, 500),
                new Size("thumb", 200, 200),
                new Size("original", null, null)
            });

            var decryptor = new SymmetricEncryptionWithKnownKey("test-key");
            var output = new MemoryStream();
            decryptor.Decrypt(File.OpenRead($"subfolder\\original-{imageGuid}"), output);
            output.Position = 0;
            origStream.Position = 0;
            Assert.True(CompareStreams(origStream, output));

            File.Delete($"subfolder\\original-{imageGuid}");
            File.Delete($"subfolder\\medium-{imageGuid}");
            File.Delete($"subfolder\\thumb-{imageGuid}");
        }
    }
}