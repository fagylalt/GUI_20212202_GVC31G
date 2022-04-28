using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tank_Combat.Logic;
using Tank_Combat.Models;

namespace Tank_Combat.Renderer
{
    internal class Display: FrameworkElement
    {
        Size area;
        Random rand;
        IGameModel model;
        string playerTankImage;
        string enemyTankImage;

        public Display()
        {
            rand = new Random();
        }

        public void SizeSetup(Size _area)
        {
            this.area = _area;
            this.InvalidateVisual();
        }
        public void SetupModel(IGameModel _model)
        {
            this.model = _model;
            this.model.Changed += (sender, EventArgs) => this.InvalidateVisual();
            this.model.Terrains.Add(new Terrain(TerrainType.HeavyWall, 500, 150));
            this.model.Terrains.Add(new Terrain(TerrainType.Building, 1500, 600));
            this.model.Terrains.Add(new Terrain(TerrainType.LightWall, 780, 1000));
            foreach (var terrain in model.Terrains)
            {
                model.Barriers.Add(terrain);
            }
        }
        public void SetUpTankImages(TankType playerTankType, TankType enemyTankType)
        {
            if(playerTankType == TankType.HeavyTank)
            {
                playerTankImage = "BLUE_heavy_tank.png";
            }
            else if(playerTankType == TankType.ArmoderTank)
            {
                playerTankImage = "BLUE_basic_tank.png";
            }
            else if(playerTankType == TankType.LightTank)
            {
                playerTankImage = "BLUE_light_tank.png";
            }

            if (enemyTankType == TankType.HeavyTank)
            {
                enemyTankImage = "RED_heavy_tank.png";
            }
            else if (enemyTankType == TankType.ArmoderTank)
            {
                enemyTankImage = "RED_basic_tank.png";
            }
            else if (enemyTankType == TankType.LightTank)
            {
                enemyTankImage = "RED_light_tank.png";
            }
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
            if(area.Width> 0 && area.Height > 0 && model != null)
            {
                drawingContext.DrawRectangle(BackGroundBrush, null, new Rect(0, 0, area.Width, area.Height));
                drawingContext.DrawGeometry(EnemyBrush, null, model.EnemyTank.Area);
                ;
                drawingContext.PushTransform(new RotateTransform(model.PlayerTank.Angle-90, model.PlayerTank.CenterX, model.PlayerTank.CenterY));
                drawingContext.DrawGeometry(FriendlyTankBrush, null, model.PlayerTank.Area);
                drawingContext.Pop();

                //drawingContext.DrawGeometry(FriendlyTankBrush, null, model.PlayerTank.Area);

                foreach (var terrain in model.Terrains)
                {
                    if (terrain.Type==TerrainType.HeavyWall)
                    {
                        drawingContext.DrawGeometry(BunkerBrush, null, terrain.Area);
                    }
                    else if (terrain.Type==TerrainType.Building)
                    {
                        drawingContext.DrawGeometry(BuildingBrush, null, terrain.Area);
                    }
                    else if (terrain.Type==TerrainType.LightWall)
                    {
                        drawingContext.DrawGeometry(WallBrush, null, terrain.Area);
                    }
                    
                }
                //drawingContext.DrawGeometry(BunkerBrush, null, new Terrain(TerrainType.HeavyWall, 75, 75).Area);
                //drawingContext.DrawGeometry(BuildingBrush, null, new Terrain(TerrainType.Building,150,150).Area);
                //drawingContext.DrawGeometry(WallBrush, null, new Terrain(TerrainType.LightWall, 0, 0).Area);
                //drawingContext.DrawGeometry(BulletBrush, null, model.SingleBullet.Area);

                if (model.PlayerTank.Bullets.Count()>0)
                {
                    foreach (var bullet in model.PlayerTank.Bullets)
                    {
                        drawingContext.PushTransform(new RotateTransform(bullet.Angle, bullet.CenterX, bullet.CenterY));
                        drawingContext.DrawGeometry(BulletBrush, null, bullet.Area);
                        drawingContext.Pop();
                    }
                }
            }



        }
       
        public Brush EnemyBrush
        {
            get 
            { 
            return new ImageBrush( new BitmapImage(new Uri(Path.Combine("Images", enemyTankImage), UriKind.RelativeOrAbsolute)));
            }
           
        }
        public Brush BulletBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "shell.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush FriendlyTankBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", playerTankImage), UriKind.RelativeOrAbsolute)));
            } 
        }
        public Brush BunkerBrush
        {
            get 
            {
            return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Bunker.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BuildingBrush
        {
            get
            {
            return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "House.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush WallBrush
        {
            get
            {
            return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Brick_wall.png"), UriKind.RelativeOrAbsolute)));
            }
        }

    }
}
