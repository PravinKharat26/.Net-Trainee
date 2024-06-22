using Employee_Management_System.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.ServiceFilters
{
    public class BuildEmployeeAdditionalDetailsFilter:IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeAdditionalFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is Null");
                return;
            }

            EmployeeAdditionalFilterCriteria filterCriteria = (EmployeeAdditionalFilterCriteria)param.Value;
            var roleFilter = filterCriteria.Filters.Find(r => r.FieldName == "employeeStatus");

            if (roleFilter == null)
            {
                roleFilter = new FilterCriteria();
                roleFilter.FieldName = "employeeStatus";
                roleFilter.FieldValue = "Active";
                filterCriteria.Filters.Add(roleFilter);
            }

            filterCriteria.Filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));

            var result = await next();
        }
    }
}
