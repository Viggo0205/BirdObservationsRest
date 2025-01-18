using BirdObservationsLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BirdObservationsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdObservationsController : ControllerBase
    {
        private BirdObservationsRepository _birdObservationsRepo;


        public BirdObservationsController(BirdObservationsRepository birdObservationsRepo)
        {
            _birdObservationsRepo = birdObservationsRepo;
        }
        // GET: api/<ParticipantController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<BirdObservation>> Get([FromQuery] int? howMany, [FromQuery] string? species)
        {

            var birdObservations = _birdObservationsRepo.GetAll(howMany,species);
            if (birdObservations != null && birdObservations.Count() != 0)
            {
                return Ok(birdObservations);
            }
            else
                return NotFound(birdObservations);

        }
        // GET api/<ParticipantController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<BirdObservation> Get(int id)
        {
            BirdObservation? birdObservation = _birdObservationsRepo.GetById(id);
            if (birdObservation != null)
            {
                return Ok(birdObservation);
            }
            else return NotFound(birdObservation);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<BirdObservation> Post([FromBody] BirdObservation birdObservation)
        {
            try
            {

                _birdObservationsRepo.Add(birdObservation);
                return Created("/" + birdObservation.Id, birdObservation);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<BirdObservation> Delete(int id)
        {
            if (_birdObservationsRepo.GetById(id) != null)
            {
                _birdObservationsRepo.Delete(id);
                return Ok(_birdObservationsRepo.GetById(id));
            }
            else return NotFound();
        }
    }
}
