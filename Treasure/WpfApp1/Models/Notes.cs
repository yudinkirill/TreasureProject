using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureProjectWPF.Models
{
    public class Note : INotifyPropertyChanged
    {
        string header;
        string text;
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
         }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public static Note[] GetNotes()
        {
            var result = new[]
            {
                new Note() {Header = "Zametka 1", Text = "RAZDVATRI" },
                new Note() {Header = "Zametka 2", Text = "RAZDVATRICHETYRE" },
                new Note() {Header = "Zametka 3", Text = "RAZDVATRICHERTYREPYAT" }
            };
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
