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
            this.model.Terrains.Add(new Terrain(TerrainType.HeavyWall, (int)area.Width, (int)area.Height, 0, 0));
            this.model.Terrains.Add(new Terrain(TerrainType.Building, (int)area.Width, (int)area.Height, 15, 8));
            this.model.Terrains.Add(new Terrain(TerrainType.LightWall, (int)area.Width, (int)area.Height, 8, 5));
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

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);


            if(area.Width> 0 && area.Height > 0 && model != null)
            {
                drawingContext.DrawRectangle(BackGroundBrush, null, new Rect(0, 0, area.Width, area.Height));

                #region Draw Terrains
                foreach (var terrain in model.Terrains)
                {
                    if (terrain.Type == TerrainType.HeavyWall)
                    {
                        drawingContext.DrawGeometry(BunkerBrush, null, terrain.Area);
                    }
                    else if (terrain.Type == TerrainType.Building)
                    {
                        drawingContext.DrawGeometry(BuildingBrush, null, terrain.Area);
                    }
                    else if (terrain.Type == TerrainType.LightWall)
                    {
                        drawingContext.DrawGeometry(WallBrush, null, terrain.Area);
                    }

                }
                #endregion

                #region Draw Tanks
                drawingContext.PushTransform(new RotateTransform(model.PlayerTank.Angle-90, model.PlayerTank.CenterX, model.PlayerTank.CenterY));
                drawingContext.DrawGeometry(FriendlyTankBrush, null, model.PlayerTank.Area);
                drawingContext.Pop();
                drawingContext.PushTransform(new RotateTransform(model.EnemyTank.Angle-90, model.EnemyTank.CenterX, model.EnemyTank.CenterY));
                drawingContext.DrawGeometry(EnemyBrush, null, model.EnemyTank.Area);
                drawingContext.Pop();
                #endregion

                #region Draw Bullets
                if (model.PlayerTank.Bullets.Count()>0)
                {
                    foreach (var bullet in model.PlayerTank.Bullets)
                    {
                        drawingContext.PushTransform(new RotateTransform(bullet.Angle, bullet.CenterX, bullet.CenterY));
                        drawingContext.DrawGeometry(BulletBrush, null, bullet.Area);
                        drawingContext.Pop();
                    }
                }

                if (model.EnemyTank.Bullets.Count() > 0)
                {
                    foreach (var bullet in model.EnemyTank.Bullets)
                    {
                        drawingContext.PushTransform(new RotateTransform(bullet.Angle, bullet.CenterX, bullet.CenterY));
                        drawingContext.DrawGeometry(BulletBrush, null, bullet.Area);
                        drawingContext.Pop();
                    }
                }
                #endregion

                #region Draw Life Indicators
                drawingContext.DrawGeometry(Brushes.DarkGray, null, model.PlayerTank.HpIndicator.Children[0]);
                drawingContext.DrawGeometry(Brushes.DeepSkyBlue, null, model.PlayerTank.HpIndicator.Children[1]);

                drawingContext.DrawGeometry(Brushes.DarkGray, null, model.EnemyTank.HpIndicator.Children[0]);
                drawingContext.DrawGeometry(Brushes.Red, null, model.EnemyTank.HpIndicator.Children[1]);

                drawingContext.DrawGeometry(Metal, null, model.PlayerTank.LifeIndicatorBackground);
                drawingContext.DrawGeometry(Metal, null, model.EnemyTank.LifeIndicatorBackground);
                //drawingContext.DrawGeometry(Brushes.DarkGray, null, model.PlayerTank.LifeIndicators.Children[3]);
                for (int i = 0; i < model.PlayerTank.Lives; i++)
                {
                    drawingContext.DrawGeometry(BluePlayerIconBrush, null, model.PlayerTank.LifeIndicators.Children[i]);
                }
                //drawingContext.DrawGeometry(BluePlayerLightIconBrush, null, model.PlayerTank.LifeIndicators.Children[0]);
                //drawingContext.DrawGeometry(BluePlayerLightIconBrush, null, model.PlayerTank.LifeIndicators.Children[1]);
                //drawingContext.DrawGeometry(BluePlayerLightIconBrush, null, model.PlayerTank.LifeIndicators.Children[2]);

                for (int i = 0; i < model.EnemyTank.Lives; i++)
                {
                    drawingContext.DrawGeometry(RedPlayerIconBrush, null, model.EnemyTank.LifeIndicators.Children[i]);
                }
                //drawingContext.DrawGeometry(RedPlayerLightIconBrush, null, model.EnemyTank.LifeIndicators.Children[0]);
                //drawingContext.DrawGeometry(RedPlayerLightIconBrush, null, model.EnemyTank.LifeIndicators.Children[1]);
                //drawingContext.DrawGeometry(RedPlayerLightIconBrush, null, model.EnemyTank.LifeIndicators.Children[2]);
                #endregion
            }
        }

        #region Get Brushes
        public Brush BackGroundBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Grassy_background.png"), UriKind.RelativeOrAbsolute)));
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
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Shell.png"), UriKind.RelativeOrAbsolute)));
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

        #region LifeIndicatorImages
        public Brush RedPlayerIconBrush
        {
            get
            {
                if (model.EnemyTank.Type == TankType.LightTank)
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "enemy_light_icon.png"), UriKind.RelativeOrAbsolute)));
                }
                else if (model.EnemyTank.Type == TankType.ArmoderTank)
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "enemy_medium_icon.png"), UriKind.RelativeOrAbsolute)));
                }
                else if (model.EnemyTank.Type == TankType.HeavyTank)
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "enemy_heavy_icon.png"), UriKind.RelativeOrAbsolute)));
                }
                else
                    return null;
            }
        }
        
        public Brush BluePlayerIconBrush
        {
            get
            {
                if (model.PlayerTank.Type == TankType.LightTank)
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "player_light_icon.png"), UriKind.RelativeOrAbsolute)));
                }
                else if (model.PlayerTank.Type == TankType.ArmoderTank)
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "player_medium_icon.png"), UriKind.RelativeOrAbsolute)));
                }
                else if (model.PlayerTank.Type == TankType.HeavyTank)
                {
                    return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "player_heavy_icon.png"), UriKind.RelativeOrAbsolute)));
                }
                else
                    return null;
                
            }
        }
        #endregion

        public Brush Metal
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "metal_background.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        #endregion
    }
}
