using Api_Catalogo.Context;
using Api_Catalogo.DTOs;
using Api_Catalogo.Models;
using Api_Catalogo.Repository;
using Api_Catalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Catalogo.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("PermitirApiRequest")]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriaProdutos()
        {
            var categorias = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }
        public string GetTeste()
        {
            return $"CategoriaController - {DateTime.Now.ToLongDateString().ToString()}";
        }

        //    obtém os produtos relacionados para cada categoria
       


        //[HttpGet("autor")]
        //public string GetAutor()
        //{
        //    var autor = _config["autor"];
        //    var conexao = _config["ConnectionStrings:Conexao"];

        //    return $"Autor : {autor} Conexao : {conexao}";
        //}

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuservico,
            string nome)
        {
            return meuservico.Saudacao(nome);
        }

        [HttpGet]

        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _uof.CategoriaRepository.Get().ToList();
            var categoriaDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriaDto;
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
          
            var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);


            if (categoria == null)
            {
              
                return NotFound();
            }
            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }

        //[HttpGet("produtos")]

        //public ActionResult<IEnumerable<Categoria>> GetCategoria()
        //{
        //    return _uof.Categorias.Include( x => x.Produtos).ToList();
        //}

        

        [HttpPost]
        public ActionResult<Categoria> Post([FromBody] CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uof.CategoriaRepository.Add(categoria);
            _uof.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("ObterCategoria",
                new  { id = categoria.CategoriaId }, categoriaDTO);           
        }

        [HttpPut("{id}")]
        public ActionResult Put (int id,[FromBody] CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
                return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria == null)
            {
               return NotFound();
            }

            _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }
    }
}
