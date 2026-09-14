using System;

namespace Kofoten.NativeCli.Internal;

/// <summary>
/// Represents a token in the command-line arguments, including its type, index in the argument array, and the start and length of the token within the argument string.
/// </summary>
/// <param name="type">The type of the token.</param>
/// <param name="index">The index of the token in the argument array.</param>
/// <param name="tokenStart">The start position of the token within the argument string.</param>
/// <param name="tokenLength">The length of the token within the argument string.</param>
public readonly struct CliToken(CliTokenType type, int index, int tokenStart, int tokenLength)
{
    public CliTokenType Type { get; } = type;
    public int Index { get; } = index;
    public int TokenStart { get; } = tokenStart;
    public int TokenLength { get; } = tokenLength;

    /// <summary>
    /// Gets the string representation of the token from the specified command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The string representation of the token.</returns>
    public string GetTokenString(ArraySegment<string> args)
    {
        return args.Array[Index].Substring(TokenStart, TokenLength);
    }
}
