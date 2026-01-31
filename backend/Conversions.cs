namespace unit_converter;
/// Conversion factors based on Google's unit converter
/// Reference: https://support.google.com/websearch/answer/3284611
/// Precision capped at 4 significant figures for approximations
/// 

// TODO: Add explicit base case + throw ArgumentException for invalid enums
// Currently falls through to `_` which assumes meters (implicit)



/// <summary>
/// Handles unit conversions using canonical form pattern.
/// Each length measurement unit converts through
/// a base unit: Meters.  
/// This reduces conversion complexity from O(n²) to O(n).
/// </summary>
public class LengthConversion
{
    public enum LengthUnits
    {
        Milimeters,
        Centimeters,
        Meters,
        Kilometers,
        Inches,
        Feet,
        Yards,
        Miles
    };

    /// <summary>
    /// Returns the unit symbol (e.g. "mm", "cm", etc.) for a given LengthUnits enum.
    /// </summary>
    /// <param name="unit">The LengthUnits enum to get the unit symbol for.</param>
    /// <returns>The unit symbol for the given LengthUnits enum.</returns>
    /// <exception cref="ArgumentException">Thrown if the unit string is not recognized.</exception>
    public static string GetUnitSymbol(LengthUnits unit)
    {
        string unitSymbol = unit switch
        {
            LengthUnits.Milimeters => "mm",
            LengthUnits.Centimeters => "cm",
            LengthUnits.Meters => "m",
            LengthUnits.Kilometers => "km",
            LengthUnits.Inches => "in",
            LengthUnits.Feet => "ft",
            LengthUnits.Yards => "yd",
            LengthUnits.Miles => "mi",
            _ => throw new ArgumentException($"Unsupported unit: {unit}")
        };
        return unitSymbol;
    }

    /// <summary>
    /// Returns all units in the LengthUnits enum as a string array, lower case.
    /// </summary>
    /// <returns>A string array of all units in the LengthUnits enum, lower case.</returns>
    /// 
    public static string[] GetAllUnits()
    {
        return Enum.GetNames(typeof(LengthUnits))
                   .Select(name => name.ToLower())
                   .ToArray();
    }

    /// <summary>
    /// Returns a dictionary mapping each LengthUnits enum value to its respective unit symbol.
    /// The keys in the dictionary are the LengthUnits enum values in lower case as strings.
    /// The values in the dictionary are the corresponding symbols for each length unit as strings.
    /// </summary>
    /// 
    public static Dictionary<string, string> GetAllUnitSymbols()
    {
        return Enum.GetValues<LengthUnits>()
                   .ToDictionary(
                       unit => unit.ToString().ToLower(),
                       unit => GetUnitSymbol(unit)
                   );
    }
    /// <summary>
    /// Parses a unit string into a LengthUnits enum (Case-Insensitive).
    /// If the unit string is not recognized, an ArgumentException is thrown.
    /// </summary>
    /// <param name="unitString">The unit string to parse. Not case-sensitive.</param>
    /// <returns>The parsed LengthUnits enum.</returns>
    /// <exception cref="ArgumentException">Thrown if the unit string is not recognized.</exception>
    /// 
    public static LengthUnits ParseUnit(string unitString)
    {
        LengthUnits parsedUnit = unitString.ToLower() switch
        {
            "milimeters" => LengthUnits.Milimeters,
            "centimeters" => LengthUnits.Centimeters,
            "meters" => LengthUnits.Meters,
            "kilometers" => LengthUnits.Kilometers,
            "inches" => LengthUnits.Inches,
            "feet" => LengthUnits.Feet,
            "yards" => LengthUnits.Yards,
            "miles" => LengthUnits.Miles,
            _ => throw new ArgumentException($"Unsupported unit: {unitString}")
        };
        return parsedUnit;
    }

