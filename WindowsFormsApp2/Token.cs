using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Token
    {
        private TokenType type;
        private object literal;
        private string lexeme;

        public Token(TokenType type, object literal, String lexeme)
        {
            this.type = type;
            this.literal = literal;
            this.lexeme = lexeme;
        }

        public TokenType GetTokenType()
        {
            return this.type;
        }

        public object GetLiteral()
        {
            return this.literal;
        }

        override public String ToString()
        {
            return $"{this.type}, {this.literal}, {this.lexeme}";
        }
    }
}
