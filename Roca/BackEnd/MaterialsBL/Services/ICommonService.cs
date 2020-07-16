using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.BL.Filters;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface ICommonService
    {
        IEnumerable<Project> GetAllRootProjects();
        Project GetProject(int projectId);
        IEnumerable<User> GetAllUsers();
        User GetUser(int userId);
        User GetUserByLongName(string longName);
        IEnumerable<LookUp> GetAllLookUpsByType(string type);
        IEnumerable<Specialty> GetAllSpecialties();
        Specialty GetSpecialty(int specialtyId);
        Document GetDocument(int documentId);
        IEnumerable<Document> GetDocuments(DocumentFilter filter, bool exactMatch = false);
        IEnumerable<User> GetUsersByRole(string role);

        
    }
}