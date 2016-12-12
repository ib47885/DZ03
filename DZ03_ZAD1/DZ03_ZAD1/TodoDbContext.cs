using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DZ03_ZAD1
{
    class TodoDbContext : DbContext
    {

        /*Relacijska baza koju opisujemo imat ce samo jednu relaciju TodoItem*/
        public IDbSet<TodoItem> TodoItem { get; set; }

        /*Connection string šalje se preko konstruktora (pazite da se prosljedi i konstruktoru roditelja).*/
        public TodoDbContext(string connectionString) : base(connectionString)
        {

        }

        /*Primarni kljuc relacije TodoItem je Id, a sva polja relacije su obavezna osim DateCompleted*/
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(t => t.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(t => t.DateCreated).IsRequired();
        }

    }
}
