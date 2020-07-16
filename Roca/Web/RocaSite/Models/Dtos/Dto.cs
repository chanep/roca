using System.Collections.Generic;
using Omu.ValueInjecter;

namespace Cno.Roca.Web.RocaSite.Models.Dtos
{
    public abstract class Dto<TEntity, TDto> where TDto : new() where TEntity : new()
    {
        protected Dto()
        {
        }

        protected Dto(TEntity entity)
        {
            this.InjectFrom<FlatLoopValueInjection>(entity);
        }

        public static IList<TDto> CreateList(IEnumerable<TEntity> entities)
        {
            var list = new List<TDto>();
            foreach (var e in entities)
            {
                var dto = new TDto();
                dto.InjectFrom<FlatLoopValueInjection>(e);
                list.Add(dto);
            }
            return list;
        }

        public virtual TEntity GetEntity()
        {
            var entity = CreateEntity();
            entity.InjectFrom(this);
            return entity;
        }

        public virtual TEntity GetEntityDeep()
        {
            var entity = CreateEntity();
            entity.InjectFrom<UnflatLoopValueInjection>(this);
            return entity;
        }

        protected virtual TEntity CreateEntity()
        {
            return new TEntity();
        }
    }
}