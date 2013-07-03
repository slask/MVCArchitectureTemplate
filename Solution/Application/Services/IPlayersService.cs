using System;
using System.Collections.Generic;
using Application.DTO;
using Domain.Entities;

namespace Application.Services
{
    public interface IPlayersService
    {
        //TODO: maybe add method to get paged subset of players etc.
        IEnumerable<ScrabblePlayer> GetAllPlayers();
        IOperationResult SavePlayer(PlayerDto playerDto);
        ScrabblePlayer GetPlayer(Guid id);
        IOperationResult ExcludePlayerFromClub(Guid id);
    }
}
