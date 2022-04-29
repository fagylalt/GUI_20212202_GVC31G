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
        public int ReloadTime { get; set; }
        public int Damage { get; set; }
        public List<Bullet> Bullets { get; set; }
        public Stopwatch Time { get; set; }

        public Tank(TankType type, int centerX, int centerY, double angle = 0)
        {
            Type = type;
            CenterX = centerX;
            CenterY = centerY;
            Angle = angle;
            SetUpBasicTankProperties();
            Bullets = new List<Bullet>();
            Time = new Stopwatch();
            Time.Start();
        }

        public void SetUpBasicTankProperties()
        {
            if (Type == TankType.HeavyTank)
            {
                Hp = 10;
                ReloadTime = 800;
                Damage = 3;
                SpeedX = 2;
                SpeedY = 2;
            }
            else if (Type == TankType.ArmoderTank)
            {
                Hp = 8;
                ReloadTime = 600;
                Damage = 2;
                SpeedX = 3;
                SpeedY = 3;
            }
            else if (Type == TankType.LightTank)
            {
                Hp = 5;
                ReloadTime = 500;
                Damage = 1;
                SpeedX = 4;
                SpeedY = 4;
            }
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
            Tank tankAtNewPosition = new(Type, newCenterX, newCenterY);

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
            if (Time.ElapsedMilliseconds>500)
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
                Time.Restart();
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
