using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Model
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        [Required]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength =5)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public User User { get; set; }
    }
}
