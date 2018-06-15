using System;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Yorishige {
	[StructLayout(LayoutKind.Sequential, Size = 1, Pack = 1)]
	public struct Month :
		IComparable,
		IComparable<Month>,
		IEquatable<Month>,
		IFormattable {

		#region "const/readonly static"
		public static readonly Month January = new Month(1);
		public static readonly Month February = new Month(2);
		public static readonly Month March = new Month(3);
		public static readonly Month April = new Month(4);
		public static readonly Month May = new Month(5);
		public static readonly Month June = new Month(6);
		public static readonly Month July = new Month(7);
		public static readonly Month August = new Month(8);
		public static readonly Month September = new Month(9);
		public static readonly Month October = new Month(10);
		public static readonly Month November = new Month(11);
		public static readonly Month December = new Month(12);
		#endregion

		#region "member variables"
		private byte _month;
		#endregion

		#region "constructors"
		private Month(byte month) {
			_month = month;
		}
		#endregion

		#region "properties"
		public int Days { get { return this.GetDays(DateTime.Today.Year); } }

		public Month Previous { get { return this.Add(-1); } }
		public Month Next { get { return this.Add(1); } }

		#endregion

		#region "methods"
		public int GetDays(int year) {
			return DateTime.DaysInMonth(year, _month);
		}

		public int ToInt32() {
			return (int)_month;
		}
		public Months ToMonths() {
			return (Months)_month;
		}
		public DateTime ToDate() {
			return new DateTime(DateTime.Today.Year, _month, 1);
		}

		public override bool Equals(object obj) {
			return obj != null && obj is Month ? this.Equals((Month)obj) : false;
		}
		public bool Equals(Month other) {
			return this.ToInt32().Equals(other.ToInt32());
		}

		public override int GetHashCode() {
			return _month.GetHashCode();
		}

		public int CompareTo(object obj) {
			if (obj != null && obj is Month) return this.CompareTo((Month)obj);
			throw new ArgumentException();
		}
		public int CompareTo(Month other) {
			return this.ToInt32().CompareTo(other.ToInt32());
		}

		public override string ToString() {
			return ToString("MMMM", null);
		}
		public string ToString(string format, IFormatProvider formatProvider) {
			switch (format) {
			case "M":
			case "MM":
				break;

			case "MMM":
			case "MMMM":
				if (formatProvider == null) formatProvider = new CultureInfo("en-US");
				break;
			}
			return this.ToDate().ToString("yyyy" + format, formatProvider).Substring(4);
		}

		public Month Add(int value) {
			return new Month(Normalize(this._month + value));
		}

		public int Subtract(Month month) {
			return Diff(this, month);
		}

		public static int Diff(Month x, Month y) {
			return (int)Normalize(x._month - y._month) % 12;
		}

		#endregion

		#region "operators"

		#region "conversions"
		public static implicit operator int(Month value) {
			return value.ToInt32();
		}
		public static implicit operator Months(Month value) {
			return value.ToMonths();
		}

		public static explicit operator Month(int value) {
			return new Month((byte)value);
		}
		#endregion

		#region "arithmetic"

        public static Month operator +(Month x, int y) {
			return x.Add(y);
		}
		public static Month operator -(Month x, int y) {
			return x.Add(-y);
		}
		public static int operator -(Month x, Month y) {
			return Diff(x, y);
		}

		public static Month operator ++(Month x) {
			return x.Next;
		}
        public static Month operator --(Month x) {
			return x.Previous;
		}
		#endregion

		#region "relational"
		public static bool operator <(Month x, Month y) {
			return x.CompareTo(y) < 0;
		}
		public static bool operator <=(Month x, Month y) {
			return x.CompareTo(y) <= 0;
		}
		public static bool operator ==(Month x, Month y) {
			return x.CompareTo(y) == 0;
		}
		public static bool operator !=(Month x, Month y) {
			return x.CompareTo(y) != 0;
		}
		public static bool operator >=(Month x, Month y) {
			return x.CompareTo(y) >= 0;
		}
		public static bool operator >(Month x, Month y) {
			return x.CompareTo(y) > 0;
		}
		#endregion

		#endregion




		#region "privates"
		private static byte Validate(int value) {
			if (value <= 0) throw new ArgumentException();
			return Normalize(value);
		}

		private static byte Normalize(int value) {
			value %= 12;
            if (value<=0) value += 12;
			return (byte)value;
		}
        #endregion
	}
}