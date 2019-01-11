using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.DataBase
{

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string userName { get; set; }
        public Usernote[] userNotes { get; set; }
    }

    public class Usernote
    {
        public string noteHeader { get; set; }
        public string noteText { get; set; }
        public int noteId { get; set; }
        public bool pin { get; set; }
        public int userId { get; set; }
        public object user { get; set; }
    }

}
