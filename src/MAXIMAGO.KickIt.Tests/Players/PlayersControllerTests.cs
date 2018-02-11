using MAXIMAGO.KickIt.Players;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MAXIMAGO.KickIt.Tests.Players
{
    public class PlayersControllerTests : ControllerTestsBase
    {
        private PlayerRepository playerRepository;
        private Player johnDoe;
        private IList<Player> players;
        private PlayersController instance;

        public PlayersControllerTests()
        {
            playerRepository = Substitute.For<PlayerRepository>();
            johnDoe = new Player()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@gmail.com",
                Gender = Gender.Male
            };
            players = new List<Player>
            {
                johnDoe
            };
            instance = new PlayersController(playerRepository);
        }

        #region Ctor

        [Fact]
        public void Ctor_PlayersRepositoryNull_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new PlayersController(null));
            Assert.Equal("playerRepository", ex.ParamName);
        }

        #endregion

        #region Get()

        [Fact]
        public async Task Get_ReturnsExistingPlayers()
        {
            playerRepository.Get().Returns(players);

            var result = await instance.Get();

            Assert.Equal(players, GetResult<OkObjectResult>(result).Value);
        }

        #endregion

        #region GetPlayer()

        [Fact]
        public async Task GetPlayer_Id1_ReturnsJohnDoe()
        {
            playerRepository.Get(1).Returns(johnDoe);

            var result = await instance.GetPlayer(1);

            Assert.Equal(johnDoe, GetResult<OkObjectResult>(result).Value);
        }

        [Fact]
        public async Task GetPlayer_Id2_ReturnsNotFound()
        {
            playerRepository.Get(2).Returns((Player)null);

            var result = await instance.GetPlayer(2);

            Assert.Equal("Player with id '2' not found",
                GetResult<NotFoundObjectResult>(result).Value);
        }

        #endregion

        #region CreatePlayer()

        [Fact]
        public async Task CreatePlayer_PlayerNull_ReturnsBadRequest()
        {
            var result = await instance.CreatePlayer(null);

            Assert.Equal("Player missing", GetResult<BadRequestObjectResult>(result).Value);
        }

        [Fact]
        public async Task CreatePlayer_PlayerWithIdExists_ReturnsConflict()
        {
            playerRepository.Exists(1).Returns(true);
            var result = await instance.CreatePlayer(new Player { Id = 1 });

            var objectResult = GetResult<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Conflict, objectResult.StatusCode);
            Assert.Equal("Player with the id '1' already exists.", objectResult.Value);
        }

        [Fact]
        public async Task CreatePlayer_CorrectPlayer_ReturnsCreatedResult()
        {
            playerRepository
                .Save(Arg.Is<Player>(x => x.Equals(johnDoe)))
                .Returns((callInfo) =>
                {
                    johnDoe.Id = 1;
                    return johnDoe;
                });
            var urlHelper = Substitute.For<IUrlHelper>();
            instance.Url = urlHelper;
            var expectedLocation = "http://localhost:8080/api/players/1";
            urlHelper.Link(Arg.Is<string>(x => x == "GetPlayer"), Arg.Any<object>())
                .Returns(expectedLocation);

            var result = await instance.CreatePlayer(johnDoe);

            await playerRepository.Received().Save(Arg.Is<Player>(x => x.Id == 1));

            var createdResult = GetResult<CreatedResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);
            Assert.Equal(1, johnDoe.Id);
            Assert.Equal(johnDoe, createdResult.Value);
            Assert.Equal(expectedLocation, createdResult.Location);
        }

        #endregion

        #region UpdatePlayer()

        [Fact]
        public async Task UpdatePlayer_PlayerNull_ReturnsBadRequest()
        {
            var result = await instance.UpdatePlayer(1, null);
            Assert.Equal("Player missing", GetResult<BadRequestObjectResult>(result).Value);
        }

        [Fact]
        public async Task UpdatePlayer_IdDoesNotEqualPlayerId_ReturnsConflict()
        {
            var result = await instance.UpdatePlayer(1, new Player() { Id = 2 });
            var statusResult = GetResult<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Conflict, statusResult.StatusCode);
            Assert.Equal("Provided id from url does not match to provided player", statusResult.Value);
        }

        [Fact]
        public async Task UpdatePlayer_NotExistingId_ReturnsNotFound()
        {
            playerRepository.Exists(1).Returns(false);

            var result = await instance.UpdatePlayer(1, new Player() { Id = 1 });
            Assert.Equal("Player with id '1' was not found", 
                GetResult<NotFoundObjectResult>(result).Value);
        }

        [Fact]
        public async Task UpdatePlayer_CorrectParams_ReturnsOkResult()
        {
            playerRepository.Exists(1).Returns(true);
            playerRepository.Save(johnDoe).Returns(johnDoe);

            johnDoe.Id = 1;
            var result = await instance.UpdatePlayer(1, johnDoe);
            Assert.Equal(johnDoe,
                GetResult<OkObjectResult>(result).Value);
        }

        #endregion

        #region DeletePlayer()

        [Fact]
        public async Task DeletePlayer_NotExistingPlayerId_ReturnsNotFoundResult()
        {
            playerRepository.Get(1).Returns((Player)null);
            var result = await instance.DeletePlayer(1);
            Assert.Equal("Player with id '1' not found", GetResult<NotFoundObjectResult>(result).Value);
        }

        [Fact]
        public async Task DeletePlayer_ExistingPlayerId_ReturnsNoContent()
        {
            playerRepository.Get(1).Returns(johnDoe);
            playerRepository.Delete(Arg.Is<Player>(x => x == johnDoe)).Returns(true);
            var result = await instance.DeletePlayer(1);
            await playerRepository.Received().Delete(johnDoe);
            Assert.Equal((int)HttpStatusCode.NoContent, GetResult<NoContentResult>(result).StatusCode);
        }

        [Fact]
        public async Task DeletePlayer_ExistingPlayerId_ErrorInDeleteAction_ReturnsNotModified()
        {
            playerRepository.Get(1).Returns(johnDoe);
            playerRepository.Delete(Arg.Is<Player>(x => x == johnDoe)).Returns(false);
            var result = await instance.DeletePlayer(1);
            await playerRepository.Received().Delete(johnDoe);
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.NotModified, statusResult.StatusCode);
        }

        #endregion
    }
}
