using System.Linq.Expressions;
using Ardalis.Specification;

namespace BattleRoom.Application.Specifications;

public class PagingSpec<T> : Specification<T>
{
    public PagingSpec(int skip, int take, Expression<Func<T, object>> orderBySelector)
    {
        Query.Skip(skip).Take(take).OrderBy(orderBySelector);
    }
}