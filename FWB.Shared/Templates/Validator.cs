using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWB.Shared.Templates
{
    public static class Validator
    {
        public const string Template = @"using FluentValidation;

using $SolutionName.Core.Database.Tables;

namespace $SolutionName.Core.Validations
{
    public class $EntityNameValidator:AbstractValidator<$EntityNameModifyModel>
    {

        public $EntityNameValidator()
        {
           
        }


    }
}";
    }
}
