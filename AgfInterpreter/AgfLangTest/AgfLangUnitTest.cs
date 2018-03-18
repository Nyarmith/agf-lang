using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AgfLang;

namespace AgfLangTest
{
    [TestClass]
    public class AgfLangUnitTest
    {
        private AgfInterpreter getFreshInterp()
        {
            Dictionary<string, Dictionary<string, int>> mem = new Dictionary<string, Dictionary<string, int>>();
            return new AgfInterpreter(ref mem);
        }

        [TestMethod]
        public void ArithmeticTest()
        {
            AgfInterpreter interp = getFreshInterp();
            Assert.AreEqual( interp.eval( "2*2" ), "4" );
            Assert.AreEqual( interp.eval( "2+3" ), "5" );
            Assert.AreEqual( interp.eval( "2-4" ),"-2" );
            Assert.AreEqual( interp.eval( "5/2" ), "2");
            Assert.AreEqual( interp.eval( "4*5-19+3/2" ), "2");
        }
        [TestMethod]
        public void ConditionalTest()
        {
            AgfInterpreter interp = getFreshInterp();
            Assert.AreEqual( interp.eval( "5==5" ), "1");
            Assert.AreEqual( interp.eval( "5==55"), "0");
            Assert.AreEqual( interp.eval( "5!=5" ), "0");
            Assert.AreEqual( interp.eval( "5!=3" ), "1");
            Assert.AreEqual( interp.eval( "5<9"  ), "1");
            Assert.AreEqual( interp.eval( "9<9"  ), "0");
            Assert.AreEqual( interp.eval( "9>9"  ), "0");
            Assert.AreEqual( interp.eval( "10>9" ), "1");
            Assert.AreEqual( interp.eval( "5<=9" ), "1");
            Assert.AreEqual( interp.eval( "9<=9" ), "1");
            Assert.AreEqual( interp.eval( "9>=9" ), "1");
            Assert.AreEqual( interp.eval( "8>=9" ), "0");
        }
        [TestMethod]
        public void AssignmentTest()
        {
            AgfInterpreter interp = getFreshInterp();
            interp.exec("ayy::lmao = 23");
            Assert.AreEqual( interp.eval( "ayy::lmao"), "23" );

            interp.exec("ayy::help = ayy::lmao - 20");
            Assert.AreEqual( interp.eval( "ayy::help"), "3" );

            interp.exec("ayy::help += 20");
            Assert.AreEqual( interp.eval( "ayy::lmao"), "23" );

            interp.exec("a::new += ayy::lmao - 3");
            Assert.AreEqual( interp.eval( "a::new"), "20" );

            interp.exec("b::c = 90; c::d = 50");
            Assert.AreEqual( interp.eval( "b::c"), "90" );
            Assert.AreEqual( interp.eval( "c::d"), "50" );

            interp.exec("a::new *= 5; pls::work /= 5; pls::work += 4;");
            Assert.AreEqual( interp.eval( "a::new"),  "100" );
            Assert.AreEqual( interp.eval( "pls::work"), "4" );
        }
        [TestMethod]
        public void NestingTest()
        {
            AgfInterpreter interp = getFreshInterp();
            Assert.AreEqual( interp.eval( "5==5 && 2>1" ), "1");
            Assert.AreEqual( interp.eval( "5==5 && 1<1" ), "0");
            Assert.AreEqual( interp.eval( "5==5 && 2>1 && 50 == 50"), "1");
            Assert.AreEqual( interp.eval( "5==4 || 5>3 && 50 == 50"), "1");
            Assert.AreEqual( interp.eval( "5==4 || 5<3 && 50 == 50"), "0");
            Assert.AreEqual( interp.eval( "5==4 && 2>1 || 51 <= 50"), "0");
            Assert.AreEqual( interp.eval( "5==5 && 2>1 || 51 <= 50"), "1");
        }
        [TestMethod]
        public void AdvancedTest()
        {
            AgfInterpreter interp = getFreshInterp();
            interp.exec("a::new=4+3;a::new *= 5; pls::work /= 5; pls::work += 4*2;");
            Assert.AreEqual( interp.eval( "a::new" ),   "35");
            Assert.AreEqual( interp.eval( "pls::work" ), "8");
            Assert.AreEqual( interp.eval( "30-(a::typo*23-pls::work*2)" ), "46");
            interp.exec("a::typo -= pls::work*2 + 4; new::guy = a::typo*-1");
            Assert.AreEqual(interp.eval("a::typo - new::guy"), "-40");
            //more could be added
        }
    }
}
