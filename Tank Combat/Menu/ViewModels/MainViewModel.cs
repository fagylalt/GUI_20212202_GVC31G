using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tank_Combat.Menu.Command;

namespace Tank_Combat.Menu.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;
        public MainViewModel()
        {
            this.UpdateViewCommand = new UpdateViewCommand(this);

            this.ExitGameCommand = new RelayCommand(() => Environment.Exit(0));
        }

        public BaseViewModel SelectedViewModel
        {
            get
            {
                return this.selectedViewModel;
            }

            set
            {
                this.selectedViewModel = value;
                this.OnPropertyChanged(nameof(this.SelectedViewModel));
            }
        }

        public ICommand ExitGameCommand { get; set; }

        public ICommand UpdateViewCommand { get; set; }
    }
}
