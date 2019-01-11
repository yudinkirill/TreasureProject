using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    /// <summary>
    /// по идее этому классу место не в модели, не с данными, но щас лень плодить сборки, поэтому пусть будет тут
    /// </summary>
    public class NotesLoader
    {        //GetNotes не относится к этому классу, способ получения данных не должен быть описан в самих данных. Если я потом захочу получать не с сервера, а с другого места? или с нескольких? переписывать класс данных?

        public static ObservableCollection<Note> GetNotes() //Коллекция заметок
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"http://127.0.0.1:5000/");
            var json = client.GetStringAsync($"api/notes");
            ObservableCollection<Note> notes = JsonConvert.DeserializeObject<ObservableCollection<Note>>(json.Result);

            return notes;
        }
    }
}
