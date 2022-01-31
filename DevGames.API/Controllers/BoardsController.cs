using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly DevGamesContext _devGamesContext;

        private readonly IMapper _mapper;
        public BoardsController(DevGamesContext devGamesContext, IMapper mapper)
        {
            this._devGamesContext = devGamesContext;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_devGamesContext.Boards);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var board = _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);
            
            if(board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }


        [HttpPost]
        public IActionResult Post(AddBoardsInputModel model)
        {
            var board = _mapper.Map<Board>(model);
            //var board = new Board(model.Id, model.GameTitle, model.Description, model.Rules);

            _devGamesContext.Boards.Add(board);

            return CreatedAtAction("GetById", new { id = model.Id}, model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBoardsInputModel model)
        {
            var board = _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            board.Update(model.Description, model.Rules);

            return NoContent();
        }
    }
}
