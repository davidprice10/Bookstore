using BookStore.Domain;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork, ILogger logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        // GET: api/<Books>
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var books = await _unitOfWork.Books.GetAllBooksAsync();
                _logger.LogInformation("Returned all books from the database");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllBooksAsync action - {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/Books/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            try
            {
                var book = await _unitOfWork.Books.GetBookByIDAsync(id);
                if (book == null)
                {
                    _logger.LogError($"Book with id - {id}, hasn't been found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Successfully found book with id - {id}");
                    return Ok(book);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetBookByIdAsync action - {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/Books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookByIdAsync(int id)
        {
            try
            {
                var book = await _unitOfWork.Books.GetBookByIDAsync(id);
                if (book == null)
                {
                    _logger.LogError($"Book with id - {id}, hasn't been found.");
                    return NotFound();
                }

                _unitOfWork.Books.Delete(book);
                _unitOfWork.Books.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteBookById action - {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/Books
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] Book request)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogError("Book request provided is null.");
                    return BadRequest("Book request is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Book request provided.");
                    return BadRequest("Invalid book request");
                }

                _unitOfWork.Books.Add(request);
                _unitOfWork.Books.SaveChanges();

                return Ok(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateBookAsync action - {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        // PUT api/Books
        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync([FromBody] Book request)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogError("Book request provided is null.");
                    return BadRequest("Book request is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Book request provided.");
                    return BadRequest("Invalid book request");
                }

                _unitOfWork.Books.Update(request);
                _unitOfWork.Books.SaveChanges();

                return Ok(request);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateBookAsync action - {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
