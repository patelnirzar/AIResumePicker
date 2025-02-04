using Microsoft.AspNetCore.Mvc;
using AIResumePicker.Data;
using AIResumePicker.Models;
using AIResumePicker.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AIResumePicker.Service;

namespace AIResumePicker.Controllers
{
    [Route("api/emp/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        private readonly AzureFormRecognizerService _formRecognizerService;
        private readonly OpenAIService _openAIService;

        private readonly HuggingFaceService _huggingFaceService;

        public EmployeeController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _formRecognizerService = new AzureFormRecognizerService();
            _openAIService = new OpenAIService();
            _huggingFaceService = new HuggingFaceService();
        }

        [HttpGet("getAllJobs")]
        public async Task<ActionResult<IEnumerable<JobDTO>>> getAllJobs()
        {
            try
            {
                var jobs = await _db.Jobs.Include(j => j.Criteria).ToListAsync();
                var jobDTOs = _mapper.Map<IEnumerable<JobDTO>>(jobs);
                return Ok(jobDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching jobs");
            }
        }

        [HttpPost("createJob")]
        public async Task<IActionResult> CreateJob(JobDTO jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            _db.Jobs.Add(job);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, job);
            //return CreatedAtAction(nameof(GetAllJobs), new { id = job.JobId }, _mapper.Map<JobDTO>(job));
        }

        [HttpPut("updateJob/{jobId}")]
        public async Task<IActionResult> UpdateJob(int jobId, JobDTO jobDto)
        {
            if (jobId != jobDto.JobId)
            {
                return BadRequest("Job not found");
            }

            var job = await _db.Jobs.Include(j => j.Criteria).FirstOrDefaultAsync(j => j.JobId == jobId);
            if (job == null)
            {
                return NotFound();
            }

            _mapper.Map(jobDto, job);
            job.UpdatedDate = DateTime.Now;

            if (jobDto.Criteria != null)
            {
                if (job.Criteria == null)
                {
                    job.Criteria = new JobCriteria();
                }
                _mapper.Map(jobDto.Criteria, job.Criteria);
            }

            _db.Update(job);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("deleteJob/{jobId}")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            var job = await _db.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return NotFound();
            }

            _db.Jobs.Remove(job);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("shortlistResume/{jobId}")]
        public async Task<IActionResult> ShortlistResume(int jobId)
        {
            //get all resume by job id
            var resumes = await _db.Resumes
                .Include(r => r.Job)
                .Where(r => r.JobId == jobId)
                .ToListAsync();

            //get job by job id
            var job = await _db.Jobs.Include(j => j.Criteria).FirstOrDefaultAsync(j => j.JobId == jobId);

            // Here you would implement your AI logic to shortlist resumes based on criteria

            var jobCriteriaJson = System.Text.Json.JsonSerializer.Serialize(job.Criteria);
            Console.WriteLine($"Job Criteria: {jobCriteriaJson}");

            List<Resume> selectedResumes = new List<Resume>();

            foreach (var resume in resumes)
            {
                // Console.WriteLine("resume : " + resume);
                var filePath = resume.ResumeFilePath;

                // Extract text
                var extractedText = await _formRecognizerService.ExtractTextFromPdfAsync(filePath);
                Console.WriteLine("extractedText : "+ extractedText);

                // Shortlist using OpenAI
                // bool isRelevant = await _openAIService.IsRelevantPdfAsync(extractedText, jobCriteriaJson);
                bool isRelevant = await _huggingFaceService.IsRelevantPdfHuggingAsync(extractedText, jobCriteriaJson);

                Console.WriteLine(resume.ResumeFilePath + "isRelevant :" + isRelevant);

                if (isRelevant){
                    selectedResumes.Add(resume);
                }

            }

            Console.WriteLine("selectedResumes" + selectedResumes);

            Console.WriteLine("------------------------------------------------------------------");

            return Ok(_mapper.Map<List<ResumeDTO>>(selectedResumes));
        }
    }
}
