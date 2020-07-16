using System;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IRocaUow : IDisposable
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        IMaterialRepository Materials { get; }
        IMaterialListRepository MaterialLists { get; }
        IRepository<int, Specialty> Specialties { get; }
        IRepository<int, Unit> Units { get; }
        IRepository<int, LookUp> LookUps { get; }
        ITaggableTypeRepository TaggableTypes { get; }
        IRepository<int, Document> Documents { get; }
        ITimeSheetRepository TimeSheets { get; }

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        void Commit();
    }
}