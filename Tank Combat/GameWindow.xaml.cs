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
using Tank_Combat.Logic;

namespace Tank_Combat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        TankCombatLogic logic;
        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new TankCombatLogic((int)gameGrid.ActualWidth, (int)gameGrid.ActualHeight);
            display.SetupModel(logic);
            display.SizeSetup(new Size(gameGrid.ActualWidth, gameGrid.ActualHeight));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SizeSetup(new Size(gameGrid.ActualWidth, gameGrid.ActualHeight));
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
