using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Intouch.Edm.Helpers
{
    public class CommandHandler :ICommand
    {
        private Action<object> _action;
        private bool _canExecute;
        private Action<object> p;

        public CommandHandler(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public CommandHandler(Action<object> p)
        {
            this.p = p;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
