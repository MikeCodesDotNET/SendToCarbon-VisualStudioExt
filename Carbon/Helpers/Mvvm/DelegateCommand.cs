using System;
using System.Windows.Input;

namespace Carbon.Helpers.Mvvm
{
    public class DelegateCommand : ICommand
    {

        private readonly Action action;

        public DelegateCommand(Action action) => this.action = action;

        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67


        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => action();

    }
}
