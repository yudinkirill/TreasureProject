using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using DataModel;
using System.Windows.Input;
using System.Windows;
using System.Runtime.CompilerServices;
using TreasureProjectMobile;
using Xamarin.Forms;

namespace TreasureProjectMobile.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Note> notes;

        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }

        //Note[] notes;
        public ObservableCollection<Note> Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
                OnPropertyChanged("notes");

            }
        }

        

        public NotesViewModel()
        {
            try
            {
                Notes = NotesLoader.GetNotes();
                Update = new Command(EditNote);
                Remove = new Command(DeleteNote);
            }
            finally {  }
        }

        
        bool CanRemoveNote(object arg)
        {
            return (arg as Note) != null;
        }

        void DeleteNote(object obj)
        {
            var selectedNote = (Note)obj; 
            Notes.Remove((Note)obj);
        }



        async void EditNote(object obj)
        {
            var selectedNote = (Note)obj;
            await NotesLoader.EditItem(selectedNote);
            

           
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
    }

}
