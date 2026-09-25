using BibliotecaASPNet.Data;
using BibliotecaASPNet.Models;
using BibliotecaASPNet.Repositories;
using BibliotecaASPNet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaASPNet.Controllers
{
    public class LivrosController : Controller
    {
        private readonly RepositoryLivro _repository;
        private readonly RepositoryAutor _repositoryAutor;

        public LivrosController(BibliotecaDbContext context)
        {
            _repository = new RepositoryLivro(context);
            _repositoryAutor = new RepositoryAutor(context);
        }

        private void CarregarAutoresNaViewBag(int? autorSelecionadoId = null)
        {
            ViewBag.Autores = new SelectList(
                _repositoryAutor.Listar(), "Id", "Nome", autorSelecionadoId);
        }

        [HttpGet]
        public IActionResult Index(string? titulo)
        {
            ViewBag.TituloPesquisado = titulo;
            var livros = _repository.Listar(titulo);
            return View(livros);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var livro = _repository.SelecionarComAutor(id);
            if (livro is null) return NotFound();
            return View(livro);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarregarAutoresNaViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LivroViewModel livroVM)
        {
            if (_repository.IsbnJaCadastrado(livroVM.Isbn))
            {
                ModelState.AddModelError(nameof(livroVM.Isbn),
                    "Já existe um livro cadastrado com esse ISBN");
            }
            if (!ModelState.IsValid)
            {
                CarregarAutoresNaViewBag(livroVM.AutorId);
                return View(livroVM);
            }

            var livro = new Livro
            {
                Titulo = livroVM.Titulo,
                Isbn = livroVM.Isbn,
                AnoPublicacao = livroVM.AnoPublicacao,
                Genero = livroVM.Genero,
                NumeroPaginas = livroVM.NumeroPaginas,
                AutorId = livroVM.AutorId
            };
            _repository.Incluir(livro);

            TempData["MensagemSucesso"] = "Livro cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var livro = _repository.SelecionarPorId(id);
            if (livro is null) return NotFound();

            var livroVM = new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Isbn = livro.Isbn,
                AnoPublicacao = livro.AnoPublicacao,
                Genero = livro.Genero,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId
            };
            CarregarAutoresNaViewBag(livro.AutorId);
            return View(livroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LivroViewModel livroVM)
        {
            if (id != livroVM.Id)
            {
                return BadRequest();
            }
            if (_repository.IsbnJaCadastrado(livroVM.Isbn, livroVM.Id))
            {
                ModelState.AddModelError(nameof(livroVM.Isbn),
                    "Já existe um livro cadastrado com esse ISBN");
            }
            if (!ModelState.IsValid)
            {
                CarregarAutoresNaViewBag(livroVM.AutorId);
                return View(livroVM);
            }

            var livro = new Livro
            {
                Id = livroVM.Id,
                Titulo = livroVM.Titulo,
                Isbn = livroVM.Isbn,
                AnoPublicacao = livroVM.AnoPublicacao,
                Genero = livroVM.Genero,
                NumeroPaginas = livroVM.NumeroPaginas,
                AutorId = livroVM.AutorId
            };
            _repository.Alterar(livro);

            TempData["MensagemSucesso"] = "Livro alterado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var livro = _repository.SelecionarComAutor(id);
            if (livro is null) return NotFound();
            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            var livro = _repository.SelecionarPorId(id);
            if (livro is null) return NotFound();

            _repository.Excluir(livro);
            TempData["MensagemSucesso"] = "Livro excluído com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}
