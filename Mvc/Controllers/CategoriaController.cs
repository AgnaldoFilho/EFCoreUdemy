using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public CategoriaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categorias = _contexto.Categoria.ToList();
            return View(categorias);
        }

        public IActionResult Editar(int id)
        {
            var categoria = _contexto.Categoria.First(x => x.Id == id);
            return View("Salvar", categoria);
        }

        public IActionResult Deletar(int id)
        {
            var categoria = _contexto.Categoria.First(x => x.Id == id);
            _contexto.Categoria.Remove(categoria);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria modelo)
        {
            if (modelo.Id == 0)
                _contexto.Categoria.Add(modelo);
            else {
                var categoria = _contexto.Categoria.First(x => x.Id == modelo.Id);
                categoria.Nome = modelo.Nome;
            }
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}