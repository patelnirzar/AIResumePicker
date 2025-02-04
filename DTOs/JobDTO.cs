using System;

namespace AIResumePicker.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JobCriteriaDTO Criteria { get; set; }  // Only visible to Employee
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class JobCriteriaDTO
    {
        public int CriteriaId { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Skills { get; set; }
    }
}
