using AmazingTODOApp.Domain;
using AmazingTODOApp.Loggers;
using AmazingTODOApp.Repositories;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AmazingTODOApp.Test.Tests.xUnit
{
    public class AmazingTodoAppTest
    {
        private AmazingTodoApp amazingTodoapp;
        private ILogger logger;
        private IAmazingTodoRepository amazingTodoRepository;

        public AmazingTodoAppTest()
        {
            //var context = new AmazingTodoEFContext();

            //amazingTodoRepository = new AmazingTodoEFRepository(context);
            //logger = new ConsoleLogger(LoggerLevel.INFO);
            //amazingTodoapp = new AmazingTodoApp(amazingTodoRepository, logger);

            amazingTodoRepository = A.Fake<IAmazingTodoRepository>();
            logger = new ConsoleLogger(LoggerLevel.INFO);
            amazingTodoapp = new AmazingTodoApp(amazingTodoRepository, logger);
        }

        [Fact]
        public void ValidateUserCredentials()
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
    }
}
