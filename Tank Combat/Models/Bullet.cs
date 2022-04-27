using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tank_Combat.Models
{
    internal class Bullet : GameItem
    {
        #region Setup
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int Angle { get; set; }

        public Bullet(int centerX, int centerY, int speedX, int speedY, int angle)
        {
            CenterX = centerX;
            CenterY = centerY;
            SpeedX = speedX;
            SpeedY = speedY;
            Angle = angle;
        }

        public override Geometry Area
        {
            get
            {
                return new EllipseGeometry(new Point(CenterX, CenterY), 10, 20);
            }
        }
        #endregion

        #region Methods
        public void Move()
        {
            CenterX += SpeedX;
            CenterY += SpeedY;
        }
        #endregion
    }
}
