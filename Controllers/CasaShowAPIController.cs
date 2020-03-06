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

        [HttpGet]
        public IActionResult Get()
        {
            var casasShow = database.CasaShows.ToList();
            return Ok(casasShow);
        }

        
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
        

        [HttpGet("nome/{nome}")]
        public IActionResult GetNome (string nome)
        {
            try
            {
                var CasaShow = database.CasaShows.Where(c => c.Nome == nome).ToList();
                return Ok(CasaShow);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Nome inválido");
            }
        }


        
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


        [HttpDelete]
        public IActionResult Delete (int id)
        {
            try
            {
                CasaShow casaShow = database.CasaShows.First(c => c.Id == id);
                database.CasaShows.Remove(casaShow);
                database.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Id inválido");
            }
        }


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
                        return new ObjectResult("");
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