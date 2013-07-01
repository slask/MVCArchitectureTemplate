using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Validation;
using Domain.Entities;

namespace Domain.Validators
{
    public class CanSavePlayer : IValidationHandler<ScrabblePlayer>
    {
       // private readonly IScrabblePlayersRepository categoryRepository;
        public CanSavePlayer(/*IScrabblePlayersRepository categoryRepository, IUnitOfWork unitOfWork*/)
        {
           // this.categoryRepository = categoryRepository;
        }
        public IEnumerable<ValidationResult> Validate(ScrabblePlayer command)
        {
            //TODO: verify for unitqueness
            //Category isCategoryExists = null;
            //if (command.CategoryId == 0)
            //    isCategoryExists = categoryRepository.Get(c => c.Name == command.Name);
            //else
            //    isCategoryExists = categoryRepository.Get(c => c.Name == command.Name && c.CategoryId != command.CategoryId);
            //if (isCategoryExists != null)
            //{
            //    yield return new ValidationResult("Name", Resources.CategoryExists);
            //}
            return new List<ValidationResult>();
        }
    }
}
