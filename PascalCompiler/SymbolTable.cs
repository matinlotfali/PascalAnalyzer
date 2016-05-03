using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalCompiler
{
    class SymbolTable
    {
        public static List<Entry> list;
        public static void Add(Tokens token, string lexeme, int index, int length)
        {
            Entry t = new Entry();
            t.token = token;
            t.lexeme = lexeme;
            t.index = index;
            t.length = length;
            list.Add(t);
        }

        public static void Add(Tokens token, double value, int index, int length)
        {
            Entry t = new Entry();
            t.token = token;
            t.value = value;
            t.index = index;
            t.length = length;
            list.Add(t);
        }

        public static void Clear()
        {
            list = new List<Entry>();
        }

        public static string TokenPattern(Tokens t)
        {
            switch (t)
            {
                case Tokens.s_addop: return "add operation";
                case Tokens.s_array: return "array";
                case Tokens.s_assignop: return "assignment operation";
                case Tokens.s_begin: return "begin";
                case Tokens.s_colon: return ":";
                case Tokens.s_comma: return ",";
                case Tokens.s_do: return "do";
                case Tokens.s_dot: return ".";
                case Tokens.s_else: return "else";
                case Tokens.s_end: return "end";
                case Tokens.s_function: return "function";
                case Tokens.s_id: return "identify";
                case Tokens.s_if: return "if";
                case Tokens.s_integer: return "integer";
                case Tokens.s_leftbracket: return "[";
                case Tokens.s_leftparan: return "(";
                case Tokens.s_mulop: return "multiply operation";
                case Tokens.s_negative: return "-";
                case Tokens.s_not: return "not";
                case Tokens.s_num: return "number";
                case Tokens.s_of: return "of";
                case Tokens.s_positive: return "+";
                case Tokens.s_procedure: return "procedure";
                case Tokens.s_Program: return "program";
                case Tokens.s_real: return "real";
                case Tokens.s_relop: return "relational operation";
                case Tokens.s_rightbracket: return "]";
                case Tokens.s_rightparan: return ")";
                case Tokens.s_semicolon: return ";";
                case Tokens.s_then: return "then";
                case Tokens.s_var: return "var";
                case Tokens.s_while: return "while";
                default:
                    return "unknown";
            }
        }
    }
}
