using System;
using Sprache;
using Xunit;

namespace Sbrite.Tests
{
    public class GrammarTests
    {
        [Fact]
        public void Test1()
        {
            var input = "name";
            var id = Grammar.Identifier.Parse(input);
            Assert.Equal("name", id);
        }

        [Fact]
        public void AnIdentifierDoesNotIncludeSpace()
        {
            var input = "a b";
            var parsed = Grammar.Identifier.Parse(input);
            Assert.Equal("a", parsed);
        }
    }
}
