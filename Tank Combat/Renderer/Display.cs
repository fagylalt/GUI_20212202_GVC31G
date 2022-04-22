using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tank_Combat.Models;

namespace Tank_Combat.Renderer
{
    internal class Display: FrameworkElement
    {
        Size area;
        Random rand;
        IGameModel model;

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
                drawingContext.DrawRectangle(FriendlyTankBrush, null, new Rect(0,0,50,50));
                drawingContext.DrawRectangle(EnemyBrush, null, new Rect(50,50, 50, 50));
                drawingContext.DrawRectangle(BunkerBrush, null, new Rect(100, 100, 50, 50));
                drawingContext.DrawRectangle(BuildingBrush, null, new Rect(150, 150, 50, 50));
                drawingContext.DrawRectangle(WallBrush, null, new Rect(200,200, 50, 50));
            }
            
            
        }
        public Brush EnemyBrush
        {
            get
            {
                ImageBrush enemyImage = new ImageBrush();
                int chosenEnemy = rand.Next(1, 4);
                switch (chosenEnemy)
                {
                    case 1:
                        enemyImage.ImageSource = new BitmapImage(new Uri(Path.Combine("Images", "RED_basic_tank.png"), UriKind.RelativeOrAbsolute));
                        break;
                    case 2:
                        enemyImage.ImageSource = new BitmapImage(new Uri(Path.Combine("Images", "RED_heavy_tank.png"), UriKind.RelativeOrAbsolute));
                        break;
                    case 3:
                        enemyImage.ImageSource = new BitmapImage(new Uri(Path.Combine("Images", "RED_light_tank.png"), UriKind.RelativeOrAbsolute));
                        break;
                }
                return enemyImage;
            }
        }
        public Brush FriendlyTankBrush
        {
            get
            {
                ImageBrush tankImage = new ImageBrush();
                int chosenEnemy = rand.Next(1, 4);
                switch (chosenEnemy)
                {
                    case 1:
                        tankImage.ImageSource = new BitmapImage(new Uri(Path.Combine("Images", "BLUE_basic_tank.png"), UriKind.RelativeOrAbsolute));
                        break;
                    case 2:
                        tankImage.ImageSource = new BitmapImage(new Uri(Path.Combine("Images", "BLUE_heavy_tank.png"), UriKind.RelativeOrAbsolute));
                        break;
                    case 3:
                        tankImage.ImageSource = new BitmapImage(new Uri(Path.Combine("Images", "BLUE_light_tank.png"), UriKind.RelativeOrAbsolute));
                        break;
                }
                return tankImage;
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
