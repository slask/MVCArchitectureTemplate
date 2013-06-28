using System.Collections.Generic;
using Domain.Entities;

namespace Application.Services
{
    public interface IPlayersService
    {
        //TODO: maybe add method to get paged subset of players etc.
        IEnumerable<ScrabblePlayer> GetAllPlayers();
    }
}
