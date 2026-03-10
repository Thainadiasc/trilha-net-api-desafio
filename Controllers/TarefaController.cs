using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Tarefa/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> ObterPorId(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();
            return tarefa;
        }

        // GET /Tarefa/ObterTodos
        [HttpGet("ObterTodos")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTodos()
        {
            return await _context.Tarefas.ToListAsync();
        }

        // GET /Tarefa/ObterPorTitulo?titulo=...
        [HttpGet("ObterPorTitulo")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorTitulo(string titulo)
        {
            var tarefas = await _context.Tarefas
                .Where(t => t.Titulo.Contains(titulo))
                .ToListAsync();
            return tarefas;
        }

        // GET /Tarefa/ObterPorData?data=...
        [HttpGet("ObterPorData")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorData(System.DateTime data)
        {
            var tarefas = await _context.Tarefas
                .Where(t => t.Data.Date == data.Date)
                .ToListAsync();
            return tarefas;
        }

        // GET /Tarefa/ObterPorStatus?status=...
        [HttpGet("ObterPorStatus")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorStatus(string status)
        {
            var tarefas = await _context.Tarefas
                .Where(t => t.Status == status)
                .ToListAsync();
            return tarefas;
        }

        // POST /Tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefa>> Criar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        // PUT /Tarefa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
                return BadRequest();

            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tarefas.Any(t => t.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE /Tarefa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
