using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tank_Combat.Menu.ViewModels;

namespace Tank_Combat.Menu.Command
{
    internal class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;
        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "GameRules")
            {
                this.viewModel.SelectedViewModel = new GameRulesViewModel();
            }
            else if (parameter.ToString() == "Credits")
            {
                this.viewModel.SelectedViewModel = new CreditsViewModel();
            }
        }
    }
}
