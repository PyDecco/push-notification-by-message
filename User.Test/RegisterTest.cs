using Moq;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.UseCases;
using PushNotificationByMessage.Ports.Out;
using PushNotificationByMessage.Models.Entites;

namespace Users.Test
{
    public class Tests
    {
        [TestFixture]
        public class UserRegistrationTests
        {
            private RegisterUseCase _registerUseCase;
            private Mock<IGenericRepository<User>> _usersRepoMock;

            [SetUp]
            public void Setup()
            {
                _usersRepoMock = new Mock<IGenericRepository<User>>();
                _registerUseCase = new RegisterUseCase(_usersRepoMock.Object);
            }

            [Test]
            public async Task Register_WithValidDto_ReturnsUserRegisterResponseWithId()
            {
                // Arrange
                var userDto = new UserRegisterDto
                {
                    Name = "John Doe",
                    Telephone = "123456789",
                    CompanyName = "ABC Inc.",
                    Address = "123 Main St",
                    Email = "johndoe@example.co",
                    Password = "Abc123!@#"
                };

                var expectedUser = new User
                {
                    Id = 1,
                    Name = userDto.Name,
                    PhoneNumber = userDto.Telephone,
                    CompanyName = userDto.CompanyName,
                    CompanyAddress = userDto.Address,
                    Email = userDto.Email,
                    Password = userDto.Password
                };

                int expectedId = 1; 
                
                _usersRepoMock.Setup(repo => repo.PostAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
                {
                    user.Id = 1;
                    return 1;
                });

                // Act
                var result = await _registerUseCase.Register(userDto);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedId, result.Id);
            }

            [Test]
            public async Task Register_WithInvalidDto_ThrowsUnauthorizedAccessException()
            {
                // Arrange
                var userDto = new UserRegisterDto
                {
                    Name = "Jo",
                    Telephone = "1",
                    CompanyName = "AB",
                    Address = "123",
                    Email = "johndoeexample.com",
                    Password = "abc123"
                };

                // Assert
                Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _registerUseCase.Register(userDto));
            }
        }
    }
}