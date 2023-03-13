// using System.Security.Claims;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
//
// namespace Tickets.Core.Security
// {
//     public class CompanyAdministratorFilter : IAuthorizationFilter
//     {
//         private readonly IFactory<ClaimsPrincipal, IRolesClaimValueProvider> _rolesClaimValueProviderFactory;
//
//         public CompanyAdministratorFilter(IFactory<ClaimsPrincipal, IRolesClaimValueProvider> rolesClaimValueProviderFactory)
//         {
//             _rolesClaimValueProviderFactory = rolesClaimValueProviderFactory;
//         }
//
//         public void OnAuthorization(AuthorizationFilterContext context)
//         {
//             var user = context.HttpContext.User;
//             if (!user.Identity.IsAuthenticated)
//             {
//                 return;
//             }
//
//             var rolesClaimValueProvider = _rolesClaimValueProviderFactory.Create(user);
//             var roles = rolesClaimValueProvider.GetValue();
//
//             if (!roles.Contains(UserRoles.CompanyAdministrator.Name))
//             {
//                 context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
//                 return;
//             }
//         }
//     }
// }
