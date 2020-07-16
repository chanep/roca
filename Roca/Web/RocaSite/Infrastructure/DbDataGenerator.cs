using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;
using Cno.Roca.BackEnd.Materials.Data.Users;
using Cno.Roca.BackEnd.Materials.EfDal;

namespace Cno.Roca.Web.RocaSite.Infrastructure
{
    public class DbDataGenerator
    {
        public static void DeleteTimeSheetTestData()
        {
            using (var roca = new RocaUow())
            {
                var userIds = new List<int>() {2, 3};
                var ts = roca.TimeSheets.GetAll().Where(t => userIds.Contains(t.UserId)).ToList();
                foreach (var timeSheet in ts)
                {
                    roca.TimeSheets.Delete(timeSheet);
                }
                roca.Commit();
            }
        }

        public static void CreateTestData()
        {
            int prevMlId = 0;
            MaterialList newRev = null;
            using (var roca = new RocaUow())
            {
                var users = roca.Users.GetAll().ToList();
                var specialties = roca.Specialties.GetAll().ToList();
                var projects = roca.Projects.GetAll().ToList();
                var materials = CreateEiMaterial(100);
                var purposes = roca.LookUps.GetAll().Where(l => l.Type == LookUpTypes.MlPurpose).ToList();
                foreach (var material in materials)
                {
                    roca.Materials.Add(material);
                }
                var mls = CreateMaterialLists(projects, users, specialties, materials, purposes);
                mls[0].Status = MaterialListStatus.Issued;

                foreach (var ml in mls)
                {
                    roca.MaterialLists.Add(ml);
                }

                var taggableType = CreateTaggableType("A", 2, 4);
                roca.TaggableTypes.Add(taggableType);
                taggableType = CreateTaggableType("B", 2, 4);
                roca.TaggableTypes.Add(taggableType);

                roca.Commit();
                prevMlId = mls[0].Id;
            }

            using (var roca = new RocaUow())
            {
                newRev = roca.MaterialLists.Get(prevMlId);
                newRev.Revision = "B";
                newRev.Status = MaterialListStatus.Elaboration;
                newRev.PreviousRevisionMlId = prevMlId;
                newRev.Id = 0;
            }

            using (var roca = new RocaUow())
            {
                var mlService = new RocaService(roca).MaterialListService;
                mlService.CreateNewRevision(newRev);
            }

        }

        public static void DeleteTestData()
        {
            using (var roca = new RocaUow())
            {
                var mls = roca.MaterialLists.GetAll();
                foreach (var ml in mls)
                {
                    roca.MaterialLists.Delete(ml);
                }

                roca.Commit();

                var mats = roca.Materials.GetAll();
                foreach (var mat in mats)
                {
                    roca.Materials.Delete(mat);
                }

                var taggableTypes = roca.TaggableTypes.GetAll();
                foreach (var type in taggableTypes)
                {
                    roca.TaggableTypes.Delete(type);
                }

                roca.Commit();
            }
        }


        private static EiMaterial CreateEiMaterial(string suffix)
        {
            string logDesc = "Descripcion Larga de InstrumentoMongo" + suffix + Environment.NewLine +
                             "AtrubutoX: ValorX" + Environment.NewLine + "AtrubutoY: ValorY";
            return new EiMaterial()
            {
                Description = "InstrumentoMongo" + suffix,
                IdentCode = "E12345678" + suffix,
                Power = "300W",
                Details = new EiMaterialDetails() { LongDescription = logDesc}
            };
        }

        private static IList<EiMaterial> CreateEiMaterial(int count)
        {
            var list = new List<EiMaterial>();
            for (int i = 0; i < count; i++)
            {
                list.Add(CreateEiMaterial(i.ToString()));
            }
            return list;
        }



        private static IList<MaterialList> CreateMaterialLists(IList<Project> projects, IList<User> users,
            IList<Specialty> specialties, IEnumerable<Material> mats, IList<LookUp> purposes)
        {
            var materials = mats.Take(2).ToList();
            var items = new List<MlItem>()
            {
                new MlItem()
                {
                    Material = materials[0],
                    Quantity = 12.1

                },
                new MlItem()
                {
                    Material = materials[1],
                    Quantity = 23.1
                }

            };
            var items2 = new List<MlItem>()
            {
                new MlItem()
                {
                    Material = materials[0],
                    Quantity = 12.1

                },
                new MlItem()
                {
                    Material = materials[1],
                    Quantity = 23.1
                }

            };
            var items3 = new List<MlItem>()
            {
                new MlItem()
                {
                    Material = materials[0],
                    Quantity = 12.1

                },
                new MlItem()
                {
                    Material = materials[1],
                    Quantity = 23.1
                }

            };
            var items4 = new List<MlItem>()
            {
                new MlItem()
                {
                    Material = materials[0],
                    Quantity = 12.1

                },
                new MlItem()
                {
                    Material = materials[1],
                    Quantity = 23.1
                }

            };

            var user = users[1];
            var user2 = users[0];

            var mls = new List<MaterialList>()
            {
                new MaterialList()
                {
                    Id = 101,
                    ApproverId = user.Id,
                    CreatedOn = DateTime.Now,
                    CreatorId = user.Id,
                    DocNumber = "GIO-CUCAMONA-101",
                    Items = items,
                    PreviousRevisionMl = null,
                    PreviousRevisionMlId = null,
                    Revision = "A",
                    RevisorId = user.Id,
                    SpecialtyId = specialties[2].Id,
                    Title = "Lista de Materiales 1 blablablabla bla bla bla bla ",
                    ProjectId = projects[0].Id,
                    Purpose = purposes[0].Value
                },
                new MaterialList()
                {

                    ApproverId = user.Id,
                    CreatedOn = DateTime.Now.AddDays(1),
                    CreatorId = user.Id,
                    DocNumber = "GIO-CUCAMONA-202",
                    Items = items2,
                    PreviousRevisionMl = null,
                    PreviousRevisionMlId = null,
                    Revision = "C",
                    Revisor = user2,
                    RevisorId = user2.Id,
                    SpecialtyId = specialties[2].Id,
                    Title = "Lista de Materiales 2",
                    ProjectId = projects[0].Id,
                    Purpose = purposes[0].Value
                },
                new MaterialList()
                {
                    ApproverId = user.Id,
                    CreatedOn = DateTime.Now,
                    CreatorId = user.Id,
                    DocNumber = "GIO-CUCAMONA-303",
                    Items = items3,
                    PreviousRevisionMl = null,
                    PreviousRevisionMlId = null,
                    Revision = "C",
                    RevisorId = user2.Id,
                    SpecialtyId = specialties[2].Id,
                    Title = "Lista de Materiales 3",
                    ProjectId = projects[1].Id,
                    Purpose = purposes[0].Value
                },
            };
            return mls;
        }

        private static TaggableType CreateTaggableType(string suffix)
        {
            return new TaggableType()
            {
                SpecialtyId = 3,
                Name = "Valvula" + suffix
            };
        }

        private static TaggableType CreateTaggableType(string suffix, int childCount, int attributesCount)
        {
            var type = CreateTaggableType(suffix);
            char c = 'A';
            for (int i = 0; i < childCount; i++)
            {
                c = (char)((int)c + i);
                var subType = CreateTaggableType(suffix + c);
                for (int j = 0; j < attributesCount; j++)
                {
                    var attr = new TaggableAttribute() { Name = "Attributo" + i.ToString() + j.ToString() };
                    subType.Attributes.Add(attr);
                }
                type.Subtypes.Add(subType);
            }
            return type;
        }
    }
}