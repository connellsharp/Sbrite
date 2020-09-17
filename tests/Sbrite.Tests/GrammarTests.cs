using System;
using System.Linq;
using Sprache;
using Xunit;

namespace Sbrite.Tests
{
    public class GrammarTests
    {
        [Fact]
        public void IdentifierCanHaveSimpleName()
        {
            var input = "name";
            var id = Grammar.Identifier.Parse(input);
            Assert.Equal("name", id);
        }

        [Fact]
        public void IdentifierDoesNotIncludeSpace()
        {
            var input = "a b";
            var parsed = Grammar.Identifier.Parse(input);
            Assert.Equal("a", parsed);
        }

        [Fact]
        public void EmptyStringIsEmpty()
        {
            var input = "\"\"";
            var parsed = Grammar.String.Parse(input);
            Assert.Empty(parsed);
        }

        [Fact]
        public void StringCanContainCharacters()
        {
            var input = "\"abcdefghijklmnopqrstuvwxyz\"";
            var parsed = Grammar.String.Parse(input);
            Assert.Equal(26, parsed.Length);
        }

        [Fact]
        public void NumberIsConstant()
        {
            var input = "123";
            var parsed = Grammar.Constant.Parse(input);
            Assert.Equal(3, parsed.Length);
        }

        [Fact]
        public void StringIsConstant()
        {
            var input = "\"abc\"";
            var parsed = Grammar.Constant.Parse(input);
            Assert.Equal(3, parsed.Length);
        }

        [Fact]
        public void FunctionCanHaveIdentifierAssignee()
        {
            var input = "a => b";
            var parsed = Grammar.Function.Parse(input);
            Assert.Equal("a", parsed.Assignee.Identifier);
        }

        [Fact]
        public void FunctionCanHaveIdentifierExecution()
        {
            var input = "a => b";
            var parsed = Grammar.Function.Parse(input);
            Assert.Equal("b", parsed.Execution.Identifier);
        }

        [Fact]
        public void EmptyObjectContainsNoStatements()
        {
            var input = "{}";
            var parsed = Grammar.Object.Parse(input);
            Assert.Empty(parsed.Statements);
        }

        [Fact]
        public void EmptyObjectWithSpaceContainsNoStatements()
        {
            var input = "{ }";
            var parsed = Grammar.Object.Parse(input);
            Assert.Empty(parsed.Statements);
        }

        [Fact]
        public void EmptyObjectWithMoreWhitespaceContainsNoStatements()
        {
            var input = "{       }";
            var parsed = Grammar.Object.Parse(input);
            Assert.Empty(parsed.Statements);
        }

        [Fact]
        public void ObjectCanContainManyAssignmentsWithComma()
        {
            var input = "{ a = b, c = d }";
            var parsed = Grammar.Object.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void ObjectCanContainSingleAssignmentOnNewLine()
        {
            var input = "{\na = b\n}";
            var parsed = Grammar.Object.Parse(input);
            Assert.Equal(1, parsed.Statements.Count());
        }
        [Fact]
        public void ObjectCanContainManyAssignmentsOnNewLinesWithComma()
        {
            var input = "{\na = b,\nc = d\n}";
            var parsed = Grammar.Object.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void ObjectCanContainManyAssignmentsOnNewLines()
        {
            var input = "{\na = b\nc = d\n}";
            var parsed = Grammar.Object.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void ObjectCanContainManyAssignmentsOnNewLinesWithBlankLines()
        {
            var input = "{\n\na = b\n\n\nc = d\n}";
            var parsed = Grammar.Object.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void ObjectCanContainManyAssignmentsOnNewLinesWithSpaces()
        {
            var input = @"{
                a = b
                c = d
            }";
            var parsed = Grammar.Object.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void EmptyTupleContainsNoStatements()
        {
            var input = "()";
            var parsed = Grammar.Tuple.Parse(input);
            Assert.Empty(parsed.Statements);
        }

        [Fact]
        public void EmptyTupleWithWhitespaceContainsNoStatements()
        {
            var input = "( )";
            var parsed = Grammar.Tuple.Parse(input);
            Assert.Empty(parsed.Statements);
        }

        [Fact]
        public void TupleCanHaveSingleStatement()
        {
            var input = "(a)";
            var parsed = Grammar.Tuple.Parse(input);
            Assert.Single(parsed.Statements);
        }

        [Fact]
        public void TupleCanHaveTwoStatementsWithComma()
        {
            var input = "( a, b )";
            var parsed = Grammar.Tuple.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void TupleCanHaveTwoNumbersWithComma()
        {
            var input = "( 1, 2 )";
            var parsed = Grammar.Tuple.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void TupleCanHaveTwoStatementsOnNewLines()
        {
            var input = "(\nfirst\nsecond\n)";
            var parsed = Grammar.Tuple.Parse(input);
            Assert.Equal(2, parsed.Statements.Count());
        }

        [Fact]
        public void FunctionCanReturnEmptyObject()
        {
            var input = "a => {}";
            var parsed = Grammar.Function.Parse(input);
            Assert.NotNull(parsed.Execution.Object);
        }

        [Fact]
        public void FunctionCanReturnEmptyTuple()
        {
            var input = "a => ()";
            var parsed = Grammar.Function.Parse(input);
            Assert.NotNull(parsed.Execution.Tuple);
        }

        [Fact]
        public void CanAssignAlias()
        {
            var input = "a = b";
            var parsed = Grammar.Assignment.Parse(input);
            Assert.Equal("b", parsed.Execution.Identifier);
        }

        [Fact]
        public void CanAssignEmptyObject()
        {
            var input = "a = {}";
            var parsed = Grammar.Assignment.Parse(input);
            Assert.NotNull(parsed.Execution.Object);
        }
    }
}
