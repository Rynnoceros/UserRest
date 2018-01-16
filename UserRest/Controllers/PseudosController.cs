using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserRest.Contexts;
using UserRest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserRest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PseudosController : Controller
    {
        private static UserContext _context;

        public PseudosController(UserContext context)
        {
            _context = context;
        }


        /// <summary>
        /// GET: api/pseudos
        /// Get this instance.
        /// Method used to get list of all pseudos.
        /// </summary>
        /// <returns>
        /// 200 : List of pseudos
        /// 400 : Error message otherwise
        /// </returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Pseudos.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(new Message() { Detail = ex.Message });
            }
        }

        /// <summary>
        /// GET api/pseudos/5
        /// Get the specified id.
        /// Method used to get pseudo by Id.
        /// </summary>
        /// <returns>
        /// 200 : Pseudo if found
        /// 404 : Not found if pseudo not found
        /// 400 : Error message otherwise
        /// </returns>
        /// <param name="id">Pseudo identifier</param>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Pseudo user = _context.Pseudos.Where(usr => usr.Id == id).FirstOrDefault();

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new Message() { Detail = ex.Message });
            }
        }

        /// <summary>
        /// POST api/pseudos
        /// Post the specified value.
        /// Method used to register unique pseudos.
        /// </summary>
        /// <returns>
        /// 201 : Created if pseudo registered
        /// 409 : Error already exists
        /// 400 : Error message otherwise
        /// </returns>
        /// <param name="value">Pseudo to register</param>
        [HttpPost]
        public IActionResult Post([FromBody]Pseudo value)
        {
            try
            {
                if (!_context.Pseudos.Where(pseudo => pseudo.Name == value.Name).Any())
                {
                    _context.Pseudos.Add(value);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
                }
                else
                {
                    return StatusCode(409, new Message() { Detail = "Pseudo already exists" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Message() { Detail = ex.Message });
            }
        }

        /// <summary>
        /// PUT api/pseudos/5
        /// Put the specified id and value.
        /// Method used to update a pseudo
        /// </summary>
        /// <returns>
        /// 202 : When modified
        /// 404 : Not Found where pseudo does not exist
        /// 409 : Conflict if pseudo already exists
        /// 400 : Error message otherwise
        /// </returns>
        /// <param name="id">Pseudo identifier</param>
        /// <param name="value">New value</param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Pseudo value)
        {
            try
            {
                Pseudo usr = _context.Pseudos.Where(pseudo => pseudo.Id == id).FirstOrDefault();
                if (usr != null)
                {
                    if (!_context.Pseudos.Where(pseudo => pseudo.Name == value.Name).Any())
                    {
                        usr.Name = value.Name;
                        usr.Avatar = value.Avatar;
                        _context.Update(usr);
                        _context.SaveChanges();
                        return Accepted(usr);
                    }
                    else
                    {
                        return StatusCode(409, new Message() { Detail = "Pseudo already exists" });
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new Message() { Detail = ex.Message });
            }
        }

        /// <summary>
        /// DELETE api/values/5
        /// Delete the specified id.
        /// Method used to delete a pseudo.
        /// </summary>
        /// <returns>
        /// 404 : Not found if pseudo does not exist
        /// 400 : Error message otherwise
        /// </returns>
        /// <param name="id">Pseudo identifier to delete</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Pseudo usr = _context.Pseudos.Where(pseudo => pseudo.Id == id).FirstOrDefault();
                if (usr != null)
                {
                    _context.Pseudos.Remove(usr);
                    _context.SaveChanges();
                    return Accepted(new Message() { Detail = $"Pseudo {usr.Name} deleted"});
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Message() { Detail = ex.Message });
            }
        }
    }
}
