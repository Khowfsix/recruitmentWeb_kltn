using System.Collections;

namespace InsternShip.Common;

public static class Validation
{
    /// <summary>
    /// Check if any property of the object is null or default value.
    /// <para>
    /// Warning: This method will not work with nested objects.
    /// </para>
    /// </summary>
    /// <returns>True if any property is null or default value, otherwise false</returns>
    /// <param name="value">Object to check</param>
    /// <param name="propertiesToInclude">Properties to include. If null, all properties will be included</param>
    /// <param name="propertiesToIgnore">Properties to ignore</param>
    public static bool IsAnyPropertyDefaultOrNull<T>(
        T value,
        string[]? propertiesToInclude = null,
        string[]? propertiesToIgnore = null
    )
    {
        var properties = typeof(T).GetProperties();
        if (propertiesToInclude != null)
        {
            properties = properties.Where(p => propertiesToInclude.Contains(p.Name)).ToArray();
        }
        return (
            from property in properties
            where propertiesToIgnore == null || !propertiesToIgnore.Contains(property.Name)
            select property.GetValue(value)
        ).Any(IsNullOrDefault);
    }

    private static bool IsNullOrDefault<T>(T value)
    {
        return value switch
        {
            null => true,
            string s => string.IsNullOrEmpty(s),
            IEnumerable e => !e.Cast<object>().Any(),
            _ when typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) != null
                => value.Equals(Activator.CreateInstance(typeof(T))),
            _ when typeof(T).IsValueType => value.Equals(default(T)),
            _ => false
        };
    }
}
