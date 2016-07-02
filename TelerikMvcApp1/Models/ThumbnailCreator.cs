using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Kendo.Mvc.UI;

namespace TelerikMvcApp1.Models
{
    public class ThumbnailCreator
    {
        private static readonly IDictionary<string, ImageFormat> ImageFormats = new Dictionary<string, ImageFormat>{
            {"image/png", ImageFormat.Png},
            {"image/gif", ImageFormat.Gif},
            {"image/jpeg", ImageFormat.Jpeg}
        };

        public byte[] Create(Stream source, ImageSize desiredSize, string contentType)
        {
            using (var image = Image.FromStream(source))
            {
                using (var newImage = image.Resize(desiredSize.Width, desiredSize.Height))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        newImage.Save(memoryStream, ImageFormats[contentType]);

                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public byte[] CreateFill(Stream source, ImageSize desiredSize, string contentType)
        {
            using (var image = Image.FromStream(source))
            {
                using (var memoryStream = new MemoryStream())
                {
                    FixedSize(image, desiredSize.Width, desiredSize.Height, true).Save(memoryStream, ImageFormats[contentType]);
                    return memoryStream.ToArray();
                }
            }
        }

        private Image FixedSize(Image imgPhoto, int Width, int Height, bool needToFill)
        {
            var sourceWidth = imgPhoto.Width;
            var sourceHeight = imgPhoto.Height;
            var sourceX = 0;
            var sourceY = 0;
            var destX = 0;
            var destY = 0;

            float nPercent;

            var nPercentW = (Width / (float)sourceWidth);
            var nPercentH = (Height / (float)sourceHeight);
            if (!needToFill)
            {
                nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;
            }
            else
            {
                if (nPercentH > nPercentW)
                {
                    nPercent = nPercentH;
                    destX = (int)Math.Round((Width -
                        (sourceWidth * nPercent)) / 2);
                }
                else
                {
                    nPercent = nPercentW;
                    destY = (int)Math.Round((Height -
                        (sourceHeight * nPercent)) / 2);
                }
            }

            if (nPercent > 1)
                nPercent = 1;

            var destWidth = (int)Math.Round(sourceWidth * nPercent);
            var destHeight = (int)Math.Round(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(destWidth <= Width ? destWidth : Width, destHeight < Height ? destHeight : Height,
                PixelFormat.Format32bppRgb);

            var grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}