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
        void SavePlayer(PlayerDto dto);
        ScrabblePlayer GetPlayer(Guid id);
    }
}
