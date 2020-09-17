using System.Collections.Generic;

namespace Sbrite
{
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