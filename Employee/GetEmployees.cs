using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class GetEmployees
    {
        [Key]
        public int Id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string? lastname { get; set; }
        public DateTime birthday { get; set; }
        public short positionId { get; set; }
        public int salary { get; set; }
        public bool isActive { get; set; }
    }
    public class GetPositions
    {
        [Key]
        public int Id { get; set; }
        public string positionName { get; set; }
    }
}
