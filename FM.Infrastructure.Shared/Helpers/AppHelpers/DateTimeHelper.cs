using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class DateTimeHelper
    {
        public static List<DateTime> CreateListDate(DateTime? startDate, DateTime? endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).GetValueOrDefault().Days + 1).Select(d => startDate.GetValueOrDefault().AddDays(d)).ToList();
        }
    }
}