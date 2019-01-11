using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureProjectWPF.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TreasureProjectWPF.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
       // ObservableCollection<Note> notes;
        //Note[] notes;
        public ObservableCollection<Note> Notes
        {
            get;
            private set;
        }

        
        public NotesViewModel()
        {
            Notes = Note.GetNotes();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
