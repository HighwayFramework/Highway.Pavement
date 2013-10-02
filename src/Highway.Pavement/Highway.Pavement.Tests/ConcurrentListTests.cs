using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Highway.Pavement.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Highway.Pavement.Tests
{
    [TestClass]
    public class ConcurrentListTests
    {
        [TestMethod]
        public void ShouldAllowAddsFromSingleThreads()
        {
            var list2 = new ConcurrentList<int>();
            DoWork(list2, 10000);
            var c2 = list2.Count; // accesses list, update performed

            Assert.AreEqual(10000, c2);

        }

        [TestMethod]
        public void ShouldAllowAddsFromMultipleThreads()
        {
            //arrange 
            var list3 = new ConcurrentList<int>();
            var tasks3 = new Task[4]
              {
                  Task.Factory.StartNew(() => DoWork(list3,2500)),
                  Task.Factory.StartNew(() => DoWork(list3,2500)),
                  Task.Factory.StartNew(() => DoWork(list3,2500)),
                  Task.Factory.StartNew(() => DoWork(list3,2500))
              };
            Task.WaitAll(tasks3);
            
            //act
            var c3 = list3.Count; // accesses list, update performed
            

            //assert
            Assert.AreEqual(10000,c3);
        }

        private void DoWork(ICollection<int> list, int count)
        {
            for (var i = 0; i < count; i++)
            {
                list.Add(i);
                // use spinwait to emulate work but avoiding 
                // context switching
                Thread.SpinWait(100000);
            }
        }
    }
}