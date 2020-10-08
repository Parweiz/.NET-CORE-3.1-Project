using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MockCommanderRepository : ICommanderRepository
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            // Nothing
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>{
                new Command{Id=0, HowTo="Boil an egg", CommandLine="Boil water", Platform="Kettle & Pan"},
                new Command{Id=1, HowTo="Cut bread", CommandLine="Get a knife", Platform="knife & chopping bread"},
                new Command{Id=2, HowTo="Make a cup of tea", CommandLine="Place teabag in cup", Platform="Kettle & Cup"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "Boil an egg",
                CommandLine = "Boil water",
                Platform = "Random test"
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
