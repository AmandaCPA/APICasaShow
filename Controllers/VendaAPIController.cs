using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoShow.Data;
using ProjetoShow.Models;


namespace ProjetoShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    

    public class VendaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public VendaAPIController (ApplicationDbContext database)
        {
            this.database = database;
        }

        /// <summary>
        /// Listar todas as vendas.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {                           
            var vendas = database.Vendas.ToList();
            var cont = vendas.Count();
            {
                if (cont > 0)
                {
                    return Ok(vendas);
                }
                else
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Não existem vendas cadastradas");
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
                Venda venda = database.Vendas.First(c => c.Id == id);
                return Ok(venda);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Id inválido");
            }
        }
    }
}