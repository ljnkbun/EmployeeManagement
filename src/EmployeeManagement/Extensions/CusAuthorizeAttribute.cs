using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagement.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CusAuthorizeAttribute : ServiceFilterAttribute
    {
        public CusAuthorizeAttribute() : base(typeof(CusAuthorizeFilter))
        {
        }
    }

    public class CusAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public CusAuthorizeFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = (AppUser?)context.HttpContext.Items["User"];

            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var endpoint = context.HttpContext.GetEndpoint();
            var descriptor = endpoint!.Metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerName = descriptor!.ControllerName;
            var actioName = descriptor!.ActionName;

            using var scope = _serviceProvider.CreateScope();
            var roleActionRepository = scope.ServiceProvider.GetRequiredService<IRoleActionRepository>();

            var userAccessActions = await roleActionRepository.GetAllByUser(user.Id);
            if (!userAccessActions.Any(x => x.Controller.ToLower() == controllerName.ToLower() && x.Action.ToLower() == actioName.ToLower()))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

        }
    }
}
