using AmazingTODOApp.Domain;
using AmazingTODOApp.Loggers;
using AmazingTODOApp.Repositories;
using FakeItEasy;
using Xunit;

namespace AmazingTODOApp.Test.Tests.xUnit
{
    public class AmazingTodoAppTestGherkin
    {
        private AmazingTodoApp amazingTodoapp;
        private ILogger logger;
        private IAmazingTodoRepository amazingTodoRepository;

        public AmazingTodoAppTestGherkin()
        {
            amazingTodoRepository = A.Fake<IAmazingTodoRepository>();
            logger = new ConsoleLogger(LoggerLevel.INFO);
            amazingTodoapp = new AmazingTodoApp(amazingTodoRepository, logger);
        }

        /// <summary>
        /// FEATURE: Authenticate Users
        /// SCENARIO: Valid User login
        /// GIVEN a valid user input his/her credentials
        /// WHEN pressing enter
        /// THEN the user should have access to the main menu
        /// </summary>
        [Fact]
        public void GivenAValidUser_WhenValidated_ThenShouldHaveAccess()
        {
            //Arrenge
            var userName = "miguel";
            var password = "pass123";

            var fakeUser = new User
            {
                UserId = "1",
                UserName = userName,
                Password = password
            };

            A.CallTo(() => amazingTodoRepository.GetUser(userName, password)).Returns(fakeUser);


            //Act
            var authenticatedUser = amazingTodoapp.AuthenticateUser(userName, password);

            //Assert
            Assert.Equal(userName, authenticatedUser.UserName);
        }

        /// <summary>
        /// FEATURE: Authenticate Users
        /// SCENARIO: Invalid User login
        /// GIVEN an invalid user input his/her credentials
        /// WHEN pressing enter
        /// THEN the user should not have access to the main menu
        /// </summary>
        [Fact]
        public void GivenAnInValidUser_WhenValidated_ThenShouldNotHaveAccess()
        {
            //Arrenge
            var userName = "john";
            var password = "pass456";

            A.CallTo(() => amazingTodoRepository.GetUser(userName, password)).Returns(null);

            // Act
            var authenticatedUser = amazingTodoapp.AuthenticateUser(userName, password);

            // Assert
            Assert.Null(authenticatedUser);
        }
    }
}
