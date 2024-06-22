using Employee_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee_Management_System.ServiceFilters
{
    public class BuildEmployeeBasicDetailsFilter:IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeBasicFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is Null");
                return;
            }

            EmployeeBasicFilterCriteria filterCriteria=(EmployeeBasicFilterCriteria)param.Value;
            var roleFilter = filterCriteria.Filters.Find(r => r.FieldName == "role");

            if (roleFilter == null)
            {
                roleFilter = new FilterCriteria2();
                roleFilter.FieldName = "role";
                roleFilter.FieldValue = "Employee";
                filterCriteria.Filters.Add(roleFilter); 
            }

            filterCriteria.Filters.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));

            var result = await next();
        }
    }
}
