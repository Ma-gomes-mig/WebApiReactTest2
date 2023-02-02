using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("ALunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunoByNome(nome);
                if (alunos == null)
                    return NotFound($"Não existem alunos com esse critério {nome}");
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }


        [HttpGet("{id:int}", Name ="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Not Exist student with id={id}");

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch
            {
                return BadRequest("Request Invalido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno com id={id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Invalido");
            }
        }
            
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno!=null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"ALuno de Id={id} foi excluido com sucesso.");
                }
                else
                {
                    return NotFound($"Aluno com id={id} não encontrado");
                }
            }
            catch
            {
                return BadRequest("Request Invalido");
            }
        }
    }
}
