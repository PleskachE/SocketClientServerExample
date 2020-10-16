using Common;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Model
{
    [Serializable]
    public class Message : INotifyPropertyChanged
    {
        private string _text;
        private User _user;
        private MessageTypes _messageType;
        private DateTime _dateTime;

        public Message() { }

        public Message(User user, string text)
        {
            this._user = user;
            this._text = text;
        }

        public Message(User user, string text, MessageTypes types)
        {
            this._user = user;
            this._text = text;
            this._messageType = types;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        public MessageTypes MessageType
        {
            get { return _messageType; }
            set
            {
                _messageType = value;
                OnPropertyChanged("Type");
            }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged("Data");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
