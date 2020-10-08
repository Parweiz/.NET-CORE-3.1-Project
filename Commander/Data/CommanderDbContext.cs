using Commander.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class CommanderDbContext : DbContext
    {
        public CommanderDbContext(DbContextOptions<CommanderDbContext> opt) : base(opt)
        {
             
        }

        // Mapping the db - Represents the Command model as a dbset
        public DbSet<Command> Commands { get; set; }

    }
}
