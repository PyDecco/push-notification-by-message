using BCrypt;
using Moq;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.In;
using PushNotificationByMessage.Ports.Out;
using PushNotificationByMessage.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Test.Wrappers;

namespace Users.Test
{
    public class LoginTest
    {
        private LoginUseCase _loginUseCase;
        private Mock<IGenericRepository<User>> _usersRepoMock;
        private Mock<IJWTCore> _jwtCoreMock;
        private Mock<IBCrypt> _bCryptMock;
        [SetUp]
        public void Setup()
        {
            _usersRepoMock = new Mock<IGenericRepository<User>>();
            _jwtCoreMock = new Mock<IJWTCore>();
            _bCryptMock = new Mock<IBCrypt>();
            _loginUseCase = new LoginUseCase(_usersRepoMock.Object, _jwtCoreMock.Object, _bCryptMock.Object);
        }
        [Test]
        public async Task Login_ComDtoValido_RetornaTokenComSucesso()
        {
            var fakeBCrypt = new FakeBCryptWrapper();
            //Arrange

            var loginDto = new LoginDto { Login = "user@test.com", Password = "123456" };
            var expectedJwt = "jwt_token_here";
            var expectedUser = new User { Id = 1, Name = "John Doe", Email = "user@test.com", Password = "hashed_password_here" };
            var expectedResponse = new LoginToReturnDto { Token = expectedJwt, User = new UserLogin { id = expectedUser.Id, name = expectedUser.Name, email = expectedUser.Email } };

            _usersRepoMock.Setup(repo => repo.GetByEmaildAsync(loginDto.Login)).ReturnsAsync(expectedUser);
            _bCryptMock.Setup(bCrypt => bCrypt.Verify(loginDto.Password ,expectedUser.Password)).Returns(true);
            _jwtCoreMock.Setup(jwt => jwt.GeradorDeJwt(loginDto.Login)).ReturnsAsync(expectedJwt);
            var loginUseCase = new LoginUseCase(_usersRepoMock.Object, _jwtCoreMock.Object, _bCryptMock.Object);

            var response = await loginUseCase.Login(loginDto); 

            Assert.IsNotNull(response);
        }
    }
}
