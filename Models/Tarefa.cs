using System;
using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public string Status { get; set; }  // Pendente, Concluída etc.
    }
}
