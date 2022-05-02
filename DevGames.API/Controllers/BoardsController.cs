using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        public BoardsController(IMapper mapper, IBoardRepository repository)
        {
            this._boardRepository = repository;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var boards = _boardRepository.GetAll();

            return Ok(boards);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var board = _boardRepository.GetById(id);
            
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

            _boardRepository.Add(board);

            return CreatedAtAction("GetById", new { id = board.Id}, model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateBoardsInputModel model)
        {
            var board = _boardRepository.GetById(id);

            if (board == null)
            {
                return NotFound();
            }

            board.Update(model.Description, model.Rules);
            _boardRepository.Update(board);

            return NoContent();
        }
    }
}
