using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.CoreData.Entity;

namespace Cno.Roca.BackEnd.Materials.Data.Users
{
    public class User : Entity<int>
    {
        public User()
        {
            Specialties = new List<Specialty>();
        }

        public string LongUserName { get; set; }

        public string UserName
        {
            get { return LongUserName.Split(new[] {'\\'}).Last(); }
        }

        public string Domain
        {
            get { return LongUserName.Split(new[] { '\\' }).First(); }
        }


        /// <summary>
        /// El nombre que se muestra por default en la aplicacion
        /// </summary>
        public string ShowName
        {
            get { return UserName; }
        }

        public string FullName
        {
            get { return Name + " " + LastName; }
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Mail { get; set; }
        public string Roles { get; set; }
        public ICollection<Specialty> Specialties { get; set; }


        public string[] RoleList
        {
            get
            {
                return Roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public bool IsInRole(string role)
        {
            return RoleList.Contains(role);
        }

    }
}
