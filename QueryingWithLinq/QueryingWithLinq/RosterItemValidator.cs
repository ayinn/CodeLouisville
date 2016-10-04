using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryingWithLinq
{
    //this class makes it easy to write riles for each property
    //see https://github.com/JeremySkinner/FluentValidation for more info
    public class RosterItemValidator : AbstractValidator<RosterItem>
    { 
        public RosterItemValidator()
        {
            //make sure the ID is not zero
            RuleFor(x => x.ID).NotEqual(0);

            //make FirstName required
            RuleFor(rosterItem => rosterItem.FirstName).NotEmpty().WithMessage("First name cannot be left blank");

            //set rules for the last name
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be left blank");
            RuleFor(x => x.LastName.Length).GreaterThan(2).WithMessage("LastName must be greater than 2 Characters");

            //make Gender required
            RuleFor(rosterItem => rosterItem.Gender).NotEmpty().WithMessage("Gender name cannot be left blank");
            
            //if we set the property Gender to the enum instead of string we could make a validation rule
            //like the one below. Think about how easy it would be to change the rule. 
            //RuleFor(rosterItem => rosterItem.Gender).IsInEnum().WithMessage("Gender must be either Male or Female");
        }
    }
}
