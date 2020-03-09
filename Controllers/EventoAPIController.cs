using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoShow.Data;
using ProjetoShow.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoAPIController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public EventoAPIController (ApplicationDbContext database)
        {
            this.database = database;
        }


        /// <summary>
        /// Listar todos os eventos.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        //listando eventos
        {
            var eventos = database.Eventos.Include(c => c.CasaShow).ToList();
            var cont = eventos.Count();
            if (cont > 0)
                {
                    return Ok(eventos);
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem eventos cadastrados");
                }
        }   


        /// <summary>
        /// Buscar por ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
             Evento evento = database.Eventos.First(e => e.Id == id);
             return Ok(evento);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Id inválido");
            }
        }


        /// <summary>
        /// Listar eventos em ordem alfabética crescente por nome.
        /// </summary>
        [HttpGet("asc")]
        public IActionResult GetAsc()
        {                           
            var eventos = database.Eventos.ToList();
            var cont = eventos.Count();
            {
                if (cont > 0)
                {
                    return Ok(eventos.OrderBy(c => c.NomeEvento));
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem eventos cadastrados");
                }
            }
        }


        /// <summary>
        /// Listar eventos em ordem alfabética decrescente por nome.
        /// </summary>
        [HttpGet("des")]
        public IActionResult GetDesc()
        {                           
            var eventos = database.Eventos.ToList();
            var cont = eventos.Count();
            {
                if (cont > 0)
                {
                    return Ok(eventos.OrderByDescending(c => c.NomeEvento));
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem eventos cadastrados");
                }
            }
        }


        /// <summary>
        /// Listar eventos em ordem crescente por capacidade.
        /// </summary>
        [HttpGet("asc")]
        public IActionResult GetCapacidadeAsc()
        {                           
            var eventos = database.Eventos.ToList();
            var cont = eventos.Count();
            {
                if (cont > 0)
                {
                    return Ok(eventos.OrderBy(c => c.Capacidade));
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem eventos cadastrados");
                }
            }
        }

        
        
        /// <summary>
        /// Inserir um evento.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] EventoTemp eTemp) //vai receber um evento da requisição (temporário) (Model Temp)
        {
            Evento e  = new Evento();
            e.NomeEvento = eTemp.NomeEvento;
            e.Capacidade = eTemp.Capacidade;
            e.Data = eTemp.Data;
            e.Valor = eTemp.Valor;
            e.CasaShow = database.CasaShows.First(c => c.Id == eTemp.IdCasaShow);
            e.Genero = eTemp.Genero;
            e.Ingressos = eTemp.Ingressos;

            database.Eventos.Add(e);
            database.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult("");
        }


        /// <summary>
        /// Deletar um evento.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            try
            {
                Evento evento = database.Eventos.First(p => p.Id == id);
                database.Eventos.Remove(evento);
                database.SaveChanges();
                return Ok();              
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }

        /// <summary>
        /// Alterar evento.
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] EventoTemp eventoTemp) //vai receber um evento da requisição (Model Temp)
        {
            if (eventoTemp.Id > 0)
            {
                try
                    {
                    var evento = database.Eventos.First(e => e.Id == eventoTemp.Id); //buscando id no banco de dados que seja igual ao id enviado na requisição
                    
                    if (evento != null)
                    {
                        evento.NomeEvento = eventoTemp.NomeEvento != null ? eventoTemp.NomeEvento : evento.NomeEvento; 
                        evento.Capacidade = eventoTemp.Capacidade != 0 ? eventoTemp.Capacidade : evento.Capacidade;
                        evento.Data = eventoTemp.Data != null ? eventoTemp.Data : evento.Data; 
                        evento.Valor = eventoTemp.Valor != 0 ? eventoTemp.Valor : evento.Valor;
                        evento.Genero = eventoTemp.Genero != null ? eventoTemp.Genero : evento.Genero;
                        evento.Ingressos = eventoTemp.Ingressos != 0 ? eventoTemp.Ingressos : evento.Ingressos;

                        if(database.CasaShows.Any(c => c.Id == eventoTemp.IdCasaShow))
                        {
                            var casa = database.CasaShows.First(registro => registro.Id == eventoTemp.IdCasaShow);
                            evento.CasaShow = eventoTemp.IdCasaShow != 0 ? casa : evento.CasaShow; 
                        }
                        else
                        {
                            Response.StatusCode = 400;
                            return new ObjectResult("id não encontrado");
                        }          
                                               
                        database.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult("erro");
                    }
                }
                catch
                {
                    Response.StatusCode = 400;
                    return new ObjectResult("erro2");
                }
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult("erro3");
            }
        }


        
    }
}