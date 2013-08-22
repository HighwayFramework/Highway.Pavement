using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Common.Logging;
using Highway.Pavement;

namespace Highway.Pavement.Tests
{
    [TestClass]
    public class DebugScopeTests
    {
        [TestMethod]
        public void Should_Log_Enter_And_Exit()
        {
            // Arrange
            var logger = MockRepository.GenerateMock<ILog>();
            logger.Expect(e => e.IsDebugEnabled).Return(true);

            // Act
            using (logger.DebugScope("Executing"))
            {
            }

            // Assert
            logger.AssertWasCalled(
                e => e.Debug(Arg<string>.Is.Anything), 
                mo => mo.Repeat.Times(2));
        }

        [TestMethod]
        public void Should_Return_Null_When_Debug_Disabled()
        {
            // Arrange
            var logger = MockRepository.GenerateMock<ILog>();
            logger.Expect(e => e.IsDebugEnabled).Return(false);

            // Act
            var foo = logger.DebugScope("Test");

            // Assert
            Assert.IsNull(foo);
        }
    }
}
