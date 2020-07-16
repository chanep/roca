using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.TimeSheets;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.BackEnd.Materials.EfDal;

namespace Cno.Roca.BackEnd.Tests.Materials
{
    public class TestHelper
    {
        protected IRocaUow _roca;
        protected User _user;

        protected IRocaUow GetRocaUow()
        {
            if(_roca == null)
                _roca = new RocaUow();
            return _roca;
        }

        public void Dispose()
        {
            if(_roca != null)
                _roca.Dispose();
        }

        public User GetUser()
        {
            if (_user == null)
            {
                var roca = GetRocaUow();
                var userLongName = @"ODEBRECHT\ecanepa";
                _user = roca.Users.GetAll().SingleOrDefault(u => u.LongUserName == userLongName);
            }
            return _user;
        }

        public User GetLeader()
        {
            return _roca.Users.GetAllByRole(Roles.Leader).FirstOrDefault();
        }

        public EiMaterial CreateEiMaterial()
        {
            return CreateEiMaterial("");
        }

        public EiMaterial CreateEiMaterial(string suffix)
        {
            return new EiMaterial()
            {
                Description = "InstrumentoMongo" + suffix,
                IdentCode = "E92345678" + suffix,
                Power = "300W",
                Details = new EiMaterialDetails()
                {
                    LongDescription = "Descripcion Larga" + suffix + Environment.NewLine + "Material: Acero" + Environment.NewLine + "Dimensiones: bla bla bla bla"
                }
            };
        }

        public IList<EiMaterial> CreateEiMaterial(int count)
        {
            var list = new List<EiMaterial>();
            for (int i = 0; i < count; i++)
            {
                list.Add(CreateEiMaterial(i.ToString()));
            }
            return list;
        }

        public MlItem CreateMlItem(string suffix)
        {
            var mat = InsertEiMaterial(suffix);
            return new MlItem()
            {
                MaterialId = mat.Id,
                Quantity = 10.1
            };
        }

        public EiMaterial InsertEiMaterial()
        {
            return InsertEiMaterial("");
        }

        public EiMaterial InsertEiMaterial(string suffix)
        {
            var roca = GetRocaUow();
            var entity = (EiMaterial) roca.Materials.Add(CreateEiMaterial(suffix));
            roca.Commit();
            return entity;
        }

        public IList<EiMaterial> InsertEiMaterial(int count)
        {
            var mats = CreateEiMaterial(count);
            var matsAux = new List<EiMaterial>();
            var roca = GetRocaUow();

            foreach (var mat in mats)
            {
                var matAux = roca.Materials.GetAll<EiMaterial>().SingleOrDefault(m => m.IdentCode == mat.IdentCode);
                if (matAux == null)
                {
                    roca.Materials.Add(mat);
                    matsAux.Add(mat);
                }
                else
                {
                    matsAux.Add(matAux);
                }
                    
            }
            roca.Commit();
            return matsAux;
        }

        public MaterialList CreateMaterialList()
        {
            return new MaterialList()
            {
                DocNumber = "GIO-Mongo",
                Title = "Lista de MaterialesX",
                CreatedOn = new DateTime(2013, 12, 29, 10, 30, 0),
                Revision = "A",
                SpecialtyId = 1,
                ProjectId = 1,
                CreatorId = GetUser().Id
            };
        }


        public MaterialList InsertMaterialList()
        {
            var user = GetUser();
            var mats = InsertEiMaterial(2);

            var ml = CreateMaterialList();

            var roca = GetRocaUow();

            var mlItem = new MlItem()
            {
                MaterialId = mats[0].Id,
                Quantity = 13.1
            };

            var mlItem2 = new MlItem()
            {
                MaterialId = mats[1].Id,
                Quantity = 14.1
            };

            bool iguales = mlItem.Equals(mlItem2);

            //ml.AddItem(mlItem);
            //ml.AddItem(mlItem2);

            ml.Items.Add(mlItem);
            ml.Items.Add(mlItem2);

            ml.ApproverId = user.Id;
            ml.RevisorId = user.Id;


            ml = roca.MaterialLists.Add(ml);
            roca.Commit();
            return ml;

        }

