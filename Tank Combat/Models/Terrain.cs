using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tank_Combat.Models
{
    enum TerrainType
    {
        HeavyWall, Building, LightWall
    }
    internal class Terrain : GameItem
    {
        public TerrainType Type { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Hp { get; set; }
        public int ScreenHeight { get; set; }
        public int ScreenWidth { get; set; }

        public Terrain(TerrainType type, int screenWidth, int screenHeight, int centerX, int centerY)
        {
            Type = type;
            CenterX = centerX;
            CenterY = centerY;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            if (Type == TerrainType.HeavyWall)
            {
                Hp = 8;
            }
            else if (Type == TerrainType.LightWall)
            {
                Hp = 5;
            }
            else if (Type == TerrainType.Building)
                Hp = int.MaxValue;
        }

        public override Geometry Area
        {
            get
            {
                return new RectangleGeometry(new Rect(new Point(ScreenWidth/16 * CenterX, ScreenHeight/9 * CenterY), new Size(ScreenWidth/16, ScreenHeight/9)));
            }
        }

        public void GotHit(int dmg)
        {
            Hp -= dmg;
        }
    }
}
