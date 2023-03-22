using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Lexer
    {
        private String source;
        private int start = 0;
        private int current = 0;
        List<Token> tokens = new List<Token>();

        public Lexer(String source)
        {
            this.source = source;
        }

        public List<Token> GetTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }
            tokens.Add(new Token(TokenType.EOF, null, ""));
            return tokens;
        }

        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        private void AddToken(TokenType type, object literal)
        {
            String lexeme = source.Substring(start, current - start);
            tokens.Add(new Token(type, literal, lexeme));
        }

        private bool IsAtEnd()
        {
            return current >= source.Length;
        }

        private char Advance()
        {
            return source[current++];
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return source[current];
        }

        private char PeekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(': AddToken(TokenType.LEFT_PAREN); break;
                case ')': AddToken(TokenType.RIGHT_PAREN); break;
                case '*': AddToken(TokenType.STAR); break;
                case '/': AddToken(TokenType.SLASH); break;
                case '+': AddToken(TokenType.PLUS); break;
                case '-': AddToken(TokenType.MINUS); break;
                case ' ': break;
                case '\t': break;
                case '\n': break;

                default:
                    if(IsDigit(c))
                    {
                        number();
                    } else
                    {
                        throw new Exception($"Lexer error: Bad character : \'{c}\'");
                    }
                    break;
            }
        }

        private void number()
        {
            while (IsDigit(Peek()) && !IsAtEnd())
                Advance();

            char systemSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char separator = Peek();

            if (separator == systemSeparator && IsDigit(PeekNext()))
            {
                Advance(); // consume the separator
                while (IsDigit(Peek()) && !IsAtEnd())
                    Advance();
            }

            String lexeme = source.Substring(start, current - start);
            Double value = Double.Parse(lexeme);
            tokens.Add(new Token(TokenType.NUMBER, value, lexeme));
        }
    }
}
