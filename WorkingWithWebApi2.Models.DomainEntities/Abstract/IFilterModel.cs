using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WorkingWithWebApi2.Models.DomainEntities.Abstract
{
    public interface IFilterModel<TEntity>
    {
        List<Expression<Func<TEntity, bool>>> GenerateFilterList(string filterString);
    }
}
