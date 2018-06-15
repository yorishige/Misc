using System;
using System.Collections.Generic;
using System.Linq;

namespace Yorishige {
	public static class Misc {


		#region "Permutations, Combinations"

		public static IEnumerable<T[]> Permutations<T>(IEnumerable<T> values, int count) {
			if (count < 1) {
				throw new ArgumentException();
			}
			else if (count == 1) {
                foreach(var value in values) yield return new[] { value };
			}
			else {
				var i = 0;
				foreach (var first in values) {
					foreach (var remaining in Permutations(values.Take(i).Concat(values.Skip(i + 1)), count - 1)) {
						yield return new[] { first }.Concat(remaining).ToArray();
					}
					i++;
				}
			}
		}

		public static IEnumerable<T[]> Combinations<T>(IEnumerable<T> values, int count) {
			if (count < 1) {
				throw new ArgumentException();
			}
			else if (count == 1) {
				foreach (var value in values) yield return new[] { value };
			}
			else {
				var i = 0;
				foreach (var first in values) {
					i++;
					foreach (var remaining in Permutations(values.Skip(i), count - 1)) {
						yield return new[] { first }.Concat(remaining).ToArray();
					}
				}
			}
		}

		#endregion


		#region "enum"

		public static IEnumerable<T> GetEnumValues<T>() where T : struct {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		#endregion
	}
}
