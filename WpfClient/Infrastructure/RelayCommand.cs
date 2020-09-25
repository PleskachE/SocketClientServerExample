﻿using System;
using System.Windows.Input;

namespace WpfTcpClient.Infrastructure
{
	public class RelayCommand : ICommand
	{
		readonly Action<object> _execute;
		readonly Predicate<object> _canExecute;
		public RelayCommand(Action<object> execute) : this(execute, null)
		{
		}
		public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}
		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}
		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}
		public void Execute(object parameter)
		{
			_execute.Invoke(parameter);
		}
	}
}
