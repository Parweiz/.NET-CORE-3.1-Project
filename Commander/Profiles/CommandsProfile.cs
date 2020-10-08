using AutoMapper;
using Commander.Dto;
using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Maps/Converts the Command object to an CommandReadDto object
            CreateMap<Command, CommandReadDto>();

            // Maps/Converts the CommandCreateDto object to an Command object
            CreateMap<CommandCreateDto, Command>();

            // Maps/Converts the CommandUpdateDto object to an Command object
            CreateMap<CommandUpdateDto, Command>();

            // Maps/Converts the Command object to an CommandUpdateDto object
            CreateMap<Command, CommandUpdateDto>();

        }
    }
}
