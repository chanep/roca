using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.Data;
using Cno.Roca.BackEnd.Materials.Data.Materials;

namespace Cno.Roca.BackEnd.Materials.BL.Services
{
    public class MaterialListService : BaseService, IMaterialListService
    {
        public MaterialListService(IRocaUow rocaUow) : base(rocaUow)
        {
        }

        protected IQueryable<MaterialList> GetAll()
        {
            return RocaUow.MaterialLists.GetAll();
        }

        public MaterialList Get(int id)
        {
            return RocaUow.MaterialLists.Get(id);
        }

        public MaterialList GetFull(int id)
        {
            return RocaUow.MaterialLists.GetFull(id);
        }

        public MaterialList Add(MaterialList materialList)
        {
            materialList.Validate();
            RocaUow.MaterialLists.Add(materialList);
            RocaUow.Commit();
            return materialList;
        }

        public void Update(MaterialList materialList)
        {
            materialList.Validate();
            RocaUow.MaterialLists.Update(materialList);
            RocaUow.Commit();
        }

        public void Delete(int id)
        {
            RocaUow.MaterialLists.LogicalDelete(id);
            RocaUow.Commit();
        }

        public IEnumerable<MaterialList> GetAllHeadRevision(int projectId)
        {
            var oldRevisions = GetAll().Where(ml => ml.ProjectId == projectId).Select(ml => ml.PreviousRevisionMlId).Distinct();
            return GetAll().Where(ml => ml.ProjectId == projectId && !oldRevisions.Contains(ml.Id)).AsEnumerable();
        }

        public MaterialList GetHeadRevision(int mlId)
        {
            var next = GetAll().SingleOrDefault(ml => ml.PreviousRevisionMlId == mlId);
            if (next == null)
                return GetFull(mlId);
            return GetHeadRevision(next.Id);
        }

        public IEnumerable<MaterialList> GetHistory(int mlId)
        {
            var head = GetHeadRevision(mlId);
            RecursivePrevRevisionLoad(head);
            var history = new List<MaterialList>();
            history.Add(head);
            var ml = head;
            while (ml.PreviousRevisionMl != null)
            {
                history.Add(ml.PreviousRevisionMl);
                ml = ml.PreviousRevisionMl;
            }
            return history;
        }

        private void RecursivePrevRevisionLoad(MaterialList materilList)
        {
            var prevId = materilList.PreviousRevisionMlId;
            if (prevId != null)
            {
                var prev = GetFull(prevId.Value);
                materilList.PreviousRevisionMl = prev;
                RecursivePrevRevisionLoad(prev);
            }                
        }

        public IEnumerable<MlItem> GetAllItems(int id)
        {
            return RocaUow.MaterialLists.GetAllItems(id).AsEnumerable();
        }

        public MlItem AddItem(MlItem item)
        {
            var mlId = item.MlId;
            var ml = Get(mlId);
            if (ml == null)
                throw new RocaException("No existe la lista de materiales id:" + mlId);
            if (ml.Status != MaterialListStatus.Elaboration)
                throw new RocaUserException("No se puede agregar items a una Lista de Materiales que no se encuentra en Elaboracion");

            var existing = GetItemByMaterialId(mlId, item.MaterialId);
            if (existing != null)
                throw new RocaException(string.Format("Ya existe el material id:{0} en la lista de materiales id:{1}",
                        existing.MaterialId, mlId));

            item.PrevQuantity = GetPrevQuantity(ml.PreviousRevisionMlId, item.MaterialId);

            RocaUow.MaterialLists.AddItem(item);
            RocaUow.Commit();
            return item;
        }

        public void UpdateItem(MlItem item)
        {
            var existing = GetItemById(item.MlId, item.Id);
            if (existing == null)
                throw new RocaException(string.Format("No existe el material id:{0} en la lista de materiales id:{1}",
                        item.MaterialId, item.MlId));
            var ml = Get(item.MlId);
            if (ml.Status != MaterialListStatus.Elaboration)
                throw new RocaUserException("No se puede modificar items a una Lista de Materiales que no se encuentra en Elaboracion");

            RocaUow.MaterialLists.UpdateItem(item);
            RocaUow.Commit();
        }

