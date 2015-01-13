using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.DomainServices
{
    public class ImageResizer
    {
        public static Bitmap Resize(System.Drawing.Image image, ImageCombination imageCombination)
        {
            int width = imageCombination.Width;
            int height = imageCombination.Height;
            double actualHeight = image.Height;
            double actualWidth = image.Width;

            if (width == 0 || height == 0)
                return (Bitmap)image;

            Bitmap imgResized = new Bitmap(width, height);
            imgResized.SetResolution(72, 72);

            using (Graphics graphic = Graphics.FromImage(imgResized))
            {
                graphic.Clear(Color.White);
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;

                double factor = 1;
                if (width > 0)
                    factor = width / actualWidth;
                else if (height > 0)
                    factor = height / actualHeight;

                var sourceRectangle = new Rectangle(0, 0, (int)actualWidth, (int)actualHeight);
                var destinationRectangle = new Rectangle(0, 0, (int)(factor * actualWidth), (int)(factor * actualHeight));

                if (width > destinationRectangle.X)
                {
                    destinationRectangle.X = (width - destinationRectangle.Width) / 2;
                }
                if (height > destinationRectangle.Y)
                {
                    destinationRectangle.Y = (height - destinationRectangle.Height) / 2;
                }
                graphic.DrawImage(image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }
            return imgResized;
        }
    }
}
