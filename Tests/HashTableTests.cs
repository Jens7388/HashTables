

using Library;

using System;

using Xunit;

namespace Tests
{
    public class HashTableTests
    {

        [Fact]
        public void TestCreation()
        {
            HashTable<int, string> hashTable = new(100);
        }

        [Fact]
        public void TestAddition()
        {
            HashTable<int, string> hashTable = new(100);
            hashTable.Add(10, "abcd");
        }

        [Fact]
        public void TestAdditionOnMaxLoad()
        {
            HashTable<string, int> hashTable = new(10);
            hashTable.Add("a", 1);
            hashTable.Add("ab", 2);
            hashTable.Add("abc", 3);
            hashTable.Add("abcd", 4);
            hashTable.Add("abcde", 5);
            hashTable.Add("abcdef", 6);
            hashTable.Add("abcdefg", 7);
            hashTable.Add("abcdefgh", 8);
            hashTable.Add("abcdefghi", 9);
            hashTable.Add("abcdefghij", 10);
        }

        [Fact]
        public void TestLookup()
        {
            HashTable<string, int> hashTable = new(10);
            hashTable.Add("a", 1);
            Assert.True(hashTable.LookUp("a").Value != default);
        }
    }
}
