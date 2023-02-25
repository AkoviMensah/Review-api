using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.Dtos;

namespace API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class FeedbacksController : ControllerBase
        {
            public FeedbacksController(DataContext context)
            {
                _context = context;
            }

            public DataContext _context { get; set; }

            [HttpGet]
            public async Task<ActionResult<List<Feedback>>> GetAll()
            {
            try
            {
                return await _context.Feedbacks.ToListAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Feedback>> GetReview(int id)
            {
            try
            {
                var data = await _context.Feedbacks.FindAsync(id);
                if (data == null) return NotFound("Review not found");
                return data;
            }
            catch (Exception)
            {
                return BadRequest();
            }
            }

            [HttpPost("new")]
            public async Task<ActionResult<Feedback>> Add(FeedbackDto dto)
            {
                var data = new Feedback();
                data.Rating = dto.Rating;
                data.Text = dto.Text;
                await _context.Feedbacks.AddAsync(data);
                await _context.SaveChangesAsync();
                return data;


            }

            [HttpPut("update")]
            public async Task<ActionResult<Feedback>> Update(Feedback dto)
            {
            try
            {
                var data = await _context.Feedbacks.FindAsync(dto.Id);

                if (data != null)
                {
                    data.Rating = dto.Rating;
                    data.Text = dto.Text;
                    _context.Feedbacks.Update(data);
                    _context.SaveChanges();

                    return data;
                }
                return NotFound("Review to update not found in DB");

            }
            catch (Exception)
            {

                return NotFound("Review to update not found in DB");
            }

            }

            [HttpDelete("delete/{id}")]
            public async Task<ActionResult<Feedback>> Delete(int id)
            {
            try
            {
                var data = await _context.Feedbacks.FindAsync(id);
                if (data == null) return NotFound("Review to delete not found in DB");
                _context.Feedbacks.Remove(data);
                await _context.SaveChangesAsync();

                return data;
            }
            catch (Exception)
            {

                return NotFound("Review to delete not found in DB");
            }
            }
        }
}
