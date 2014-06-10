using System;
using NUnit.Framework;

namespace Demo.WorkingWithLegacyCode.UnitTests.Ch4SeamModel
{
    [TestFixture]
    public class given_a_long_method
    {
        [Test]
        public void when_testing_should_try_to_use_object_seams_to_isolate_code_under_test()
        {
            new DerivedClassWithObjectSeam().LongMethod();
        }

        public class ClassWithLongMethod
        {   
            public void LongMethod()
            {
                ProcessAPartMethod();
            }
            protected virtual void ProcessAPartMethod()
            {
                Console.WriteLine("This is the original part method.");
            }
        }

        public class DerivedClassWithObjectSeam : ClassWithLongMethod
        {
            protected override void ProcessAPartMethod()
            {
                Console.WriteLine("This is an overridden base method used as an object seam.");
            }
        }
    }
}
