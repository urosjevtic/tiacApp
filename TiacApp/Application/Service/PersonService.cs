using AutoMapper;
using System.Text.RegularExpressions;
using TiacApp.Application.DTOs;
using TiacApp.Models;
using TiacApp.Repository;

namespace TiacApp.Application.Service
{
    public class PersonService : Interface.IPersoneService
    {
        private PersonRepository _personRepository;
        private IMapper _mapper;

        public PersonService(PersonRepository personRepository, IMapper mapper) 
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<object> AddPerson(object newPerson)
        {
            Person person = _mapper.Map<Person>(newPerson);
            if (person != null) {
                SocialMedia socialMedias = new SocialMedia();
                Person addedPerson = await _personRepository.AddPersonAsync(person);
                return GetOutputData(addedPerson);
            }

            return null;
        }

        private PersoneOutputDTO GetOutputData(Person person)
        {
            PersoneOutputDTO output = new PersoneOutputDTO();
            output.FullName = $"{person.FirstName} {person.LastName}";
            output.NumberOfVowels = GetNumberOfVowels(output.FullName);
            output.NumberOfConsonants = GetNumberOfConsonants(output.FullName);
            output.ReversedName = ReverseName(output.FullName);
            output.JsonData = person;

            return output;
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

        public Task<object> GetPersons()
        {
            var person = new Models.Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                SocialSkills = new List<string> { "Communication", "Teamwork" },
                SocialMedias = new List<Models.SocialMedia>
                {
                    new()
                    {
                        Id = 1,
                        Platform = "Twitter",
                        Username = "@johndoe"
                    }
                }
            };

            return Task.FromResult((object)person);
        }

        public Task<object> GetPersonById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
