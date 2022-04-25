using System.Linq.Expressions;
using Ardalis.Specification;

namespace BattleRoom.Application.Specifications;

public class PagingSpec<T> : Specification<T>
{
    public PagingSpec(int offset, int count, Expression<Func<T, object>> orderBySelector)
    {
        Query.Skip(offset).Take(count).OrderBy(orderBySelector);
    }
}