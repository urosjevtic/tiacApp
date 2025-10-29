using AutoMapper;
using FluentAssertions;
using Moq;
using TiacApp.Application.DTOs;
using TiacApp.Application.Exceptions;
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
        public async Task AddPerson_Success()
        {
            // Arrange
            var newPersonDto = new PersoneInputDTO
            {
                FirstName = "Sima",
                LastName = "Simic",
                SocialMedias = new List<SocialMediaDTO>
                {
                    new SocialMediaDTO { Platform = "Twitter", Username = "simasimic" }
                }
            };

            var mappedPerson = new Person
            {
                FirstName = "Sima",
                LastName = "Simic",
                SocialMedias = new List<SocialMedia>
                {
                    new SocialMedia("Twitter", "simasimic")
                }
            };

            var addedPerson = new Person
            {
                Id = 5,
                FirstName = "Sima",
                LastName = "Simic",
                SocialMedias = new List<SocialMedia>
                {
                    new SocialMedia("Twitter", "simasimic") { Id = 1 }
                }
            };

            _mapperMock.Setup(m => m.Map<Person>(It.IsAny<PersoneInputDTO>())).Returns(mappedPerson);
            _personRepositoryMock.Setup(r => r.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(addedPerson);

            // Act
            var result = await _personService.AddPerson(newPersonDto) as PersoneOutputDTO;

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("Sima Simic");
            result.NumberOfVowels.Should().Be(4);
            result.NumberOfConsonants.Should().Be(5);
            result.ReversedName.Should().Be("cimiS amiS");
            (result.JsonData as Person).Should().BeEquivalentTo(addedPerson);
        }

        [Theory]
        [InlineData("", "Simic")]
        [InlineData("Sima", "")]
        [InlineData(null, "Simic")]
        [InlineData("Sima", null)]
        public async Task AddPerson_Fail_NoDataValidationException(string firstName, string lastName)
        {
            // Arrange
            var newPersonDto = new PersoneInputDTO
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                _personService.AddPerson(newPersonDto));
        }

        [Theory]
        [InlineData("Sima123")]
        [InlineData("S..ma")]
        [InlineData("Sim@")]
        public async Task AddPerson_Fail_WrongCharacterValidationException(string firstName)
        {
            // Arrange
            var newPersonDto = new PersoneInputDTO
            {
                FirstName = firstName,
                LastName = "Simic"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                _personService.AddPerson(newPersonDto));
        }

        [Fact]
        public async Task GetPeople_Success()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Id = 1, FirstName = "Sima", LastName = "Simic" },
                new Person { Id = 2, FirstName = "Pera", LastName = "Peric" }
            };

            _personRepositoryMock.Setup(r => r.GetAllPersonsAsync())
                .ReturnsAsync(people);

            // Act
            var result = await _personService.GetPeople() as IEnumerable<PersoneOutputDTO>;

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().FullName.Should().Be("Sima Simic");
            result.Last().FullName.Should().Be("Pera Peric");
        }

        [Fact]
        public async Task GetPersonById_Success()
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                FirstName = "Sima",
                LastName = "Simic"
            };

            _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(1))
                .ReturnsAsync(person);

            // Act
            var result = await _personService.GetPersonById(1) as PersoneOutputDTO;

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("Sima Simic");
            (result.JsonData as Person).Id.Should().Be(1);
        }

        [Fact]
        public async Task GetPersonById_Fail_NotFound()
        {
            // Arrange
            _personRepositoryMock.Setup(r => r.GetPersonByIdAsync(999))
                .ReturnsAsync((Person)null);

            // Act
            var result = await _personService.GetPersonById(999);

            // Assert
            result.Should().BeNull();
        }
    }
}
