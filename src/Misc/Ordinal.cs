using System;
using System.Collections.Generic;
using System.Linq;

namespace Yorishige {
    public struct Ordinal : 
        IEquatable<Ordinal>, 
        IComparable,
        IComparable<Ordinal> {

		#region "members"

		#region "private const"
		private const int _minValue = 1;
		private const int _maxValue = int.MaxValue;
		#endregion

		private int _value;
		#endregion

		#region "constructors"
		public Ordinal(int ordinal) {
			Validate(ordinal);
			_value = ordinal;
		}
		#endregion

		#region "properties"
		public static readonly Ordinal MinValue = new Ordinal(_minValue);
        public static readonly Ordinal MaxValue = new Ordinal(_maxValue);

		public static Ordinal First { get; } = new Ordinal(1);
		public static Ordinal Second { get; } = new Ordinal(2);
		public static Ordinal Third { get; } = new Ordinal(3);
		public static Ordinal Fourth { get; } = new Ordinal(4);
		public static Ordinal Fifth { get; } = new Ordinal(5);
		public static Ordinal Sixth { get; } = new Ordinal(6);
		public static Ordinal Seventh { get; } = new Ordinal(7);
		public static Ordinal Eighth { get; } = new Ordinal(8);
		public static Ordinal Ninth { get; } = new Ordinal(9);
		public static Ordinal Tenth { get; } = new Ordinal(10);
		public static Ordinal Eleventh { get; } = new Ordinal(11);
		public static Ordinal Twelfth { get; } = new Ordinal(12);
		public static Ordinal Thirteenth { get; } = new Ordinal(13);
		public static Ordinal Fourteenth { get; } = new Ordinal(14);
		public static Ordinal Fifteenth { get; } = new Ordinal(15);
		public static Ordinal Sixteenth { get; } = new Ordinal(16);
		public static Ordinal Seventeenth { get; } = new Ordinal(17);
		public static Ordinal Eighteenth { get; } = new Ordinal(18);
		public static Ordinal Nineteenth { get; } = new Ordinal(19);
		public static Ordinal Twentieth { get; } = new Ordinal(20);
		public static Ordinal Thirtieth { get; } = new Ordinal(30);
		public static Ordinal Fortieth { get; } = new Ordinal(40);
		public static Ordinal Fiftieth { get; } = new Ordinal(50);
		#endregion

		#region "methods"
		public int ToInt32() {
			return _value;
		}
		public override string ToString() {
			var s = _value.ToString();
			var suffix = "th";
			if (s.EndsWith("1") && !s.EndsWith("11")) suffix = "st";
			if (s.EndsWith("2") && !s.EndsWith("12")) suffix = "nd";
			if (s.EndsWith("3") && !s.EndsWith("13")) suffix = "rd";
			return s + suffix;
		}
		public override int GetHashCode() {
			return this.ToInt32().GetHashCode();
		}

		public override bool Equals(object obj) {
			if (!(obj is Ordinal)) return false;
			return this.Equals((Ordinal)obj);
		}
		public bool Equals(Ordinal other) {
			return this.ToInt32().Equals(other.ToInt32());
		}

        public int CompareTo(object obj) {
			if (!(obj is Ordinal)) throw new ArgumentException();
			return this.CompareTo((Ordinal)obj);
		}
        public int CompareTo(Ordinal other) {
			return this.ToInt32().CompareTo(other.ToInt32());
		}

        public Ordinal Add(int value) {
			return new Ordinal(this.ToInt32() + value);
		}
		#endregion

		#region "operators"

        #region "conversions"
        public static implicit operator Ordinal(int value) {
			return new Ordinal(value);
		}
        public static implicit operator int(Ordinal value) {
			return value.ToInt32();
		}
		#endregion

		#region "arithmetic"
		public static Ordinal operator +(Ordinal x, int y) {
			return x.Add(y);
		}
		public static Ordinal operator -(Ordinal x, int y) {
			return x.Add(-y);
		}
		public static int operator -(Ordinal x, Ordinal y) {
			return x.ToInt32() - y.ToInt32();
		}
		#endregion

		#region "relational"
		public static bool operator <(Ordinal x, Ordinal y) {
			return x.CompareTo(y) < 0;
		}
		public static bool operator <=(Ordinal x, Ordinal y) {
			return x.CompareTo(y) <= 0;
		}
		public static bool operator ==(Ordinal x, Ordinal y) {
			return x.CompareTo(y) == 0;
		}
		public static bool operator !=(Ordinal x, Ordinal y) {
			return x.CompareTo(y) != 0;
		}
		public static bool operator >=(Ordinal x, Ordinal y) {
			return x.CompareTo(y) >= 0;
		}
		public static bool operator >(Ordinal x, Ordinal y) {
			return x.CompareTo(y) > 0;
		}
		#endregion

		#endregion

		#region "privates"
		private static void Validate(int value) {
			if (value < _minValue || value > _maxValue) throw new OverflowException();
		}

		#endregion
	}

    public static class OrdinalExtensions {
		public static IEnumerable<T> SkipTo<T>(this IEnumerable<T> source, Ordinal ordinal) {
			return source.Skip(ordinal.ToInt32());
		}
        public static T ElementAt<T>(this IEnumerable<T> source, Ordinal ordinal) {
			return source.ElementAt(ordinal.ToInt32());
		}
		public static T ElementAtOrDefault<T>(this IEnumerable<T> source, Ordinal ordinal) {
			return source.ElementAtOrDefault(ordinal.ToInt32());
		}
	}
}