using BibliotecaASPNet.Data;
using BibliotecaASPNet.Models;
using BibliotecaASPNet.Repositories;
using BibliotecaASPNet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaASPNet.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly RepositoryEmprestimo _repository;
        private readonly RepositoryLivro _repositoryLivro;

        public EmprestimosController(BibliotecaDbContext context)
        {
            _repository = new RepositoryEmprestimo(context);
            _repositoryLivro = new RepositoryLivro(context);
        }

        private void CarregarLivrosNaViewBag(int? livroSelecionadoId = null)
        {
            ViewBag.Livros = new SelectList(
                _repositoryLivro.Listar(), "Id", "Titulo", livroSelecionadoId);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var emprestimos = _repository.Listar();
            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var emprestimo = _repository.SelecionarComLivro(id);
            if (emprestimo is null) return NotFound();
            return View(emprestimo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarregarLivrosNaViewBag();
            var emprestimoVM = new EmprestimoViewModel
            {
                DataEmprestimo = DateTime.Now,
                DataDevolucaoPrevista = DateTime.Now.AddDays(14)
            };
            return View(emprestimoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmprestimoViewModel emprestimoVM)
        {
            if (!ModelState.IsValid)
            {
                CarregarLivrosNaViewBag(emprestimoVM.LivroId);
                return View(emprestimoVM);
            }

            var emprestimo = new Emprestimo
            {
                LivroId = emprestimoVM.LivroId,
                NomeLeitor = emprestimoVM.NomeLeitor,
                DataEmprestimo = emprestimoVM.DataEmprestimo,
                DataDevolucaoPrevista = emprestimoVM.DataDevolucaoPrevista,
                DataDevolucaoReal = emprestimoVM.DataDevolucaoReal,
                Devolvido = emprestimoVM.Devolvido
            };
            _repository.Incluir(emprestimo);

            TempData["MensagemSucesso"] = "Empréstimo cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var emprestimo = _repository.SelecionarPorId(id);
            if (emprestimo is null) return NotFound();

            var emprestimoVM = new EmprestimoViewModel
            {
                Id = emprestimo.Id,
                LivroId = emprestimo.LivroId,
                NomeLeitor = emprestimo.NomeLeitor,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucaoPrevista = emprestimo.DataDevolucaoPrevista,
                DataDevolucaoReal = emprestimo.DataDevolucaoReal,
                Devolvido = emprestimo.Devolvido
            };
            CarregarLivrosNaViewBag(emprestimo.LivroId);
            return View(emprestimoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmprestimoViewModel emprestimoVM)
        {
            if (id != emprestimoVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                CarregarLivrosNaViewBag(emprestimoVM.LivroId);
                return View(emprestimoVM);
            }

            var emprestimo = new Emprestimo
            {
                Id = emprestimoVM.Id,
                LivroId = emprestimoVM.LivroId,
                NomeLeitor = emprestimoVM.NomeLeitor,
                DataEmprestimo = emprestimoVM.DataEmprestimo,
                DataDevolucaoPrevista = emprestimoVM.DataDevolucaoPrevista,
                DataDevolucaoReal = emprestimoVM.DataDevolucaoReal,
                Devolvido = emprestimoVM.Devolvido
            };
            _repository.Alterar(emprestimo);

            TempData["MensagemSucesso"] = "Empréstimo alterado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emprestimo = _repository.SelecionarComLivro(id);
            if (emprestimo is null) return NotFound();
            return View(emprestimo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            var emprestimo = _repository.SelecionarPorId(id);
            if (emprestimo is null) return NotFound();

            _repository.Excluir(emprestimo);
            TempData["MensagemSucesso"] = "Empréstimo excluído com sucesso";
            return RedirectToAction(nameof(Index));
        }
    }
}
