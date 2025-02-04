using Microsoft.AspNetCore.Mvc;
using AIResumePicker.Data;
using AIResumePicker.Models;
using AIResumePicker.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AIResumePicker.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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


        [HttpPost("submitResume/{jobId}")]
        public async Task<IActionResult> SubmitResume(int jobId, IFormFile resumeFile)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                return BadRequest("Please select a resume file.");
            }

            var resumeFileName = jobId.ToString() + "-" + resumeFile.FileName;
            var filePath = Path.Combine("resumes", resumeFileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resumeFile.CopyToAsync(stream);
            }

            var resume = new Resume { JobId = jobId, ResumeFilePath = filePath };
            _db.Resumes.Add(resume);
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<ResumeDTO>(resume));
        }
    }
}