    /// <summary>
    /// Converts a length measurement from a given unit to another unit.
    /// The result is rounded to 4 significant figures.
    /// </summary>
    /// <param name="len">The length measurement to convert.</param>
    /// <param name="oldUnits">The unit to convert from.</param>
    /// <param name="newUnits">The unit to convert to.</param>
    /// <returns>The converted length measurement in the new unit, rounded to 4 significant figures.</returns>
    public static double ConvertLength(double len, LengthUnits oldUnits, LengthUnits newUnits)
    {
        double convertedLen = newUnits switch
        {
            LengthUnits.Milimeters => ConvertLengthHelper(len, oldUnits) * 1000,
            LengthUnits.Centimeters => ConvertLengthHelper(len, oldUnits) * 100,
            LengthUnits.Kilometers => ConvertLengthHelper(len, oldUnits) * 0.001,
            LengthUnits.Inches => ConvertLengthHelper(len, oldUnits) * 39.3701,
            LengthUnits.Yards => ConvertLengthHelper(len, oldUnits) * 1.094,
            LengthUnits.Miles => ConvertLengthHelper(len, oldUnits) * 0.0006214,
            _ => ConvertLengthHelper(len, oldUnits),
            // LengthUnits.Meters => ConvertLengthHelper(len, oldUnit),
        };
        return Math.Round(convertedLen, 4);
    }



    /// <summary>
    /// Converts a length measurement from a given unit to meters.
    /// </summary>
    /// <param name="len">The length measurement to convert.</param>
    /// <param name="units">The unit to convert from.</param>
    /// <returns>The converted length measurement in meters.</returns>
    /// 
    private static double ConvertLengthHelper(double len, LengthUnits units)
    {
        double convertedLen = units switch
        {
            LengthUnits.Milimeters => len * 0.001,
            LengthUnits.Centimeters => len * 0.01,
            LengthUnits.Kilometers => len * 1000,
            LengthUnits.Inches => len * 0.0254,
            LengthUnits.Feet => len * 0.3048,
            LengthUnits.Yards => len * 0.9144,
            LengthUnits.Miles => len * 1609.34,
            // LengthUnits.Meters => len,
            _ => len
        };

        return convertedLen;
    }
}










/// <summary>
/// Handles unit conversions using canonical form pattern.
/// Each weight measurement unit converts through a
/// base unit: Grams.  
/// This reduces conversion complexity from O(n²) to O(n).
/// </summary>
public class WeightConversion
{
    public enum WeightUnits
    {
        Miligrams,
        Grams,
        Kilograms,
        Ounces,
        Pounds
    };

    /// <summary>
    /// Returns the unit symbol (e.g. "mg", "g", etc.) for a given WeightUnits enum.
    /// </summary>
    /// <param name="unit">The WeightUnits enum to get the unit symbol for.</param>
    /// <returns>The unit symbol for the given WeightUnits enum.</returns>
    /// <exception cref="ArgumentException">Thrown if the unit string is not recognized.</exception>
    public static string GetUnitSymbol(WeightUnits unit)
    {
        string unitSymbol = unit switch
        {
            WeightUnits.Miligrams => "mg",
            WeightUnits.Grams => "g",
            WeightUnits.Kilograms => "kg",
            WeightUnits.Ounces => "oz",
            WeightUnits.Pounds => "lb",
            _ => throw new ArgumentException($"Unsupported unit: {unit}")
        };
        return unitSymbol;
    }


    /// <summary>
    /// Returns all units in the WeightUnits enum as a string array, lower case.
    /// </summary>
    /// /// <returns>A string array of all units in the WeightUnits enum, lower case.</returns>
    /// 
    public static string[] GetAllUnits()
    {
        return Enum.GetNames(typeof(WeightUnits))
                   .Select(name => name.ToLower())
                   .ToArray();
    }
    /// <summary>
    /// Returns a dictionary mapping each WeightUnits enum value to its respective unit symbol.
    /// The keys in the dictionary are the WeightUnits enum values in lower case as strings.
    /// The values in the dictionary are the corresponding symbols for each weight unit as strings.
    /// </summary>
    /// /// 
    public static Dictionary<string, string> GetAllUnitSymbols()
    {
        return Enum.GetValues<WeightUnits>()
                   .ToDictionary(
                       unit => unit.ToString().ToLower(),
                       unit => GetUnitSymbol(unit)
                   );
    }

