using Core.Models.Response;
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
            var userContext = context.HttpContext.User;

            if (!userContext.Identity!.IsAuthenticated)
            {
                context.Result = new JsonResult(new Response<string>() { Succeeded = false, Message = "Unauthorized", Data = null! })
                { StatusCode = StatusCodes.Status401Unauthorized };

                return;
            }

            var endpoint = context.HttpContext.GetEndpoint();
            var descriptor = endpoint!.Metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerName = descriptor!.ControllerName;
            var actioName = descriptor!.ActionName;

            using var scope = _serviceProvider.CreateScope();
            var roleActionRepository = scope.ServiceProvider.GetRequiredService<IRoleActionRepository>();

            var userId = userContext.Claims.FirstOrDefault(x => x.Type == "id")!.Value;
            var userAccessActions = await roleActionRepository.GetAllByUser(int.Parse(userId));
            if (userId != "1")//ADMIN can do everything
            {
                if (!userAccessActions.Any(x => x.Controller.ToLower() == controllerName.ToLower() && x.Action.ToLower() == actioName.ToLower()))
                {
                    context.Result = new JsonResult(new Response<string>() { Succeeded = false, Message = "Unauthorized", Data = null! })
                    { StatusCode = StatusCodes.Status401Unauthorized };
                    return;
                }
            }

        }
    }
}
