using Autofac;
using InfrastructureBase;
using InfrastructureBase.AuthBase;
using InfrastructureBase.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Http
{
    public class PermissionAuthenticationHandler : AuthenticationManager
    {
        public static new void RegisterAllFilter()
        {
            AuthenticationManager.RegisterAllFilter();
        }
        public override async Task AuthenticationCheck(string routePath)
        {
            if (AuthenticationMethods.Any(x => x.Equals(routePath)))
            {
                var AccountInfo = await HttpContextExt.Current.RequestService.Resolve<IServiceProxyFactory>().CreateProxy<IApplicationService.AccountService.QueryService>().GetAccountInfo();
                HttpContextExt.SetUser((CurrentUser)AccountInfo.Data);
                if (!HttpContextExt.Current.User.IgnorePermission && !HttpContextExt.Current.GetAuthIgnore() && !HttpContextExt.Current.User.Permissions.Contains(routePath))
                    throw new InfrastructureException("��ǰ��¼�û�ȱ��ʹ�øýӿڵı�ҪȨ��,������!");
            }
        }
    }
}