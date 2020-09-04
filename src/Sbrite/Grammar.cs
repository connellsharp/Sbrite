using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace Sbrite
{
    public static class Grammar
    {
        /// <summary>
        /// A type name or alias.
        /// </summary>
        public static readonly Parser<string> Identifier =
            (Parse.Letter
            .Or(Parse.Numeric)
            .Or(Parse.Char('_')))
            .AtLeastOnce().Text().Token();

        /// <summary>
        /// A string of characters.
        /// </summary>
        public static readonly Parser<string> String =
            (from open in Parse.Char('"')
            from content in Parse.CharExcept('"').Many().Text()
            from close in Parse.Char('"')
            select content).Token();
            
        /// <summary>
        /// A compile-time constant value. A string or a number.
        /// </summary>
        public static readonly Parser<string> Constant =
            Parse.Number.Or(String);
            
        /// <summary>
        /// The arrow => operator used to denote a function.
        /// </summary>
        public static readonly Parser<string> FunctionOperator =
            Parse.String("=>").Text().Token();
            
        /// <summary>
        /// The = operator to assign new types.
        /// </summary>
        public static readonly Parser<string> AssignOperator =
            Parse.String("=").Text().Token();
            
        /// <summary>
        /// A line break or comma, used to separate statements.
        /// </summary>
        public static readonly Parser<string> Break =
            Parse.Or(Parse.LineEnd, Parse.String(",").Text());
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Tuple> Tuple =
            from open in Parse.Char('(')
            from openWhitespace in Parse.WhiteSpace.Optional()
            from statements in Statement.DelimitedBy(Break).Optional()
            from closeWhitespace in Parse.WhiteSpace.Optional()
            from close in Parse.Char(')')
            select new Tuple(statements.GetOrElse(Enumerable.Empty<Statement>()));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Object> Object =
            from open in Parse.Char('{')
            from openWhitespace in Parse.WhiteSpace.Optional()
            from statements in Statement.DelimitedBy(Break).Optional()
            from closeWhitespace in Parse.WhiteSpace.Optional()
            from close in Parse.Char('}')
            select new Object(statements.GetOrElse(Enumerable.Empty<Statement>()));
            
        /// <summary>
        /// 
        /// </summary>
        public static readonly Parser<Sequence> Sequence =
            from open in Parse.Char('[')
            from openWhitespace in Parse.WhiteSpace.Optional()
            from statements in Statement.DelimitedBy(Break).Optional()
            from closeWhitespace in Parse.WhiteSpace.Optional()
            from close in Parse.Char(']')
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
            Statement.Many().Select(statements => new Document(statements)).End();
    }

    public class Document
    {
        public Document(IEnumerable<Statement> statements)
        {
            Statements = statements;
        }

        public IEnumerable<Statement> Statements { get; }
    }

    public class Function
    {
        public Function(Assignee assignee, Execution execution)
        {
            Assignee = assignee;
            Execution = execution;
        }

        public Assignee Assignee { get; }
        public Execution Execution { get; }
    }

    public class Assignee
    {
        public Assignee(string identifier)
        {
            Identifier = identifier;
        }

        public Assignee(Object @object)
        {
            Object = @object;
        }

        public Assignee(Tuple tuple)
        {
            Tuple = tuple;
        }

        public string Identifier { get; }
        public Object Object { get; }
        public Tuple Tuple { get; }
    }

    public class Execution : Statement
    {
        public Execution(string identifier)
        {
            Identifier = identifier;
        }

        public Execution(Object @object)
        {
            Object = @object;
        }

        public Execution(Tuple tuple)
        {
            Tuple = tuple;
        }

        public string Identifier { get; }
        public Object Object { get; }
        public Tuple Tuple { get; }
    }

    public class Assignment : Statement
    {
        public Assignment(Assignee assignee, Execution execution)
        {
            Assignee = assignee;
            Execution = execution;
        }

        public Assignee Assignee { get; internal set; }
        public Execution Execution { get; internal set; }
    }

    public class Statement
    {

    }

    public class Block
    {
        public IEnumerable<Statement> Statements { get; protected set; }
    }

    public class Tuple : Block
    {
        public Tuple(IEnumerable<Statement> statements)
        {
            Statements = statements;
        }
    }

    public class Object : Block
    {
        public Object(IEnumerable<Statement> statements)
        {
            Statements = statements;
        }
    }

    public class Sequence : Block
    {
        public Sequence(IEnumerable<Statement> statements)
        {
            Statements = statements;
        }
    }
}