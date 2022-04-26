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

namespace Tank_Combat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        TankCombatLogic logic;
        private void Dt_Tick(object? sender, EventArgs e)
        {
            logic.TimeStep();
        }
        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new TankCombatLogic((int)gameGrid.ActualWidth, (int)gameGrid.ActualHeight);
            display.SetupModel(logic);
            display.SizeSetup(new Size(gameGrid.ActualWidth, gameGrid.ActualHeight));
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(100);
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
            if(e.Key == Key.Left)
            {
                logic.Control(TankCombatLogic.Controls.Left);
            }
            else if(e.Key == Key.Up)
            {
                logic.Control(TankCombatLogic.Controls.Up);
            }
            else if(e.Key == Key.Right)
            {
                logic.Control(TankCombatLogic.Controls.Right);
            }
            else if(e.Key == Key.Down)
            {
                logic.Control(TankCombatLogic.Controls.Down);
            }
            else if(e.Key == Key.Space)
            {
                logic.Control(TankCombatLogic.Controls.Space);
            }
        }
        private void CloseCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Close?", "Close", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                this.Close();
        }
    }
}
