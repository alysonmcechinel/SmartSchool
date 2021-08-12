using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {

            return Ok(new AlunoResgistrarDto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null) 
                return BadRequest("Aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoResgistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoResgistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoResgistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null)
                return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }

            return BadRequest("Aluno não deletado");
        }
    }
}
