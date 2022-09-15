using System.Threading;
using AutoFixture;
using ChuckJokes.Infrastructure.ApiClients;
using ChuckJokes.Infrastructure.Services;
using Moq;
using Xunit;

namespace ChuckJokes.UnitTests.Infrastructure.Services
{
    public class ChuckApiScrapingServiceShould
    {
        [Fact]
        public async void ReturnScrappedJokeWhenCallingScrapeJokesFromApi()
        {
            //Arrange
            var fixture = new Fixture();
            var chuckJokeResponse = fixture.Create<ChuckJokeResponse>();
            var chuckApiClientMock = new Mock<IChuckApiClient>();
            chuckApiClientMock
                .Setup(x => x.GetRandomJoke(It.IsAny<CancellationToken>()))
                .ReturnsAsync(chuckJokeResponse);
            var sut = new ChuckApiScrapingService(chuckApiClientMock.Object);
            
            //Act
            var scappedJoke = await sut.ScrapeJokesFromApi(1, CancellationToken.None);
            
            //Assert
            Assert.NotNull(scappedJoke);
            Assert.NotEmpty(scappedJoke);
            Assert.Contains(scappedJoke, x => x.Id.Equals(chuckJokeResponse.Id));
        }
        
        [Fact]
        public async void NotReturnScrappedJokeWhenJokeLenghtIsMoreThan200Characters()
        {
            //Arrange
            var fixture = new Fixture();
            var longString = string.Join(string.Empty, fixture.CreateMany<char>(201));
            fixture.Customize<ChuckJokeResponse>(composer => composer.With(p => p.Value, longString));
            var chuckJokeResponse = fixture.Create<ChuckJokeResponse>();
            var chuckApiClientMock = new Mock<IChuckApiClient>();
            chuckApiClientMock
                .Setup(x => x.GetRandomJoke(It.IsAny<CancellationToken>()))
                .ReturnsAsync(chuckJokeResponse);
            var sut = new ChuckApiScrapingService(chuckApiClientMock.Object);
            
            //Act
            var scappedJoke = await sut.ScrapeJokesFromApi(1, CancellationToken.None);
            
            //Assert
            Assert.NotNull(scappedJoke);
            Assert.Empty(scappedJoke);
        }
    }
}
