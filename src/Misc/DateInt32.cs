using System;
using System.Runtime.InteropServices;

namespace Yorishige {
	[StructLayout(LayoutKind.Sequential, Size = sizeof(int), Pack = sizeof(int))]
	public struct DateInt32 {
		#region "member variables"
		private int _Value;
		#endregion

		#region "constructors"
		public DateInt32(DateTime date) {
			_Value = DateToInt32(date);
		}
		public DateInt32(int year, int month, int day) : this(new DateTime(year, month, day)) {
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
				return _Value;
			}
            set {
                _Value = DateToInt32(Int32ToDate(value));
			}
        }

        public DateTime Date {
			get {
				return Int32ToDate(_Value);
			}
			set {
                _Value = DateToInt32(value);
			}
		}

		public int Year     { get { return YearOf(_Value); } }
		public int Month    { get { return MonthOf(_Value); } }
		public int Day      { get { return DayOf(_Value); } }

        public DayOfWeek DayOfWeek {get { return this.Date.DayOfWeek; } }


		#endregion




		#region "privates"
		private static int DateToInt32(DateTime value) {
			return value.Year * 10000 + value.Month * 100 + value.Day;
		}
		private static DateTime Int32ToDate(int value) {
			return new DateTime(YearOf(value), MonthOf(value), DayOf(value));
		}
		private static int YearOf(int value) {
			return value / 10000;
		}
        private static int MonthOf(int value) {
			return (value / 100) % 100;
		}
        private static int DayOf(int value) {
			return value % 100;
		}

		private static int GetCurrent() {
			return DateToInt32(DateTime.Today);
		}
        #endregion
	}
}