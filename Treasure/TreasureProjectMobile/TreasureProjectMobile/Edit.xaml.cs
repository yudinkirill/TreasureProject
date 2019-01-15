using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureProjectMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TreasureProjectMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        private readonly Note note; // Инициализируем

        public Edit()
        {
            InitializeComponent();

            BindingContext = new NotesViewModel();


            ToolbarItem tb = new ToolbarItem
            {
                Text = "Delete",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            ToolbarItems.Add(tb);


            tb.Clicked += (s, e) =>
            {
                NotesLoader.DeleteItem(note.NoteId);
                
            };



        }

        
        public Edit(Note note) 
            : this()
        {
            this.note = note;
            BindingContext = note;
        }
    }
}