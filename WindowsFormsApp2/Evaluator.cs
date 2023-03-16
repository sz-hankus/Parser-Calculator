using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Evaluator
    {
        public static object Evaluate(String source)
        {
            Lexer lexer = new Lexer(source);
            
            List<Token> tokens = lexer.GetTokens();
            foreach (Token token in tokens)
            {
                Debug.WriteLine(token);
            }

            Parser parser = new Parser(tokens);
            Expr finalExpression = parser.Parse();

            Interpreter interpreter = new Interpreter();
            Double finalValue = (Double)interpreter.Evaluate(finalExpression);
            return finalValue;
            
        }
    }
}
