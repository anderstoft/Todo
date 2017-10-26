using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Model
{
    public class User
    {
        public long Id { get; set; }

        [Display(Name = "First name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
