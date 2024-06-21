using System;

namespace Extensionator {
    public static class MathExtension {

        public static readonly float PI = 3.141593f;
        public static readonly float DEG2RAD = 57.29578f;
        public static readonly float COMMON_EPSILON = 0.000001f;  // 1.0e-6
        public static readonly float PRECISE_EPSILON = 0.00000001f; // 1.0e-8
        public static readonly float E = 2.71828182845904523536f;
        public static readonly float Delta = 4.6692016091f;  // FEIGENBAUM CONSTANT
        public static readonly float FeigenbaumAlpha = 2.5029078750f;
        public static readonly float AperyConstant = 1.2020569031f;
        public static readonly float GoldenRatio = 1.6180339887f;
        public static readonly float EulerMascheroniConstant = 0.5772156649f;
        public static readonly float KhinchinsConstant = 2.6854520010f;
        public static readonly float GaussKuzminWirsingConstant = 0.3036630028f;
        public static readonly float BernsteinsConstant = 0.2801694990f;
        public static readonly float HafnerSarnakMcCurleyConstant = 0.3532363718f;
        public static readonly float MeisselMertensConstant = 0.2614972128f;
        public static readonly float GlaisherKinkelinConstant = 1.2824271291f;
        public static readonly float OmegaConstant = 0.5671432904f;
        public static readonly float GolombDickmanConstant = 0.6243299885f;
        public static readonly float CahensConstant = 0.6434105462f;
        public static readonly float TwinPrimeConstant = 0.6601618158f;
        public static readonly float LaplaceLimit = 0.6627434193f;
        public static readonly float LandauRamanujanConstant = 0.7642236535f;
        public static readonly float CatalansConstant = 0.9159655941f;
        public static readonly float ViswanathsConstant = 1.13198824f;
        public static readonly float ConwaysConstant = 1.3035772690f;
        public static readonly float MillsConstant = 1.3063778838f;
        public static readonly float PlasticConstant = 1.3247179572f;
        public static readonly float RamanujanSoldnerConstant = 1.4513692348f;
        public static readonly float BackhouseConstant = 1.4560749485f;
        public static readonly float PortersConstant = 1.4670780794f;
        public static readonly float LiebsSquareIceConstant = 1.5396007178f;
        public static readonly float ErdosBorweinConstant = 1.6066951524f;
        public static readonly float NivensConstant = 1.7052111401f;
        public static readonly float UniversalParabolicConstant = 2.2955871493f;
        public static readonly float SierpinskisConstant = 2.5849817595f;
        public static readonly float FransenRobinsonConstant = 2.807770f;

        public static bool IsZero(this int number) => number == 0;
        public static bool IsZero(this float number) => number == 0;
        public static bool IsNotZero(this int number) => number != 0;
        public static bool IsNotZero(this float number) => number != 0;
        public static bool IsGreaterThanZero(this int number) => number > 0;
        public static bool IsGreaterThanZero(this float number) => number > 0;
        public static bool IsBelowZero(this int number) => number < 0;
        public static bool IsBelowZero(this float number) => number < 0;
        public static bool IsNegative(this int number) => number.IsBelowZero();
        public static bool IsNegative(this float number) => number.IsBelowZero();
        public static bool IsEven(this int number) => number % 2 == 0;
        public static bool IsOdd(this int number) => number % 2 != 0;

        /// <summary>
        /// Normalizes an angle in radians to be between 0 and 2 * PI radians, assuming the angle is represented in radians.
        /// </summary>
        /// <param name="angle">The angle in radians to normalize.</param>
        /// <returns>The normalized angle between 0 and 2 * PI radians.</returns>
        public static double NormalizeRadiansAngle(this int angle) => (angle % Math.Tau + Math.Tau) % Math.Tau;
        /// <summary>
        /// Normalizes an angle in radians to be between 0 and 2 * PI radians, assuming the angle is represented in radians.
        /// </summary>
        /// <param name="angle">The angle in radians to normalize.</param>
        /// <returns>The normalized angle between 0 and 2 * PI radians.</returns>
        public static double NormalizeRadiansAngle(this float angle) => (angle % Math.Tau + Math.Tau) % Math.Tau;
        /// <summary>
        /// Normalizes an angle in radians to be between 0 and 2 * PI radians, assuming the angle is represented in radians.
        /// </summary>
        /// <param name="angle">The angle in radians to normalize.</param>
        /// <returns>The normalized angle between 0 and 2 * PI radians.</returns>
        public static double NormalizeRadiansAngle(this double angle) => (angle % Math.Tau + Math.Tau) % Math.Tau;

        /// <summary>
        /// Checks if an integer value falls between a specified minimum and maximum range.
        /// </summary>
        /// <param name="value">The integer value to check.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <param name="inclusive">Optional flag indicating whether the range includes the minimum and maximum values (default: true).</param>
        /// <returns>True if the value is between min and max (inclusive or exclusive based on the flag), False otherwise.</returns>
        public static bool IsBetween(this int value, int min, int max, bool inclusive = true) {
            int minValue = Math.Min(min, max);
            int maxValue = Math.Max(min, max);

            return inclusive ? value >= minValue && value <= maxValue : value > minValue && value < maxValue;
        }

        /// <summary>
        /// Checks if a float value falls between a specified minimum and maximum range, considering a small precision offset.
        /// </summary>
        /// <param name="value">The float value to check.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <param name="inclusive">Optional flag indicating whether the range includes the minimum and maximum values (default: true).</param>
        /// <param name="precision">Optional precision value to account for floating-point rounding errors (default: 0.00001f).</param>
        /// <returns>True if the value is between min and max (inclusive or exclusive based on the flag), False otherwise.</returns>
        public static bool IsBetween(this float value, float min, float max, bool inclusive = true, float precision = 0.00001f) {
            float minValue = Math.Min(min, max) - precision;
            float maxValue = Math.Max(min, max) + precision;

            return inclusive ? value >= minValue && value <= maxValue : value > minValue && value < maxValue;
        }

        /// <summary>
        /// Formats an integer value by adding a thousand separator (e.g., comma) for readability.
        /// </summary>
        /// <param name="value">The integer value to format.</param>
        /// <param name="separator">Optional character to use as the thousand separator (default: comma).</param>
        /// <returns>The formatted string with thousand separators.</returns>
        public static string ThousandSeparator(this int value, string separator = ",") {
            bool wasNegative = value.IsNegative();
            value = Math.Abs(value);

            string numberAsText = value.ToString();
            float mod = numberAsText.Length % 3;
            string result = string.Empty;

            foreach (int index in Enumerable.Range(0, numberAsText.Length)) {
                if (index != 0 && index % 3 == mod)
                    result += separator;

                result += numberAsText[index];
            }

            return wasNegative ? "-" + result : result;
        }


    }
}
