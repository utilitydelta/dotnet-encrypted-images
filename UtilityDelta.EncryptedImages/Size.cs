namespace UtilityDelta.EncryptedImages
{
    public class Size
    {
        public Size(string imageSizePrefix, int? maxWidth, int? maxHeight)
        {
            ImageSizePrefix = imageSizePrefix;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }

        public string ImageSizePrefix { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
    }
}