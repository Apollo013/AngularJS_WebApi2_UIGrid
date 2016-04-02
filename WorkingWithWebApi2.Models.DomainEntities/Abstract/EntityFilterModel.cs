using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithWebApi2.Models.DomainEntities.Abstract
{
    public class EntityFilterModel<TEntity>
    {
        public static List<Expression<Func<TEntity, bool>>> GenerateFilterList(string filterString) { return null; }
    }
}