        public void DeleteItem(MlItem item)
        {
            var existing = GetItemById(item.MlId, item.Id);
            if (existing == null)
                throw new RocaException(string.Format("No existe el material id:{0} en la lista de materiales id:{1}",
                    item.MaterialId, item.MlId));
            var ml = Get(item.MlId);
            if (ml.Status != MaterialListStatus.Elaboration)
                throw new RocaUserException(
                    "No se le puede borrar items a una Lista de Materiales que no se encuentra en Elaboracion");

            RocaUow.MaterialLists.DeleteItem(item);
            RocaUow.Commit();
        }

        public void DeleteItem(int itemId)
        {
            var item = RocaUow.MaterialLists.GetItem(itemId);
            DeleteItem(item);
        }

        public void Issue(int id)
        {
            var ml = Get(id);
            if (ml.Status != MaterialListStatus.Elaboration)
                throw new RocaUserException(string.Format("Error al emitir la Lista de Materiales. La lista no esta en estado 'En Elaboracion'"));
            ml.Status = MaterialListStatus.Issued;
            Update(ml);

            if (ml.PreviousRevisionMlId != null)
            {
                var prev = Get(ml.PreviousRevisionMlId.Value);
                prev.Status = MaterialListStatus.Superseded;
                Update(prev);
            }
            RocaUow.Commit();
        }

        public MaterialList CreateNewRevision(MaterialList materialList)
        {
            materialList.Validate();
            if (materialList.PreviousRevisionMlId == null)
                throw new RocaException(string.Format("Error al crear nueva revision de MaterialList. El campo PreviousRevisionMlId es null"));
            var prev = GetFull(materialList.PreviousRevisionMlId.Value);

            if (prev.Status != MaterialListStatus.Issued)
                throw new RocaUserException(string.Format("Error al crear nueva revision de MaterialList. La revision actual no esta en estado 'emitida'"));
            if (materialList.ProjectId != prev.ProjectId)
                throw new RocaException(string.Format("Error al crear nueva revision de MaterialList. El proyecto nuevo ({0}) es diferente de la previo({1})", materialList.ProjectId, prev.ProjectId));
            if (materialList.SpecialtyId != prev.SpecialtyId)
                throw new RocaException(string.Format("Error al crear nueva revision de MaterialList. La especialidad nueva ({0}) es diferente de la previa({1})", materialList.SpecialtyId, prev.SpecialtyId));
            if (materialList.Title != prev.Title)
                throw new RocaException(string.Format("Error al crear nueva revision de MaterialList. El titulo nuevo ({0}) es diferente del previo({1})", materialList.Title, prev.Title));
            if (materialList.Title != prev.Title)
                throw new RocaException(string.Format("Error al crear nueva revision de MaterialList. El DocNumber nuevo ({0}) es diferente del previo({1})", materialList.DocNumber, prev.DocNumber));


            materialList.Status = MaterialListStatus.Elaboration;

            //se cargan todos los items de la revision anterior
            foreach (var prevItem in prev.Items)
            {
                var newItem = new MlItem()
                {
                    MaterialId = prevItem.MaterialId,
                    Quantity = prevItem.Quantity,
                    PrevQuantity = prevItem.Quantity
                };
                materialList.Items.Add(newItem);
            }

            Add(materialList);
            RocaUow.Commit();
            return materialList;
        }


        protected MlItem GetItemById(int mlId, int itemId)
        {
            return RocaUow.MaterialLists.GetAllItems(mlId).FirstOrDefault(i => i.Id == itemId);
        }

        protected MlItem GetItemByMaterialId(int mlId, int materialId)
        {
            return RocaUow.MaterialLists.GetAllItems(mlId).FirstOrDefault(i => i.MaterialId == materialId);
        }

        protected double GetPrevQuantity(int? prevMlId, int materialId)
        {
            if (prevMlId == null)
                return 0;
            var prevItem = GetItemByMaterialId(prevMlId.Value, materialId);
            if (prevItem == null)
                return 0;
            return prevItem.Quantity;
        }
    }
}
