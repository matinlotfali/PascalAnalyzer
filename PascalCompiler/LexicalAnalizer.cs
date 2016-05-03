using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PascalCompiler
{
    class LexicalAnalizer
    {
        static RichTextBox rtb;
        static bool IsDigit(char ch)
        {
            return (ch >= '0' && ch <= '9');
        }


        static bool IsLetter(char ch)
        {
            return (ch >= 'a' && ch <= 'z');
        }

        static void MakeBold(int index, int length)
        {
            rtb.Select(index, length);
            rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
            rtb.SelectionColor = Color.Navy;
        }

        static void MakeGreen(int index, int length)
        {
            rtb.Select(index, length);
            rtb.SelectionColor = Color.Green;
        }

        static void MakeItalic(int index, int length)
        {
            rtb.Select(index, length);
            rtb.SelectionFont = new Font(rtb.Font, FontStyle.Italic);
            rtb.SelectionColor = Color.Green;
        }

        static void MakeRed(int index, int length)
        {
            rtb.Select(index, length);
            rtb.SelectionColor = Color.DarkRed;
        }

        public static void Analize(RichTextBox rtb)
        {
            LexicalAnalizer.rtb = rtb;
            string s = rtb.Text.ToLower();
            for (int i = 0; i < s.Length; i++)
            {
                try
                {
                    while (s[i] == ' ' || s[i] == '\t' || s[i] == '\n') i++;
                    switch (s[i])
                    {
                        case '{':
                            int open = 1, z;
                            for (z = 1; i + z < s.Length; z++)
                            {
                                if (s[i + z] == '{') open++;
                                else if (s[i + z] == '}') open--;
                                if (open == 0) break;
                            }
                            if (open > 0)
                                ErrorHandler.LexicalError(open.ToString() + " '}' expected.", i + z, 1);
                            MakeItalic(i, z + 1);
                            i += z;
                            break;
                        case ':':
                            if (s[i + 1] == '=')
                            {
                                SymbolTable.Add(Tokens.s_assignop, ":=", i, 2);
                                i++;
                            }
                            else
                                SymbolTable.Add(Tokens.s_colon, ":", i, 1);
                            break;
                        case '<':
                            if (s[i + 1] == '>')
                            {
                                SymbolTable.Add(Tokens.s_relop, "<>", i, 2);
                                i++;
                            }
                            else if (s[i + 1] == '=')
                            {
                                SymbolTable.Add(Tokens.s_relop, "<=", i, 2);
                                i++;
                            }
                            else
                                SymbolTable.Add(Tokens.s_relop, "<", i, 1);
                            break;
                        case '>':
                            if (s[i + 1] == '=')
                            {
                                SymbolTable.Add(Tokens.s_relop, ">=", i, 2);
                                i++;
                            }
                            else
                                SymbolTable.Add(Tokens.s_relop, ">", i, 1);
                            break;
                        case '=':
                            SymbolTable.Add(Tokens.s_relop, "=", i, 1); break;
                        case '.':
                            SymbolTable.Add(Tokens.s_dot, ".", i, 1); break;
                        case '(':
                            SymbolTable.Add(Tokens.s_leftparan, "(", i, 1); break;
                        case ')':
                            SymbolTable.Add(Tokens.s_rightparan, ")", i, 1); break;
                        case '[':
                            SymbolTable.Add(Tokens.s_leftbracket, "[", i, 1); break;
                        case ']':
                            SymbolTable.Add(Tokens.s_rightbracket, "]", i, 1); break;
                        case ';':
                            SymbolTable.Add(Tokens.s_semicolon, ";", i, 1); break;
                        case '+':
                            if (SymbolTable.list[SymbolTable.list.Count - 1].token == Tokens.s_id
                                || SymbolTable.list[SymbolTable.list.Count - 1].token == Tokens.s_num
                                || SymbolTable.list[SymbolTable.list.Count - 1].token == Tokens.s_rightparan)
                                SymbolTable.Add(Tokens.s_addop, "+", i, 1);
                            else
                                SymbolTable.Add(Tokens.s_positive, "+", i, 1);
                            break;
                        case '-':
                            if (SymbolTable.list[SymbolTable.list.Count - 1].token == Tokens.s_id
                                || SymbolTable.list[SymbolTable.list.Count - 1].token == Tokens.s_num
                                || SymbolTable.list[SymbolTable.list.Count - 1].token == Tokens.s_rightparan)
                                SymbolTable.Add(Tokens.s_addop, "-", i, 1);
                            else
                                SymbolTable.Add(Tokens.s_negative, "-", i, 1);
                            break;
                        case '*':
                            SymbolTable.Add(Tokens.s_mulop, "*", i, 1); break;
                        case '/':
                            SymbolTable.Add(Tokens.s_mulop, "/", i, 1); break;
                        case ',':
                            SymbolTable.Add(Tokens.s_comma, ",", i, 1); break;
                        default:

                            //reserved words
                            if (s[i] == 'p' && s[i + 1] == 'r' && s[i + 2] == 'o' && s[i + 3] == 'g' && s[i + 4] == 'r' && s[i + 5] == 'a' && s[i + 6] == 'm')
                            {
                                SymbolTable.Add(Tokens.s_Program, "program", i, 7);
                                MakeBold(i, 7);
                                i += 6;
                            }
                            else if (s[i] == 'p' && s[i + 1] == 'r' && s[i + 2] == 'o' && s[i + 3] == 'c' && s[i + 4] == 'e' && s[i + 5] == 'd' && s[i + 6] == 'u' && s[i + 7] == 'r' && s[i + 8] == 'e')
                            {
                                SymbolTable.Add(Tokens.s_procedure, "procedure", i, 9);
                                MakeBold(i, 9);
                                i += 8;
                            }
                            else if (s[i] == 'b' && s[i + 1] == 'e' && s[i + 2] == 'g' && s[i + 3] == 'i' && s[i + 4] == 'n')
                            {
                                SymbolTable.Add(Tokens.s_begin, "begin", i, 5);
                                MakeBold(i, 5);
                                i += 4;
                            }
                            else if (s[i] == 'e' && s[i + 1] == 'n' && s[i + 2] == 'd')
                            {
                                SymbolTable.Add(Tokens.s_end, "end", i, 3);
                                MakeBold(i, 3);
                                i += 2;
                            }
                            else if (s[i] == 'e' && s[i + 1] == 'l' && s[i + 2] == 's' && s[i + 3] == 'e')
                            {
                                SymbolTable.Add(Tokens.s_else, "else", i, 4);
                                MakeBold(i, 4);
                                i += 3;
                            }
                            else if (s[i] == 'f' && s[i + 1] == 'u' && s[i + 2] == 'n' && s[i + 3] == 'c' && s[i + 4] == 't' && s[i + 5] == 'i' && s[i + 6] == 'o' && s[i + 7] == 'n')
                            {
                                SymbolTable.Add(Tokens.s_function, "function", i, 8);
                                MakeBold(i, 8);
                                i += 7;
                            }
                            else if (s[i] == 'o' && s[i + 1] == 'f')
                            {
                                SymbolTable.Add(Tokens.s_of, "of", i, 2);
                                MakeBold(i, 2);
                                i++;
                            }
                            else if (s[i] == 'o' && s[i + 1] == 'r')
                            {
                                SymbolTable.Add(Tokens.s_addop, "or", i, 2);
                                MakeBold(i, 2);
                                i++;
                            }
                            else if (s[i] == 'r' && s[i + 1] == 'e' && s[i + 2] == 'a' && s[i + 3] == 'l')
                            {
                                SymbolTable.Add(Tokens.s_real, "real", i, 4);
                                MakeBold(i, 4);
                                i += 3;
                            }
                            else if (s[i] == 'i' && s[i + 1] == 'n' && s[i + 2] == 't' && s[i + 3] == 'e' && s[i + 4] == 'g' && s[i + 5] == 'e' && s[i + 6] == 'r')
                            {
                                SymbolTable.Add(Tokens.s_integer, "integer", i, 7);
                                MakeBold(i, 7);
                                i += 6;
                            }
                            else if (s[i] == 'i' && s[i + 1] == 'f')
                            {
                                SymbolTable.Add(Tokens.s_if, "if", i, 2);
                                MakeBold(i, 2);
                                i++;
                            }
                            else if (s[i] == 't' && s[i + 1] == 'h' && s[i + 2] == 'e' && s[i + 3] == 'n')
                            {
                                SymbolTable.Add(Tokens.s_then, "then", i, 4);
                                MakeBold(i, 4);
                                i += 3;
                            }
                            else if (s[i] == 'w' && s[i + 1] == 'h' && s[i + 2] == 'i' && s[i + 3] == 'l' && s[i + 4] == 'e')
                            {
                                SymbolTable.Add(Tokens.s_while, "while", i, 5);
                                MakeBold(i, 5);
                                i += 4;
                            }
                            else if (s[i] == 'm' && s[i + 1] == 'o' && s[i + 2] == 'd')
                            {
                                SymbolTable.Add(Tokens.s_mulop, "mod", i, 3);
                                MakeBold(i, 3);
                                i += 2;
                            }
                            else if (s[i] == 'd' && s[i + 1] == 'i' && s[i + 2] == 'v')
                            {
                                SymbolTable.Add(Tokens.s_mulop, "div", i, 3);
                                MakeBold(i, 3);
                                i += 2;
                            }
                            else if (s[i] == 'd' && s[i + 1] == 'o')
                            {
                                SymbolTable.Add(Tokens.s_do, "do", i, 2);
                                MakeBold(i, 2);
                                i++;
                            }
                            else if (s[i] == 'a' && s[i + 1] == 'r' && s[i + 2] == 'r' && s[i + 3] == 'a' && s[i + 4] == 'y')
                            {
                                SymbolTable.Add(Tokens.s_array, "array", i, 5);
                                MakeBold(i, 5);
                                i += 4;
                            }
                            else if (s[i] == 'a' && s[i + 1] == 'n' && s[i + 2] == 'd')
                            {
                                SymbolTable.Add(Tokens.s_mulop, "and", i, 3);
                                MakeBold(i, 3);
                                i += 2;
                            }
                            else if (s[i] == 'v' && s[i + 1] == 'a' && s[i + 2] == 'r')
                            {
                                SymbolTable.Add(Tokens.s_var, "var", i, 3);
                                MakeBold(i, 3);
                                i += 2;
                            }
                            else if (s[i] == 'n' && s[i + 1] == 'o' && s[i + 2] == 't')
                            {
                                SymbolTable.Add(Tokens.s_not, "not", i, 3);
                                MakeBold(i, 3);
                                i += 2;
                            }
                            else if (IsDigit(s[i]))
                            {

                                //numbers
                                int j = 1;
                                double a = Convert.ToInt32(s[i] - '0');
                                while (IsDigit(s[i + j]))
                                {
                                    a = a * 10 + Convert.ToInt32(s[i + j] - '0');
                                    j++;
                                }
                                if (s[i + j] == '.')
                                {
                                    j++;
                                    int k = 0;
                                    double b = 0;
                                    while (IsDigit(s[i + j + k]))
                                    {
                                        b = b * 10 + Convert.ToInt32(s[i + j + k] - '0');
                                        k++;
                                    }
                                    b = b / Math.Pow(10, k);
                                    a += b;
                                    j += k;
                                }
                                if (s[i + j] == 'e')
                                {
                                    j++;
                                    bool neg = false;
                                    double b = 0;
                                    if (s[i + j] == '+')
                                        j++;
                                    else if (s[i + j] == '-')
                                    {
                                        j++;
                                        neg = true;
                                    }
                                    if (IsDigit(s[i + j]))
                                    {
                                        do
                                        {
                                            b = b * 10 + Convert.ToInt32(s[i + j] - '0');
                                            j++;
                                        }
                                        while (IsDigit(s[i + j]));
                                    }
                                    else
                                    {
                                        ErrorHandler.LexicalError("A number is expected after exp", i, j);
                                        i += j - 1;
                                        continue;
                                    }
                                    if (!neg)
                                        a *= Math.Exp(b);
                                    else
                                        a *= Math.Exp(-b);
                                }
                                SymbolTable.Add(Tokens.s_num, a, i, j);
                                MakeGreen(i, j);
                                i += j - 1;
                            }
                            else
                            {
                                if (IsLetter(s[i]))
                                {
                                    string id = s[i].ToString();
                                    int j = 1;
                                    while (IsLetter(s[i + j]) || IsDigit(s[i + j]))
                                        id += s[i + j++];
                                    SymbolTable.Add(Tokens.s_id, id, i, j);
                                    MakeRed(i, j);
                                    i += j - 1;
                                }
                                else
                                {
                                    ErrorHandler.LexicalError("Unsopported character: " + s[i], i, 1);
                                    continue;
                                }
                            }
                            break;

                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.LexicalError("C# error!!: " + ex.Message, i, 1);
                }
            }


        }
    }
}
