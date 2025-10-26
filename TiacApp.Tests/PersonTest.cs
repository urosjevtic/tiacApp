using AutoMapper;
using FluentAssertions;
using Moq;
using TiacApp.Application.DTOs;
using TiacApp.Application.Service;
using TiacApp.Application.Service.Interface;
using TiacApp.Models;
using TiacApp.Repository;
using TiacApp.Repository.Interface;
using Xunit;

namespace TiacApp.Tests
{
    public class PersonTest
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PersonService _personService;

        public PersonTest()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _mapperMock = new Mock<IMapper>();
            _personService = new PersonService(_personRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddPerson_ShouldReturnCorrectOutput_WhenPersonIsValid()
        {
            var newPersonDto = new { FirstName = "Sima", LastName = "Simic" };
            var mappedPerson = new Person { FirstName = "Sima", LastName = "Simic" };
            var addedPerson = new Person { Id = 5, FirstName = "Sima", LastName = "Simic" };

            _mapperMock.Setup(m => m.Map<Person>(It.IsAny<object>())).Returns(mappedPerson);
            _personRepositoryMock.Setup(r => r.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(addedPerson);

            var result = await _personService.AddPerson(newPersonDto) as PersoneOutputDTO;

            result.Should().NotBeNull();
            result.FullName.Should().Be("Sima Simic");
            result.NumberOfVowels.Should().Be(4);
            result.NumberOfConsonants.Should().Be(5);
            result.ReversedName.Should().Be("cimiS amiS");
            result.JsonData.Should().Be(addedPerson);
        }
    }
}
