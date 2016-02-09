using System;
using System.Windows.Input;

namespace Prezentacja.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecute = null;
        private readonly Action _executeAction = null;

        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecute = null)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute();
            return true;
        }

        public void UpdateCanExecuteState()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            if (_executeAction != null)
                _executeAction();
            UpdateCanExecuteState();
        }
    }
}
