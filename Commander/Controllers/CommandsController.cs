using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dto;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]     // Can also write [Route("api/[controller]")] --> Commands will be put inside the [controller]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepository repository, IMapper mapper)
        {
            // Assigning the dependency injected value to the private field variabel _repository
            _repository = repository;
            _mapper = mapper;
        }

        // HTTP GET api/commands - Takes none arguments and returns an enumerable object of CommandReadDto - https://www.c-sharpcorner.com/UploadFile/0c1bb2/ienumerable-interface-in-C-Sharp/
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // HTTP GET api/commands/{id} - Take an id as an argument and returns CommandReadDto
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem == null)
            {
                return NotFound();
            }

            // Mapping from Command model that had data to a new empty CommandReadDto
            var mapping = _mapper.Map<CommandReadDto>(commandItem);
            return Ok(mapping);
     
        }

        // HTTP POST api/commands - Take a DTO as an argument and returns CommandReadDto
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto createDto)
        {
            //Since we're taking CommandCreateDto as argument, then the only way the object can be put into the db is by 
            //mapping it into a Command model/ object, as that's the way db understands it
            var commandModel = _mapper.Map<Command>(createDto);

            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // Using the CreatedAtRoute method to return a 201 status code - https://stackoverflow.com/a/61691888/11124256
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        // HTTP PUT api/commands/{id} - Takes id and CommandUpdateDto as arguments and returns nothing back
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            // The client is expecting to update the resource - For that reason, we're checking to see whether the resource exists or not
            var commandModelFromRepository = _repository.GetCommandById(id);
            if(commandModelFromRepository == null)
            {
                return NotFound();
            }

            // The mapping exercise has basically updated the commandModelFromRepository, so it's up-to-date. For that reason, 
            // there is no need to implement the UpdateCommand method from our interface.
            _mapper.Map(commandUpdateDto, commandModelFromRepository);

           //  _repository.UpdateCommand(commandModelFromRepository);
            _repository.SaveChanges();

            // Returning a HTTP Status 204
            return NoContent();

        }

        // HTTP PATCH api/commands/{id} - Takes id and CommandUpdateDto as arguments and returns nothing back 
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDto> jsonPatchDocument)
        {
            var commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository == null)
            {
                return NotFound();
            }


            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepository);
            jsonPatchDocument.ApplyTo(commandToPatch, ModelState);
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepository);

            _repository.SaveChanges();

            return NoContent();

        }

        // HTTP DELETE api/commands/{id} - Takes id as argument and returns nothing back
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository == null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandModelFromRepository);
            _repository.SaveChanges();

            return NoContent();

        }
    }
}
