using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClasSy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClasSy.Helpers
{
    // Made by: Muhammed Yasin Yildirim
    public class RoleHelper
    {
        public ApplicationDbContext _context { get; set; }

        public RoleHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public static RoleManager<IdentityRole> RoleManager { get; set; } // property which is not doing anything with db currently

        public void CreateRoleIfDoesntExist(string roleName)
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context)); // Role store handles operations with db

            if (!RoleManager.RoleExists(roleName))
            {
                // Creating new role if it does not exist
                var role = new IdentityRole(roleName);
                RoleManager.Create(role);
            }
        }
    }
}