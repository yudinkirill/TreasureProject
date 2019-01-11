using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace DataModel
{
    /// <summary>
    /// по идее этому классу место не в модели, не с данными, но щас лень плодить сборки, поэтому пусть будет тут
    /// </summary>
    public class NotesLoader
    {        //GetNotes не относится к этому классу, способ получения данных не должен быть описан в самих данных. Если я потом захочу получать не с сервера, а с другого места? или с нескольких? переписывать класс данных?
        public string adress = "http://127.0.0.1:5000/";
        public static ObservableCollection<Note> GetNotes() //Коллекция заметок
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"http://127.0.0.1:5000/");
            var json = client.GetStringAsync($"api/notes");
            ObservableCollection<Note> notes = JsonConvert.DeserializeObject<ObservableCollection<Note>>(json.Result);

            return notes;
        }

        public static bool AddItem(Note note)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"http://127.0.0.1:5000/");
            //Note note = new Note()
            //{
                
            //    NoteHeader = "ыыы",
            //    NoteText = "аыфаывфа",
            //    UserId = "4"
            //};
            var serializedItem = JsonConvert.SerializeObject(note);

            var response = client.PostAsync($"api/notes", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            

            return response.IsCompleted;
            
        }

        //public static bool EditItem(Client item, int id)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri($"http://127.0.0.1:5000/");

        //    var serializedItem = JsonConvert.SerializeObject(item);

        //    var response = client.PutAsync(new Uri($"api/notes/{id}")).Result;

        //    //Content.ReadAsStringAsync() - сериализованное в json значение, возвращенное Post фуункцией

        //    return response.IsSuccessStatusCode;
        //}
        /// <summary>
        /// Запрос на удаление ноты по ее id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteItemA(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"http://127.0.0.1:5000/");
            var response = client.DeleteAsync($"api/notes/{id}").Result;

            return response.IsSuccessStatusCode;
        }
    }
}
