using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO;
using Application.Resources;
using Domain.Core.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Validators
{
    public class PlayerMustHaveUniqueEmailAddressValidator : IValidationHandler<PlayerDto>
    {
        private readonly IScrabblePlayerRepository _playerRepository;

        public PlayerMustHaveUniqueEmailAddressValidator(IScrabblePlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public IEnumerable<Notification> Validate(PlayerDto dto)
        {
            var errors = new List<Error>();

            ScrabblePlayer playerWithSameEmail;
            if (dto.Id == Guid.Empty) //player is new, and not editing
            {
                playerWithSameEmail = _playerRepository.GetFiltered(p => p.Email == dto.Email).FirstOrDefault();
            }
            else
            {
                playerWithSameEmail = _playerRepository.GetFiltered(p => p.Email == dto.Email && p.Id != dto.Id).FirstOrDefault();
            }

            if (playerWithSameEmail != null)
            {
                //if the convention of dto-Name == viewModel-Name then this will display where it should
                errors.Add(new Error("Email", Messages.CanSavePlayer_Validate_A_player_with_same_email_already_exists));
            }
            return errors;
        }
    }
}
