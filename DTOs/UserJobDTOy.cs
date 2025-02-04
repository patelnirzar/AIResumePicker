using System;

namespace AIResumePicker.DTOs
{
    public class UserJobDTO
    {
        public int JobId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
