using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tank_Combat.Renderer
{
    internal class Display: FrameworkElement
    {
        Size area;

        public void SizeSetup(Size _area)
        {
            this.area = _area;
            this.InvalidateVisual();
        }
        public Brush BackGroundBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Grassy_background.png"),UriKind.RelativeOrAbsolute)));
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(BackGroundBrush, null, new Rect(0, 0, area.Width, area.Height));
        }
    }
}