    /// <summary>
    /// Parses a unit string into a WeightUnits enum (Case-Insensitive).
    /// If the unit string is not recognized, an ArgumentException is thrown.
    /// </summary>
    /// <param name="unitString">The unit string to parse. Not case-sensitive.</param>
    /// <returns>The parsed WeightUnits enum.</returns>
    /// <exception cref="ArgumentException">thrown if the unit string is not recognized.</exception>
    /// 
    public static WeightUnits ParseUnit(string unitString)
    {
        WeightUnits parsedUnit = unitString.ToLower() switch
        {
            "miligrams" => WeightUnits.Miligrams,
            "grams" => WeightUnits.Grams,
            "kilograms" => WeightUnits.Kilograms,
            "ounces" => WeightUnits.Ounces,
            "pounds" => WeightUnits.Pounds,
            _ => throw new ArgumentException($"Unsupported unit: {unitString}")
        };
        return parsedUnit;
    }

    /// <summary>
    /// Converts a weight measurement from a given unit to another unit.
    /// The result is rounded to 4 significant figures.
    /// </summary>
    /// <param name="w">The weight measurement to convert.</param>
    /// <param name="oldUnits">The unit to convert from.</param>
    /// <param name="newUnits">The unit to convert to.</param>
    /// <returns>The converted weight measurement in the new unit, rounded to 4 significant figures.</returns>
    /// 
    public static double ConvertWeight(double w, WeightUnits oldUnits, WeightUnits newUnits)
    {
        double convertedWeight = newUnits switch
        {
            WeightUnits.Miligrams => ConvertWeightHelper(w, oldUnits) * 1000,
            WeightUnits.Kilograms => ConvertWeightHelper(w, oldUnits) * 0.001,
            WeightUnits.Ounces => ConvertWeightHelper(w, oldUnits) * 0.03527,
            WeightUnits.Pounds => ConvertWeightHelper(w, oldUnits) * 0.002205,
            _ => ConvertWeightHelper(w, oldUnits)
        };
        return Math.Round(convertedWeight, 4);
    }

    /// <summary>
    /// Converts a weight measurement from a given unit to grams.
    /// </summary>
    /// <param name="w">The weight measurement to convert.</param>
    /// <param name="unit">The unit to convert from.</param>
    /// <returns>The converted weight measurement in grams.</returns>
    private static double ConvertWeightHelper(double w, WeightUnits unit)
    {
        double convertedWeight = unit switch
        {
            WeightUnits.Miligrams => w * 0.001,
            WeightUnits.Kilograms => w * 1000,
            WeightUnits.Ounces => w * 28.3495,
            WeightUnits.Pounds => w * 453.592,
            _ => w,
        };
        return convertedWeight;
    }

}










/// <summary>
/// Handles unit conversions using canonical form pattern.
/// Each temperature measurement unit converts through a
/// base unit: Celsius.  
/// This reduces conversion complexity from O(n²) to O(n).
/// </summary>
public class TemperatureConversion
{
    public enum TemperatureUnits
    {
        Celsius,
        Fahrenheit,
        Kelvin
    };

    /// <summary>
    /// Returns the unit symbol (e.g. '°C', '°F', '°K') for a given TemperatureUnits enum.
    /// </summary>
    /// <param name="unit">The TemperatureUnits enum to get the unit symbol for.</param>
    /// <returns>The unit symbol for the given TemperatureUnits enum.</returns>
    /// <exception cref="ArgumentException">Thrown if the unit string is not recognized.</exception>
    public static string GetUnitSymbol(TemperatureUnits unit)
    {
        string unitSymbol = unit switch
        {
            TemperatureUnits.Celsius => "°C",
            TemperatureUnits.Fahrenheit => "°F",
            TemperatureUnits.Kelvin => "K",
            _ => throw new ArgumentException($"Unsupported argument: {unit}")
        };
        return unitSymbol;
    }

