using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Parser
    {
        private List<Token> tokens;
        private int current = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Expr Parse()
        {
            Expr finalResult = Expression();
            if (!Match(TokenType.EOF))
                throw new Exception("Parser error: EOF expected");
            return finalResult;
        }

        // HELPER METHODS
        private bool IsAtEnd()
        {
            return current >= tokens.Count();
        }

        private Token Peek()
        {
            return tokens.ElementAt(current);
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd()) return false;
            return Peek().GetTokenType() == type;
        }

        private Token Previous()
        {
            return tokens.ElementAt(current - 1);
        }

        private Token Advance()
        {
            if (!IsAtEnd()) current++;
            return Previous();
        }

        private bool Match(params TokenType[] types)
        {
            foreach (TokenType type in types)
            {
                if (Peek().GetTokenType() == type)
                {
                    Advance();
                    return true;
                }
            }
            return false;
        }

        // ACTUAL PARSING
        private Expr Expression()
        {
            Expr expr = Term();

            while (Match(TokenType.PLUS, TokenType.MINUS))
            {
                Token _operator = Previous();
                Expr rightTerm = Term();
                expr = new Expr.Binary(expr, _operator, rightTerm);
            }
            
            return expr;
        }

        private Expr Term()
        {
            Expr expr = Factor();

            while (Match(TokenType.STAR, TokenType.SLASH))
            {
                Token _operator = Previous();
                Expr rightTerm = Factor();
                expr = new Expr.Binary(expr, _operator, rightTerm);
            }

            return expr;
        }

        private Expr Factor()
        {
            if (Match(TokenType.MINUS))
            {
                Token _operator = Previous();
                Expr expr = Primary();
                return new Expr.Unary(_operator, expr);
            }

            return Primary();
        }

        private Expr Primary()
        {
            if (Match(TokenType.LEFT_PAREN))
            {
                Expr expr = Expression();
                if (Match(TokenType.RIGHT_PAREN))
                    return new Expr.Grouping(expr);
                throw new Exception("Parser Error: Unclosed parentheses.");
            }

            if (Match(TokenType.NUMBER))
                return new Expr.Literal(Previous().GetLiteral());

            throw new Exception($"Parser error: Unexpected Token {Peek()}");
        }
    }
}
