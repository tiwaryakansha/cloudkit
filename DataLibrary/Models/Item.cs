using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public int ShelfLifeInDays { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string? Description { get; set; }
        public int CategoryId { get;set; }
        public MenuItem Category { get; set; }
    }
}
