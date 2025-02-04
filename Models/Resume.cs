using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIResumePicker.Models
{
    public class Resume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResumeId { get; set; }
        public int JobId { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public string ResumeFilePath { get; set; }

        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}
