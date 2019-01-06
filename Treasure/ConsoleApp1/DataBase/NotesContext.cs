using Microsoft.EntityFrameworkCore;
using System;


namespace TreasureServer.DataBase
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
        public NotesContext()
            
        {
            
            Database.EnsureCreated();
        }
        /// <summary>
        /// эта штука будет вызываться при создании контекста БД, потом на сервере ее можно будет убрать, там другой механизм
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder modelBuilder)
        {
            modelBuilder.UseSqlite("Data Source=mydatabase.db");
        }
    }

}
