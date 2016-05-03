using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalCompiler
{
    enum Tokens
    {
        s_Program, s_id, s_dot, s_comma, s_var, s_colon, s_semicolon, s_array, s_leftbracket, s_rightbracket,
        s_num, s_of, s_integer, s_real, s_leftparan, s_rightparan, s_function, s_procedure, s_begin, s_end,
        s_if, s_while, s_then, s_do, s_else, s_assignop, s_relop, s_addop, s_positive, s_negative, s_mulop,
        s_not, s_eof
    }

    class Entry
    {
        public Tokens token;
        public string lexeme;
        public double value;
        public int index;
        public int length;
    }
}
