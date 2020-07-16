using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class CustomRoleProvider : System.Web.Security.RoleProvider
    {

        private ICommonService CommonService
        {
            get { return DependencyResolver.Current.GetService<IRocaService>().CommonService; }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                return "Roca";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var users = CommonService.GetAllUsers().Where(u => u.RoleList.Contains(roleName) && u.LongUserName.Contains(usernameToMatch));
            return users.Select(u => u.LongUserName).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return Roles.All;
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = CommonService.GetUserByLongName(username);
            return user.RoleList;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var users = CommonService.GetAllUsers().Where(u => u.RoleList.Contains(roleName));
            return users.Select(u => u.LongUserName).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = CommonService.GetUserByLongName(username);
            return user.RoleList.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return Roles.All.Contains(roleName);
        }
    }
}