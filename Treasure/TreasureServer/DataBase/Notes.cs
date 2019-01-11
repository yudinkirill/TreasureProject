using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreasureServer.DataBase
{
    public class Note
    {
        public int NoteId { get; set; }
        public bool Pin { get; set; }
        public string NoteHeader { get; set; }
        public string NoteText { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}