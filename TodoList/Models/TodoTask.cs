using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models
{
    public class TodoTask
    {
        [Key]
        public int Id { get; set; }

        [StringLength(130)]
        [Required]
        public string? Name { get; set; }

        public DateTime? DueTo { get; set;}

        public bool IsCompleted { get; set; }

        public int? Category { get; set; }

    }
}
