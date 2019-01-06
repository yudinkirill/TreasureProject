using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TreasureServer.DataBase;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            NotesContext context = new NotesContext();

            Console.WriteLine("Введите имя пользователя:");
            var uName = Console.ReadLine();

            //запрос вернет или найденного пользователя, или null, если пользователь не найден
            var dbUser = context.Users.FirstOrDefault( u => u.Login == uName );
            if (dbUser == null)
            {
                Console.WriteLine("пользователь не найден, регаем");
            }
            else {
                Console.WriteLine("ну нет");
            }

            var user = new User() { Login = "Ivan", Password = "fkjdshfkjsahfkdsa" };
            user.Notes.Add(new Note() { NoteHeader = "Заголовок 1", NoteText = "авыораыовравыолрарвыло", Pin = true });
            user.Notes.Add(new Note() { NoteHeader = "Заголовок 2", NoteText = "fsjdfhdskj", Pin = true });
            user.Notes.Add(new Note() { NoteHeader = "Заголовок 3", NoteText = "gfsjkjsa", Pin = false });
            context.Users.Add(user);
            //добавление заметок для пользователя
            context.SaveChanges();
            Console.WriteLine("Hello World!");
        }
    }
}
