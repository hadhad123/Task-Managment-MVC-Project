using Model;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IApplicationUserService
    {
        List<UserViewModel> GetUsers();
        UserViewModel GetUserByID(string ID);
        void CreateUser(UserViewModel User);
        void EditUser(UserViewModel User);
        void DeactivateUser(string ID);
        void SaveUser();
    }
}
