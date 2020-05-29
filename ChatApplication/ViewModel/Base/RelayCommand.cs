using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApplication
{
    class RelayCommand : ICommand
    {
        #region Private Members
        private Action action;
        #endregion
        #region Public Event
        /// <summary>
        /// The event that fired when <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) =>
        {
        };
        #endregion
        #region Contructor
        public RelayCommand(Action action)
        {
            this.action = action;
        }
        #endregion
        /// <summary>
        /// Relay command CamExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>

        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            action();
        }
        #endregion
    }
}
