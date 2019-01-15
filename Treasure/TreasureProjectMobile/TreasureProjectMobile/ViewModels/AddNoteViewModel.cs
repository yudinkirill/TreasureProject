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
using TreasureProjectMobile;
using Xamarin.Forms;
using System.Runtime.CompilerServices;


namespace TreasureProjectMobile.ViewModels
{
    public class AddNoteViewModel : INotifyPropertyChanged
    {
        public ICommand Add { get; private set; }

        public AddNoteViewModel(int userId)
        {
            NewNote = new Note() { UserId = userId };  //userId надо как то сюда передать

            Add = new Command(
                (param) => NotesLoader.AddItem(NewNote)
              
            );
        }

        bool check(object p)
        {
            return !String.IsNullOrEmpty(NewNote.NoteText) && !String.IsNullOrEmpty(NewNote.NoteHeader);
        }

        private Note newNote;

        public Note NewNote
        {
            get { return newNote; }
            set
            {
                newNote = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