        public TaggableType CreateTaggableType()
        {
            return CreateTaggableType("");
        }

        public TaggableType CreateTaggableType(string suffix)
        {
            return new TaggableType()
            {
                SpecialtyId = 3,
                Name = "Valvula" + suffix
            };
        }

        public TaggableType CreateTaggableType(string suffix, int childCount)
        {
            var type = CreateTaggableType(suffix);
            char c = 'A';
            for (int i = 0; i < childCount; i++)
            {
                c = (char) ((int) c + i);
                var subType = CreateTaggableType(suffix + c);
                type.Subtypes.Add(subType);
            }
            return type;
        }

        public TaggableType CreateTaggableType(string suffix, int childCount, int attributesCount)
        {
            var type = CreateTaggableType(suffix);
            char c = 'A';
            for (int i = 0; i < childCount; i++)
            {
                c = (char)((int)c + i);
                var subType = CreateTaggableType(suffix + c);
                for (int j = 0; j < attributesCount; j++)
                {
                    var attr = new TaggableAttribute() {Name = "Attributo" + i.ToString() + j.ToString()};
                    subType.Attributes.Add(attr);
                }
                type.Subtypes.Add(subType);
            }
            return type;
        }

        public IList<TaggableType> CreateTaggableTypes(int typeCount, int childCount)
        {
            var types = new List<TaggableType>();
            for (int i = 0; i < typeCount; i++)
            {
                var type = CreateTaggableType(i.ToString(), childCount);
                types.Add(type);
            }
            return types;
        }

        public TimeSheet CreateTimeSheet()
        {
            var t = new TimeSheet();
            t.ControlDate = TimeSheet.GetNextFriday(DateTime.Now);
            t.UserId = GetUser().Id;
            t.LeaderId = GetLeader().Id;
            t.SpecialtyId = GetUser().Specialties.First().Id;
            return t;
        }

        public IList<TimeSheetItem> CreateDocTimeSheetItem(int timeSheetId, int count)
        {
            var roca = GetRocaUow();
            var user = GetUser();
            var specialty = user.Specialties.FirstOrDefault();
            var docs = roca.Documents.GetAll().Where(d => d.SpecialtyId == specialty.Id).ToList();
            var projects = roca.Projects.GetFull(1).Subprojects;
            var items = new List<TimeSheetItem>();
            for (int i = 0; i < count; i++)
            {
                var item = new TimeSheetItem()
                {
                    TimeSheetId = timeSheetId,
                    SubprojectId = projects.First().Id,
                    DocumentId = docs[i].Id,
                    Hours = 1
                };
                items.Add(item);
            }
            return items;
        }

        public IList<TimeSheetItem> CreateTaskTimeSheetItem(int timeSheetId, int count)
        {
            var roca = GetRocaUow();
            var user = GetUser();
            var specialty = user.Specialties.FirstOrDefault();
            var tasks = roca.LookUps.GetAll().Where(l => l.Type == LookUpTypes.TsTask).ToList();
            var projects = roca.Projects.GetFull(1).Subprojects;
            var items = new List<TimeSheetItem>();
            for (int i = 0; i < count; i++)
            {
                var item = new TimeSheetItem()
                {
                    TimeSheetId = timeSheetId,
                    SubprojectId = projects.First().Id,
                    TaskId = tasks[i].Id,
                    Hours = 1
                };
                items.Add(item);
            }
            return items;
        }

        public TimeSheet InsertTimeSheet(int docs, int tasks)
        {

            var t = CreateTimeSheet();
            var roca = GetRocaUow();

            roca.TimeSheets.Add(t);
            var docsItems = CreateDocTimeSheetItem(t.Id, docs);
            var taskItems = CreateTaskTimeSheetItem(t.Id, tasks);
            var items = docsItems.Concat(taskItems).ToList();
            items[0].Hours = 40 - docs - tasks + 1;

            foreach (var item in items)
            {
                roca.TimeSheets.AddItem(item);
            }
            roca.Commit();
            return t;
        }

    }
}
