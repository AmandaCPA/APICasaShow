using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoShow.Data;
using ProjetoShow.Models;

namespace ProjetoShow.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CasaShowAPIController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public CasaShowAPIController(ApplicationDbContext database)
        {
            this.database = database;
        }

        /// <summary>
        /// Listar todas as casas de show.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {                           
            var casasShow = database.CasaShows.ToList();
            var cont = casasShow.Count();
            {
                if (cont > 0)
                {
                    return Ok(casasShow);
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem casas de show cadastradas");
                }
            }

        }


        /// <summary>
        /// Buscar por ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get (int id)
        {
            try
            {
                CasaShow casaShow = database.CasaShows.First(c => c.Id == id);
                return Ok(casaShow);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Id inválido");
            }
        }
        

        /// <summary>
        /// Listar as casas em ordem alfabética crescente por nome.
        /// </summary>
        [HttpGet("asc")]
        public IActionResult GetAsc()
        {                           
            var casasShow = database.CasaShows.ToList();
            var cont = casasShow.Count();
            {
                if (cont > 0)
                {
                    return Ok(casasShow.OrderBy(c => c.Nome));
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem casas de show cadastradas");
                }
            }
        }
        

        /// <summary>
        /// Listar as casas em ordem alfabética decrescente por nome.
        /// </summary>
        [HttpGet("desc")]
        public IActionResult GetDesc()
        {                           
            var casasShow = database.CasaShows.ToList();
            var cont = casasShow.Count();
            {
                if (cont > 0)
                {
                    return Ok(casasShow.OrderByDescending(c => c.Nome));
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem casas de show cadastradas");
                }
            }
        }


        /// <summary>
        /// Buscar casa por nome.
        /// </summary>
        [HttpGet("nome/{nome}")]
        public IActionResult GetNome (string nome)
        {
            try
            {
                var casaShow = database.CasaShows.Where(c => c.Nome == nome).ToList();
                return Ok(casaShow);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Nome inválido");
            }
        }


        /// <summary>
        /// Inserir casa de show.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] CasaShowTemp cTemp)
        {
            try
            {
                CasaShow c  = new CasaShow();
                c.Nome = cTemp.Nome;
                    if (c.Nome == null || c.Nome.Length < 1)
                    {
                    Response.StatusCode = 400;
                    return new ObjectResult("Nome Inválido");
                    }

                c.Endereco = cTemp.Endereco;
                    if (c.Endereco == null || c.Endereco.Length < 1)
                    {
                    Response.StatusCode = 400;
                    return new ObjectResult("Endereço Inválido");
                    }
            
                database.CasaShows.Add(c);
                database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult("Casa criada com sucesso");
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Erro");
            }
        }


        /// <summary>
        /// Deletar uma casa de show.
        /// </summary>
        [HttpDelete]
        public IActionResult Delete (int id)
        {
            try
            {
                CasaShow casaShow = database.CasaShows.First(c => c.Id == id);
                database.CasaShows.Remove(casaShow);
                database.SaveChanges();
                return new ObjectResult("Casa deletada com sucesso");
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Id inválido");
            }
        }


        /// <summary>
        /// Alterar casa de show.
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] CasaShowTemp casaTemp)
        {
           if(casaTemp.Id > 0)
           {
               try
               {
                   var casa = database.CasaShows.First(c => c.Id == casaTemp.Id);

                   if(casa != null)
                   {
                        casa.Nome = casaTemp.Nome != null ? casaTemp.Nome : casa.Nome; 
                        casa.Endereco = casaTemp.Endereco != null ? casaTemp.Endereco : casa.Endereco;   
                        database.SaveChanges();
                        return Ok();                                                  
                   }
                   else
                   {
                        Response.StatusCode = 400;
                        return new ObjectResult("Parametros nulos");
                   } 
                }   
                catch
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("Id inválido");
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult("Id inválido");
            }               
        }
    }
}