using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    abstract class Expr
    {
        public interface IVisitor<R>
        {
            R VisitBinaryExpr(Binary expr);
            R VisitGroupingExpr(Grouping expr);
            R VisitLiteralExpr(Literal expr);
            R VisitUnaryExpr(Unary expr);
        }

        // accept visitor
        abstract public R Accept<R>(IVisitor<R> visitor);

        public class Binary : Expr
        {
            public readonly Expr left;
            public readonly Token _operator;
            public readonly Expr right;

            public Binary(Expr left, Token _operator, Expr right)
            {
                this.left = left;
                this._operator = _operator;
                this.right = right;
            }

            override public R Accept<R>(IVisitor<R> visitor)
            {
                return visitor.VisitBinaryExpr(this);
            }
        }

        public class Grouping : Expr
        {
            public readonly Expr expr;

            public Grouping(Expr expr)
            {
                this.expr = expr;
            }

            override public R Accept<R>(IVisitor<R> visitor)
            {
                return visitor.VisitGroupingExpr(this);
            }
        }

        public class Literal : Expr
        {
            public readonly Object value;

            public Literal(Object value)
            {
                this.value = value;
            }

            override public R Accept<R>(IVisitor<R> visitor)
            {
                return visitor.VisitLiteralExpr(this);
            }
        }

        public class Unary : Expr
        {
            public readonly Token _operator;
            public readonly Expr right;

            public Unary(Token _operator, Expr right)
            {
                this._operator = _operator;
                this.right = right;
            }

            override public R Accept<R>(IVisitor<R> visitor)
            {
                return visitor.VisitUnaryExpr(this);
            }
        }
    }

}
