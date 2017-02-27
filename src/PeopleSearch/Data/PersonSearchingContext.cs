using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Data.Models;

namespace PeopleSearch.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonSearchingContext : DbContext
    {
        public PersonSearchingContext(DbContextOptions<PersonSearchingContext> options) :base(options)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbSet<Person> People { get; set; }


        public async Task<int> Count()
        {
            return await People.CountAsync();
        }

        public async Task<IEnumerable<Person>> GetAllUsers()
        {
            return await this.People.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=../../../Data/PeopleSearch.db");
        }*/
    }
}