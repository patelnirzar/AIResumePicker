using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIResumePicker.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}
