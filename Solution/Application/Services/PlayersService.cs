using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Services
{
    internal class PlayersService : IPlayersService
    {
        public IEnumerable<ScrabblePlayer> GetAllPlayers()
        {
            return new List<ScrabblePlayer>
                {
                    new ScrabblePlayer
                        {
                            ContactPhoneNumber = "123123",
                            Email = "a@a.com",
                            JoinDate = DateTime.UtcNow,
                            Id = new Guid("7A26AF86-4011-4231-ADA6-38C10BA9AB52"),
                            Name = "player1",
                            StreetAddress = "sadasdas dasd ad"
                        },
                        new ScrabblePlayer
                        {
                            ContactPhoneNumber = "89898",
                            Email = "abbbbbb@a.com",
                            JoinDate = DateTime.UtcNow.AddDays(-6),
                            Id = new Guid("0A69938E-3D25-4495-9AFF-2C8AB600C810"),
                            Name = "player2",
                            StreetAddress = "this is secodn plyer adresse"
                        }
                };
        }
    }
}