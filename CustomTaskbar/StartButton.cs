using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CustomTaskbar
{
    class StartButton
    {
        public Bitmap onMove { get; set; }
        public Bitmap onClick { get; set; }
        public Bitmap Normal { get; set; }

        private Color color;

        public StartButton()
        {
            this.Normal = new Bitmap(CustomTaskbar.Properties.Resources.StartButton_img);
            this.color = Color.Black;
            this.onMove = ChangeColor(CustomTaskbar.Properties.Resources.StartButton_img);
            this.color = Color.Gray;
            this.onClick = ChangeColor(CustomTaskbar.Properties.Resources.StartButton_img);
            
        }

        private Bitmap ChangeColor(Bitmap bitmap)
        {
            Bitmap b = bitmap;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color bitColor = b.GetPixel(i, j);
                    bitmap.SetPixel(i, j, Color.FromArgb(bitColor.A, color.R, color.G, color.B));
                }
            }
            return b;
        }
    }
}
