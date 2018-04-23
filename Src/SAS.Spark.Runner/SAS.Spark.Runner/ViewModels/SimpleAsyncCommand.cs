using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels
{
    public class SimpleAsyncCommand<T1, T2> : ICommand
    {
        private Func<T1, bool> canExecuteMethod;
        private Func<T2, Task> executeMethod;

        public SimpleAsyncCommand(Func<T1, bool> canExecuteMethod, Func<T2, Task> executeMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public SimpleAsyncCommand(Func<T2, Task> executeMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = (x) => { return true; };
        }

        public bool CanExecute(T1 parameter)
        {
            if (canExecuteMethod == null) return true;
            return canExecuteMethod(parameter);
        }

        public async Task Execute(T2 parameter)
        {
            if (executeMethod != null)
            {
                await executeMethod(parameter);
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((T1)parameter);
        }

        public async void Execute(object parameter)
        {
            await Execute((T2)parameter);
        }


        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecuteMethod != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (canExecuteMethod != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }


        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();

        }
    }
}
