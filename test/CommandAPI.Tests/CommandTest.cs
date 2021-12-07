using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {   
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }

        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            //Act
            testCommand.HowTo = "Execute Unit tests";

            //Assert
            Assert.Equal("Execute Unit tests", testCommand.HowTo);

        }

        [Fact]
        public void CanChangePlatform()
        {
            //Act
            testCommand.Platform = "Unit Testing";

            //Assert
            Assert.Equal("Unit Testing", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandline()
        {
            //Act
            testCommand.CommandLine = "dotnet Unit tests";

            //Assert
            Assert.Equal("dotnet Unit tests", testCommand.CommandLine);
        }
    }
}