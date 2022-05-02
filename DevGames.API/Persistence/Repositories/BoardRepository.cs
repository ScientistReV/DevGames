using DevGames.API.Entities;

namespace DevGames.API.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly DevGamesContext _devGamesContext;
        public BoardRepository(DevGamesContext devGamesContext)
        {
            _devGamesContext = devGamesContext;
        }
        public void Add(Board board)
        {
            _devGamesContext.Boards.Add(board);
            _devGamesContext.SaveChanges();

        }

        public IEnumerable<Board> GetAll()
        {
            return _devGamesContext.Boards.ToList();        
        }

        public Board GetById(int id)
        {
            return _devGamesContext.Boards.SingleOrDefault(p => p.Id == id);
        }

        public void Update(Board board)
        {
            _devGamesContext.Boards.Update(board);
            _devGamesContext.SaveChanges();
        }
    }
}
