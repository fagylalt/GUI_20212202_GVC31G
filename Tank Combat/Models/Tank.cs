using System;
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

    public enum Team
    {
        Blue, Red
    }

    internal class Tank : GameItem
    {
        #region Setup
        public Team Team { get; set; }
        public TankType Type { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public double Angle { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int Lives { get; set; }
        public int ReloadTime { get; set; }
        public int Damage { get; set; }
        public int ScreenWidth { get; set; }
        public bool IsRespawning { get; set; }
        public bool IsRespawnOvertime { get; set; }
        public List<Bullet> Bullets { get; set; }
        public Stopwatch Time { get; set; }

        public Tank(Team team, TankType type, int screenWidth, int centerX, int centerY, double angle = 0)
        {
            Team = team;
            Type = type;
            CenterX = centerX;
            CenterY = centerY;
            Angle = angle;
            ScreenWidth = screenWidth;
            SetUpBasicTankProperties();
            Lives = 3;
            IsRespawning = false;
            IsRespawnOvertime = false;
            Bullets = new List<Bullet>();
            Time = new Stopwatch();
            Time.Start();
        }

        public void SetUpBasicTankProperties()
        {
            double _const = (double)ScreenWidth / 1000;
            if (Type == TankType.HeavyTank)
            {
                MaxHp = 10;
                Hp = MaxHp;
                ReloadTime = 800;
                Damage = 3;
                SpeedX = (int)(2 * _const);
                SpeedY = (int)(2 * _const);
            }
            else if (Type == TankType.ArmoderTank)
            {
                MaxHp = 8;
                Hp = MaxHp;
                ReloadTime = 600;
                Damage = 2;
                SpeedX = (int)(3 * _const);
                SpeedY = (int)(3 * _const);
            }
            else if (Type == TankType.LightTank)
            {
                MaxHp = 5;
                Hp = MaxHp;
                ReloadTime = 500;
                Damage = 1;
                SpeedX = (int)(4 * _const);
                SpeedY = (int)(4 * _const);
            }
        }

        public GeometryGroup HpIndicator
        {
            get
            {
                GeometryGroup group = new GeometryGroup();
                double xSize = ScreenWidth / 20;
                double ySize = xSize * 0.75;
                double remainingHpRate;
                if (Hp>0)
                {
                    remainingHpRate = (double)Hp/(double)MaxHp;
                }
                else
                {
                    remainingHpRate = 0;
                }

                Geometry backround = new RectangleGeometry(new Rect(new Point(CenterX - xSize / 4, CenterY - ySize), new Size(xSize / 2, xSize / 12.8)));
                Geometry foreground = new RectangleGeometry(new Rect(new Point(CenterX - xSize / 4, CenterY - ySize), new Size((xSize/2)*remainingHpRate, xSize / 12.8)));
                group.Children.Add(backround);
                group.Children.Add(foreground);
                return group;
            }
        }

        public GeometryGroup LifeIndicators
        {
            get
            {
                if (this.Team == Team.Blue)
                {
                    GeometryGroup geometryGroup = new GeometryGroup();
                    Geometry first = new RectangleGeometry(new Rect(new Point((ScreenWidth / 100) * 2, ScreenWidth / 100), new Size(ScreenWidth / 50, ScreenWidth / 50)));
                    Geometry second = new RectangleGeometry(new Rect(new Point((ScreenWidth / 100) * 6, ScreenWidth / 100), new Size(ScreenWidth / 50, ScreenWidth / 50)));
                    Geometry third = new RectangleGeometry(new Rect(new Point((ScreenWidth / 100) * 10, ScreenWidth / 100), new Size(ScreenWidth / 50, ScreenWidth / 50)));
                    geometryGroup.Children.Add(first);
                    geometryGroup.Children.Add(second);
                    geometryGroup.Children.Add(third);
                    return geometryGroup;
                }
                else
                {
                    GeometryGroup geometryGroup = new GeometryGroup();
                    Geometry first = new RectangleGeometry(new Rect(new Point((ScreenWidth - (ScreenWidth / 100) * 6 + ScreenWidth / 50), ScreenWidth / 100), new Size(ScreenWidth / 50, ScreenWidth / 50)));
                    Geometry second = new RectangleGeometry(new Rect(new Point((ScreenWidth - (ScreenWidth / 100) * 10 + ScreenWidth / 50), ScreenWidth / 100), new Size(ScreenWidth / 50, ScreenWidth / 50)));
                    Geometry third = new RectangleGeometry(new Rect(new Point((ScreenWidth - (ScreenWidth / 100) * 14 + ScreenWidth / 50), ScreenWidth / 100), new Size(ScreenWidth / 50, ScreenWidth / 50)));
                    geometryGroup.Children.Add(first);
                    geometryGroup.Children.Add(second);
                    geometryGroup.Children.Add(third);
                    return geometryGroup;
                }
            }
        }

        public GeometryGroup LifeIndicatorBackground
        {
            get
            {
                if (this.Team == Team.Blue)
                {
                    GeometryGroup geometryGroup = new GeometryGroup();
                    Geometry background = new RectangleGeometry(new Rect(new Point(0, ScreenWidth / 100), new Size((ScreenWidth / 100) * 14, ScreenWidth / 50)));
                    Geometry background2 = new EllipseGeometry(new Point((ScreenWidth / 100) * 14, ScreenWidth / 50), ScreenWidth / 100, ScreenWidth / 100);
                    geometryGroup.Children.Add(background);
                    geometryGroup.Children.Add(background2);
                    geometryGroup.FillRule = FillRule.Nonzero;
                    return geometryGroup;
                }
                else
                {
                    GeometryGroup geometryGroup = new GeometryGroup();
                    Geometry background = new RectangleGeometry(new Rect(new Point(ScreenWidth - (ScreenWidth / 100) * 14, ScreenWidth / 100), new Size((ScreenWidth / 100) * 14, ScreenWidth / 50)));
                    Geometry background2 = new EllipseGeometry(new Point((ScreenWidth - (ScreenWidth / 100) * 14), ScreenWidth / 50), ScreenWidth / 100, ScreenWidth / 100);
                    geometryGroup.Children.Add(background);
                    geometryGroup.Children.Add(background2);
                    geometryGroup.FillRule = FillRule.Nonzero;
                    return geometryGroup;
                }
            }
        }

        public override Geometry Area
        {
            get
            {
                double xSize = ScreenWidth/20;
                double ySize = xSize*0.75;
                Geometry tankGeometry = new RectangleGeometry(new Rect(new Point(CenterX-xSize/2, CenterY-ySize/2), new Size(xSize, ySize)));
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
            Tank tankAtNewPosition = new(Team, Type, ScreenWidth, newCenterX, newCenterY);

            foreach (var barrier in barriers)
            {
                if (barrier.GetType() == this.GetType())
                {
                    if (!(barrier as Tank)!.IsRespawning && !this.IsRespawning)
                    {
                        if (tankAtNewPosition.IsCollision(barrier) && !this.Equals(barrier))
                        {
                            isInBarrier = true;
                        }
                    }
                }
                else
                {
                    if (tankAtNewPosition.IsCollision(barrier) && !this.Equals(barrier))
                    {
                        isInBarrier = true;
                    }
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
            if (Time.ElapsedMilliseconds>ReloadTime)
            {
                if (!this.IsRespawning)
                {
                    int _const = ScreenWidth / 100;
                    double dx = 0;
                    double dy = 0;
                    if (angle == 0)
                    {
                        dy -= _const;
                    }
                    else if (angle == 90)
                    {
                        dx += _const;
                    }
                    else if (angle == 180)
                    {
                        dy += _const;
                    }
                    else if (angle == 270)
                    {
                        dx -= _const;
                    }

                    Bullets.Add(new Bullet(this.CenterX, this.CenterY, (int)dx, (int)dy, ScreenWidth, angle));
                    Time.Restart();
                }
            }
            ;
        }

        public void GotHit(int dmg)
        {
            Hp -= dmg;
            if (Lives > 0)
            {
                if (Hp <= 0)
                {
                    Lives -= 1;
                    Hp = MaxHp;
                }
            }
        }
        #endregion
    }
}
