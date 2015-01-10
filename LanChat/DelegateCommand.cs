using System;
using System.Windows.Input;

namespace LanChat
{
    public sealed class DelegateCommand : ICommand
    {
        public Predicate<object> OnCanExecute { get; set; }
        public Action<object> OnExecute { get; set; }

        public DelegateCommand(Action<object> onExecute)
        {
            OnExecute = onExecute;
        }

        public DelegateCommand(Predicate<object> onCanExecute, Action<object> onExecute)
        {
            OnExecute = onExecute;
            OnCanExecute = onCanExecute;
        }

        public void Execute(object parameter)
        {
            if (OnExecute != null)
                OnExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.OnCanExecute == null || this.OnCanExecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
