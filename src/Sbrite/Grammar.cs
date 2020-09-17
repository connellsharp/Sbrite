using System;
using System.Linq;
using Sprache;

namespace Sbrite
{
    public static class Grammar
    {
        public static Parser<T> LineToken<T>(this Parser<T> parser)
        {
            if (parser == null) throw new ArgumentNullException(nameof(parser));

            return from leading in Parse.Chars(' ', '\t').Many()
                   from item in parser
                   from trailing in Parse.Chars(' ', '\t').Many()
                   select item;
        }

        /// <summary>
        /// A type name or alias.
        /// </summary>
        public static readonly Parser<string> Identifier =
            (Parse.Letter
            .Or(Parse.Numeric)
            .Or(Parse.Char('_')))
            .AtLeastOnce().Text().LineToken();

        /// <summary>
        /// A string of characters.
        /// </summary>
        public static readonly Parser<string> String =
            (from open in Parse.Char('"')
            from content in Parse.CharExcept('"').Many().Text()
            from close in Parse.Char('"')
            select content).LineToken();
            
        /// <summary>
        /// A compile-time constant value. A string or a number.
        /// </summary>
        public static readonly Parser<string> Constant =
            Parse.Number.Or(String);
            
        /// <summary>
        /// The arrow => operator used to denote a function.
        /// </summary>
        public static readonly Parser<string> FunctionOperator =
            Parse.String("=>").Text().LineToken();
            
        /// <summary>
        /// The = operator to assign new types.
        /// </summary>
        public static readonly Parser<string> AssignOperator =
            Parse.String("=").Text().LineToken();
            
        /// <summary>
        /// A line break or comma, used to separate statements.
        /// </summary>
        public static readonly Parser<string> Break =
            from br in Parse.String(",").Text().Or(Parse.LineEnd)
            from tail in Parse.LineEnd.Many().Optional()
            select br;
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Tuple> Tuple =
            from open in Parse.Char('(').Token()
            from statements in Statement.DelimitedBy(Break).Optional()
            from close in Parse.Char(')').Token()
            select new Tuple(statements.GetOrElse(Enumerable.Empty<Statement>()));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Object> Object =
            from open in Parse.Char('{').Token()
            from statements in Statement.DelimitedBy(Break).Optional()
            from close in Parse.Char('}').Token()
            select new Object(statements.GetOrElse(Enumerable.Empty<Statement>()));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Sequence> Sequence =
            from open in Parse.Char('[').Token()
            from statements in Statement.DelimitedBy(Break).Optional()
            from close in Parse.Char(']').Token()
            select new Sequence(statements.GetOrElse(Enumerable.Empty<Statement>()));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Assignee> Assignee =
            Identifier.Select(identifier => new Assignee(identifier))
            .Or(Object.Select(@object => new Assignee(@object)))
            .Or(Tuple.Select(tuple => new Assignee(tuple)));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Execution> Execution =
            Identifier.Select(identifier => new Execution(identifier))
            .Or(Constant.Select(constant => new Execution(constant)))
            .Or(Object.Select(@object => new Execution(@object)))
            .Or(Tuple.Select(tuple => new Execution(tuple)));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Function> Function =
            from assignee in Assignee
            from op in FunctionOperator
            from execution in Execution
            select new Function(assignee, execution);
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Assignment> Assignment =
            from assignee in Assignee
            from op in AssignOperator
            from execution in Execution
            select new Assignment(assignee, execution);
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Statement> Statement =
            Parse.Or<Statement>(Assignment, Execution);

        /// <summary>
        /// The whole document.
        /// </summary>
        public static readonly Parser<Document> Document =
            Statement.DelimitedBy(Break).Select(statements => new Document(statements)).End();
    }
}