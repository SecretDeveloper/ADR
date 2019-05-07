using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace adr
{
    public static class Utility
    {
        public static void WriteLine(params string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }
        }
        public static void WriteErrorLine(params string[] args)
        {
            foreach (var item in args)
            {
                Console.Error.WriteLine(item);
            }
        }

        public static bool InvariantEquals(string left, string right)
        {
            return left.Equals(right, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}