using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Linq;

namespace Yorishige {
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size= 1)]
	public struct Bit : IEquatable<Bit>, IComparable, IComparable<Bit>, IFormattable, IConvertible {
		#region "internal decl"
		private enum Bits : byte {
            Off = 0,
            On = 1
		}
		#endregion

		#region "members"
		private Bits _value;
		#endregion

        #region "constructors"
        private Bit(Bits value) {
			_value = value;
		}
        public Bit(bool value):this(value ? Bits.On : Bits.Off) {
        }
        public Bit(int value) {
            try {
				_value = new Bits[] { Bits.On, Bits.Off }.First(x => (int)x == value);
			}
            catch {
				throw new OverflowException();
			}
        }
		#endregion

		#region "properties"
		public static readonly Bit Off = new Bit(Bits.Off);
		public static readonly Bit On = new Bit(Bits.On);
		#endregion

		#region "methods"

		#region "conversions"
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ToInt32() {
			return (int)_value;
		}
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ToBoolean() {
			return _value == Bits.On;
		}

		#region "IConvertible implements"
		TypeCode IConvertible.GetTypeCode() { return TypeCode.Byte; }
		bool IConvertible.ToBoolean(IFormatProvider provider) { return ((IConvertible)_value).ToBoolean(provider); }
		byte IConvertible.ToByte(IFormatProvider provider) {return ((IConvertible)_value).ToByte(provider); }
		char IConvertible.ToChar(IFormatProvider provider) {return ((IConvertible)_value).ToChar(provider); }
		DateTime IConvertible.ToDateTime(IFormatProvider provider) {return ((IConvertible)_value).ToDateTime(provider); }
		decimal IConvertible.ToDecimal(IFormatProvider provider) {return ((IConvertible)_value).ToDecimal(provider); }
		double IConvertible.ToDouble(IFormatProvider provider) {return ((IConvertible)_value).ToDouble(provider); }
		short IConvertible.ToInt16(IFormatProvider provider) {return ((IConvertible)_value).ToInt16(provider); }
		int IConvertible.ToInt32(IFormatProvider provider) {return ((IConvertible)_value).ToInt32(provider); }
		long IConvertible.ToInt64(IFormatProvider provider) {return ((IConvertible)_value).ToInt64(provider); }
		sbyte IConvertible.ToSByte(IFormatProvider provider) {return ((IConvertible)_value).ToSByte(provider); }
		float IConvertible.ToSingle(IFormatProvider provider) {return ((IConvertible)_value).ToSingle(provider); }
		string IConvertible.ToString(IFormatProvider provider) {return ((IConvertible)_value).ToString(provider); }
		object IConvertible.ToType(Type conversionType, IFormatProvider provider) {return ((IConvertible)_value).ToType(conversionType, provider); }
		ushort IConvertible.ToUInt16(IFormatProvider provider) {return ((IConvertible)_value).ToUInt16(provider); }
		uint IConvertible.ToUInt32(IFormatProvider provider) {return ((IConvertible)_value).ToUInt32(provider); }
		ulong IConvertible.ToUInt64(IFormatProvider provider) {return ((IConvertible)_value).ToUInt64(provider); }
		#endregion

		#endregion

		#region "formatting"
		public override string ToString() {
			return ToString(null, null);
		}
		public string ToString(string format) {
			return ToString(format, null);
		}
		public string ToString(string format, IFormatProvider formatProvider) {
			IFormattable target = string.IsNullOrEmpty(format) || StartsWithAny(format, "G", "F", "D", "X")
				? (IFormattable)_value
				: (IFormattable)this.ToInt32();
			return target.ToString(format, formatProvider);
		}
		#endregion

		#endregion

		#region "comparison"
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode() {
			return _value.GetHashCode();
		}

		public override bool Equals(object obj) {
			return obj != null && obj is Bit ? this.Equals((Bit)obj) : false;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Bit other) {
			return _value.Equals(other._value);
		}

		public int CompareTo(object obj) {
			if (obj == null || !(obj is Bit)) throw new ArgumentException();
			return this.CompareTo((Bit)obj);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CompareTo(Bit other) {
			return this.ToInt32().CompareTo(other.ToInt32());
		}
		#endregion

		#region "operators"

		#region "conversion"
		public static implicit operator int(Bit x) => x.ToInt32();
		public static implicit operator bool(Bit x) => x.ToBoolean();
		#endregion

		#region "arithmetics"
		public static int operator +(Bit x, Bit y) => x.ToInt32() + y.ToInt32();
		public static int operator +(int x, Bit y) => x + y.ToInt32();
		public static int operator +(Bit x, int y) => x.ToInt32() + y;

		public static int operator -(Bit x, Bit y) => x.ToInt32() - y.ToInt32();
		public static int operator -(int x, Bit y) => x - y.ToInt32();
		public static int operator -(Bit x, int y) => x.ToInt32() - y;

		public static int operator *(Bit x, Bit y) => x.ToInt32() * y.ToInt32();
		public static int operator *(int x, Bit y) => x * y.ToInt32();
		public static int operator *(Bit x, int y) => x.ToInt32() * y;

		public static int operator /(Bit x, Bit y) => x.ToInt32() / y.ToInt32();
		public static int operator /(int x, Bit y) => x / y.ToInt32();
		public static int operator /(Bit x, int y) => x.ToInt32() / y;

		public static int operator %(Bit x, Bit y) => x.ToInt32() % y.ToInt32();
		public static int operator %(int x, Bit y) => x % y.ToInt32();
		public static int operator %(Bit x, int y) => x.ToInt32() % y;

		public static Bit operator ++(Bit x) => new Bit(x.ToInt32() + 1);
		public static Bit operator --(Bit x) => new Bit(x.ToInt32() - 1);
		#endregion

		#region "bitwise/logical"
		public static Bit operator ~(Bit x) => new Bit(!x.ToBoolean());

		public static Bit operator &(Bit x, Bit y) => new Bit(x.ToBoolean() & y.ToBoolean());
		public static Bit operator |(Bit x, Bit y) => new Bit(x.ToBoolean() | y.ToBoolean());
		public static Bit operator ^(Bit x, Bit y) => new Bit(x.ToBoolean() ^ y.ToBoolean());

		public static int operator <<(Bit x, int y) => x.ToInt32() << y;
		public static int operator >>(Bit x, int y) => x.ToInt32() >> y;

		public static Bit operator !(Bit x) => ~x;
		public static bool operator true(Bit x) => x.ToBoolean();
		public static bool operator false(Bit x) => !x.ToBoolean();
		#endregion

		#region "relational"
		public static bool operator ==(Bit x, Bit y) => x.Equals(y);
		public static bool operator !=(Bit x, Bit y) => !x.Equals(y);

		public static bool operator <(Bit x, Bit y) => x.CompareTo(y) < 0;
		public static bool operator <=(Bit x, Bit y) => x.CompareTo(y) <= 0;
		public static bool operator >=(Bit x, Bit y) => x.CompareTo(y) >= 0;
		public static bool operator >(Bit x, Bit y) => x.CompareTo(y) > 0;
		#endregion

		#endregion


        #region "privates/utility"
        private bool StartsWithAny(string s, params string[] values) {
			return values.Any(x => s.StartsWith(x) || s.StartsWith(x.ToLower()));
		}
        #endregion
	}
    public static class BitExtensions {
    }
}