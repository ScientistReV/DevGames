using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DevGamesContext _devGamesContext;
        public PostsController(DevGamesContext devGamesContext)
        {
            this._devGamesContext = devGamesContext;
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            var board = _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);

            if(board == null)
            {
                return NotFound();
            }

            return Ok(board.Posts);
        }

        [HttpGet("{postId}")]
        public IActionResult GetById(int id, int postId)
        {
            var board = _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);

            if (board == null)
                return NotFound();
            
            var post = board.Posts.SingleOrDefault(p => p.Id == id);

            if(post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }


        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            var board = _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);

            if (board == null)
                return NotFound();

            var post = new Post(model.Id, model.Title, model.Description);

            board.AddPost(post);

            return CreatedAtAction("GetById", new { id = id, postId = model.Id }, model);
        }


        [HttpPost("{postId}/comments")]
        public IActionResult PostComment(int id, int postId, AddCommentInputModel model)
        {

            var board = _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);

            if (board == null)
                return NotFound();

            var post = board.Posts.SingleOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var comment = new Comment(model.Title, model.Description, model.User);

            post.AddComment(comment);

            return NoContent();
        }


    }
}
