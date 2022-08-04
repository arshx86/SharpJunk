#region

using System;
using System.Linq;

#endregion


namespace SharpJunk
{
    internal class Util
    {
        private static readonly Random rnd = new();

        /// <summary>
        ///     Generates random class type. <b>class, static class</b>
        /// </summary>
        public static string RandomClass()
        {
            string[] ClassArray =
            {
                "class ",
                "static class "
            };

            int len = rnd.Next(0, ClassArray.Length);
            return ClassArray[len];
        }

        /// <summary>
        ///     Generates random stuff. <b>Delegate, Random, Void</b>
        /// </summary>
        public static string RandomStuff()
        {
            string[] stuffs =
            {
                $"Random {RandomStr()} = new Random();",
                $"delegate {RandomVariable()} {RandomStr()}();",
                $"void {RandomStr()}({RandomVariable()} {RandomStr()}) {{ return; }}"
            };

            int len = rnd.Next(0, stuffs.Length);

            return stuffs[len];
        }

        /// <summary>
        ///     Generates random variable with random type.
        /// </summary>
        /// <param name="Nullable">Determines if variable should be nullable.</param>
        public static string RandomVariable(bool Nullable = false)
        {
            string[] variableNames =
            {
                "string ",
                "bool ",
                "int ",
                "double ",
                "decimal ",
                "short ",
                "byte ",
                "float ",
                "long ",
                "DateTime "
            };

            int result = rnd.Next(0, variableNames.Length);

            if (!Nullable) return variableNames[result];

            if (variableNames[result].Contains("string"))
                RandomVariable(true); // "?" cant be applied to string
            else
                return variableNames[result] + "?";

            return variableNames[result];
        }

        /// <summary>
        ///     Generates random array with random variable type.
        /// </summary>
        public static string AddArray()
        {
            string[] variableNames =
            {
                $"string[] {RandomStr()} = \n {{\n\n {RandomArrayCount(true)}, \n\n        }}; ",
                $"int[] {RandomStr()} = \n {{\n\n {RandomArrayCount()}, \n\n        }}; ",
                $"double[] {RandomStr()} = \n {{\n\n {RandomArrayCount()}, \n\n        }}; ",
                $"float[] {RandomStr()} = \n {{\n\n {RandomArrayCount()}, \n\n        }}; ",
                $"decimal[] {RandomStr()} = \n {{\n\n {RandomArrayCount()}, \n\n        }}; ",
                $"long[] {RandomStr()} = \n {{\n\n {RandomArrayCount()}, \n\n        }}; "
            };

            int result = rnd.Next(0, variableNames.Length);
            return variableNames[result];
        }

        /// <summary>
        ///     Generates random operator.
        /// </summary>
        public static string RandomOperator()
        {
            string[] ops =
            {
                "==",
                "<=",
                ">=",
                "<",
                ">",
                "!="
            };

            int len = rnd.Next(0, ops.Length);
            return ops[len];
        }

        /// <summary>
        ///     Generates random using.
        /// </summary>
        /// <returns></returns>
        public static string RandomUsing()
        {
            string[] NS =
            {
                "using System.Collections;",
                "using System.Collections.Specialized;",
                "using System.Collections.Generic;",
                "using System.Net.Sockets;",
                "using System.Net;",
                "using System.Threading;",
                "using Microsoft.Win32;",
                "using Microsoft.Win32.SafeHandles;",
                "using System.Collections.Generic;",
                "using System.Globalization;",
                "using System.Linq;",
                "using System.Text;",
                "using System.Threading;",
                "using System.Windows.Forms;",
                "using System.Diagnostics;",
                "using System.Drawing;",
                "using System.Drawing.Imaging;",
                "using System.Runtime.CompilerServices;",
                "using System.CodeDom.Compiler;",
                "using System.ComponentModel.Design.Serialization;",
                "using System.Collections.Generic;",
                "using System.Buffers.Text;",
                "using System.ComponentModel.Design.Serialization;",
                "using System.Management.Instrumentation;",
                "using System.Dynamic;",
                "using System.Threading.Tasks.Sources;",
                "using System.Timers;",
                "using System.IO.IsolatedStorage;",
                "using System.Timers;",
                "using System.Deployment.Internal.Isolation.Manifest;",
                "using System.Diagnostics.Tracing;"
            };

            int len = rnd.Next(0, NS.Length);
            return NS[len];
        }

        /// <summary>
        ///     Returns random string.
        /// </summary>
        /// <returns></returns>
        public static string RandomStr()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int chance = rnd.Next(10, 30);
            var stringChars = new char[chance];
            for (var i = 0; i < stringChars.Length; i++) stringChars[i] = chars[rnd.Next(chars.Length)];

            var finalString = new string(stringChars);
            return finalString;
        }

        #region Private

        private static string RandomArrayCount(bool isString = false)
        {
            int count = rnd.Next(5);

            if (isString)
            {
                var isstringstr = $"\"{RandomStr()}\"";

                for (var i = 0; i < count; i++) isstringstr += $",\"{RandomStr()}\"";

                return isstringstr;
            }

            var onlynumber = $"{RandomNumber()}";

            for (var i = 0; i < count; i++) onlynumber += $",{RandomNumber()}";

            return onlynumber;
        }

        private static int RandomNumber()
        {
            return rnd.Next(int.MaxValue - 99999);
        }

        #endregion
    }
}