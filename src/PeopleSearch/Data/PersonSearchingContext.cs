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
        /// <summary>
        /// Initializes a new instance of the PersonSearchingContext class
        /// </summary>
        /// <param name="options"></param>
        public PersonSearchingContext(DbContextOptions<PersonSearchingContext> options) 
            : base(options)
        { 
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbSet<Person> People { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> Count()
        {
            return await People.CountAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetAllUsers()
        {
            return await this.People.ToListAsync();
        }
    }
}