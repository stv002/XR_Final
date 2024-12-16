/// <summary>
/// Interface for objects that provide power status.
/// </summary>
public interface IPowerProvider
{
    /// <summary>
    /// Checks if the object is currently powered.
    /// </summary>
    /// <returns>True if powered, false otherwise.</returns>
    bool IsPowered();
}
