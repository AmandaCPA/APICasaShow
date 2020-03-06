using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoShow.Data;
using ProjetoShow.Models;


namespace ProjetoShow.Controllers
{
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext Database;

        public EventoController(ApplicationDbContext database)
        {
            this.Database = database;
        }
        
        public IActionResult Home()
        {
            var listaShow = Database.CasaShows.ToList();
            var evento = Database.Eventos.ToList();
            return View(evento);
        }

         
        [Authorize(Policy = "Administrador")]
        public IActionResult Cadastrar() 
        {   
            ViewBag.CasaShow = Database.CasaShows.ToList();
            var contagem = Database.CasaShows.ToList();
            var aux = contagem.Count();
            if(aux == 0)
            {
                return View("Erro");
            }
            return View();            
        }

        [Authorize(Policy = "Administrador")]
        public IActionResult Index()
        {
            var listaShow = Database.CasaShows.ToList();
            var evento = Database.Eventos.ToList();
            return View(evento);
        }
        
        public IActionResult Editar(int id)
        {
            ViewBag.CasaShow = Database.CasaShows.ToList();
            Evento evento = Database.Eventos.First(registroEvento => registroEvento.Id == id);
            return View("Cadastrar", evento);
        }
        
        public IActionResult Deletar(int id)
        {
            Evento evento = Database.Eventos.First(registroEvento => registroEvento.Id == id);
            Database.Eventos.Remove(evento);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Salvar(Evento evento)
        {
            if(evento.Id == 0)
            {
            evento.CasaShow = Database.CasaShows.First(registroCasa => registroCasa.Id == evento.CasaShow.Id);
            Database.Eventos.Add(evento);
            }
            else 
            {
                evento.CasaShow = Database.CasaShows.First(registroCasa => registroCasa.Id == evento.CasaShow.Id);
                Database.Update(evento);
            }
            ViewBag.CasaShow = Database.CasaShows.ToList(); 
            Database.SaveChanges();            
            return RedirectToAction("Index");
        }


    }
}