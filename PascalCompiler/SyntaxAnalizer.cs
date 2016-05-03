using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalCompiler
{
    class SyntaxAnalizer
    {
        static Tokens c_Symbol
        {
            get
            {
                if (currentIndex < SymbolTable.list.Count)
                    return SymbolTable.list[currentIndex].token;
                else
                    return Tokens.s_eof;
            }
        }
        static void NextSymbol()
        {
            if (c_Symbol != Tokens.s_eof)
                currentIndex++;
        }
        static int currentIndex;

        public static void program(Set stop)
        {
            currentIndex = 0;
            Expect(Tokens.s_Program, stop + Tokens.s_id + Tokens.s_leftparan + Tokens.s_rightparan + Tokens.s_semicolon + Tokens.s_dot + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure + Tokens.s_var);
            Expect(Tokens.s_id, stop + Tokens.s_leftparan + Tokens.s_rightparan + Tokens.s_semicolon + Tokens.s_dot + Tokens.s_id + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure + Tokens.s_var);
            Expect(Tokens.s_leftparan, stop + Tokens.s_rightparan + Tokens.s_semicolon + Tokens.s_dot + Tokens.s_id + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure + Tokens.s_var);

            identifier_list(stop + Tokens.s_rightparan + Tokens.s_semicolon + Tokens.s_dot + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure + Tokens.s_var);

            Expect(Tokens.s_rightparan, stop + Tokens.s_semicolon + Tokens.s_dot + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure + Tokens.s_var);
            Expect(Tokens.s_semicolon, stop + Tokens.s_dot + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure + Tokens.s_var);

            declarations(stop + Tokens.s_dot + Tokens.s_begin + Tokens.s_function + Tokens.s_procedure);
            subprogram_declarations(stop + Tokens.s_dot + Tokens.s_begin);
            compound_statement(stop + Tokens.s_dot);

            Expect(Tokens.s_dot, stop);
        }

        static void identifier_list(Set stop)
        {
            Expect(Tokens.s_id, stop + Tokens.s_comma + Tokens.s_id);
            while (c_Symbol == Tokens.s_comma)
            {
                Expect(Tokens.s_comma, stop + Tokens.s_comma + Tokens.s_id);
                Expect(Tokens.s_id, stop + Tokens.s_comma + Tokens.s_id);
            }
        }

        static void declarations(Set stop)
        {
            while (c_Symbol == Tokens.s_var)
            {
                Expect(Tokens.s_var, stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_var + Tokens.s_id + Tokens.s_array + Tokens.s_integer + Tokens.s_real);
                identifier_list(stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_var + Tokens.s_id + Tokens.s_array + Tokens.s_integer + Tokens.s_real);
                Expect(Tokens.s_colon, stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_var + Tokens.s_id + Tokens.s_array + Tokens.s_integer + Tokens.s_real);
                type(stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_var + Tokens.s_id + Tokens.s_array + Tokens.s_integer + Tokens.s_real);
                Expect(Tokens.s_semicolon, stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_var + Tokens.s_id + Tokens.s_array + Tokens.s_integer + Tokens.s_real);
            }
        }

        static void type(Set stop)
        {
            if (c_Symbol == Tokens.s_array)
            {
                Expect(Tokens.s_array, stop + Tokens.s_leftbracket + Tokens.s_rightbracket + Tokens.s_num + Tokens.s_dot + Tokens.s_of);
                Expect(Tokens.s_leftbracket, stop + Tokens.s_rightbracket + Tokens.s_num + Tokens.s_dot + Tokens.s_of);
                Expect(Tokens.s_num, stop + Tokens.s_rightbracket + Tokens.s_num + Tokens.s_dot + Tokens.s_of);
                Expect(Tokens.s_dot, stop + Tokens.s_rightbracket + Tokens.s_num + Tokens.s_dot + Tokens.s_of);
                Expect(Tokens.s_dot, stop + Tokens.s_rightbracket + Tokens.s_num + Tokens.s_of);
                Expect(Tokens.s_num, stop + Tokens.s_rightbracket + Tokens.s_of);
                Expect(Tokens.s_rightbracket, stop + Tokens.s_of);
                Expect(Tokens.s_of, stop);
            }
            standard_type(stop);
        }

        static void standard_type(Set stop)
        {
            if (c_Symbol == Tokens.s_integer)
                Expect(Tokens.s_integer, stop);
            else
                Expect(Tokens.s_real, stop);
        }

        static void subprogram_declarations(Set stop)
        {
            while (c_Symbol == Tokens.s_function || c_Symbol == Tokens.s_procedure)
            {
                subprogram_declaration(stop + Tokens.s_semicolon + Tokens.s_procedure + Tokens.s_function);
                Expect(Tokens.s_semicolon, stop + Tokens.s_semicolon + Tokens.s_procedure + Tokens.s_function);
            }
        }

        static void subprogram_declaration(Set stop)
        {
            subprogram_head(stop + Tokens.s_var + Tokens.s_begin);
            declarations(stop + Tokens.s_begin);
            compound_statement(stop);
        }

        static void subprogram_head(Set stop)
        {
            if (c_Symbol == Tokens.s_function)
            {
                Expect(Tokens.s_function, stop + Tokens.s_id + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_leftparan + Tokens.s_integer + Tokens.s_real);
                Expect(Tokens.s_id, stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_leftparan + Tokens.s_integer + Tokens.s_real);
                arguments(stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_integer + Tokens.s_real);
                Expect(Tokens.s_colon, stop + Tokens.s_semicolon + Tokens.s_integer + Tokens.s_real);
                standard_type(stop + Tokens.s_semicolon);
                Expect(Tokens.s_semicolon, stop);
            }
            else
            {
                Expect(Tokens.s_procedure, stop + Tokens.s_id + Tokens.s_semicolon + Tokens.s_leftparan);
                Expect(Tokens.s_id, stop + Tokens.s_semicolon + Tokens.s_leftparan);
                arguments(stop + Tokens.s_semicolon);
                Expect(Tokens.s_semicolon, stop);
            }
        }

        static void arguments(Set stop)
        {
            if (c_Symbol == Tokens.s_leftparan)
            {
                Expect(Tokens.s_leftparan, stop + Tokens.s_rightparan + Tokens.s_id);
                parameter_list(stop + Tokens.s_rightparan);
                Expect(Tokens.s_rightparan, stop);
            }
        }

        static void parameter_list(Set stop)
        {
            identifier_list(stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id);
            Expect(Tokens.s_colon, stop + Tokens.s_semicolon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id + Tokens.s_colon);
            type(stop + Tokens.s_semicolon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id + Tokens.s_colon);
            while (c_Symbol == Tokens.s_semicolon)
            {
                Expect(Tokens.s_semicolon, stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id + Tokens.s_colon);
                identifier_list(stop + Tokens.s_colon + Tokens.s_semicolon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id + Tokens.s_colon);
                Expect(Tokens.s_colon, stop + Tokens.s_semicolon + Tokens.s_colon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id + Tokens.s_colon);
                type(stop + Tokens.s_semicolon + Tokens.s_colon + Tokens.s_array + Tokens.s_integer + Tokens.s_real + Tokens.s_id + Tokens.s_colon);
            }
        }

        static void compound_statement(Set stop)
        {
            Expect(Tokens.s_begin, stop + Tokens.s_end + Tokens.s_id + Tokens.s_begin + Tokens.s_if + Tokens.s_while);
            optional_statements(stop + Tokens.s_end);
            Expect(Tokens.s_end, stop);
        }

        static void optional_statements(Set stop)
        {
            if (c_Symbol == Tokens.s_id || c_Symbol == Tokens.s_begin || c_Symbol == Tokens.s_if || c_Symbol == Tokens.s_while)
                statement_list(stop);
        }

        static void statement_list(Set stop)
        {
            statement(stop + Tokens.s_semicolon + Tokens.s_id + Tokens.s_begin + Tokens.s_if + Tokens.s_while);
            while (c_Symbol == Tokens.s_semicolon)
            {
                Expect(Tokens.s_semicolon, stop + Tokens.s_semicolon + Tokens.s_id + Tokens.s_begin + Tokens.s_if + Tokens.s_while);
                statement(stop + Tokens.s_semicolon + Tokens.s_id + Tokens.s_begin + Tokens.s_if + Tokens.s_while);
            }
        }

        static void statement(Set stop)
        {
            while (c_Symbol == Tokens.s_while || c_Symbol == Tokens.s_if)
            {
                if (c_Symbol == Tokens.s_while)
                {
                    Expect(Tokens.s_while, stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                    expression(stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                    Expect(Tokens.s_do, stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                }
                else
                {
                    Expect(Tokens.s_if, stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                    expression(stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                    Expect(Tokens.s_then, stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                    statement(stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                    Expect(Tokens.s_else, stop + Tokens.s_then + Tokens.s_else + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan + Tokens.s_if + Tokens.s_while + Tokens.s_do);
                }
            }

            if (c_Symbol == Tokens.s_begin)
                compound_statement(stop);
            else
            {
                Expect(Tokens.s_id, stop + Tokens.s_leftbracket + Tokens.s_rightbracket + Tokens.s_assignop + Tokens.s_leftparan + Tokens.s_rightparan);
                if (c_Symbol == Tokens.s_leftbracket)
                {
                    Expect(Tokens.s_leftbracket, stop + Tokens.s_rightbracket + Tokens.s_assignop + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan);
                    expression(stop + Tokens.s_rightbracket + Tokens.s_assignop);
                    Expect(Tokens.s_rightbracket, stop + Tokens.s_assignop);
                    if (c_Symbol == Tokens.s_assignop)
                    {
                        Expect(Tokens.s_assignop, stop);
                        expression(stop);
                    }
                }
                else if (c_Symbol == Tokens.s_assignop)
                {
                    Expect(Tokens.s_assignop, stop + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan);
                    expression(stop);
                }
                else if (c_Symbol == Tokens.s_leftparan)
                {
                    Expect(Tokens.s_leftparan, stop + Tokens.s_rightparan + Tokens.s_comma + Tokens.s_positive + Tokens.s_negative + Tokens.s_not + Tokens.s_num + Tokens.s_id + Tokens.s_leftparan);
                    expression_list(stop + Tokens.s_rightbracket);
                    Expect(Tokens.s_rightparan, stop);
                }
            }

        }

        static void expression_list(Set stop)
        {
            expression(stop + Tokens.s_comma);
            while (c_Symbol == Tokens.s_comma)
            {
                Expect(Tokens.s_comma, stop + Tokens.s_comma);
                expression(stop + Tokens.s_comma);
            }
        }

        static void expression(Set stop)
        {
            simple_expression(stop + Tokens.s_relop);
            while (c_Symbol == Tokens.s_relop)
            {
                Expect(Tokens.s_relop, stop + Tokens.s_num + Tokens.s_not + Tokens.s_positive + Tokens.s_negative + Tokens.s_leftparan + Tokens.s_id);
                simple_expression(stop + Tokens.s_relop);
            }
        }

        static void simple_expression(Set stop)
        {
            if (c_Symbol == Tokens.s_positive || c_Symbol == Tokens.s_negative)
            {
                sign(stop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
                term(stop);
            }
            else
            {
                term(stop + Tokens.s_addop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
                while (c_Symbol == Tokens.s_addop)
                {
                    Expect(Tokens.s_addop, stop + Tokens.s_addop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
                    term(stop + Tokens.s_addop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
                }
            }

        }

        static void term(Set stop)
        {
            factor(stop + Tokens.s_mulop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
            while (c_Symbol == Tokens.s_mulop)
            {
                Expect(Tokens.s_mulop, stop + Tokens.s_mulop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
                factor(stop + Tokens.s_mulop + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan);
            }
        }

        static void factor(Set stop)
        {
            while (c_Symbol == Tokens.s_not)
                Expect(Tokens.s_not, stop + Tokens.s_id + Tokens.s_num + Tokens.s_leftparan + Tokens.s_rightparan + Tokens.s_not);
            if (c_Symbol == Tokens.s_leftparan)
            {
                Expect(Tokens.s_leftparan, stop + Tokens.s_rightparan + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan + Tokens.s_positive + Tokens.s_negative);
                expression(stop + Tokens.s_rightparan);
                Expect(Tokens.s_rightparan, stop);
            }
            else if (c_Symbol == Tokens.s_num)
                Expect(Tokens.s_num, stop);
            else
            {
                Expect(Tokens.s_id, stop + Tokens.s_leftparan + Tokens.s_rightparan + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan + Tokens.s_positive + Tokens.s_negative);
                if (c_Symbol == Tokens.s_leftparan)
                {
                    Expect(Tokens.s_leftparan, stop + Tokens.s_rightparan + Tokens.s_num + Tokens.s_id + Tokens.s_not + Tokens.s_leftparan + Tokens.s_positive + Tokens.s_negative);
                    expression_list(stop + Tokens.s_rightparan);
                    Expect(Tokens.s_rightparan, stop);
                }
            }
        }

        static void sign(Set stop)
        {
            if (c_Symbol == Tokens.s_negative)
                Expect(Tokens.s_negative, stop);
            else
                Expect(Tokens.s_positive, stop);
        }

        static void Expect(Tokens t, Set stop)
        {
            if (c_Symbol == t)
            {
                NextSymbol();
            }
            else
            {
                int index, length;
                if (c_Symbol != Tokens.s_eof)
                {
                    index = SymbolTable.list[currentIndex].index;
                    length = SymbolTable.list[currentIndex].length;
                }
                else
                {
                    index = SymbolTable.list[currentIndex - 1].index;
                    length = 5;
                }
                ErrorHandler.SyntaxError("'" + SymbolTable.TokenPattern(t) + "' is expected", index, length);
                while (!stop.IsExist(c_Symbol))
                    NextSymbol();
            }
        }
    }
}
