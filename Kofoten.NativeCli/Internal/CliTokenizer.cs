using System;
using System.Collections.Generic;
using System.Linq;

namespace Kofoten.NativeCli.Internal;

/// <summary>
/// Provides functionality to tokenize command-line arguments into a sequence of <see cref="CliToken"/> instances, identifying options, values, and special markers based on known long and short options.
/// </summary>
public static class CliTokenizer
{
    /// <summary>
    /// Tokenizes the specified command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments to tokenize.</param>
    /// <param name="knownLongOptions">An array of known long option names.</param>
    /// <param name="knownShortOptions">An array of known short option characters.</param>
    /// <returns>An enumerable sequence of <see cref="CliToken"/> instances representing the tokenized arguments.</returns>
    public static IEnumerable<CliToken> Tokenize(ArraySegment<string> args, string[] knownLongOptions, char[] knownShortOptions)
    {
        for (int i = args.Offset; i < args.Offset + args.Count; i++)
        {
            if (args.Array[i].StartsWith("--"))
            {
                if (args.Array[i] == "--")
                {
                    yield return new CliToken(CliTokenType.EndOfOptions, i, 0, 2);
                }

                int equalsIndex = args.Array[i].IndexOf('=');
                int optionLength = (equalsIndex == -1 ? args.Array[i].Length : equalsIndex) - 2;
                if (IsKnownLongOption(args.Array[i], optionLength, knownLongOptions))
                {
                    yield return new CliToken(CliTokenType.Option, i, 2, optionLength + 2);
                }
                else
                {
                    yield return new CliToken(CliTokenType.UnknownOption, i, 2, optionLength + 2);
                }

                if (equalsIndex != -1)
                {
                    yield return new CliToken(CliTokenType.Value, i, equalsIndex + 1, args.Array[i].Length - equalsIndex - 1);
                }
            }
            else if (args.Array[i].StartsWith("-") && args.Array[i].Length > 1)
            {
                int equalsIndex = args.Array[i].IndexOf('=');
                int optionEndIndex = equalsIndex == -1 ? args.Array[i].Length : equalsIndex;
                for (int j = 1; j < optionEndIndex; j++)
                {
                    if (knownShortOptions.Contains(args.Array[i][j]))
                    {
                        yield return new CliToken(CliTokenType.Option, i, j, 1);
                    }
                    else
                    {
                        yield return new CliToken(CliTokenType.UnknownOption, i, j, 1);
                    }
                }

                if (equalsIndex != -1)
                {
                    yield return new CliToken(CliTokenType.Value, i, equalsIndex + 1, args.Array[i].Length - equalsIndex - 1);
                }
            }
            else
            {
                yield return new CliToken(CliTokenType.Value, i, 0, args.Array[i].Length);
            }
        }
    }

    private static bool IsKnownLongOption(string arg, int optionLength, string[] knownLongOptions)
    {
        for (int i = 0; i < knownLongOptions.Length; i++)
        {
            if (optionLength == knownLongOptions[i].Length
                &&
                string.Compare(arg, 2, knownLongOptions[i], 0, optionLength, StringComparison.Ordinal) == 0)
            {
                return true;
            }
        }

        return false;
    }
}
