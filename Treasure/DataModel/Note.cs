using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataModel
{
    public class Note : INotifyPropertyChanged
    {
        string header;
        string text;
        public string NoteHeader
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        public string NoteText
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public int NoteId { get; set; }
        public bool Pin { get; set; }


        public int UserId { get; set; }
        public virtual User User { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
