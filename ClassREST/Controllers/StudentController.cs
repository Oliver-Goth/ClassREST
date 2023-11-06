using ClassREST.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClassREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository; 
        }

        // GET: api/Student
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get(
            [FromHeader] string? amount,
            [FromQuery] string? nameFilter
            )
        {
            int? parsedAmount = null;
            if (amount != null)
            {
                if (int.TryParse(amount, out var tempParsedAmount))
                {
                    parsedAmount = tempParsedAmount;
                }
                else
                {
                    return BadRequest("Amount must be a integer");
                }
            }

            return Ok(_studentRepository.Get(nameFilter,
                null, null, parsedAmount));
        }

        // GET api/<StudentController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Student> GetById([FromHeader] string color
            , string id)
        {
            if (!int.TryParse(id, out int studentId))
            {

                Response.Headers.Add("Volume", "1l");
                return StatusCode(StatusCodes.Status418ImATeapot);
            }
            Student? student = _studentRepository.GetById(studentId);
            if (student == null) return NotFound();
            return Ok(student);
        }

        // POST api/<StudentController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student newStudent)
        {
            try
            {
                Student addedStudent = _studentRepository.Add(newStudent);
                return Created("/" + addedStudent.Id, addedStudent);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public Student? Put(int id, [FromBody] Student value)
        {
            return _studentRepository.Update(id, value);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public Student? Delete(int id)
        {
            return _studentRepository.Delete(id);
        }
    }
}