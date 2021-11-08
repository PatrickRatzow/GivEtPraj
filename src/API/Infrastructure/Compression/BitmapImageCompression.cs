using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Commentor.GivEtPraj.Infrastructure.Compression;

public class BitmapImageCompression : IImageCompression
{
    public MemoryStream CompressImage(Stream content, int level = 30)
    {
        var image = Image.FromStream(content, true);

        return VaryQualityLevel(image, level);
    }

    private static MemoryStream VaryQualityLevel(Image img, int level)
    {
        using var bitmap = new Bitmap(img);

        var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
        if (jpgEncoder is null) throw new("Unable to find specified encoder");
        var myEncoder = Encoder.Quality;
        var myEncoderParameters = new EncoderParameters(1);
        var compressedImage = new MemoryStream();
        var myEncoderParameter = new EncoderParameter(myEncoder, level);
        myEncoderParameters.Param[0] = myEncoderParameter;

        bitmap.Save(compressedImage, jpgEncoder, myEncoderParameters);

        // Need to reset the stream for any future use
        compressedImage.Flush();
        compressedImage.Position = 0;

        return compressedImage;
    }

    private static ImageCodecInfo? GetEncoder(ImageFormat format)
    {
        var codecs = ImageCodecInfo.GetImageEncoders();

        return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
    }
}