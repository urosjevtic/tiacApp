
using System.Text.RegularExpressions;
using TiacApp.Application.DTOs;

namespace TiacApp.Application.Service
{
    public class PersonService : Interface.IPersoneService
    {

        public PersonService() { }
        public Task<object> AddPerson(object newPerson)
        {
            var person = newPerson as PersoneInputDTO;

            return Task.FromResult((object)GetOutputData(person));
        }

        private PersoneOutputDTO GetOutputData(PersoneInputDTO personeInputDTO)
        {
            PersoneOutputDTO output = new PersoneOutputDTO();
            output.FullName = $"{personeInputDTO.FirstName} {personeInputDTO.LastName}";
            output.NumberOfVowels = GetNumberOfVowels(output.FullName);
            output.NumberOfConsonants = GetNumberOfConsonants(output.FullName);
            output.ReversedName = ReverseName(output.FullName);
            output.JsonData = personeInputDTO;

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
