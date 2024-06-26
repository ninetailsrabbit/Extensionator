﻿using System.Diagnostics.Contracts;

namespace Extensionator {
    public static class ListExtension {
        private static readonly Random _rng = new(Guid.NewGuid().GetHashCode());


        /// <summary>
        /// Creates an enumeration that assigns indexes to each item in the original sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="sequence">The sequence to be enumerated with indexes.</param>
        /// <returns>An IEnumerable of tuples containing the item and its corresponding index.</returns>
        /// <remarks>
        /// This method leverages the `Select` method from LINQ. It iterates through the original sequence (`sequence`)
        /// and for each item (`item`), it creates a tuple containing the item itself and its current index within the sequence.
        /// The resulting IEnumerable contains these tuples, allowing you to access both the item and its position during iteration.
        /// </remarks>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> sequence)
            => sequence.Select((item, index) => (item, index));

        public static List<T> Diff<T>(this IList<T> sequence, IList<T> otherSequence) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sequence));
            ArgumentException.ThrowIfNullOrEmpty(nameof(otherSequence));

            return sequence.Except(otherSequence).ToList();
        }

        /// <summary>
        /// Removes and returns the element at the specified index from the list.
        /// </summary>
        /// <param name="sequence">The list to remove the element from.</param>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentException">Thrown if the sequence is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is less than 0 or greater than the last index of the sequence.</exception>
        /// <returns>The element that was removed at the specified index.</returns>
        public static T PopAt<T>(this IList<T> sequence, int index) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sequence), "Cannot pop at in an empty collection");
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, sequence.LastIndex());

            var result = sequence[index];
            sequence.RemoveAt(index);

            return result;
        }

        /// <summary>
        /// Removes and returns the first element from the list.
        /// </summary>
        /// <param name="sequence">The list to remove the element from.</param>
        /// <exception cref="ArgumentException">Thrown if the sequence is null or empty.</exception>
        /// <returns>The first element of the list.</returns>
        public static T PopFront<T>(this IList<T> sequence) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sequence), "Cannot pop front an empty collection");

            var result = sequence.First();
            sequence.RemoveAt(0);

            return result;
        }

        /// <summary>
        /// Removes and returns the last element from the list.
        /// </summary>
        /// <param name="sequence">The list to remove the element from.</param>
        /// <exception cref="ArgumentException">Thrown if the sequence is null or empty.</exception>
        /// <returns>The last element of the list.</returns>
        public static T PopBack<T>(this IList<T> sequence) {
            ArgumentException.ThrowIfNullOrEmpty(nameof(sequence), "Cannot pop back an empty collection");

            var result = sequence[^1];
            sequence.RemoveAt(sequence.LastIndex());

            return result;
        }


        /// <summary>
        /// Gets the zero-based index of the last element in a sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="sequence">The sequence to get the last index from.</param>
        /// <returns>The zero-based index of the last element in the sequence, or -1 if the sequence is empty.</returns>
        /// <remarks>
        /// This extension method leverages the `Count()` method to efficiently determine the last index. 
        /// It assumes zero-based indexing, meaning the first element has an index of 0.
        /// </remarks>
        public static int LastIndex<T>(this IEnumerable<T> sequence) => sequence.Count() - 1;

        /// <summary>
        /// Gets the middle element of an IList if it has at least 3 elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the IList.</typeparam>
        /// <param name="sequence">The IList to find the middle element of.</param>
        /// <returns>The middle element of the sequence if it has at least 3 elements, throws an ArgumentException otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if the sequence has less than 3 elements.</exception>
        public static T MiddleElement<T>(this IList<T> sequence) {
            if (sequence.Count >= 3) {
                int middleIndex = (sequence.Count - 1) / 2;

                return sequence[middleIndex];
            }

            throw new ArgumentException("ListExtension:MiddleElement -> Sequence must contain at least 3 elements.");
        }

        /// <summary>
        /// Filters an iterable of strings to include only those that are not null, empty, or whitespace.
        /// </summary>
        /// <param name="sequence">The sequence of strings to be filtered.</param>
        /// <returns>An iterator containing only elements that are not null, empty, or whitespace.</returns>
        /// <remarks>
        /// This extension method utilizes the `Where` clause on the provided `sequence` of strings.
        /// It employs a lambda expression (`item => !string.IsNullOrWhiteSpace(item)`) as the filtering condition.
        /// The `string.IsNullOrWhiteSpace` method checks if a string is null, empty, or consists only of whitespace characters.
        /// By negating the result (`!`), the filter includes only elements that are not null, empty, or whitespace.
        /// This approach offers a concise way to filter out unwanted strings from a sequence while leveraging LINQ for efficient iteration.
        /// </remarks>
        public static IEnumerable<string> WhereNotEmpty(this IEnumerable<string> sequence)
            => sequence.Where(item => !string.IsNullOrWhiteSpace(item));

        /// <summary>
        /// Removes duplicate elements from an IEnumerable sequence and returns a new list containing the unique elements.
        /// </summary>
        /// <param name="sequence">The IEnumerable sequence to remove duplicates from.</param>
        /// <returns>A new List containing the unique elements from the input sequence.</returns>
        public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> sequence) => sequence.Distinct();

        /// <summary>
        /// Filters out null elements from an IEnumerable sequence and returns a new sequence containing only non-null elements.
        /// </summary>
        /// <param name="sequence">The IEnumerable sequence to remove null elements from.</param>
        /// <returns>A new IEnumerable sequence containing only non-null elements from the input sequence.</returns>
        /// <typeparam name="T">The type of elements in the sequence (assumed to be nullable).</typeparam>
        /// <exception cref="InvalidOperationException">Throws if the source sequence is null.</exception>
        public static IEnumerable<T> RemoveNullables<T>(this IEnumerable<T> sequence) => sequence.Where(x => x is not null);

        /// <summary>
        /// Checks if an IEnumerable sequence is empty (contains no elements).
        /// </summary>
        /// <param name="sequence">The IEnumerable sequence to check for emptiness.</param>
        /// <returns>True if the sequence contains no elements, False otherwise.</returns>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if the input sequence is null.</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> sequence) {
            ArgumentNullException.ThrowIfNull(sequence);

            return !sequence.Any();
        }

        /// <summary>
        /// Shuffles the elements of the specified IList in random order using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the IList.</typeparam>
        /// <param name="sequence">The IList to be shuffled.</param>
        public static void Shuffle<T>(this IList<T> sequence) {
            int n = sequence.Count;

            while (n > 1) {
                n--;
                int k = _rng.Next(n + 1);
                (sequence[n], sequence[k]) = (sequence[k], sequence[n]);
            }
        }

        /// <summary>
        /// Calculates the average (arithmetic mean) of a sequence of integer values.
        /// </summary>
        /// <param name="numbers">The sequence of integer numbers.</param>
        /// <returns>The average value of the numbers, or 0 if the sequence is empty.</returns>
        public static int Average(this IEnumerable<int> numbers) {
            if (numbers.IsEmpty())
                return 0;

            return numbers.Aggregate(0, (accum, current) => accum + current) / numbers.Count();
        }

        /// <summary>
        /// Calculates the average (arithmetic mean) of a sequence of integer values.
        /// </summary>
        /// <param name="numbers">The sequence of integer numbers.</param>
        /// <returns>The average value of the numbers, or 0 if the sequence is empty.</returns>
        public static int Mean(this IEnumerable<int> numbers) => Average(numbers);

        /// <summary>
        /// Calculates the average (arithmetic mean) of a sequence of floating-point numbers.
        /// </summary>
        /// <param name="numbers">The sequence of floating-point numbers.</param>
        /// <returns>The average value of the numbers, or 0 if the sequence is empty.</returns>
        public static float Mean(this IEnumerable<float> numbers) => Average(numbers);

        /// <summary>
        /// Calculates the average (arithmetic mean) of a sequence of floating-point values.
        /// </summary>
        /// <param name="numbers">The sequence of floating-point numbers.</param>
        /// <returns>The average value of the numbers, or 0.0f if the sequence is empty.</returns>
        public static float Average(this IEnumerable<float> numbers) {
            if (numbers.IsEmpty())
                return 0.0f;

            return numbers.Aggregate(0.0f, (accum, current) => accum + current) / numbers.Count();
        }

        /// <summary>
        /// Creates a deep copy of a provided IEnumerable{T} collection, where T implements the ICloneable interface.
        /// </summary>
        /// <typeparam name="T">The type of elements in the IEnumerable{T} collection, which must implement ICloneable.</typeparam>
        /// <param name="sequence">The IEnumerable{T} collection to clone.</param>
        /// <returns>A new IEnumerable{T} collection containing deep copies of the original elements.</returns>
        /// <remarks>
        /// This extension method provides a generic approach to cloning an IEnumerable{T} collection. It assumes that the elements within the collection implement the `ICloneable` interface. 
        /// 
        /// The method leverages LINQ's `Select` to iterate through each item in the original sequence. For each item, it calls the `Clone` method (assumed to be implemented by `T`) to create a deep copy. The resulting copies are then cast back to type `T` and added to a new List object using `ToList`.
        /// 
        /// This approach ensures a new, independent collection is created with deep copies of the original elements. Modifications to the cloned collection won't affect the original data.
        /// </remarks>
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> sequence) where T : ICloneable
            => sequence.Select(item => (T)item.Clone());


        /// <summary>
        /// Creates a deep copy of a provided IList{T} collection, where T implements the ICloneable interface.
        /// </summary>
        /// <typeparam name="T">The type of elements in the IList{T} collection, which must implement ICloneable.</typeparam>
        /// <param name="sequence">The IList{T} collection to clone.</param>
        /// <returns>A new IList{T} collection containing deep copies of the original elements.</returns>
        /// <remarks>
        /// This extension method provides a generic approach to cloning an IList{T} collection. It assumes that the elements within the collection implement the `ICloneable` interface. 
        /// 
        /// The method leverages LINQ's `Select` to iterate through each item in the original sequence. For each item, it calls the `Clone` method (assumed to be implemented by `T`) to create a deep copy. The resulting copies are then cast back to type `T` and added to a new List object using `ToList`.
        /// 
        /// This approach ensures a new, independent collection is created with deep copies of the original elements. Modifications to the cloned collection won't affect the original data.
        /// </remarks>
        public static IList<T> Clone<T>(this IList<T> sequence) where T : ICloneable => [.. sequence.Clone()];


        /// <summary>
        /// Performs a recursive flattening operation on an IEnumerable collection.
        /// </summary>
        /// <param name="enumerable">The source IEnumerable collection to flatten. Can contain any object types.</param>
        /// <returns>An IEnumerable sequence containing elements from the flattened structure.</returns>
        /// <remarks>
        /// This function iterates through the elements of the provided `enumerable` collection. 
        /// If an element is itself an IEnumerable (like a list or another nested collection), it recursively calls `Flatten` on that element to further flatten its contents. 
        /// Otherwise, the element is directly yielded back to the caller. 
        /// 
        /// This process continues until all nested collections are flattened, resulting in a single sequence of elements from the original structure.
        /// </remarks>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> enumerable) {
            foreach (T element in enumerable) {
                if (element is IEnumerable<T> candidate) {
                    foreach (T nested in Flatten<T>(candidate)) {
                        yield return nested;
                    }
                }
                else {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Performs a recursive `SelectMany` operation on a sequence, applying a selector function that also returns sequences.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence to start the recursive selection.</param>
        /// <param name="selector">A function that selects a sequence of elements for each element in the source.</param>
        /// <returns>A flattened sequence containing all elements from the source and nested sequences selected by the selector.</returns>
        /// <exception cref="ArgumentNullException">Throws if source or selector is null.</exception>
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector) {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return source.IsEmpty() ? source :
                source.Concat(
                    source
                    .SelectMany(i => selector(i).EmptyIfNull())
                    .SelectManyRecursive(selector)
                );
        }

        /// <summary>
        /// Creates a dictionary by pairing elements from two enumerables with the first element from the source enumerable as the key and the corresponding element from the target enumerable as the value.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the source enumerable (must not be null).</typeparam>
        /// <typeparam name="TValue">The type of the values in the target enumerable (must not be null).</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="target">The target enumerable.</param>
        /// <returns>A dictionary where keys come from the source enumerable and values come from the target enumerable.</returns>
        /// <exception cref="ArgumentNullException">Thrown if either source or target enumerable is null.</exception>
        public static Dictionary<TKey, TValue> ConvertToDictionaryWith<TKey, TValue>(this IEnumerable<TKey> source, IEnumerable<TValue> target) where TKey : notnull where TValue : notnull
            => source.Zip(target, (key, value) => new { key, value }).ToDictionary(item => item.key, item => item.value);

        /// <summary>
        /// Returns an empty sequence if the source sequence is null, otherwise returns the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence to check for null.</param>
        /// <returns>An empty sequence if source is null, otherwise the source sequence.</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source) => source ?? [];

        /// <summary>
        /// Selects a random element from the provided sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="sequence">The IEnumerable collection to select from.</param>
        /// <returns>A random element from the sequence.</returns>
        /// <remarks>
        /// This function utilizes an extension method to simplify random element selection. It delegates the actual logic to the `RandomElementUsing` function, passing a newly created `Random` instance for internal use.
        /// </remarks>
        public static T RandomElement<T>(this IEnumerable<T> sequence) => sequence.RandomElementUsing<T>(_rng);
        /// <summary>
        /// Selects a random element from the provided sequence using a specified Random instance.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="sequence">The IEnumerable collection to select from.</param>
        /// <param name="rand">The Random instance to use for generating randomness (optional, new Random() used by default in RandomElement).</param>
        /// <returns>A random element from the sequence.</returns>

        public static T RandomElementUsing<T>(this IEnumerable<T> sequence, Random rand) {
            int index = rand.Next(0, sequence.Count());
            return sequence.ElementAt(index);
        }

        /// <summary>
        /// Selects a specified number of random elements from the provided array.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="sequence">The T[] array to select from.</param>
        /// <param name="number">The number of random elements to retrieve.</param>
        /// <returns>An IEnumerable collection containing the selected random elements.</returns>
        /// <remarks>
        /// This extension method provides a way to get multiple random elements from an array. It delegates the work to the `RandomElementsUsing` function, passing a newly created `Random` instance for internal use.
        /// </remarks>
        public static IEnumerable<T> RandomElements<T>(this T[] sequence, int number) => sequence.RandomElementsUsing<T>(number, _rng);

        /// <summary>
        /// Selects a specified number of random elements from the provided array using a specified Random instance.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="sequence">The T[] array to select from.</param>
        /// <param name="number">The number of random elements to retrieve.</param>
        /// <param name="rand">The Random instance to use for generating randomness (optional, new Random() used by default in RandomElements).</param>
        /// <returns>An IEnumerable collection containing the selected random elements.</returns>
        public static IEnumerable<T> RandomElementsUsing<T>(this T[] sequence, int number, Random rand) {
            number = Math.Min(number, sequence.Length);

            return Enumerable
                    .Range(0, number)
                    .Select(x => rand.Next(0, 1 + sequence.Length - number))
                    .OrderBy(x => x)
                    .Select((x, i) => sequence[x + i]);
        }

        /// <summary>
        /// Counts the frequency of a specific element within a sequence, excluding null values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="sequence">The sequence to search (iterable).</param>
        /// <param name="target">The element to count occurrences of.</param>
        /// <returns>The number of times the target element appears in the sequence, excluding null values.</returns>
        /// <remarks>
        /// This method first filters out any null values from the sequence using `RemoveNullables` (assumed to be an extension method).
        /// Then, it uses `Where` to filter the remaining elements where they are equal to the target element.
        /// Finally, it counts the number of elements in the filtered sequence using `Count`, providing the frequency of the target element.
        /// </remarks>
        public static int FrequencyOf<T>(this IEnumerable<T> sequence, T target)
            => sequence.RemoveNullables().Where(element => element.Equals(target)).Count();

        public static List<List<T>> ChunkBy<T>(this IEnumerable<T> sequence, int chunkSize = 1) {
            chunkSize = Math.Max(1, chunkSize);

            return sequence
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        /// <summary>
        /// Converts a single item into a one-element IEnumerable sequence.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The single item to be converted into a sequence.</param>
        /// <returns>An IEnumerable sequence containing the provided item.</returns>
        public static IEnumerable<T> Only<T>(this T item) {
            yield return item;
        }

        /// <summary>
        /// Converts all strings in a sequence to lowercase.
        /// </summary>
        /// <param name="sequence">The sequence of strings to be converted.</param>
        /// <returns>A new IEnumerable containing the lowercase versions of the original strings.</returns>
        /// <remarks>
        /// This extension method iterates through each string in the provided `enumerable` sequence.
        /// For each string, it utilizes the `ToLower` method to convert it to lowercase.
        /// The `yield return` keyword is used to efficiently return each converted string
        /// without materializing the entire resulting sequence at once.
        /// This approach improves memory usage when dealing with large datasets.
        /// The original `enumerable` object remains unmodified.
        /// </remarks>
        [Pure]
        public static IEnumerable<string> ToLower(this IEnumerable<string> sequence) {
            foreach (string str in sequence)
                yield return str.ToLower();
        }

        /// <summary>
        /// Converts all strings in a sequence to uppercase.
        /// </summary>
        /// <param name="sequence">The sequence of strings to be converted.</param>
        /// <returns>A new IEnumerable containing the uppercase versions of the original strings.</returns>
        /// <remarks>
        /// This extension method behaves similarly to `ToLower`. It iterates through each string
        /// in the `enumerable` sequence and utilizes the `ToUpper` method for conversion.
        /// The `yield return` keyword ensures efficient memory usage during the iteration.
        /// A new sequence containing the uppercase strings is returned, leaving the original `enumerable` intact.
        /// </remarks>
        [Pure]
        public static IEnumerable<string> ToUpper(this IEnumerable<string> sequence) {
            foreach (string str in sequence)
                yield return str.ToUpper();
        }

        /// <summary>
        /// Checks if the specified sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="sequence">The sequence to check.</param>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence) => sequence == null || !sequence.Any();


    }
}
