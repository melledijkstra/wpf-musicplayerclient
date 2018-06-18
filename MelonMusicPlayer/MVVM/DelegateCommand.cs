using System;
using System.Windows.Input;

namespace MelonMusicPlayerWPF.MVVM
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute; 

        public DelegateCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // return true or if _canExecute is set execute it and return result
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}
