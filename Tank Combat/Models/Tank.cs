using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tank_Combat.Models
{
    internal class Tank : GameItem
    {
        #region Setup
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public double Angle { get; set; }
        public int Hp { get; set; }
        public List<Bullet> Bullets { get; set; }

        public Tank(int centerX, int centerY, int speedX, int speedY, double angle = 0)
        {
            CenterX = centerX;
            CenterY = centerY;
            SpeedX = speedX;
            SpeedY = speedY;
            Angle = angle;
            Hp = 3;
            Bullets = new List<Bullet>();
        }

        public override Geometry Area
        {
            get
            {
                return new RectangleGeometry(new Rect(new Point(CenterX, CenterY), new Size(75, 75)));
            }
        }
        #endregion

        #region Methods
        public void Move(int angle, List<GameItem> barriers)
        {
            int newCenterX = CenterX;
            int newCenterY = CenterY;
            bool isInBarrier = false;

            if (angle == 0)
            {
                newCenterY -= SpeedY;
            }
            else if (angle == 90)
            {
                newCenterX += SpeedX;
            }
            else if (angle == 180)
            {
                newCenterY += SpeedY;
            }
            else if (angle == 270)
            {
                newCenterX -= SpeedX;
            }
            Tank tankAtNewPosition = new(newCenterX, newCenterY, 0, 0);

            foreach (var barrier in barriers)
            {
                if (tankAtNewPosition.IsCollision(barrier))
                {
                    isInBarrier = false;
                }
            }
            if (!isInBarrier)
            {
                CenterX = newCenterX;
                CenterY = newCenterY;
            }
        }

        public void Shoot(int angle)
        {
            double dx = 0;
            double dy = 0;
            if (angle == 0)
            {
                dy -= 10;
            }
            else if (angle == 90)
            {
                dx += 10;
            }
            else if (angle == 180)
            {
                dy += 10;
            }
            else if (angle == 270)
            {
                dx -= 10;
            }

            Bullets.Add(new Bullet(this.CenterX, this.CenterY, (int)dx, (int)dy));
        }

        public void GotHit()
        {
            Hp -= 1;
        }
        #endregion
    }
}
