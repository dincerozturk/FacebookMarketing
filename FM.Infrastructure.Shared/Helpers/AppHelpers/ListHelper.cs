using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class ListHelper<T>
    {

        public static List<List<T>> SplitList(List<T> locations, int nSize = 20)
        {

            var list = new List<List<T>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }
            return list;
        }

        public static List<List<T>> SplitListV2(List<T> listInput, int nSize = 5)
        {
            List<T>[] arr = new List<T>[nSize];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new List<T>();
            }
            for (int i = 0; i < listInput.Count; i++)
            {
                int j = i % nSize;
                arr[j].Add(listInput[i]);
            }
            var result = arr.ToList();
            return result;
        }

        public static T GetRandomItemInListObject(List<T> list)
        {
            List<T> newList = new List<T>();
            var shuffled = list.OrderBy(x => Guid.NewGuid()).ToList();
            Random rd = new Random();
            int index = rd.Next(shuffled.Count);
            return shuffled[index];
        }
    }
}
