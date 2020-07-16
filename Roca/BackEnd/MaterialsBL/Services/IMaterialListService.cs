using System.Collections.Generic;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public interface IMaterialListService
    {
        MaterialList Get(int id);
        MaterialList GetFull(int id);
        IEnumerable<MaterialList> GetAllHeadRevision(int projectId);
        MaterialList GetHeadRevision(int mlId);

        MaterialList Add(MaterialList materialList);
        void Update(MaterialList materialList);
        void Delete(int id);

        IEnumerable<MaterialList> GetHistory(int mlId);
        MlItem AddItem(MlItem item);
        void UpdateItem(MlItem item);
        void DeleteItem(MlItem item);
        void DeleteItem(int itemId);

        void Issue(int id);
        MaterialList CreateNewRevision(MaterialList materialList);

        IEnumerable<MlItem> GetAllItems(int id);
    }
}