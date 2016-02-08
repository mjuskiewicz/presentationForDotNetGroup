using System;
using System.Windows.Input;

namespace Prezentacja.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _canExecute = null;
        private readonly Action _executeAction = null;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecute = null)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
        }

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

    public class DelegateCommand<T> : ICommand where T : class
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter as T);
        }

        public void Execute(object parameter)
        {
            _execute(parameter as T);
        }

        public void UpdateCanExecuteState(T parameter)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }
    }
}
