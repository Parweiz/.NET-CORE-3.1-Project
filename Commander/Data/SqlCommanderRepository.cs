using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepository : ICommanderRepository
    {

        private readonly CommanderDbContext _context;

        public SqlCommanderRepository(CommanderDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Add(command);
            
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Commands.Remove(command);

        }

        public IEnumerable<Command> GetAllCommands()
        {
            // Returning all the values in a list
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            // returning the first or default value where the assigned id is equal to ".Id"
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        // The purpose of the SaveChanges() function is to make sure that our data gets saved in db context (it's necessary as the db won't be updated)
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command command)
        {
            // Nothing
        }
    }
}
