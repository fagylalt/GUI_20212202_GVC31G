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

namespace Tank_Combat.Menu.Views
{
    /// <summary>
    /// Interaction logic for GameOverViewRed.xaml
    /// </summary>
    public partial class GameOverViewRed : Window
    {
        public GameOverViewRed()
        {
            InitializeComponent();
        }
        private void CloseCommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit to the main menu?", "Quit to main menu", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show();
                this.Close();
            }

        }
    }
}
