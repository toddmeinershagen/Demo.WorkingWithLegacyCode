using System;
using NUnit.Framework;

namespace Demo.WorkingWithLegacyCode.UnitTests
{
        [TestFixture]
        public class given_a_hidden_method
        {
            [Test]
            public void when_testing_can_use_a_derived_class_with_shadowing()
            {
                new DerivedClassWithShadowing().HiddenMethod();
            }
        }

        public class ClassWithHiddenMethod
        {
            protected void HiddenMethod()
            {
                Console.WriteLine("This is a hidden method.");
            }
        }

        public class DerivedClassWithShadowing : ClassWithHiddenMethod
        {
            public new void HiddenMethod()
            {
                base.HiddenMethod();
            }
        }
    }
