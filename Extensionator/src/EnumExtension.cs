namespace Extensionator {
    public static class EnumExtension {

        /// <summary>
        /// Gets a random value from the specified enum type. 
        /// Example: EnumExtension.RandomEnum&lt;Input.MouseModeEnum&gt;()
        /// </summary>
        /// <typeparam name="T">The enum type to get a random value from.</typeparam>
        /// <returns>A random enum value of type T.</returns>
        public static T RandomEnum<T>() {
            T[] values = (T[])Enum.GetValues(typeof(T));

            int randomIndex = new Random().Next(values.Length);

            return values[randomIndex];
        }

        /// <summary>
        /// Gets a random value from an enum type.
        /// Example: Input.MouseModeEnum.Visible.Random()
        /// </summary>
        /// <typeparam name="T">The enum type to get a random value from.</typeparam>
        /// <returns>A random enum value of type T.</returns>
        /// <exception cref="ArgumentException">Throws an ArgumentException if T is not a struct or an Enum</exception>
        public static T RandomEnum<T>(this T _) where T : struct, Enum {
            T[] values = Enum.GetValues<T>();

            int randomIndex = new Random().Next(values.Length);

            return values[randomIndex];
        }

        /// <summary>
        /// Attempts to convert the specified string value to an enum of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the enum to convert to. Must be a struct.</typeparam>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The enum value if the conversion is successful; throws an exception otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if the string value does not represent a valid enum value of type T.</exception>
        public static T ToEnum<T>(this string value) where T : struct => (T)Enum.Parse(typeof(T), value);

        /// <summary>
        /// Attempts to convert the specified string value to an enum of the given type. 
        /// If conversion fails, returns a provided fallback value.
        /// </summary>
        /// <typeparam name="T">The type of the enum to convert to. Must be a struct.</typeparam>
        /// <param name="value">The string value to convert.</param>
        /// <param name="fallback">The default value to return if the conversion fails.</param>
        /// <returns>The enum value if the conversion is successful; otherwise, the provided fallback value.</returns>
        public static T ToEnumOrDefault<T>(this string value, T fallback) where T : struct
            => Enum.TryParse<T>(value, out var result) ? result : fallback;
    }
}
