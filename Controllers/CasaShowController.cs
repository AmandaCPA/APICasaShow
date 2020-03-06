using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoShow.Data;
using ProjetoShow.Models;

namespace ProjetoShow.Controllers
{
    [Authorize(Policy = "Administrador")]
    public class CasaShowController : Controller
    {
        public readonly ApplicationDbContext Data;

        public CasaShowController(ApplicationDbContext data)
        {
            this.Data = data;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Index(int id)
        {
            var casa = Data.CasaShows.ToList();
            return View(casa);
        }
        
        public IActionResult Editar(int id)
        {
            CasaShow casa = Data.CasaShows.First(registroCasa => registroCasa.Id == id);
            return View("Cadastrar", casa);
        }
        
        public IActionResult Deletar(int id)
        {
            CasaShow casa = Data.CasaShows.First(registroCasa => registroCasa.Id == id);
            Data.CasaShows.Remove(casa);
            Data.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Salvar(CasaShow casa)
        {
            if(casa.Id == 0)
            {
                Data.CasaShows.Add(casa);
            }
            else
            {
                CasaShow casaDoBanco = Data.CasaShows.First(registroCasa => registroCasa.Id == casa.Id);
                casaDoBanco.Nome = casa.Nome;
                casaDoBanco.Endereco = casa.Endereco;
            }        
            Data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}