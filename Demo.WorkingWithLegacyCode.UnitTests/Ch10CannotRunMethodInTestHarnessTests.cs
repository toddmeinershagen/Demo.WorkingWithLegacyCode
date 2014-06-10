using System;
using NUnit.Framework;

namespace Demo.WorkingWithLegacyCode.UnitTests
{
        [TestFixture]
        public class Ch10CannotRunMethodInTestHarnessTests
        {
            [Test]
            public void given_hidden_method_run_a_test()
            {
                new DerivedClassWithHiddenMethod().HiddenMethod();
            }

            [Test]
            public void given_long_method_run_a_test()
            {
                new DerivedClassWithHiddenMethod().LongMethod();
            }
        }

        public class ClassWithHiddenMethod
        {
            public void LongMethod()
            {
                ProcessAPartMethod();
            }

            protected void HiddenMethod()
            {
                Console.WriteLine("This is a hidden method.");
            }

            protected virtual void ProcessAPartMethod()
            {
                Console.WriteLine("This is the original part method.");
            }

        }

        public class DerivedClassWithHiddenMethod : ClassWithHiddenMethod
        {
            public new void HiddenMethod()
            {
                base.HiddenMethod();
            }

            protected override void ProcessAPartMethod()
            {
                Console.WriteLine("This is an overridden base method used as an object seam.");
            }
        }
    }
