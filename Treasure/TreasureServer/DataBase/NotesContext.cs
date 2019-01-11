using Microsoft.EntityFrameworkCore;
using System;


namespace TreasureProjectWPF.DataBase
{
    public class NotesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }

}
