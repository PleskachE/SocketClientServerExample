using Common;
using Service;
using Service.Intefaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using WpfTcpClient.Infrastructure;
using Model;
using WpfTcpClient.View;
using Repositoryes.Interfaces;
using Repositoryes;
using System.Linq;
using System.Windows.Data;

namespace WpfTcpClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly int _updateInterval = 6;

        public IWindowFactory WindowFactory = new WindowFactory();
        private IClientService _service;
        private DispatcherTimer _timer;

        public MainViewModel()
        {
            AddMesageCommand = new RelayCommand(ExecuteAddMesageCommand, CanExecuteAddMesageCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(_updateInterval);
            _timer.Tick += Timer_tick;
        }

        #region timer

        private void Timer_tick(object sender, EventArgs e)
        {
            var updateMessage = new Message(new User(User.Name), null);
            updateMessage.MessageType = MessageTypes.Update;
            _allMessages = new ObservableCollection<Message>(_service.Listen(updateMessage).Get());
            AllMessages = _allMessages;
        }

        #endregion

        #region command

        public RelayCommand AddMesageCommand { get; }
        private void ExecuteAddMesageCommand(object parameter)
        {
            var message = new Message(new User(User.Name), UserMessage.Text);
            message.DateTime = DateTime.Now;
            message.MessageType = MessageTypes.Message;
            _allMessages = new ObservableCollection<Message>(_service.Listen(message).Get());
            AllMessages = _allMessages;
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
            _service = new ClientService();
            _timer.Start();
        }

        public bool CanExecuteLoginCommand(object parameter)
        {
            return User.Name == null;
        }

        #endregion

        #region Message

        private ObservableCollection<Message> _allMessages = new ObservableCollection<Message>();
        public ObservableCollection<Message> AllMessages
        {
            get
            { return _allMessages; }
            set
            {
                _allMessages = value;
                OnPropertyChanged();
            }
        }

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
