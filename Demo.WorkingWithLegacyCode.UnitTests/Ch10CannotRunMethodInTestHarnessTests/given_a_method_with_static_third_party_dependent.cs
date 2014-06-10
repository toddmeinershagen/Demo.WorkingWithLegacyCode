using System;
using System.Diagnostics.Eventing.Reader;
using NUnit.Framework;

namespace Demo.WorkingWithLegacyCode.UnitTests.Ch10CannotRunMethodInTestHarnessTests
{
    [TestFixture]
    public class given_a_method_with_static_third_party_dependent
    {
        [Test]
        public void when_testing_can_use_skin_and_wrapper_method()
        {
            new ClassWithMethodWithStaticDependentUsingMethodInjection().MethodWithStaticDependent(new FakeDateTimeProvider(new DateTime(2014, 1, 1)));
            
            new ClassWithMethodWithStaticDependentUsingPropertyInjection
            {
                GetCurrentDate = () => new DateTime(2014, 1, 1)
            }.MethodWithStaticDependent();

            new ClassWithMethodWithStaticDependentUsingConstructorInjection(new FakeDateTimeProvider(new DateTime(2014, 1, 1))).MethodWithStaticDependent();
        }
    }

    public class ClassWithMethodWithStaticDependentUsingMethodInjection
    {
        public void MethodWithStaticDependent()
        {
            MethodWithStaticDependent(new DateTimeWrapper());
        }

        public void MethodWithStaticDependent(IDateTimeProvider provider)
        {
            if (provider.GetCurrentDateTime() == new DateTime(2014, 1, 1))
                Console.WriteLine("We have a matching date.");
        }
    }

    public class ClassWithMethodWithStaticDependentUsingPropertyInjection
    {
        public ClassWithMethodWithStaticDependentUsingPropertyInjection()
        {
            GetCurrentDate = GetCurrentDateLocalDefault;
        }

        public void MethodWithStaticDependent()
        {
            if (GetCurrentDate() == new DateTime(2014, 1, 1))
                Console.WriteLine("We have a matching date.");
        }

        public Func<DateTime> GetCurrentDate { get; set; }

        private DateTime GetCurrentDateLocalDefault()
        {
            return DateTime.Now;
        }
    }

    public class ClassWithMethodWithStaticDependentUsingConstructorInjection
    {
        private readonly IDateTimeProvider _provider;

        public ClassWithMethodWithStaticDependentUsingConstructorInjection(IDateTimeProvider provider)
        {
            _provider = provider;
        }

        public void MethodWithStaticDependent()
        {
            if (_provider.GetCurrentDateTime() == new DateTime(2014, 1, 1))
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
