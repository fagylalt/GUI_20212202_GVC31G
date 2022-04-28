using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tank_Combat.Logic;
using Tank_Combat.Models;

namespace Tank_Combat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        TankCombatLogic logic;
        TankType playerTankType;
        TankType enemyTankType;
        private void Dt_Tick(object? sender, EventArgs e)
        {
            logic.TimeStep();
            display.InvalidateVisual();
        }
        public GameWindow(TankType playerTankType)
        {
            InitializeComponent();

            this.playerTankType = playerTankType;

            Array values = Enum.GetValues(typeof(TankType));
            Random random = new Random();
            enemyTankType = (TankType)values.GetValue(random.Next(values.Length))!;
            ;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new TankCombatLogic((int)gameGrid.ActualWidth, (int)gameGrid.ActualHeight, playerTankType, enemyTankType);
            display.SetUpTankImages(playerTankType, enemyTankType);
            display.SetupModel(logic);
            display.SizeSetup(new Size(gameGrid.ActualWidth, gameGrid.ActualHeight));
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(10);
            dt.Tick += Dt_Tick;
            dt.Start();
            display.InvalidateVisual();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SizeSetup(new Size(gameGrid.ActualWidth, gameGrid.ActualHeight));
            display.InvalidateVisual();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if(Keyboard.IsKeyDown(Key.Left))
            //{
            //    logic.isLeftKeyDown = true;
            //}
            //if(Keyboard.IsKeyDown(Key.Up))
            //{
            //    logic.isUpKeyDown = true;
            //}
            //if(Keyboard.IsKeyDown(Key.Right))
            //{
            //    logic.isRightKeyDown = true;
            //}
            //if(Keyboard.IsKeyDown(Key.Down))
            //{
            //    logic.isDownKeyDown = true;
            //}
            //if(Keyboard.IsKeyDown(Key.Space))
            //{
            //    logic.Control(TankCombatLogic.Controls.Space);
            //}
        }
        private void CloseCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Close?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //logic.isRightKeyDown=false;
            //logic.isLeftKeyDown = false;
            //logic.isUpKeyDown = false;
            //logic.isDownKeyDown = false;
        }
    }
}
