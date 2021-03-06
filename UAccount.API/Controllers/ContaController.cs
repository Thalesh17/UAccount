using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using UAccount.Domain.Dtos;
using UAccount.Domain.Models;
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

                var results = _mapper.Map<ContaDTO[]>(usuarioConta);
                
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados falhou. {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Conta model)
        {
            try
            {
                var conta = _mapper.Map<Conta>(model);

                _repo.Add(conta);

                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/Conta/{conta.UserId}", _mapper.Map<ContaDTO>(conta));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados falhou. {ex.Message}");
            } 

            return BadRequest();
        }

        [HttpPut("{ContaId}")]
        public async Task<ActionResult> Put(int ContaId, ContaDTO model)
        {
            try
            {
                var conta = await _repo.ObterPorId(ContaId);

                if(conta == null){return NotFound();}
                
                _mapper.Map(model, conta);

                _repo.Update(conta);

                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/Conta/{conta.UserId}", _mapper.Map<ContaDTO>(conta));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            } 

            return BadRequest();
        }

        [HttpDelete("{ContaId}")]
        public async Task<ActionResult> Delete(int ContaId)
        {
            try
            {
                var conta = await _repo.ObterPorId(ContaId);

                if(conta == null){return NotFound();}

                _repo.Delete(conta);

                if(await _repo.SaveChangesAsync()){
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            } 

            return BadRequest();
        }
    }
}