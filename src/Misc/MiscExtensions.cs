using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;

namespace Yorishige {
    public static class MiscExtensions {

        #region "Null Safe"

		public static T NullSafe<T>(this T value) where T : class, new() {
			return value ?? new T();
		}

        public static string NullSafe(this string value) {
			return value ?? string.Empty;
		}

		#endregion


		#region "Comments"

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Comment<T>(this T value, string comment) {
			return value;
		}

		#endregion


        #region "Linq extensions"

        public static T Nth<T>(this IEnumerable<T> values, int n) {
			return values.Skip(n - 1).First();
		}
        public static T NthOrDefault<T>(this IEnumerable<T> values, int n) {
			return values.Skip(n - 1).FirstOrDefault();
		}

        public static T Second<T>(this IEnumerable<T> values) {
			return Nth(values, 2);
		}
        public static T SecondOrDefault<T>(this IEnumerable<T> values) {
			return NthOrDefault(values, 2);
		}

        public static T Third<T> (this IEnumerable<T> values) {
			return Nth(values, 3);
		}
		public static T ThirdOrDefault<T>(this IEnumerable<T> values) {
			return NthOrDefault(values, 3);
		}

		public static IEnumerable<T> Loop<T>(this IEnumerable<T> values) {
			for (; ; ) {
                foreach(var value in values) {
					yield return value;
				}
            }
		}

		public static IEnumerable<T> RepeatWhile<T>(this IEnumerable<T> values, Func<long, long, bool> predicate) {
			var done = false;
			for (long i = 0, n = 0; ; i++) {
				foreach (var value in values) {
					done = !predicate(i, n);
					if (done) break;
					yield return value;
					n++;
				}
				if (done) break;
			}
		}

		public static IEnumerable<T> Repeat<T>(this IEnumerable<T> values, long times) {
			return RepeatWhile(values, (i, n) => i < times);
		}

		#endregion
	}

}