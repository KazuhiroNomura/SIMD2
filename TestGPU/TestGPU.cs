using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alea;
using Alea.Parallel;
namespace TestGPU {
    [TestClass]
    public class TestGPU {
        [TestMethod]
        public void GPU1() {
            var GPU = Gpu.Default;
            GPU.For(0,100,i => {
                Console.WriteLine("{0}",i);
            });
            Console.WriteLine("\n完了!");
        }
    }
}
