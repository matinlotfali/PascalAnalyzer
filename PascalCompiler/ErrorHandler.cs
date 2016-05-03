using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalCompiler
{
    class ErrorHandler
    {
        public static List<Error> Lerrors;
        public static List<Error> Serrors;
        public static void LexicalError(string message, int position, int length)
        {
            Error a = new Error();
            a.message = message;
            a.position = position;
            a.length = length;
            Lerrors.Add(a);
        }
        public static void SyntaxError(string message, int position, int length)
        {
            Error a = new Error();
            a.message = message;
            a.position = position;
            a.length = length;
            Serrors.Add(a);
        }

        public static void Clear()
        {
            Lerrors = new List<Error>();
            Serrors = new List<Error>();
        }
    }

    class Error
    {
        public string message;
        public int position;
        public int length;
    }
}
