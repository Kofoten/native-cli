namespace Kofoten.NativeCli.Internal;

/// <summary>
/// Represents the type of a command-line token, which can be a value, an option, an unknown option, or an end-of-options marker.
/// </summary>
public enum CliTokenType
{
    Unknown = 0,
    Value,
    Option,
    UnknownOption,
    EndOfOptions,
}
