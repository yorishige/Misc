using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

using Yorishige;

namespace Tests {
	public class MiscExtensionsTest {
        #region "Null safe"
        [Fact]
        public void NullSafeTest1() {
        }
		#endregion

		#region "Comment"
		[Fact]
		public void CommentTest() {
			var exp1 = 1;       			Assert.Equal(exp1, exp1.Comment("int is OK."));
			var exp2 = 1.0;     			Assert.Equal(exp2, exp2.Comment("double is OK."));
			var exp3 = "3";     			Assert.Equal(exp3, exp3.Comment("string is OK."));
			var exp4 = new DateTime();		Assert.Equal(exp4, exp4.Comment("DateTime is OK."));
			var exp5 = DayOfWeek.Sunday;	Assert.Equal(exp5, exp5.Comment("DayOfWeek(enum) is OK."));
			var exp6 = new int[] { 1, 2 }; 	Assert.Equal(exp6, exp6.Comment("Array is OK."));
			var exp7 = new Exception();		Assert.Equal(exp7, exp7.Comment("Exception(class) is OK."));
		}
		#endregion
	}
}