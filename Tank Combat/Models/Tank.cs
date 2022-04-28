﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tank_Combat.Models
{
    public enum TankType
    {
        HeavyTank, LightTank, ArmoderTank
    }

    internal class Tank : GameItem
    {
        #region Setup
        public TankType Type { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public double Angle { get; set; }
        public int Hp { get; set; }
        public List<Bullet> Bullets { get; set; }
        public Stopwatch time { get; set; }

        public Tank(TankType type, int centerX, int centerY, int speedX, int speedY, double angle = 0)
        {
            Type = type;
            CenterX = centerX;
            CenterY = centerY;
            SpeedX = speedX;
            SpeedY = speedY;
            Angle = angle;
            Hp = 3;
            Bullets = new List<Bullet>();
            time = new Stopwatch();
            time.Start();
        }

        public override Geometry Area
        {
            get
            {
                double xSize = 100;
                double ySize = 75;
                Geometry tankGeometry = new RectangleGeometry(new Rect(new Point(CenterX-xSize/2, CenterY-ySize/2), new Size(100, 75)));
                //Point p = new Point(tankGeometry.Bounds.TopLeft.X + tankGeometry.Bounds.Width / 2, tankGeometry.Bounds.TopLeft.Y + tankGeometry.Bounds.Height / 2);
                //tankGeometry.Transform = new RotateTransform(Angle, p.X, p.Y);
                return tankGeometry;
                //return new RectangleGeometry(new Rect(new Point(CenterX, CenterY), new Size(75, 75)));
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
            Tank tankAtNewPosition = new(Type, newCenterX, newCenterY, 0, 0);

            foreach (var barrier in barriers)
            {
                if (tankAtNewPosition.IsCollision(barrier) && !this.Equals(barrier))
                {
                    isInBarrier = true;
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
            if (time.ElapsedMilliseconds>500)
            {
                double dx = 0;
                double dy = 0;
                if (angle == 0)
                {
                    dy -= 30;
                }
                else if (angle == 90)
                {
                    dx += 30;
                }
                else if (angle == 180)
                {
                    dy += 30;
                }
                else if (angle == 270)
                {
                    dx -= 30;
                }

                Bullets.Add(new Bullet(this.CenterX, this.CenterY, (int)dx, (int)dy, angle));
                time.Restart();
            }
            ;
        }

        public void GotHit()
        {
            Hp -= 1;
        }
        #endregion
    }
}
