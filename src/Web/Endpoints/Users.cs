#if (UseApiOnly)
using SaasApi.Infrastructure.Identity;

namespace SaasApi.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapIdentityApi<ApplicationUser>();
    }
}
#endif
