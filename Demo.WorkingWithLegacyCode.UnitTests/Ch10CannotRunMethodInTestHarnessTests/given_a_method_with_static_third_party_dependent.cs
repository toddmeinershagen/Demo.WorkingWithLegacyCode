using System;
using NUnit.Framework;

namespace Demo.WorkingWithLegacyCode.UnitTests.Ch10CannotRunMethodInTestHarnessTests
{
    [TestFixture]
    public class given_a_method_with_static_third_party_dependent
    {
        [Test]
        public void when_testing_can_use_skin_and_wrapper_method()
        {
            new ClassWithMethodWithStaticDependent().MethodWithStaticDependentWithMethodInjection(new FakeDateTimeProvider(new DateTime(2014, 1, 1)));   
        }
    }

    public class ClassWithMethodWithStaticDependent
    {
        public void MethodWithStaticDependent()
        {
            if (DateTime.Now == new DateTime(2014, 1, 1))
                Console.WriteLine("We have a matching date.");
        }

        public void MethodWithStaticDependentWithMethodInjection(IDateTimeProvider provider)
        {
            if (provider.GetCurrentDateTime() == new DateTime(2014, 1, 1))
                Console.WriteLine("We have a matching date.");
        }
    }

    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTime();
    }

    public class DateTimeWrapper : IDateTimeProvider
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }

    public class FakeDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _datetimeToReturn;

        public FakeDateTimeProvider(DateTime datetimeToReturn)
        {
            _datetimeToReturn = datetimeToReturn;
        }

        public DateTime GetCurrentDateTime()
        {
            return _datetimeToReturn;
        }
    }
}
