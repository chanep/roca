using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.CoreData;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class CommonService : BaseService, ICommonService
    {
        private const int GeneralSpecialtyId = 10;

        public CommonService(IRocaUow rocaUow) : base(rocaUow)
        {
        }

        public IEnumerable<Project> GetAllRootProjects()
        {
            return RocaUow.Projects.GetAllRoot();
        }

        public Project GetProject(int projectId)
        {
            return RocaUow.Projects.GetFull(projectId);
        }


        public IEnumerable<User> GetAllUsers()
        {
            return RocaUow.Users.GetAll();
        }

        public IEnumerable<User> GetUsersByRole(string role)
        {
            return RocaUow.Users.GetAllByRole(role);
        }

        public User GetUser(int userId)
        {
            return RocaUow.Users.Get(userId);
        }

        public User GetUserByLongName(string longName)
        {
            return RocaUow.Users.GetAll().SingleOrDefault(u => u.LongUserName.Contains(longName));
        }

        public IEnumerable<LookUp> GetAllLookUpsByType(string type)
        {
            return RocaUow.LookUps.GetAll().Where(l => l.Type == type);
        }

        public IEnumerable<Specialty> GetAllSpecialties()
        {
            return RocaUow.Specialties.GetAll();
        }

        public Specialty GetSpecialty(int specialtyId)
        {
            return RocaUow.Specialties.Get(specialtyId);
        }

        public Document GetDocument(int documentId)
        {
            return RocaUow.Documents.Get(documentId);
        }

        public IEnumerable<Document> GetDocuments(DocumentFilter filter, bool exactMatch = false)
        {
            var f = filter;
            IQueryable<Document> docs;
                docs = RocaUow.Documents.GetAll()
                .Where(d => ((f.ProjectId == null || f.ProjectId == 0 || f.ProjectId == d.ProjectId) &&
                             (f.SpecialtyId == null || f.SpecialtyId == 0 || f.SpecialtyId == d.SpecialtyId || d.SpecialtyId == GeneralSpecialtyId) &&
                             (f.TypeId == null || f.TypeId == 0 || f.TypeId == d.TypeId)));


            if (exactMatch)
            {
                return docs.Where(d => ((f.DocNumber == null || f.DocNumber == "" || f.DocNumber == d.DocNumber) &&
                                        (f.Title == null || f.Title == "" || f.Title == d.Title)));
            }

            return docs.Where(d => ((f.DocNumber == null || f.DocNumber == "" || d.DocNumber.ToLower().Contains(f.DocNumber.ToLower()) ) &&
                                        (f.Title == null || f.Title == "" || d.Title.ToLower().Contains(f.Title.ToLower()))));

            

        }

    }
}
