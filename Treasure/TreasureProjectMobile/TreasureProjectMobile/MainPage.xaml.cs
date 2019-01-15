using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureProjectMobile.ViewModels;
using Xamarin.Forms;

namespace TreasureProjectMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
           
            BindingContext = new NotesViewModel();


        }
        private async void AddButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Add());
        }


        private async void ImageCell_Tapped(object sender, EventArgs e)
        {
            var imageCell = sender as ImageCell; //Формируем sender
            var note = imageCell.BindingContext as Note; //Создаём объект, который будем передавать
            await Navigation.PushAsync(new Edit(note)); // Передаём текущий объект
        }
    }
}
