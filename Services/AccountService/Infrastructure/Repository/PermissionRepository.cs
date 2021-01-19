using Infrastructure.EfDataAccess;
using Infrastructure.PersistenceObject;
using InfrastructureBase.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PermissionRepository : RepositoryBase<EfDbContext, Domain.Permission, Permission>, Domain.Repository.IPermissionRepository
    {
        private readonly EfDbContext context;
        public PermissionRepository(EfDbContext context) : base(context) { this.context = context; }

        public override void Delete(Expression<Func<Domain.Permission, bool>> condition)
        {
            var delPermissions = context.Permission.Where(condition.ReplaceParameter<Domain.Permission, Permission>());
            context.Permission.RemoveRange(delPermissions);//ɾ����ӦȨ��
            var delPermissionIds = delPermissions.Select(x => x.Id);
            context.RolePermission.RemoveRange(context.RolePermission.Where(x => delPermissionIds.Contains(x.PermissionId)));//ɾ����ӦȨ�޽�ɫ��ϵ
        }
    }
}