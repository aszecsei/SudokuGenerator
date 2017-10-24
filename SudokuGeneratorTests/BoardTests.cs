using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGenerator.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void NoDuplicatesTest()
        {
            Board b = new Board(4);
            Assert.IsTrue(b.NoDuplicates(new int[] { 0, 0, 1, 2 }));
            Assert.IsFalse(b.NoDuplicates(new int[] { 1, 1, 0, 2 }));
        }
    }
}