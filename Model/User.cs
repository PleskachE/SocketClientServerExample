using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        private string _name { get; set; }

        public User(){}

        public User(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}