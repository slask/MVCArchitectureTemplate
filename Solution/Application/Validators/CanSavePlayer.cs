using System.Collections.Generic;
using Application.DTO;
using Domain.Core.Validation;

namespace Application.Validators
{
    public class CanSavePlayer : IValidationHandler<PlayerDto>
    {
       // private readonly IScrabblePlayersRepository categoryRepository;
        public CanSavePlayer(/*IScrabblePlayersRepository categoryRepository, IUnitOfWork unitOfWork*/)
        {
           // this.categoryRepository = categoryRepository;
        }
      

        public IEnumerable<Notification> Validate(PlayerDto dto)
        {
            return new List<Notification>();
        }
    }
}
