using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

using Yorishige;

namespace Tests {
	public class MiscTest {
        #region "GetEnumValues"
		[Fact]
		public void GetEnumValuesTest() {
			var exp = Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>();
			var act = Misc.GetEnumValues<DayOfWeek>();

			Assert.NotNull(act);
			Assert.Equal(exp, act);
		}
		#endregion

		#region "Permutations"
		[Fact]
		public void PermutationsTest1() {
			for (var i = 1; i < 10;i++){
				var values = Enumerable.Range(0, i);
				var exp = values.Select((x) => new[] { x });
				var act = Misc.Permutations(values, 1);

				Assert.NotNull(act);
				Assert.Equal(exp, act);
			}
        }
       
        #endregion
	}
}
