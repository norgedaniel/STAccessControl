using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCA_ServiceLayer
{
    /// <summary>
    /// Class to supply generic extension methods for our classes work with.
    /// Like the method to make the paging.
    /// </summary>
    public static class GenericList
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> list, int pageNumZeroStart, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize cannot be zero.");

            if (pageNumZeroStart != 0)
                // skip to the correct number of pages
                list = list.Skip(pageNumZeroStart * pageSize);

            // takes the number for this page size.
            return list.Take(pageSize);

        }
    }
}
