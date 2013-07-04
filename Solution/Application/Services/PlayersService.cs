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

        private readonly IValidationBus _validationBus;
        private readonly IScrabblePlayerRepository _scrabblePlayerRepository;

        public IEnumerable<ScrabblePlayer> GetAllPlayers()
        {
            return _scrabblePlayerRepository.GetAll();
        }

        public IOperationResult SavePlayer(PlayerDto playerDto)
        {
            //1. this method handles both add and update but as very well could have been 2 separate methods
            //2. the transaction in this case is for demo purposes (as to where transactions go)

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

            using (var scope = new TransactionScope())
            {
                if (playerDto.Id == Guid.Empty)
                {
                    currentPlayer.GenerateNewIdentity();
                    _scrabblePlayerRepository.Add(currentPlayer);
                }
                else
                {
                    currentPlayer.ChangeCurrentIdentity(playerDto.Id);
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
                scope.Complete();
            }

            return new OperationResult(true, null);
        }

        public ScrabblePlayer GetPlayer(Guid id)
        {
           return _scrabblePlayerRepository.Get(id);
        }

        public IOperationResult ExcludePlayerFromClub(Guid id)
        {
            var existing = _scrabblePlayerRepository.Get(id);
            if (existing != null)
            {
                _scrabblePlayerRepository.Remove(existing);
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }
    }
}