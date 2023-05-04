using Microsoft.AspNetCore.Mvc;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.In;
using PushNotificationByMessage.Ports.Out;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;

namespace PushNotificationByMessage.UseCases
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IGenericRepository<User> _usersRepo;

        public RegisterUseCase(IGenericRepository<User> usersRepo)
        {
            _usersRepo = usersRepo;
        }

        public async Task<UserRegisterResponse> Register(UserRegisterDto userDto)
        {
            ValideSePayloadEstaCorreto(userDto);
            var user = await TransformeOUserRegisterDtoEmUser(userDto);
            await CrieUmUsuarioOuLanceError(user);

            return new UserRegisterResponse() { Id = user.Id };
        }

        private void ValideSePayloadEstaCorreto(UserRegisterDto userDto)
        {
            List<ObjectResult> erros = new List<ObjectResult>();
            if (userDto.Name.Length <= 3)
            {
                erros.Add(new ObjectResult("Nome deve ter no minimo 3 caracteres"));
            }
            if (userDto.CompanyName.Length <= 3)
            {
                erros.Add(new ObjectResult("Compania deve ter no minimo 3 caracteres"));
            }
            if (userDto.Telephone.Length <= 3)
            {
                erros.Add(new ObjectResult("Telefone deve ter no minimo 3 caracteres"));
            }
            if (!ValidacaoEmail(userDto.Email))
            {
                erros.Add(new ObjectResult("Email invalido!"));
            }
            if (userDto.Password.Length < 8)
            {
                erros.Add(new ObjectResult("- A senha deve ter pelo menos 8 caracteres.\n"));
            }
            if (!userDto.Password.Any(char.IsUpper))
            {
                erros.Add(new ObjectResult("- A senha deve conter pelo menos uma letra maiúscula.\n"));
            }
            if (!userDto.Password.Any(char.IsLower))
            {
                erros.Add(new ObjectResult("- A senha deve conter pelo menos uma letra minúscula.\n"));
            }
            if (!userDto.Password.Any(char.IsDigit))
            {
                erros.Add(new ObjectResult("- A senha deve conter pelo menos um número.\n"));
            }
            if (!userDto.Password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
            {
                erros.Add(new ObjectResult("- A senha deve conter pelo menos um caractere especial.\n"));
            }
            if(erros.Count() > 0)
            {
                var mensagem = string.Join("\n", erros.Select(e => e.Value.ToString()));
                throw new UnauthorizedAccessException(mensagem);
            }
        }

        private bool ValidacaoEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]{2}$";
            Match match = Regex.Match(email, pattern);
            return match.Success;
        }

        private async Task<User> TransformeOUserRegisterDtoEmUser(UserRegisterDto userDto)
        {
            return new User()
            {
                Name = userDto.Name,
                PhoneNumber = userDto.Telephone,
                CompanyName = userDto.CompanyName,
                CompanyAddress = userDto.Address,
                Email = userDto.Email,
                Password = HashPassword(userDto.Password),
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private async Task CrieUmUsuarioOuLanceError(User userDto)
        {
            var result = await _usersRepo.PostAsync(userDto);

            if (result == 0)
            {
                throw new UnauthorizedAccessException("Não foi possivel criar um usuario");
            }
        }
    }
}
