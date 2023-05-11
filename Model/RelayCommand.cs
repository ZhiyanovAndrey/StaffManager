using System;
using System.Windows.Input;

namespace StaffManager.Model
{
    public class RelayCommand : ICommand //для автозаполнения реализовать интерфейс
    {
        private Action<Object> _execute;
        private Func<object, bool> _canexecute;

        public event EventHandler CanExecuteChanged //срабатывает каждый раз при смене состояния CanExecute
                                                    //передаем управление событием CommandManager внутренней системе WPF
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<Object> Execute, Func<object, bool> CanExecute = null) //конструктор для использования класса для любой команды принимает два делегата
        {
            _execute = Execute; 
                
             //?? throw new ArgumentNullException(nameof(Execute)); //присвоим переданный Execute только если он не null,
                                                                                    //а если null то смысла выполнять команду нет выбросим исключение
            _canexecute = CanExecute;
        }
        public bool CanExecute(object parameter) 
            //=> canexecute?.Invoke(parameter) ?? true; //проверяет доступность команды
        {
        return _canexecute==null||_canexecute(parameter);
        }
        public void Execute(object parameter)
        //=> execute(parameter); //сработает при вызове команды
        {
            _execute(parameter);    
        }

    }
}
