using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClasSy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClasSy.Helpers
{
    public class RoleHelper
    {
        public ApplicationDbContext _context { get; set; }

        public RoleHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public static RoleManager<IdentityRole> RoleManager { get; set; }

        public void CreateRoleIfDoesntExist(string roleName)
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            if (!RoleManager.RoleExists(roleName))
            {
                var role = new IdentityRole(roleName);
                RoleManager.Create(role);
            }
        }
    }
}