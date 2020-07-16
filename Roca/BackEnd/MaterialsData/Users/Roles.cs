using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cno.Roca.BackEnd.Materials.Data.Users
{
    public static class Roles
    {
        public const string None = "";
        public const string Read = "Read";
        public const string Write = "Write";
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";
        public const string Leader = "Leader";

        public static string[] All = { None, Read, Write, Admin, SuperAdmin, Leader};
    }
}
