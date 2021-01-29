using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_PROJECT.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP_MVC_PROJECT.Controllers
{
    public class UserWithRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Dictionary<string, string> TransaltedRoles = new Dictionary<string, string>()
        {
            {"admin","admin"},
            {"parent","rodzic"},
            {"student","uczeń"},
            {"teacher","nauczyciel"},
            {"",""}
        };


        // GET: UserWithRole
        public ActionResult Index(int? roleID)
        {
            IdentityRole selectedRole = null;
            if (roleID!=null)
            {
                selectedRole = db.Roles.FirstOrDefault(obj => obj.Id == roleID.ToString());
            }
           
            var usersWithRoles = (from user in db.Users 
                                  select new
                                  {
                                      Id = user.Id,
                                      Name = user.Name,
                                      Surname = user.Surname,
                                      Email = user.Email,
                                      phoneNumber = user.PhoneNumber,
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
                                      phoneNumber = p.phoneNumber,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            var rolename = "";
            if (selectedRole!=null)
            {
                rolename = selectedRole.Name;
            }

            usersWithRoles = usersWithRoles.Where(user => user.Role == rolename);

            var roleList =  db.Roles.ToList();

            for (int i = 0; i < roleList.Count; i++)
            {
                roleList[i].Name=TransaltedRoles[roleList[i].Name];
            }

            ViewBag.roleList = roleList;
            return View(usersWithRoles);
        }

        //GET 
        public ActionResult EditUser(string userID)
        {
            var userObj = db.Users.FirstOrDefault(user => user.Id == userID);
            var userRoles = userObj.Roles.ToArray();

            string roleName = "";
            if (userRoles.Count() > 0)
            {
                roleName = userRoles.ElementAt(0).RoleId;
            }


            UserWithRoleViewModel model = new UserWithRoleViewModel()
            {
                Id = userObj.Id,
                Name = userObj.Name,
                Surname = userObj.Surname,
                Email = userObj.Email,
                phoneNumber = userObj.PhoneNumber,
                Role = roleName
            };

            var roleList = db.Roles.ToList();
            List<SelectListItem> roleSelectList = new List<SelectListItem>();

            for (int i = 0; i < roleList.Count; i++)
            {
                var tmp = TransaltedRoles[roleList[i].Name];
                roleSelectList.Add(new SelectListItem()
                {
                    Selected= roleName== roleList[i].Id,
                    Text = tmp,
                    Value = roleList[i].Id
                });
            }

            ViewBag.roleSelectList = roleSelectList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserWithRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userObj = db.Users.FirstOrDefault(user => user.Id == model.Id);

            if (userObj == null)
            {
                return HttpNotFound();
            }

            userObj.Name = model.Name;
            userObj.Surname = model.Surname;
            userObj.PhoneNumber = model.phoneNumber;

            if (model.Role==null) {
                userObj.Roles.Clear();
            }
            else
            {
                userObj.Roles.Add(new IdentityUserRole() { RoleId = model.Role, UserId = userObj.Id });
            }


            await db.SaveChangesAsync();


            return RedirectToAction("Index", new {  });
        }


        public ActionResult RemoveUser(string userID)
        {
            var userObj = db.Users.FirstOrDefault(user => user.Id == userID);
            var userRoles = userObj.Roles.ToArray();

            string roleIndex = "";
            if (userRoles.Count() > 0)
            {
                roleIndex = userRoles.ElementAt(0).RoleId;
            }

            var role = db.Roles.FirstOrDefault(re => re.Id == roleIndex);
            string roleName = "brak";
            if (role != null)
            {
                roleName = TransaltedRoles[role.Name];
            }

            UserWithRoleViewModel model = new UserWithRoleViewModel()
            {
                Id = userObj.Id,
                Name = userObj.Name,
                Surname = userObj.Surname,
                Email = userObj.Email,
                phoneNumber = userObj.PhoneNumber != null ? userObj.PhoneNumber : "brak" ,
                Role = roleName
            };

            return View(model);
        }

        // POST: Subjects/Delete/5
        [HttpPost,]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUser(UserWithRoleViewModel user)
        {
            var userObj = db.Users.FirstOrDefault(item=>item.Id==user.Id);
            db.Users.Remove(userObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }


}
