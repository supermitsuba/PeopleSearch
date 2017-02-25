using Microsoft.EntityFrameworkCore;
using PeopleSearch.Data.Models;

namespace PeopleSearch.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbSet<Person> People { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./People.db");
        }
    }
}