using System;
using System.ComponentModel.DataAnnotations;

namespace UAccount.Domain.Dtos
{
    public class ContaDTO
    {
        public int Id {get; set;}
        
        [Required (ErrorMessage="Campo Obrigatório.")]
        [StringLength(100, MinimumLength=3, ErrorMessage="Titulo é entre 3 e 100 caracteres.")]
        public string Titulo { get; set; }
        [Required (ErrorMessage="Campo Obrigatório.")]
        [StringLength(100, MinimumLength=3, ErrorMessage="Resumo é entre 3 e 100 caracteres.")]
        public string Resumo { get; set; }
        public DateTime Vencimento { get; set; }
        [Required]
        public double Valor { get; set;}
    }
}