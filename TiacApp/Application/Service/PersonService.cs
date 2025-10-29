using AutoMapper;
using System.Text.RegularExpressions;
using TiacApp.Application.DTOs;
using TiacApp.Models;
using TiacApp.Repository;
using TiacApp.Repository.Interface;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TiacApp.Application.Exceptions;

namespace TiacApp.Application.Service
{
    public class PersonService : Interface.IPersoneService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(
            IPersonRepository personRepository, 
            IMapper mapper) 
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<object> AddPerson(object newPerson)
        {
            try
            {
                if (newPerson == null)
                {
                    throw new ValidationException("Person data can't be null");
                }

                if (newPerson is PersoneInputDTO inputDto)
                {
                    ValidatePersonInput(inputDto);
                }
                else
                {
                    throw new ValidationException("Invalid input type");
                }

                Person person = _mapper.Map<Person>(newPerson);

                return GetOutputData(await _personRepository.AddPersonAsync(person));
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to save person to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the request", ex);
            }
        }

        private void ValidatePersonInput(PersoneInputDTO input)
        {
            if (string.IsNullOrWhiteSpace(input.FirstName))
            {
                throw new ValidationException("First name is required");
            }

            if (string.IsNullOrWhiteSpace(input.LastName))
            {
                throw new ValidationException("Last name is required");
            }

            var namePattern = new Regex(@"^[\p{L}\s]+$");

            if (!namePattern.IsMatch(input.FirstName))
            {
                throw new ValidationException("First name can only contain letters");
            }

            if (!namePattern.IsMatch(input.LastName))
            {
                throw new ValidationException("Last name can only contain letters");
            }

            if (input.SocialMedias != null)
            {
                foreach (var socialMedia in input.SocialMedias)
                {
                    if (string.IsNullOrWhiteSpace(socialMedia.Platform))
                    {
                        throw new ValidationException("Social media platform is required");
                    }
                    if (string.IsNullOrWhiteSpace(socialMedia.Username))
                    {
                        throw new ValidationException("Social media username is required");
                    }
                }
            }
        }

        private PersoneOutputDTO GetOutputData(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            try
            {
                PersoneOutputDTO output = new PersoneOutputDTO();
                output.FullName = $"{person.FirstName} {person.LastName}";
                output.NumberOfVowels = GetNumberOfVowels(output.FullName);
                output.NumberOfConsonants = GetNumberOfConsonants(output.FullName);
                output.ReversedName = ReverseName(output.FullName);
                output.JsonData = person;

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Error processing person data", ex);
            }
        }

        private static int GetNumberOfVowels(string fullName)
        {
            return Regex.Matches(fullName.ToLower(), "[aeiou]").Count;
        }

        private static int GetNumberOfConsonants(string fullName)
        {
            return Regex.Matches(fullName.ToLower(), "[bcdfghjklmnpqrstvwxyz]").Count;
        }

        private string ReverseName(string fullName)
        {
            char[] nameArray = fullName.ToCharArray();
            Array.Reverse(nameArray);
            return new string(nameArray);
        }

        public async Task<object> GetPeople()
        {
            try
            {
                var people = await _personRepository.GetAllPersonsAsync();
                var result = people.Select(person => GetOutputData(person));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the request", ex);
            }

        }

        public async Task<object> GetPersonById(int id)
        {
            try
            {
                var person = await _personRepository.GetPersonByIdAsync(id);
                if (person == null)
                    return null;
                return GetOutputData(person);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while processing the request", ex);
            }

        }
    }
}
