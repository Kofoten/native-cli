using System;

namespace Kofoten.NativeCli.Generator.Tests
{
    internal class OptionsTestCommand
    {
        [CliOption("string-test", Short = 's')]
        internal string StringTest { get; set; }

        [CliOption("int-test", Short = 'i')]
        internal int IntTest { get; set; }

        [CliOption("datetime-test")]
        internal DateTime DateTimeTest { get; set; }
    }
}
