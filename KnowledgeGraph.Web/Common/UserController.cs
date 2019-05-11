using KnowledgeGraph.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetAuthenticatedUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
    }
}
