# dotnet-encrypted-images
Take an image stream, save thumbnail, medium and original sizes on the file system, all encrypted with a secret password.

## Usage
```c#

using System;
using System.IO;
using System.Text;
using UtilityDelta.EncryptedImages;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SaveImages();
            var imageGuid = Guid.NewGuid(); //Unique key from your database
            service.FromPath("image.jpg", "test-key", "subfolder", imageGuid, new List<Size>
            {
                new Size("original", null, null),
                new Size("medium", 1000, 500),
                new Size("thumb", 200, 200)
            });
        }
    }
}

```
