using Example.ReflectionHelper;
using NUnit.Framework;
using System;
namespace Example.NUnitIgnoreByReflection
{
    [TestFixture("Run")]
    [TestFixture("Ignore_Constructor")]
    [TestFixture("Inconclusive_Constructor")]
    [TestFixture("IgnoreByReflection_Constructor")]
    [TestFixture("InconclusiveByReflection_Constructor")]
    [TestFixture("Ignore_SetUp")]
    [TestFixture("Inconclusive_SetUp")]
    [TestFixture("IgnoreByReflection_SetUp")]
    [TestFixture("InconclusiveByReflection_SetUp")]
    public class Test
    {
        private string _ignoreOrInconclusive;

        public Test(string ignoreOrInconclusive)
        {
            _ignoreOrInconclusive = ignoreOrInconclusive;

            // The code works when we call it in this method (construction of the Test Class).
            var methodInvoker = new MethodInvoker();

            switch (_ignoreOrInconclusive)
            {
                case "Ignore_Constructor":
                    Assert.Ignore("Constructor: Ignored directly :-)");
                    break;
                case "Inconclusive_Constructor":
                    Assert.Inconclusive("Constructor: Who knows what happened here?");
                    break;
                case "IgnoreByReflection_Constructor":
                    methodInvoker.Ignore("Constructor: I am ignoring you, but not directly :-p", null);
                    break;
                case "InconclusiveByReflection_Constructor":
                    methodInvoker.Inconclusive("Constructor: Results are inconclusive at this time :-o", null);
                    break;
                default:
                    break;
            }
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            // Whereas this code fails for the Reflection cases (SetUp called before each test).
            var methodInvoker = new MethodInvoker();

            switch (_ignoreOrInconclusive)
            {
                case "Ignore_SetUp":
                    Assert.Ignore("SetUp: Ignored directly :-)");
                    break;
                case "Inconclusive_SetUp":
                    Assert.Inconclusive("SetUp: Who knows what happened here?");
                    break;
                case "IgnoreByReflection_SetUp":
                    methodInvoker.Ignore("Constructor: I am ignoring you, but not directly :-p", null);
                    break;
                case "InconclusiveByReflection_SetUp":
                    methodInvoker.Inconclusive("Constructor: Results are inconclusive at this time :-o", null);
                    break;
                default:
                    break;
            }
        }

        [Test()]
        public void PassingTestCase()
        {
            Assert.AreEqual(1, 1);
        }

        [Test()]
        public void FailingTestCase()
        {
            Assert.AreEqual(1, 2);
        }
    }
}
