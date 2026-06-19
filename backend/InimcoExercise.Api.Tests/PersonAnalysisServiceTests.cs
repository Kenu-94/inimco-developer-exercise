using InimcoExercise.Api.DTOs;
using InimcoExercise.Api.Repositories;
using InimcoExercise.Api.Services;
using Moq;
using Xunit;

namespace InimcoExercise.Api.Tests
{
    public class PersonAnalysisServiceTests
    {
        private readonly Mock<IPersonRepository> _repositoryMock;
        private readonly PersonAnalysisService _service;

        public PersonAnalysisServiceTests()
        {
            _repositoryMock = new Mock<IPersonRepository>();
            _service = new PersonAnalysisService(_repositoryMock.Object);
        }

        [Fact]
        public void Analyze_JohnDoe_ReturnsCorrectVowelAndConsonantCount()
        {
            // Arrange
            var request = new PersonRequestDto { FirstName = "John", LastName = "Doe" };

            // Act
            var result = _service.Analyze(request);

            // Assert
            Assert.Equal(3, result.VowelCount);
            Assert.Equal(4, result.ConsonantCount);
        }

        [Fact]
        public void Analyze_JohnDoe_ReturnsCorrectReversedFullName()
        {
            var request = new PersonRequestDto { FirstName = "John", LastName = "Doe" };

            var result = _service.Analyze(request);

            Assert.Equal("eoD nhoJ", result.ReversedFullName);
        }

        [Fact]
        public void Analyze_JohnDoe_ReturnsCorrectFullName()
        {
            var request = new PersonRequestDto { FirstName = "John", LastName = "Doe" };

            var result = _service.Analyze(request);

            Assert.Equal("John Doe", result.FullName);
        }

        [Theory]
        [InlineData("Joe", "", "eoJ")]      // voorbeeld uit de opdracht
        [InlineData("Anna", "Bo", "oB annA")]
        public void Analyze_VariousNames_ReturnsCorrectReversal(string first, string last, string expectedReversed)
        {
            var request = new PersonRequestDto { FirstName = first, LastName = last };

            var result = _service.Analyze(request);

            Assert.Equal(expectedReversed, result.ReversedFullName.Trim());
        }

        [Fact]
        public void Analyze_CallsRepositorySaveExactlyOnce()
        {
            var request = new PersonRequestDto { FirstName = "John", LastName = "Doe" };

            _service.Analyze(request);

            _repositoryMock.Verify(r => r.Save(It.IsAny<Models.Person>()), Times.Once);
        }

        [Fact]
        public void Analyze_PersonWithNoVowels_ReturnsZeroVowelCount()
        {
            var request = new PersonRequestDto { FirstName = "Brrr", LastName = "Pfft" };

            var result = _service.Analyze(request);

            Assert.Equal(0, result.VowelCount);
        }
    }
}