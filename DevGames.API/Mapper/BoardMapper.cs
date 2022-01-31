using AutoMapper;
using DevGames.API.Entities;
using DevGames.API.Models;

namespace DevGames.API.Mapper
{
    public class BoardMapper : Profile
    {
        public BoardMapper()
        {
            CreateMap<AddBoardsInputModel, Board>();
        }
    }
}
