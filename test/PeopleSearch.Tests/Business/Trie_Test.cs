using System;
using System.Linq;
using Gma.DataStructures.StringSearch;
using Moq;
using Xunit;

namespace PeopleSearch.Tests.Business
{
    public class Trie_Test
    {
        [Fact]
        public void Trie_Find2SameRecords() 
        {
            Trie<int> subject = new Trie<int>();
            subject.Add("abc", 1);
            subject.Add("abc", 2);

            var result = subject.Retrieve("abc");

            Assert.Equal(result.Count(), 2);
        }

        [Fact]
        public void Trie_Find2SameRecordsWithPartialWord() 
        {
            Trie<int> subject = new Trie<int>();
            subject.Add("abc", 1);
            subject.Add("abc", 2);

            var result = subject.Retrieve("a");

            Assert.Equal(result.Count(), 2);
        }

        [Fact]
        public void Trie_DoNotFindWord() 
        {
            Trie<int> subject = new Trie<int>();
            subject.Add("abc", 1);
            subject.Add("abc", 2);

            var result = subject.Retrieve("z");

            Assert.Equal(result.Count(), 0);
        }

        [Fact]
        public void Trie_UseEmptyTrie() 
        {
            Trie<int> subject = new Trie<int>();

            var result = subject.Retrieve("z");

            Assert.Equal(result.Count(), 0);
        }

        [Fact]
        public void Trie_RetrieveNull() 
        {
            Trie<int> subject = new Trie<int>();

            Assert.Throws<NullReferenceException>(() => { 
                subject.Retrieve(null);
            });
        }
    }
}