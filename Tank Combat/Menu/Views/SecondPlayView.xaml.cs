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
using System.Windows.Shapes;
using Tank_Combat.Models;

namespace Tank_Combat.Menu.Views
{
    /// <summary>
    /// Interaction logic for SecondPlayView.xaml
    /// </summary>
    public partial class SecondPlayView : Window
    {
        TankType playerTankType;
        TankType enemyTankType;
        public SecondPlayView(TankType blueplayerTankType)
        {
            InitializeComponent();
            playerTankType = blueplayerTankType;

        }

        private void Button_Click_Light_Tank(object sender, RoutedEventArgs e)
        {
            enemyTankType = TankType.LightTank;
            GameWindow win = new GameWindow(playerTankType,enemyTankType);
            win.Show();
            this.Close();
        }

        private void Heavy_Button_Click(object sender, RoutedEventArgs e)
        {
            enemyTankType = TankType.HeavyTank;
            GameWindow win = new GameWindow(playerTankType, enemyTankType);
            win.Show();
            this.Close();
        }

        private void Medium_Button_Click(object sender, RoutedEventArgs e)
        {
            enemyTankType = TankType.ArmoderTank;
            GameWindow win = new GameWindow(playerTankType, enemyTankType);
            win.Show();
            this.Close();
        }
    }
}
