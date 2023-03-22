using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Interpreter : Expr.IVisitor<Object>
    {
        public object Evaluate(Expr expr)
        {
            return expr.Accept(this);
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            switch (expr._operator.GetTokenType())
            {
                case TokenType.PLUS:
                    return (Double)Evaluate(expr.left) + (Double)Evaluate(expr.right);
                case TokenType.MINUS:
                    return (Double)Evaluate(expr.left) - (Double)Evaluate(expr.right);
                case TokenType.STAR:
                    return (Double)Evaluate(expr.left) * (Double)Evaluate(expr.right);
                case TokenType.SLASH:
                    if ((Double)Evaluate(expr.right) == 0.0D)
                        throw new Exception("Interpreter error: Division by zero.");
                    return (Double)Evaluate(expr.left) / (Double)Evaluate(expr.right);
            }

            // Unreachable
            return null;
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return (Double)Evaluate(expr.expr);
        }

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return (Double)expr.value;
        }

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            // Leave room for other Unary expressions e.g. +NUMBER or !NUMBER
            switch (expr._operator.GetTokenType())
            {
                case TokenType.MINUS:
                    return -1D * (Double)Evaluate(expr.right);
            }
            return null;
        }
    }
}
