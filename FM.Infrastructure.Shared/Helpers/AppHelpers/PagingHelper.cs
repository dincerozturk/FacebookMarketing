using System.Collections.Generic;
using System.Linq;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class PagingHelper<T>
    {
        public static List<T> GetPage(List<T> list, int pageNumber, int pageSize = 10)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}