    /// <summary>
    /// Returns all units in the TemperatureUnits enum as a string array, lower case.
    /// </summary>
    /// <returns>A string array of all units in the TemperatureUnits enum, lower case.</returns>
    /// 
    /// 
    /// /// 
    public static string[] GetAllUnits()
    {
        return Enum.GetNames(typeof(TemperatureUnits))
                   .Select(name => name.ToLower())
                   .ToArray();
    }

    /// <summary>
    /// Returns a dictionary mapping each TemperatureUnits enum value to its respective unit symbol.
    /// The keys in the dictionary are the TemperatureUnits enum values in lower case as strings.
    /// The values in the dictionary are the corresponding symbols for each temperature unit as strings.
    /// </summary>
    /// /// 
    public static Dictionary<string, string> GetAllUnitSymbols()
    {
        return Enum.GetValues<TemperatureUnits>()
                   .ToDictionary(
                       unit => unit.ToString().ToLower(),
                       unit => GetUnitSymbol(unit)
                   );
    }
    /// <summary>
    /// Parses a unit string into a TemperatureUnits enum (Case-Insensitive).
    /// If the unit string is not recognized, an ArgumentException is thrown.
    /// </summary>
    /// <param name="unitString">The unit string to parse. Not case-sensitive</param>
    /// <returns>The parsed TemperatureUnits enum.</returns>
    /// <exception cref="ArgumentException">Thrown if the unit string is not recognized.</exception>
    public static TemperatureUnits ParseUnit(string unitString)
    {
        TemperatureUnits parsedUnit = unitString.ToLower() switch
        {
            "celsius" => TemperatureUnits.Celsius,
            "fahrenheit" => TemperatureUnits.Fahrenheit,
            "kelvin" => TemperatureUnits.Kelvin,
            _ => throw new ArgumentException($"Unsupported argument: {unitString}")
        };

        return parsedUnit;
    }

    /// <summary>
    /// Converts a temperature measurement from a given unit to another unit.
    /// The result is rounded to 4 significant figures.
    /// </summary>
    /// <param name="temp">The temperature measurement to convert.</param>
    /// <param name="oldUnits">The unit to convert from.</param>
    /// <param name="newUnits">The unit to convert to.</param>
    /// <returns>The converted temperature measurement in the new unit, rounded to 4 significant figures.</returns>
    public static double ConvertTemperature(double temp, TemperatureUnits oldUnits, TemperatureUnits newUnits)
    {
        double convertedTemp = newUnits switch
        {
            TemperatureUnits.Fahrenheit => ConvertTemperatureHelper(temp, oldUnits) * (9.0 / 5.0) + 32,
            TemperatureUnits.Kelvin => ConvertTemperatureHelper(temp, oldUnits) + 273.15,
            _ => ConvertTemperatureHelper(temp, oldUnits)
        };
        return Math.Round(convertedTemp, 4);
    }

    /// <summary>
    /// Converts a temperature measurement from a given unit to Celsius.
    /// </summary>
    /// <param name="temp">The temperature measurement to convert.</param>
    /// <param name="unit">The unit to convert from.</param>
    /// <returns>The converted temperature measurement in Celsius.</returns>
    private static double ConvertTemperatureHelper(double temp, TemperatureUnits unit)
    {
        double convertedTemp = unit switch
        {
            TemperatureUnits.Fahrenheit => (temp - 32) * (5.0 / 9.0),
            TemperatureUnits.Kelvin => temp - 273.15,
            _ => temp,
        };
        return convertedTemp;
    }

}

