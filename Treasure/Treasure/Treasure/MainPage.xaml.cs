using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Treasure
{
    public partial class MainPage : ContentPage
    {

        
        
        public MainPage()
        {
            InitializeComponent();
        }

        public class LoginTime
        {

        }


            private async void RegisterButton_Click(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Register());
        }

        HttpClient client;
        IEnumerable<Item> items;

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"https://109.171.72.102:5001/");
            var json = await client.GetStringAsync($"api/users");
            items = new List<Item>();
            items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));




            /* NotesContext context = new NotesContext();
             var uName = LoginE.Text;
             var uPass = PasswordE.Text;
             var LoginB = context.Users.FirstOrDefault(u => u.Login == uName);
             var PasswordB = context.Users.FirstOrDefault(u => u.Password == uPass);

             if (LoginB == null)
             {
                 DisplayAlert("Treasure", "Имя пользователя или пароль не верны.", "OK");
             }
             else
             {
                 if (LoginB == null)
                 {
                     DisplayAlert("Treasure", "Имя пользователя или пароль не верны.", "OK");
                 }
                 else
                 {
                     await Navigation.PushAsync(new Notes());
                 }
             }*/

        }
    }
}
