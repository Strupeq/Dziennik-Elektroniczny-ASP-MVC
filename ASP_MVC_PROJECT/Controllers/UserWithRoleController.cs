using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_PROJECT.Models;

namespace ASP_MVC_PROJECT.Controllers
{
    public class UserWithRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserWithRole
        public ActionResult Index()
        {
            var usersWithRoles = (from user in db.Users
                                  select new
                                  {
                                      Id = user.Id,
                                      Name = user.Name,
                                      Surname = user.Surname,
                                      Email = user.Email,
                                      Activated = user.Activated,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in db.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserWithRoleViewModel()

                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Surname = p.Surname,
                                      Activated = p.Activated,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            return View(usersWithRoles);
        }
    }
}
