using System;
using System.Text.RegularExpressions;

namespace Yorishige {
    public struct Temperature : IFormattable {



		#region "member variables"
		private decimal _Value;
		#endregion

		#region "Kelvin"
		/// <summary>
		/// Kelvin scale value
		/// </summary>
		/// <returns></returns>
		public decimal Kelvin {
			get {
				return _Value;
			}
			set {
				_Value = value;
			}
		}
		#endregion

		#region "Celsius"
		private const decimal T0 = 273.15m;

		public decimal Celsius {
			get {
				return Kelvin - T0;
			}
			set {
				Kelvin = value + T0;
			}
		}
		#endregion

		#region "Fahrenheit"
		private const decimal F0 = 459.67m;

		public decimal Fahrenheit {
            get {
				return Rankine - F0;
			}
            set {
				Rankine = value + F0;
			}
        }
		#endregion

		#region "Rankine"
		private const decimal Ra = 9m;
		private const decimal Rb = 5m;

		public decimal Rankine {
            get{
				return Kelvin * Ra / Rb;
			}
			set {
				Kelvin = value * Rb / Ra;
			}
		}


		public override string ToString() {
            return Kelvin.ToString();
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			var m = Regex.Match(format, "^([KCFR])\\d+$");
            if (m.Success) {
				var value = Kelvin;
				switch (m.Groups[1].Value) {
                    case "C": value = Celsius;      break;
				    case "F": value = Fahrenheit;   break;
				    case "R": value = Rankine;      break;
				}
				return value.ToString("G" + m.Value.Substring(1), formatProvider);
			}
            return Kelvin.ToString(format, formatProvider);
		}
		#endregion
	}
}