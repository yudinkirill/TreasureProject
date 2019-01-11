using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreasureServer.DataBase
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        //не дописал..для работы с заметками нужно создать коллекци.:
        public virtual ICollection<Note> Notes { get; set; }
        /// <summary>
        /// конструктор для создания списка, когда он не загружен из БД
        /// </summary>
        public User()
        {
            Notes = new List<Note>();
        }
    }
}