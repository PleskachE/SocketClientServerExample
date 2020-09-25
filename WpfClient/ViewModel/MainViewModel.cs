using Common;
using Service;
using Service.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfTcpClient.Infrastructure;
using WpfTcpClient.Model;
using WpfTcpClient.View;

namespace WpfTcpClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly int _updateInterval = 7;

        public IWindowFactory WindowFactory = new WindowFactory();
        private IClientService _service;
        private DispatcherTimer _timer;
        private SystemMessage _systemMessage;

        public MainViewModel()
        {
            _systemMessage = new SystemMessage();
            AddMesageCommand = new RelayCommand(ExecuteAddMesageCommand, CanExecuteAddMesageCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(_updateInterval);
            _timer.Tick += Timer_tick;
        }

        #region timer

        private void Timer_tick(object sender, EventArgs e)
        {
            _allMessages.Text = _service.Listen(_systemMessage.SystemMessages[1]);
            AllMessages.Text = _allMessages.Text;
        }

        #endregion

        #region command

        public RelayCommand AddMesageCommand { get; }
        private void ExecuteAddMesageCommand(object parameter)
        {
            _allMessages.Text = _service.Listen(UserMessage.Text);
            AllMessages.Text = _allMessages.Text;
            UserMessage.Text = "";
        }

        public bool CanExecuteAddMesageCommand(object parameter)
        {
            return User.Name != null;
        }

        public RelayCommand LoginCommand { get; }
        private void ExecuteLoginCommand(object parameter)
        {
            LoginWindow();
            _service = new ClientService(User);
            _timer.Start();
        }

        public bool CanExecuteLoginCommand(object parameter)
        {
            return User.Name == null;
        }

        #endregion

        #region Message

        private Message _userMessage = new Message();
        public Message UserMessage
        {
            get
            {
                if (_userMessage == null)
                    _userMessage = new Message();
                return _userMessage;
            }
            set
            {
                if (_userMessage == null)
                    _userMessage = new Message();
                _userMessage.Text = value.Text;
                OnPropertyChanged("Text");
            }
        }

        private Message _allMessages = new Message();
        public Message AllMessages
        {
            get
            {
                if (_allMessages == null)
                    _allMessages = new Message();
                return _allMessages;
            }
            set
            {
                if (_allMessages == null)
                    _allMessages = new Message();
                _allMessages.Text += value;
                OnPropertyChanged("Text");
            }
        }

        #endregion

        #region User

        private User _user;
        public User User
        {
            get
            {
                if (_user == null)
                    _user = new User(null);
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged("Name");
            }
        }

        private void LoginWindow()
        {
            var window = WindowFactory.CreateWindow(new WindowCreationOptions()
            {
                WindowSize = new WindowSize(new Size(300, 150)),
                Title = "Login",
            });
            window.SizeToContent = SizeToContent.Height;
            var LoginWindow = new LoginWindow
            {
                DataContext = User,
                DialogCommand = new RelayCommand(r => window.DialogResult = (bool)r)
            };
            window.Content = LoginWindow;
            var result = window.ShowDialog();
            if (!result != true)
                return;
        }

        #endregion
    }
}
