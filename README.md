# Extensionator

![license](https://badgen.net/static/License/MIT/yellow)
![csharp](https://img.shields.io/badge/C%23-239120?style//for-the-badge&logo//c-sharp&logoColor//white)

Speed up your common software development tasks with Extensionator which adds a number of functionalities to the base c# classes and types.

---

<p align="center">
<img alt="Extensionator" src="Extensionator/icon.png" width="200">
</p>

---

- [Extensionator](#extensionator)
  - [Installation via CLI](#installation-via-cli)
  - [Usage](#usage)
  - [Helpers](#helpers)
    - [Singleton](#singleton)
  - [Extensions](#extensions)
    - [Bool](#bool)
    - [DateTime](#datetime)
    - [Dictionary](#dictionary)
    - [Enums](#enums)
    - [Types](#types)
    - [Enumerable](#enumerable)

### Installation via CLI

Further information can be found on the [official microsoft documentation](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-nuget-cli)

```sh
nuget install Ninetailsrabbit.Extensionator

# Or choosing version

nuget install Ninetailsrabbit.Extensionator -Version 0.2.0

# Using dotnet
dotnet add package Ninetailsrabbit.Extensionator --version 0.2.0
```

## Usage

Since these functions are C# extension methods, they become available directly on the types they extend. You only need to call them on instances of those types in your code, simplifying the syntax.

## Helpers

Here live classes or data structures that abstracts the logic for ease of use.

### Singleton

Easily convert any class to a singleton using `SingletonBase` class

```csharp

public sealed class Preloader : SingletonBase<Preloader> {

   public readonly CompressedTexture2D OrangeSelectedTileTexture = GD.Load<CompressedTexture2D>("res://scenes/components/cells/assets/1.png");

    //..
 }


// Access the instance properties & methods with

Preloader.Instance.OrangeSelectedTileTexture
```

## Extensions

### Bool

```csharp

//Shorcut to toggle a boolean value
true.Toggle(); // False

// Convert the boolean value into a sign integer
true.ToSign(); // 1
false.ToSign(); // -1

// Conver the boolean into a number representation
true.ToInt(); // 1
false.ToInt(); // 0

```

### DateTime

```csharp
// AddWeeks to a specific date
DateTime.Now.AddWeeks(2);

// Calculate a human age from the year of selected datetime
var birthdate = new DateTime(1990, 10, 05);
birthdate.Age(); // 34

// Convert datetime based on TimeZoneInfo
DateTime specificDate = new DateTime(2023, 06, 23, 12, 0, 0, DateTimeKind.Utc); // Coordinated Universal Time (UTC)

// Convert to Pacific Standard Time (PST)
TimeZoneInfo pacificTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
DateTime convertedToPST = specificDate.To(pacificTimeZone); // Output will be in PST format (e.g., 2023-06-22 17:00:00)

- - - - - -

// Converts a DateTime object to the number of seconds since a specific epoch (default: 1970-01-01 UTC)
DateTime specificDate = new DateTime(2023, 06, 23, 12, 0, 0, DateTimeKind.Utc);

// Using default epoch (1970-01-01 UTC)
int secondsSinceEpoch = specificDate.To(); // Approximate output: 1656098400 (might differ slightly due to rounding)

// Specifying a custom epoch
DateTime customEpoch = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
int secondsSinceCustomEpoch = specificDate.To(customEpoch);
// This will result in a different integer value based on the custom epoch

- - - - - -

//Converts an integer representing the number of seconds since an epoch (default: 1970-01-01 UTC) to a DateTime object
nt unixTimestamp = 1656098400; // Example timestamp in seconds since 1970-01-01 UTC

// Using default epoch (1970-01-01 UTC)
DateTime convertedDateTime = unixTimestamp.To(); // Output: 2023-06-23 00:00:00 UTC

// Specifying a custom epoch
DateTime customEpoch = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
DateTime convertedFromCustomEpoch = unixTimestamp.To(customEpoch); // This will result in a different DateTime based on the custom epoch

- - - - - -

// Calculates the UTC offset of a DateTime object in hours
double utcOffset = DateTime.Now.UTCOffset();

// Calculates the time difference between two DateTime objects.

DateTime startTime = new DateTime(2023, 06, 23, 10, 0, 0);
DateTime endTime = new DateTime(2023, 06, 23, 12, 0, 0);

TimeSpan timeDifference = endTime.Diff(startTime);
timeDifference.TotalHours; // // Output: 02:00:00 (2 hours)

// Calculates the elapsed time between the current local date and time and the provided DateTime value
DateTime somePastTime = new DateTime(2023, 06, 20, 10, 0, 0);
TimeSpan elapsedTime = somePastTime.Elapsed();

//  Calculates the elapsed time between the current Coordinated Universal Time (UTC) and the provided DateTime value.
DateTime somePastTime = new DateTime(2023, 06, 20, 10, 0, 0);
TimeSpan elapsedTimeUTC = somePastTime.ElapsedUTC();

// Truncate datetime values
DateTime.Now.TruncateToMilliseconds();
DateTime.Now.TruncateToSeconds();
DateTime.Now.TruncateToMinutes();
DateTime.Now.TruncateToHours();
DateTime.Now.TruncateToDays();
```

### Dictionary

```csharp
// Add range of key-values to a dictionary
Dictionary<string, int> people = new() { { "almudena", 30 }, { "maximiliano", 45 }, { "eusebio", 30 } };

people.AddRange(new Dictionary<string, int>() { { "lolo", 25 }, { "lola", 50 } });

// Add or update a key-value pair
people.AddOrUpdate("eusebio", 30); // Added
people.AddOrUpdate("maximiliano", 55); // Updated

// Detect if dictionary contains at least one key from enumerable
people.ContainsAnyKey(["almudena", "superman", "power ranger"])); // true

// Detect if dictionary contains all keys from enumerable
people.ContainsAllKeys(["almudena", "eusebio"]); // true
```

### Enums

```csharp
public enum STATES {
    PATROL,
    IDLE,
    ATTACK,
    ESCAPE,
    HIDE
}
// Get random enum value using static method
EnumExtension.RandomEnum<STATES>();

// Get random enum value from internal value
STATES.PATROL.RandomEnum();

// Convert a string into a enum value if exists or throws exception

"patRol".toEnum(); // throws ArgumentException

"ATTACK".ToEnum(); // STATES.ATTACK

// Convert a string into enum value or fallback to default value
"SHOOT".ToEnumOrDefault(STATES.IDLE)); // STATES.IDLE
```

### Types

```csharp
//Use In to detect values on enumerables or collection of parameters

List<Point> points = [new() { X = 1, Y = 2 }, new() { X = 3, Y = 4 }, new() { X = 5, Y = 6 }];

Point targetPoint = new() { X = 3, Y = 4 };
Point notIncludedPoint = new() { X = 3, Y = 40 };

targetPoint.In(points); // true
notIncludedPoint.In(points); // false

3.In([1, 2, 3, 4]; // true
30.In([1, 2, 3, 4]; // false

// Using parameters syntax
targetPoint.In(points[0], points[1], points[2]); // true

3.In(1, 2, 3, 4); // true
30.In(1, 2, 3, 4); // false
```

### Enumerable

```csharp
// Get the items as index-value pair
 var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };

foreach ((string name, int index) in people.WithIndex()) {
    Assert.Contains(name, people);
    Assert.Equal(name, people[index]);
}

// Or
 people.WithIndex().Select(element => //.. use element.index and element.item
 )

// Get the difference elements from source sequence using a second sequence
var people = new List<string>() { "john", "esmeralda", "dragon", "powerlifter" };

var otherPeople = new List<string>() { "john", "esmeralda", "fox", "crossfitero" };

var diff = people.Diff(otherPeople);
// ["dragon", "powerlifter"]


// Pop first element from list and remove it
var item = people.PopFront(); // "john"
item = people.PopFront(); // "esmeralda"

// Pop last element from list and remove it
var item = people.PopBack(); // "powerlifter"
item = people.PopBack(); // "dragon"

// Pop element from selected index
var item = people.PopAt(1); // "esmeralda"
item = people.PopAt(1); // "dragon"


// Get last index from an enumerable or - 1 if it's empty
int[] numbers = [16, 12, 33, 54, 155];
int[] empty = [];

numbers.LastIndex(); // 4
empty.LastIndex(); // -1

// Get the middle element from a sequence of at least three elements or throws ArgumentException
int[] numbers = [1, 2, 3, 4, 5];
int[] numbers2 = [1, 2, 4, 5];
int[] numbers3 = [1, 2];

numbers.MiddleElement(); // 3
numbers2.MiddleElement(); // 2
numbers3.MiddleElement(); // throws ArgumentException


// Get non-empty element from string enumerable
string[] names = ["zeus", "", "thor", "loki", "", ""];

names.WhereNotEmpty(); // ["zeus", "thor", "loki"]

// Transform all string elements into lower case
string[] names = ["ZeUS", "THOR", "LOKI", "atENEA", "GAIa"];

 names.ToLower(); // ["zeus", "thor", "loki", "atenea", "gaia"];


// Transform all string elements into upper case
string[] names = ["zeus", "thor", "loki", "atenea", "gaia"];

names.ToUpper(); // ["ZEUS", "THOR", "LOKI", "ATENEA", "GAIA"]


// Remove duplicates or nullables from an enumerable
[1, 1, 3, 4, 4, 4, 5].RemoveDuplicates(); // [1,3,4,5]
["enabled", null, "disabled", null].RemoveNullables(); // ["enabled", "disabled"]


// Shuffle the elements in a sequence modifying the original
[1, 2, 3, 4, 5].Shuffle(); // [3,2,5,1,4]


// Get the mean or average from a number sequence (int or floats). Alternative syntax Mean or Average can be used
[1, 2, 3, 4, 5].Mean(); // 3
[3.14f, 2.72f, 1.6f, 4.5f, 9.2f].Average(); // 4.23199987f

// Get random element/s from an enumerable
string[] names = ["zeus", "thor", "loki", "atenea", "gaia"];

names.RandomElement(); // "thor"
names.RandomElementUsing(new Random(42)); // "atenea"

neames.RandomElements(2); // "loki", "gaia"
neames.RandomElementsUsing(2, new Random(42)); // "zeus", "gaia"

// Detect the frequency of an element in the list
 int[] numbers = [1, 1, 3, 6, 6, 6, 6, 8, 9];

 numbers.FrequencyOf(1); // 2
 numbers.FrequencyOf(3); // 1
 numbers.FrequencyOf(6); // 4

// Divide elements from an enumerable into chunks
var sequence = new List<string> { "a", "b", "c", "d", "e", "f" };

var chunks = sequence.ChunkBy(2); // [ ["a", "b"], ["c", "d"], ["e", "f"]]

var sequence2 = new List<int> { 1, 2, 3, 4, 5 }
var chunks2 = sequence2.ChunkBy(2); // [ [1, 2], [3, 4], [5] ]

// Shorcut to create a list with one item
"hello".Only(); // ["hello"]
```
