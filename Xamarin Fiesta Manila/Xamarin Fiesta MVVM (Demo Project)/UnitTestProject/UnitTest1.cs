using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmXF.ViewModels;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CalculatorDetailsVm calVm = new CalculatorDetailsVm();
            calVm.FirstNumber = 3;
            calVm.SecondNumber = 3;

            calVm.GetSumCommand.Execute(null);

            Assert.AreEqual(6, calVm.GetSum);
        }
    }
}
