using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Patchwork.Collections;

namespace Patchwork.Utility {

	internal static class SeqHelper {
		public static bool RemoveWhere<T>(this ICollection<T> col, Func<T, bool> predicate) {
			var what = col.Where(predicate).ToList();
			var anyRemoved = false;
			foreach (var item in what) { anyRemoved |= col.Remove(item); }
			return anyRemoved;
		}

		public static T TryGet<TKey, T>(this IDictionary<TKey, T> dict, TKey key)
		where T : class {
			return dict.ContainsKey(key) ? dict[key] : null;
		}



		public static void ForEach<T>(this IEnumerable<T> seq, Action<T> act) {
			foreach (var item in seq) { act(item); }
		}

		public static int IndexOf<T>(this IEnumerable<T> seq, Func<T, bool> pred) {
			int i = 0;
			foreach (var x in seq) {
				if (pred(x)) {
					return i;
				}
				i++;
			}
			return -1;
		}
		
		public static bool EqualsAny<T>(this T what, params T[] args) {
			return args.Any(arg => what.Equals(arg));
		}

		public static void AddRange<T>(this ICollection<T> col, IEnumerable<T> seq) {
			seq.ForEach(col.Add);
		}

		public static bool ContainsAny(this string str, params string[] options) {
			return options.Any(str.Contains);
		}

		public static SimpleTypeLookup<T> ToSimpleTypeLookup<T>(this IEnumerable<IGrouping<Type, T>> groupings) {
			var lookup =  new SimpleTypeLookup<T>();
			lookup.AddRange(groupings);
			return lookup;
		}
	}
}