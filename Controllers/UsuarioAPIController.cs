using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetoShow.Data;


namespace ProjetoShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioAPIController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public UsuarioAPIController (ApplicationDbContext database)
        {
            this.database = database;
        }

        
        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {                           
            if (database.Users.Count() > 0)
                {
                    var usuario = database.Users.Select(u => new 
                    {
                        u.Id, 
                        u.Email                         
                        }).ToList();                                      
                    
                    return Ok(usuario);
                    
                }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não existem usuarios cadastrados");
            }
        }


        /// <summary>
        /// Buscar por e-mail.
        /// </summary>
        [HttpGet("{email}")]
        public IActionResult Get (string email)
        {
            try
            {
                var usuario = database.Users.Select(user => new UserTemp
                    {
                        Id = user.Id,
                        Email = user.Email      
                    }).First(c => c.Email == email);               
    
                return Ok(usuario);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("E-mail inválido");
            }
        }


        public class UserTemp
        {
            public string Id {get; set;}
            public  string Email {get; set;}
        }
        
        

    }    
}