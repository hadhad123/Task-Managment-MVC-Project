using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoleService
    {
        List<IdentityRole> GetRoles();
        IdentityRole GetRoleByID(string ID);
        void CreateRole(IdentityRole Role);
        void EditRole(IdentityRole Role);
        void SaveRole();
    }
}
