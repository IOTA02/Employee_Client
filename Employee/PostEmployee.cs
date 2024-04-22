using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    internal class PostEmployee
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public short PositionId { get; set; }
        public int Salary { get; set; }
        public bool IsActive { get; set; }
    }
}
