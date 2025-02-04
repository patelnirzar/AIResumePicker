using System;

namespace AIResumePicker.DTOs
{
    public class ResumeDTO
    {
        public int ResumeId { get; set; }
        public int JobId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ResumeFilePath { get; set; }
    }
}
