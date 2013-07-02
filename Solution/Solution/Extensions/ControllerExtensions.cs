using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Core.Validation;

namespace Solution.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddModelErrors(this Controller controller, IEnumerable<Notification> validationResults, string defaultErrorKey = null)
        {
            if (validationResults != null)
            {
                foreach (var validationResult in validationResults)
                {
                    var error = validationResult as Error;
                    if (error != null)
                    {
                        if (!string.IsNullOrEmpty(error.MemberName))
                        {
                            controller.ModelState.AddModelError(error.MemberName, error.Message);
                        }
                        else if (defaultErrorKey != null)
                        {
                            controller.ModelState.AddModelError(defaultErrorKey, error.Message);
                        }
                        else
                        {
                            controller.ModelState.AddModelError(string.Empty, error.Message);
                        }
                    }
                }
            }
        }

        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<Notification> validationResults, string defaultErrorKey = null)
        {
            if (validationResults == null) return;

            foreach (var validationResult in validationResults)
            {
                var error = validationResult as Error;
                if (error != null)
                {
                    string key = error.MemberName ?? defaultErrorKey ?? string.Empty;
                    modelState.AddModelError(key, error.Message);
                }
            }
        }
    }
}
