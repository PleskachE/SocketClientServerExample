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

        public Message() { }

        public Message(string text)
        {
            this._text = text;
        }

        public Message(User user, string text)
        {
            this._user = user;
            this._text = text;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
