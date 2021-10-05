using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Commentor.GivEtPraj.Application.Services
{
    public static class CompressionService
    {
        public static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public static MemoryStream VaryQualityLevel(Image img, int level)
        {
            // Get a bitmap. The using statement ensures objects  
            // are automatically disposed from memory after use.  
            using (Bitmap bmp1 = new Bitmap(img))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                MemoryStream CompressedImg = new MemoryStream();

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, level);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(CompressedImg, jpgEncoder, myEncoderParameters);






                return CompressedImg;
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}

