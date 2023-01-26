using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Medismart.DataAccess;
using Medismart.Models.DataModels;
using System.Text;

namespace Medismart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MedismartDBContext _context;

        public UsersController(MedismartDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadCSV(IFormFile files)
        {
            try
            {
                if (files.Length > 0)
                {
                    User user = new User();
                    List<string> strContent = new List<string>();
                    using (var stream = new StreamReader(files.OpenReadStream()))
                    {
                        while (!stream.EndOfStream)
                        {
                            var line = stream.ReadLine();
                            strContent.Add(line);//agrego al List el contenido del archivo                   
                        }
                        foreach (var item in strContent.Skip(1))//salteo las cabeceras
                        {
                            var cells = item.Split(",");
                            user.Name = cells[0];
                            user.LastName = cells[1];
                            user.Address = cells[2];
                            user.DateBirth = DateTime.Parse(cells[3]);
                            _context.Users.Add(user);
                            _context.SaveChangesAsync();

                        }
                    }
                    return Ok("Registros guardados");
                }
                string message = "El archivo no tenia datos";
                return Ok("ERROR" + message);
                
            }
            catch (Exception ex)
            {
                string messageError = "Error procesando la información: " + ex.Message;
                return BadRequest("ERROR" + messageError);
            }
        }
    }
}
