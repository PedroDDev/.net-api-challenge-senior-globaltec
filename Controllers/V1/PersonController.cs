using Application.ViewModel;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Util;

namespace Controllers.V1
{
    public class PersonController : BaseController
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonController> _logger;
        private readonly IMapper _mapper;

        private const string nameValidationString = "Name cannot be null or empty.";
        private const string cpfValidationString = "CPF cannot be null or empty and must contain 11 digits (numbers only).";
        private const string ufValidationString = "UF cannot be null or empty and must contain 2 characters (letters only).";

        public PersonController(IPersonRepository personRepository, ILogger<PersonController> logger, IMapper mapper)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAllAsync(int page, int quantity)
        {
            try
            {
                List<PersonDTO> people = _mapper.Map<List<PersonDTO>>(await _personRepository.FindAllAsync(page, quantity));
                return Ok(people);
            }
            catch (System.Exception e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                PersonDTO person = _mapper.Map<PersonDTO>(await _personRepository.FindByIdAsync(id));
                if (person is not null) return Ok(person);
            }
            catch (System.Exception e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }

            return NotFound(new { Message = $"Person with id {id} not found." });
        }

        [Authorize]
        [HttpGet("uf/{uf}", Name = "GetByUf")]
        public async Task<IActionResult> GetByUfAsync(string uf, int page, int quantity)
        {
            try
            {
                List<PersonDTO> people = _mapper.Map<List<PersonDTO>>(await _personRepository.FindByUFAsync(uf, page, quantity));
                return Ok(people);
            }
            catch (System.Exception e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PersonViewModel personViewModel)
        {
            if(!IsValidPersonViewModel(personViewModel, out IActionResult? errorResult)) return errorResult!;

            try
            {
                Person person = new Person(personViewModel.Name, personViewModel.Cpf, personViewModel.Uf.ToUpper(), personViewModel.Birthdate);
                Person? createdPerson = await _personRepository.AddAsync(person);

                if (createdPerson is not null) return CreatedAtAction("GetById", new { id = createdPerson.Id }, createdPerson);

                return Conflict(new { Message = "A person has already been registered with this CPF." });
            }
            catch (System.Exception e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] PersonViewModel personViewModel)
        {
            if(!IsValidPersonViewModel(personViewModel, out IActionResult? errorResult)) return errorResult!;

            try
            {
                Person person = new Person(personViewModel.Name, personViewModel.Cpf, personViewModel.Uf.ToUpper(), personViewModel.Birthdate);
                Person? updatedPerson = await _personRepository.UpdateAsync(id, person);
                if (updatedPerson is not null) return Ok(updatedPerson);
            }
            catch (System.Exception e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }

            return NotFound(new { Message = $"Person with id {id} not found." });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                bool isPersonDeleted = await _personRepository.DeleteAsync(id);
                if (isPersonDeleted) return NoContent();
            }
            catch (System.Exception e)
            {
                _logger.Log(LogLevel.Error, $"{e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }

            return NotFound(new { Message = $"Person with id {id} not found." });
        }

        private bool IsValidPersonViewModel(PersonViewModel personViewModel, out IActionResult? errorResult)
        {
            if (string.IsNullOrEmpty(personViewModel.Name))
            {
                errorResult = BadRequest(new { Message = nameValidationString });
                return false;
            }
            if (string.IsNullOrEmpty(personViewModel.Cpf) || personViewModel.Cpf.Length != 11 || !StringValidations.HasOnlyNumbers(personViewModel.Cpf))
            {
                errorResult = BadRequest(new { Message = cpfValidationString });
                return false;
            }
            if (string.IsNullOrEmpty(personViewModel.Uf) || personViewModel.Uf.Length != 2 || !StringValidations.HasOnlyLetters(personViewModel.Uf))
            {
                errorResult = BadRequest(new { Message = ufValidationString });
                return false;
            }

            errorResult = null;
            return true;
        }
    }
}