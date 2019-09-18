using System;
using UAccount.Domain.Identity;

namespace UAccount.Domain.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public double Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public int UserId { get; set; }
        public User User{ get; set; }
    }
}