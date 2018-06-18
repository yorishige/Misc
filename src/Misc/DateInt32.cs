using System;
using System.Runtime.InteropServices;

namespace Yorishige {
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DateInt32 {
		#region "member variables"
		private short _year;
		private byte _month;
		private byte _day;
		#endregion

		#region "constructors"
		public DateInt32(DateTime date) : this(date.Year, date.Month, date.Day) {
		}
		public DateInt32(int year, int month, int day) {
			_year = _month = _day = 0;
			SetElements(year, month, day);
		}
		#endregion

		#region "properties"

        #region "static"
		public static readonly DateInt32 MinValue = new DateInt32(DateTime.MinValue);
		public static readonly DateInt32 MaxValue = new DateInt32(DateTime.MaxValue);

        public static DateInt32 Today { get { return new DateInt32(DateTime.Today); } }
        #endregion

        public int Value {
            get {
				return JoinElements(this.Year, this.Month, this.Day);
			}
            set {
				var e = SplitInt32ToElements(value);
				SetElements(e[0], e[1], e[2]);
			}
        }

        public DateTime Date {
			get {
				return new DateTime(this.Year, this.Month, this.Day);
			}
			set {
				SetElements(value.Year, value.Month, value.Day);
			}
		}

		public int Year     { get { return (int)_year; } }
		public int Month    { get { return (int)_month; } }
		public int Day      { get { return (int)_day; } }

        public DayOfWeek DayOfWeek {get { return this.Date.DayOfWeek; } }
		#endregion

		#region "privates"
		private void SetElements(int year, int month, int day) {
			_year = (short)year;
			_month = (byte)month;
			_day = (byte)day;
		}
		private static int[] SplitInt32ToElements(int value) {
			var e = new int[3];
			e[0] = Math.DivRem(Math.DivRem(value, 100, out e[2]), 100, out e[1]);
			return e;
		}
		private static int JoinElements(int year, int month, int day) {
			return year * 10000 + month * 100 + day;
		}


        #endregion
	}
}