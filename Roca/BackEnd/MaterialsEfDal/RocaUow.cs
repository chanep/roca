using System;
using Cno.Roca.BackEnd.Materials.BL;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;

namespace Cno.Roca.BackEnd.Materials.EfDal
{
    /// <summary>
    /// The "Unit of Work"
    ///     1) decouples the repos from the console,controllers,ASP.NET pages....
    ///     2) decouples the DbContext and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type such as a applicant.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data .
    /// </remarks>
    public class RocaUow : IRocaUow
    {
        protected RocaContext DbContext { get; set; }

        public RocaUow()
        {
            //Debug.WriteLine("RocaUow created");
            CreateDbContext();
        }

        //repositories
        #region Repositries
        private IProjectRepository _projects;
        private IUserRepository _users;
        private IMaterialRepository _materials;
        private IMaterialListRepository _materialLists;
        private IRepository<int, Specialty> _specialties;
        private IRepository<int, Unit> _units;
        private IRepository<int, LookUp> _lookUps;
        private ITaggableTypeRepository _taggableTypes;
        private IRepository<int, Document> _documents;
        private ITimeSheetRepository _timeSheets;



        public virtual IProjectRepository Projects
        {
            get
            {
                if (_projects == null)
                    _projects = new ProjectRepository(DbContext);
                return _projects;
            }
        }

        public virtual IUserRepository Users
        {
            get
            {
                if (_users == null)
                    _users = new UserRepository(DbContext);
                return _users;
            }
        }

        public virtual IMaterialRepository Materials
        {
            get
            {
                if (_materials == null)
                    _materials = new MaterialRepository(DbContext);
                return _materials;
            }
        }

        public virtual IMaterialListRepository MaterialLists
        {
            get
            {
                if (_materialLists == null)
                    _materialLists = new MaterialListRepository(DbContext);
                return _materialLists;
            }
        }

        public virtual IRepository<int, Specialty> Specialties
        {
            get
            {
                if (_specialties == null)
                    _specialties = new EfGenericRepository<int, Specialty>(DbContext);
                return _specialties;
            }
        }

        public virtual IRepository<int, Unit> Units
        {
            get
            {
                if (_units == null)
                    _units = new EfGenericRepository<int, Unit>(DbContext);
                return _units;
            }
        }

        public virtual IRepository<int, LookUp> LookUps
        {
            get
            {
                if (_lookUps == null)
                    _lookUps = new EfGenericRepository<int, LookUp>(DbContext);
                return _lookUps;
            }
        }

        public virtual ITaggableTypeRepository TaggableTypes
        {
            get
            {
                if (_taggableTypes == null)
                    _taggableTypes = new TaggableTypeRepository(DbContext);
                return _taggableTypes;
            }
        }


        public virtual IRepository<int, Document> Documents
        {
            get
            {
                if (_documents == null)
                    _documents = new EfGenericRepository<int, Document>(DbContext);
                return _documents;
            }
        }

        public virtual ITimeSheetRepository TimeSheets
        {
            get
            {
                if (_timeSheets == null)
                    _timeSheets = new TimeSheetRepository(DbContext);
                return _timeSheets;
            }
        }

        #endregion

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new RocaContext();

            // Do NOT enable proxied entities, else serialization fails.
            //if false it will not get the associated certification and skills when we
            //get the applicants
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Debug.WriteLine("RocaUow disposed");
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
