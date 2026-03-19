using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Flowers
{
    internal static class ImagePlaceholder
    {
        private static readonly object _sync = new object();
        private static Image _image;

        public static Image Get(string text = "Нет фото")
        {
            lock (_sync)
            {
                if (_image != null)
                    return _image;

                var bmp = new Bitmap(200, 150);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.FromArgb(235, 235, 235));

                    using (var pen = new Pen(Color.FromArgb(180, 180, 180), 2))
                    {
                        g.DrawRectangle(pen, 1, 1, bmp.Width - 2, bmp.Height - 2);
                    }

                    using (var font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point))
                    using (var brush = new SolidBrush(Color.FromArgb(120, 120, 120)))
                    using (var format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    })
                    {
                        g.DrawString(text ?? string.Empty, font, brush,
                            new RectangleF(0, 0, bmp.Width, bmp.Height), format);
                    }
                }

                _image = bmp;
                return _image;
            }
        }
    }
}

