using System.Collections.Generic;
using System.Linq;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Repositories
{
    public interface IMaterialListRepository : IRepository<int , MaterialList>
    {
        MaterialList GetFull(int id);
        void LogicalDelete(int id);

        IQueryable<MlItem> GetAllItems(int id);
        MlItem AddItem(MlItem item);       
        void UpdateItem(MlItem item);
        void DeleteItem(MlItem item);

        void DeleteItem(int itemId);
        MlItem GetItem(int itemId);
    }
}