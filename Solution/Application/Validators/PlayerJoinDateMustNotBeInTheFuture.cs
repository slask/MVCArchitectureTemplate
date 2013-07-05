using System;
using System.Collections.Generic;
using Application.DTO;
using Domain.Core.Validation;

namespace Application.Validators
{
    //this validation could have been done better in the presentation layer as it very easy to validate there 
    //but here is done for demo purposes of multiple validators for one DTO is
    public class PlayerJoinDateMustNotBeInTheFuture : IValidationHandler<PlayerDto>
    {
        public IEnumerable<Notification> Validate(PlayerDto dto)
        {
            if (dto.JoinDate != null && dto.JoinDate.Value.ToLocalTime() > DateTime.Now)
            {
                yield return new Error("JoinDate", "Join date must not be in the future");
            }
        }
    }
}
