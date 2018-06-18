using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Yorishige {
    [StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct YearMonth : IFormattable, IEquatable<YearMonth>, IComparable<YearMonth> {

		#region "const. etc"
		private const string DEFAULT_FORMAT = "yyyy/MM";
		#endregion

		#region "member variables"
		private short _year;
		private byte _month;
		#endregion

		#region "constructors"
		public YearMonth(DateTime value) : this(value.Year, value.Month) {
		}
		public YearMonth(int year, int month) {
			_year = (short)year;
			_month = (byte)month;
		}
		#endregion

		#region "static properties/fields"
		public static readonly YearMonth MinValue = new YearMonth(DateTime.MinValue);
		public static readonly YearMonth MaxValue = new YearMonth(DateTime.MaxValue);

        public YearMonth ThisMonth {
			get {
				return new YearMonth(DateTime.Today);
			}
		}
		#endregion

        #region "properties"
        public DateTime Date {
            get {
				return this.ToDate();
			}
        }

        public int Year {
			get {
				return (int)_year;
			}
		}

        public int Month {
            get {
				return (int)_month;
			}
		}

        public int Days {
            get {
				return DateTime.DaysInMonth(this.Year, this.Month);
			}
        }
        #endregion

        #region "methods"
        public int ToInt32() {
			return this.Year * 100 + this.Month;
		}

        public DateTime ToDate() {
            return new DateTime(this.Year, this.Month, 1);
        }

		public override string ToString() {
			return this.ToString(DEFAULT_FORMAT);
		}
		public string ToString(string format) {
			return this.ToString(format, null);

		}
		public string ToString(string format, IFormatProvider formatProvider) {
			return this.ToDate().ToString(format, formatProvider);
		}


        public static bool TryParse(string s, out YearMonth result) {
			return TryParse(s,null, DateTimeStyles.None, out result);
		}
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out YearMonth result) {
			result = new YearMonth();
			try {
				DateTime _result;
				var ret = DateTime.TryParse(s, provider, styles, out _result);
                if (ret) result = new YearMonth(_result);
				return ret;
			}
            catch {
				return false;
			}
		}

		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles styles, out YearMonth result) {
			return TryParseExact(s, new string[] { format }, provider, styles, out result);
		}
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles styles, out YearMonth result) {
			result = new YearMonth();
			try {
				DateTime _result;
				var ret = DateTime.TryParseExact(s, formats, provider, styles, out _result);
				if (ret) result = new YearMonth(_result);
				return ret;
			}
            catch (Exception e) {
				return false;
			}
		}

        public static YearMonth Parse(string s) {
			return Parse(s, null);
		}
		public static YearMonth Parse(string s, IFormatProvider provider ) {
			return Parse(s, provider, DateTimeStyles.None);
		}
	    public static YearMonth Parse(string s, IFormatProvider provider, DateTimeStyles styles) {
            try {
				return new YearMonth(DateTime.Parse(s, provider, styles));
			}
            catch {
            }
			throw new FormatException();
		}
		public static YearMonth ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles styles) {
			return new YearMonth(DateTime.ParseExact(s, formats, provider, styles));
		}
		public static YearMonth ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles styles) {
			return new YearMonth(DateTime.ParseExact(s, format, provider, styles));
		}
		public static YearMonth ParseExact(string s, string format, IFormatProvider provider) {
			return new YearMonth(DateTime.ParseExact(s, format, provider));
		}


		public YearMonth Add(int value) {
			return this.AddMonths(value);
		}

        public YearMonth Subtract(int value) {
			return this.Add(-value);
		}

        public int Subtract(YearMonth value) {
			return this.MonthIndex() - value.MonthIndex();
		}

        public YearMonth AddMonths(int value) {
			return new YearMonth(this.Date.AddMonths(value));
		}

        public YearMonth AddYears(int value) {
			return new YearMonth(this.Date.AddYears(value));
		}


        public override int GetHashCode() {
			return this.MonthIndex().GetHashCode();
		}

        public override bool Equals(object obj) {
			return obj != null && obj is YearMonth ? this.Equals((YearMonth)obj) : false;
		}
		public bool Equals(YearMonth other) {
			return this._year == other._year && this._month == other._month;
		}

		public int CompareTo(YearMonth other) {
			return this.MonthIndex().CompareTo(other.MonthIndex());
		}
		#endregion

        #region "conversions"
        public static implicit operator YearMonth(DateTime value) {
			return new YearMonth(value);
		}
        public static explicit operator DateTime(YearMonth value) {
			return value.ToDate();
		}
        public static explicit operator int(YearMonth value) {
			return value.ToInt32();
		}
		#endregion

		#region "operators"
		public static bool operator <(YearMonth x, YearMonth y) {
			return x.CompareTo(y) < 0;
		}
		public static bool operator <=(YearMonth x, YearMonth y) {
			return x.CompareTo(y) <= 0;
		}
		public static bool operator ==(YearMonth x, YearMonth y) {
			return x.CompareTo(y) == 0;
		}
		public static bool operator !=(YearMonth x, YearMonth y) {
			return x.CompareTo(y) != 0;
		}
		public static bool operator >=(YearMonth x, YearMonth y) {
			return x.CompareTo(y) >= 0;
		}
		public static bool operator >(YearMonth x, YearMonth y) {
			return x.CompareTo(y) > 0;
		}

        public static YearMonth operator +(YearMonth x, int y) {
			return x.Add(y);
		}

		public static YearMonth operator -(YearMonth x, int y) {
			return x.Subtract(y);
		}
        public static int operator -(YearMonth x, YearMonth y) {
			return x.Subtract(y);
		}
		#endregion




		#region "privates"
		private int MonthIndex() {
			return this.Year * 12 + this.Month - 1;
		}
        #endregion
	}
}