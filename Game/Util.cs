using System;
using System.Collections.Generic;

namespace Game
{
    internal static class Util
    {
        internal static void RemoveIntersections<T>(
            IList<T> list,
            ICollection<T> remove,
            Action<T> onRemoved)
        {
            for (var index = list.Count - 1; index >= 0; index--)
            {
                var item = list[index];

                if (remove.Contains(item))
                {
                    list.RemoveAt(index);

                    onRemoved?.Invoke(item);
                }
            }
        }
    }
}