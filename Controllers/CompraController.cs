using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoShow.Data;
using ProjetoShow.Models;

namespace ProjetoShow.Controllers
{
    public class CompraController : Controller
    {
        private readonly ApplicationDbContext Database;

        public CompraController(ApplicationDbContext Database)
        {
            this.Database = Database;
        }
        
        public IActionResult Comprar(int id)
        {
            Compra compra = new Compra();
            var evento = Database.Eventos.ToList();
            compra.Evento = Database.Eventos.First(registro => registro.Id == id);
            return View(compra);           
        }    

        /*public IActionResult Comprado (Compra compra)
        {
            var ingresso = Database.Eventos.First(c => c.Id == compra.Evento.Id);
            ingresso.Ingressos -= compra.Quantidade;

            Database.Update(ingresso);
            Database.Attach(compra).State = EntityState.Modified;
            Database.SaveChanges();
            return RedirectToAction("Index");
        }*/

    }
}