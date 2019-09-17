using System.Collections.Generic;
using UAccount.Domain.Dtos;

namespace ProAgil.API.Dtos
{
    public class UserDTO
    {
        public int Id { get; set;}
        public string NomeCompleto { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<ContaDTO> Contas { get; set; }
    }
}