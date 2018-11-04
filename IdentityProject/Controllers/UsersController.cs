using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Model.ViewModels;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TaskManagment.Controllers
{
    [Authorize(Roles = "Manager")]
    public class UsersController : Controller
    {
        private readonly IApplicationUserService userService;
        private readonly IRoleService roleService;

        public UsersController(IApplicationUserService UserService, IRoleService roleService)
        {
            this.userService = UserService;
            this.roleService = roleService;
        }

        // GET: Users
        public ActionResult Index(string category = null)
        {
            IEnumerable<UserViewModel> viewModelUser = userService.GetUsers();

            return View(viewModelUser);
        }

        // edit
        //public ActionResult Create(string ID)
        //{
        //    //if(ID != null) //edit
        //    //{
        //    //    UserViewModel UserView = userService.GetUserByID(ID);

        //    //    List<IdentityRole> Roles = roleService.GetRoles();
        //    //    ViewBag.RolesList = new SelectList(Roles, "Id", "Name");

        //    //    return View("../Account/Register", UserView);
        //    //}
        //    //else
        //    //{
        //    //    List<IdentityRole> Roles = roleService.GetRoles();

        //    //    ViewBag.RolesList = new SelectList(Roles, "Id", "Name");
        //    //    return View("../Account/Register");
        //    //}

        //    List<IdentityRole> Roles = roleService.GetRoles();
        //    ViewBag.RolesList = new SelectList(Roles, "Id", "Name");
        //    return View("../Account/Register");
        //}

        //// POST: UserViewModels/Create/ID
        //[HttpPost]
        //public ActionResult Create(UserViewModel UserView, int?ID)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if(ID ==null) //add
        //        {
        //            userService.CreateUser(UserView);
        //        }
        //        else //update
        //        {
        //            userService.EditUser(UserView);
        //        }


        //        return RedirectToAction("Index");
        //    }

        //    return View(UserView);
        //}

        //POST: Users/Update/5
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Update(UserViewModel UserView)
        {
            userService.EditUser(UserView);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeactivatePartialView(string ID)
        {
            UserViewModel viewModelUser = userService.GetUserByID(ID);
            return PartialView(viewModelUser);
        }

        [HttpGet]
        public ActionResult DeactivateUser(string ID)
        {
            userService.DeactivateUser(ID);
            IEnumerable<UserViewModel> viewModelUser;
            viewModelUser = userService.GetUsers();

            return View("Index", viewModelUser);
        }
    }
}