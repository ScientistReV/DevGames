using DevGames.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DevGamesContext _context;
        public PostRepository(DevGamesContext context)
        {
            _context = context;
        }
        public void Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllByBoard(int boardId)
        {
            return _context.Posts.Where(p => p.BoardId == boardId);
        }

        public Post GetById(int postId)
        {
            return _context.Posts.Include(p => p.Comments).SingleOrDefault(p => p.Id == postId);
        }

        public bool PostExits(int postId)
        {
            return _context.Posts.Any(p => p.Id == postId);
        }
    }
}
