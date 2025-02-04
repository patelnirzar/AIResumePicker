using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIResumePicker.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("CriteriaId")]
        public JobCriteria Criteria { get; set; }  // Only visible to Employee
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }

    public class JobCriteria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CriteriaId { get; set; }

        public string Experience { get; set; }  // in years
        public string Education { get; set; }  // e.g., "Bachelor's Degree"
        public string Skills { get; set; }  // e.g., ["C#", "SQL", "Communication"]
    }
}
