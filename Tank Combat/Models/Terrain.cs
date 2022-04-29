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

        public Terrain(TerrainType type, int centerX, int centerY)
        {
            Type = type;
            CenterX = centerX;
            CenterY = centerY;
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
                return new RectangleGeometry(new Rect(new Point(CenterX, CenterY), new Size(200, 200)));
            }
        }

        public void GotHit(int dmg)
        {
            Hp -= dmg;
        }
    }
}
