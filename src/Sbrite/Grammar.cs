using System;
using System.Collections.Generic;
using Sprache;

namespace Sbrite
{
    public static class Grammar
    {
        public static readonly Parser<string> Identifier =
            Parse.Letter.AtLeastOnce().Text().Token();

        public static readonly Parser<string> String =
            (from open in Parse.Char('"')
            from content in Parse.CharExcept('"').Many().Text()
            from close in Parse.Char('"')
            select content).Token();
            
        public static readonly Parser<string> FunctionOperator =
            Parse.String("=>").Text().Token();
            
        public static readonly Parser<string> AssignOperator =
            Parse.String("=").Text().Token();
            
        public static readonly Parser<string> Break =
            Parse.Or(Parse.LineEnd, Parse.String(",").Text());
            
        public static readonly Parser<string> Assignee =
            Parse.AnyChar.Many().Text();
            
        public static readonly Parser<string> Execution =
            Parse.AnyChar.Many().Text();
            
        public static readonly Parser<Assignment> Assignment =
            from assignee in Assignee
            from op in AssignOperator
            from execution in Execution
            select new Assignment(assignee, execution);
            
        public static readonly Parser<Statement> Statement =
            Parse.Or(Assignment, Execution);

        public static readonly Parser<Tuple> Tuple =
            from open in Parse.Char('(')
            from statements in Statement.Many().DelimitedBy(Break)
            from close in Parse.Char(')')
            select new Tuple(statements);

        public static readonly Parser<Tuple> Object =
            from open in Parse.Char('{')
            from statements in Statement.Many().DelimitedBy(Break)
            from close in Parse.Char('}')
            select new Object(statements);

        public static readonly Parser<Tuple> Sequence =
            from open in Parse.Char('[')
            from statements in Statement.Many().DelimitedBy(Break)
            from close in Parse.Char(']')
            select new Sequence(statements);
        
    }

    public class Assignment
    {
    }

    public class Statement
    {

    }

    public class Tuple
    {
        public ICollection<Statement> Statements { get; set; }
    }
}