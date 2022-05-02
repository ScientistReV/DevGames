using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;
using DevGames.API.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Controllers
{
    [Route("api/boards/{id}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;
        public PostsController(IPostRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            var posts = _repository.GetAllByBoard(id);

            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public IActionResult GetById(int postId)
        {
            var posts = _repository.GetById(postId);

            if (posts == null)
                return NotFound();

            return Ok(posts);
        }


        [HttpPost]
        public IActionResult Post(int id, AddPostInputModel model)
        {
            var posts = new Post(model.Title, model.Description, id);

            _repository.Add(posts);

            return CreatedAtAction("GetById", new { id = id, postId = posts.Id }, model);
        }


        [HttpPost("{postId}/comments")]
        public IActionResult PostComment(int postId, AddCommentInputModel model)
        {
            var postExists = _repository.PostExits(postId);

            if (!postExists)
                return NotFound();

            var comment = new Comment(model.Title, model.Description, model.User, postId);

            _repository.AddComment(comment);

            return NoContent();
        }


    }
}
