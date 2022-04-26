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
using Tank_Combat.Renderer;

namespace Tank_Combat.Menu.Views
{
    /// <summary>
    /// Interaction logic for PlayView.xaml
    /// </summary>
    public partial class PlayView : Window
    {
        public PlayView()
        {
            InitializeComponent();
        }

        private void Button_Click_Light_Tank(object sender, RoutedEventArgs e)
        {
            //Display display = new Display();
            GameWindow win = new GameWindow("light");
            win.Show();
            this.Close();
        }

        private void Heavy_Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow win = new GameWindow("heavy");
            win.Show();
            this.Close();
        }

        private void Medium_Button_Click(object sender, RoutedEventArgs e)
        {
            GameWindow win = new GameWindow("basic");
            win.Show();
            this.Close();

        }
    }
}
