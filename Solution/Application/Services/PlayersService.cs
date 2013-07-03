using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Application.DTO;
using Domain.Core.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    internal class PlayersService : IPlayersService
    {
        public PlayersService(IValidationBus validationBus, IScrabblePlayerRepository scrabblePlayerRepository)
        {
            _scrabblePlayerRepository = scrabblePlayerRepository;
            _validationBus = validationBus;
        }

        private List<ScrabblePlayer> players = new List<ScrabblePlayer>
            {
                new ScrabblePlayer
                    {
                        ContactPhoneNumber = "123123",
                        Email = "a@a.com",
                        JoinDate = DateTime.UtcNow,
                      //  Id = new Guid("7A26AF86-4011-4231-ADA6-38C10BA9AB52"),
                        Name = "player1",
                        StreetAddress = "sadasdas dasd ad"
                    },
                new ScrabblePlayer
                    {
                        ContactPhoneNumber = "89898",
                        Email = "abbbbbb@a.com",
                        JoinDate = DateTime.UtcNow.AddDays(-6),
                      //  Id = new Guid("0A69938E-3D25-4495-9AFF-2C8AB600C810"),
                        Name = "player2",
                        StreetAddress = "this is secodn plyer adresse"
                    }
            };

        private readonly IValidationBus _validationBus;
        private readonly IScrabblePlayerRepository _scrabblePlayerRepository;

        public IEnumerable<ScrabblePlayer> GetAllPlayers()
        {
            return players;
        }

        public IOperationResult SavePlayer(PlayerDto playerDto)
        {
            var validationErrors = _validationBus.Validate(playerDto);
            var validationResults = validationErrors as IList<Notification> ?? validationErrors.ToList();
            if (validationErrors != null && validationResults.Any())
                return new OperationResult(false, validationResults);

            var currentPlayer = new ScrabblePlayer
                {
                    ContactPhoneNumber = playerDto.ContactPhoneNumber,
                    Email = playerDto.Email,
                    JoinDate = playerDto.JoinDate,
                    Name = playerDto.Name,
                    StreetAddress = playerDto.StreetAddress
                };
            currentPlayer.GenerateNewIdentity();

          //  using (var scope = new TransactionScope())
            {
                if (playerDto.Id == Guid.Empty)
                {
                    _scrabblePlayerRepository.Add(currentPlayer);
                }
                else
                {
                    var persistedPlayer =_scrabblePlayerRepository.Get(playerDto.Id);
                    if (persistedPlayer != null)
                    {
                        _scrabblePlayerRepository.Merge(persistedPlayer, currentPlayer);
                    }
                    else
                    {
                        //log the problem or do something else
                    }
                }

                _scrabblePlayerRepository.UnitOfWork.Commit();
           //     scope.Complete();
            }

            return new OperationResult(true, null);
        }

        public ScrabblePlayer GetPlayer(Guid id)
        {
            //TODO: retrive the player from storage
            return players.Find(p => p.Id == id);
        }

        public IOperationResult ExcludePlayerFromClub(Guid id)
        {
            var result = players.RemoveAll(p => p.Id == id);
            if (result > 0)
                return new OperationResult(true);

            return new OperationResult(false);
        }
    }
}