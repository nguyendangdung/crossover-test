using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Pager<T>(this IOrderedQueryable<T> queryable, int page, int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException();
            }
            if (page < 0)
            {
                throw new ArgumentException();
            }

            return queryable.Skip((page - 1)*size).Take(size);
        }
    }
}
