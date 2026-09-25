using BibliotecaASPNet.Data;
using BibliotecaASPNet.Models;
using BibliotecaASPNet.Repositories;
using BibliotecaASPNet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaASPNet.Controllers
{
    public class AutoresController : Controller
    {
        private readonly RepositoryAutor _repository;

        public AutoresController(BibliotecaDbContext context)
        {
            _repository = new RepositoryAutor(context);
        }

        [HttpGet]
        public IActionResult Index(string? nome)
        {
            ViewBag.NomePesquisado = nome;
            var autores = _repository.Listar(nome);
            return View(autores);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var autor = _repository.SelecionarPorId(id);
            if (autor is null) return NotFound();
            return View(autor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AutorViewModel autorVM)
        {
            if (_repository.EmailJaCadastrado(autorVM.Email))
            {
                ModelState.AddModelError(nameof(autorVM.Email),
                    "Já existe um autor cadastrado com esse e-mail");
            }
            if (!ModelState.IsValid)
            {
                return View(autorVM);
            }

            var autor = new Autor
            {
                Nome = autorVM.Nome,
                Nacionalidade = autorVM.Nacionalidade,
                DataNascimento = autorVM.DataNascimento,
                Email = autorVM.Email,
                Biografia = autorVM.Biografia
            };
            _repository.Incluir(autor);

            TempData["MensagemSucesso"] = "Autor cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var autor = _repository.SelecionarPorId(id);
            if (autor is null) return NotFound();

            var autorVM = new AutorViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome,
                Nacionalidade = autor.Nacionalidade,
                DataNascimento = autor.DataNascimento,
                Email = autor.Email,
                Biografia = autor.Biografia
            };
            return View(autorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AutorViewModel autorVM)
        {
            if (id != autorVM.Id)
            {
                return BadRequest();
            }
            if (_repository.EmailJaCadastrado(autorVM.Email, autorVM.Id))
            {
                ModelState.AddModelError(nameof(autorVM.Email),
                    "Já existe um autor cadastrado com esse e-mail");
            }
            if (!ModelState.IsValid)
            {
                return View(autorVM);
            }

            var autor = new Autor
            {
                Id = autorVM.Id,
                Nome = autorVM.Nome,
                Nacionalidade = autorVM.Nacionalidade,
                DataNascimento = autorVM.DataNascimento,
                Email = autorVM.Email,
                Biografia = autorVM.Biografia
            };
            _repository.Alterar(autor);

            TempData["MensagemSucesso"] = "Autor alterado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var autor = _repository.SelecionarPorId(id);
            if (autor is null) return NotFound();
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            var autor = _repository.SelecionarPorId(id);
            if (autor is null) return NotFound();

            _repository.Excluir(autor);
            TempData["MensagemSucesso"] = "Autor excluído com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}
