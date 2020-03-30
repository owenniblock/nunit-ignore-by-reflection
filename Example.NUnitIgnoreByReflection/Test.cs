using Example.ReflectionHelper;
using NUnit.Framework;
using System;
namespace Example.NUnitIgnoreByReflection
{
    [TestFixture("Run")]
    [TestFixture("Ignore")]
    [TestFixture("Inconclusive")]
    [TestFixture("IgnoreByReflection")]
    [TestFixture("InconclusiveByReflection")]
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
                case "Ignore":
                    Assert.Ignore("Ignored directly :-)");
                    break;
                case "Inconclusive":
                    Assert.Inconclusive("Who knows what happened here?");
                    break;
                case "IgnoreByReflection":
                    methodInvoker.Invoke("Ignore", "I am ignoring you, but not directly :-p");
                    break;
                case "InconclusiveByReflection":
                    methodInvoker.Invoke("Inconclusive", "Results are inconclusive at this time :-o");
                    break;
                default:
                    break;
            }
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            // Whereas this code fails for the Reflection cases (SetUp called before each test).
            //var methodInvoker = new MethodInvoker();

            //switch (_ignoreOrInconclusive)
            //{
            //    case "Ignore":
            //        Assert.Ignore("Ignored directly :-)");
            //        break;
            //    case "Inconclusive":
            //        Assert.Inconclusive("Who knows what happened here?");
            //        break;
            //    case "IgnoreByReflection":
            //        methodInvoker.Invoke("Ignore", "I am ignoring you, but not directly :-p");
            //        break;
            //    case "InconclusiveByReflection":
            //        methodInvoker.Invoke("Inconclusive", "Results are inconclusive at this time :-o");
            //        break;
            //    default:
            //        break;
            //}
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
