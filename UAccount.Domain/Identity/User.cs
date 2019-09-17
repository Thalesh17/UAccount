using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using UAccount.Domain.Models;

namespace UAccount.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string NomeCompleto { get; set; }
        public List<Conta> Contas { get; set; }
        public List<UserRole>  UserRoles { get; set; }
    }
}