namespace UtilityDelta.EncryptedImages
{
    public static class ScaleCalc
    {
        public static (int Width, int Height) Downsize(int currentHeight, int currentWidth, int? maxHeight,
            int? maxWidth)
        {
            if (maxHeight != null && currentHeight > maxHeight.Value)
            {
                var downscaleRatio = maxHeight.Value / (double) currentHeight;
                currentHeight = maxHeight.Value;
                currentWidth = (int) (currentWidth * downscaleRatio);
            }

            // ReSharper disable once InvertIf
            if (maxWidth != null && currentWidth > maxWidth.Value)
            {
                var downscaleRatio = maxWidth.Value / (double) currentWidth;
                currentWidth = maxWidth.Value;
                currentHeight = (int) (currentHeight * downscaleRatio);
            }

            return (currentWidth, currentHeight);
        }
    }
}