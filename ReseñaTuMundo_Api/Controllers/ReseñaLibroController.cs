using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReseñaTuMundo_Api.Data.Repositories;
using ReseñaTuMundo_Api.Model;

namespace ReseñaTuMundo_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseñaLibroController : ControllerBase
    {
        private readonly IReseñaLibroRepository _reseñaLibroRepository;

        public ReseñaLibroController(IReseñaLibroRepository reseñaLibroRepository)
        {
            _reseñaLibroRepository = reseñaLibroRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _reseñaLibroRepository.GetAllReseña());
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetUserDetail(int id)
        {
            return Ok(await _reseñaLibroRepository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReseña([FromBody] ReseñaLibro reseñaLibro)
        {
            if (reseñaLibro == null)

                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _reseñaLibroRepository.InsertReseña(reseñaLibro);
            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] ReseñaLibro reseñaLibro)
        {
            if (reseñaLibro == null)

                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reseñaLibroRepository.UpdateReseña(reseñaLibro);
            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _reseñaLibroRepository.DeleteReseña(new ReseñaLibro { Id_resena = id });
            return NoContent();
        }




    }
}
