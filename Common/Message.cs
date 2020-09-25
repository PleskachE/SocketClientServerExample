using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfTcpClient.Model
{
    public class Message : INotifyPropertyChanged
    {
        private string _text;

        public Message() { }

        public Message(string text)
        {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
