using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using Tank_Combat.Menu.ViewModels;

namespace Tank_Combat.Menu.Views
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        SoundPlayer player;
        public MenuWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();

            player = new SoundPlayer(Properties.Resources.CEPHEI___The_Enemy_Will_Not_Pass_Epic_Music);
            //player.Play();

            //bool soundFinished = true;

            //if (soundFinished)
            //{
            //    soundFinished = false;
            //    Task.Factory.StartNew(() => { player.PlaySync(); soundFinished = true; });
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayView playView = new PlayView();
            playView.Show();
            this.Close();
        }
    }
}
