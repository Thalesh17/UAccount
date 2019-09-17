using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using UAccount.Repository.Interfaces;

namespace UAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController: ControllerBase
    {
        private readonly IContaRepository _repo;

        private readonly IMapper _mapper;
        public ContaController(IContaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{UsuarioId}")]
        public async Task<IActionResult> Get(int UsuarioId)
        {
            try
            {
                var usuarioConta = await _repo.ObterPorUsuario(UsuarioId);

                var results = _mapper.Map<UserDTO>(usuarioConta);
                
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados falhou. {ex.Message}");
            }
        }
    }
}