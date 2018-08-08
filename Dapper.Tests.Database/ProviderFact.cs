using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Dapper.Tests.Database
{
    public enum Provider
    {
        SqlServer,
        SqlCE,
        SQLite,
        MySql,
        Postgres

    }

    //public class SkipTestException : Exception
    //{
    //    public SkipTestException(string reason) : base(reason)
    //    {
    //    }
    //}

    //public class ProviderAttribute : Attribute
    //{
    //    public ProviderAttribute(Provider provider)
    //    {
    //        Provider = provider;
    //    }

    //    public Provider Provider { get; set; }
    //}

    //[XunitTestCaseDiscoverer("Dapper.Tests.Database.FactDiscoverer", "Dapper.Tests.Database")]
    //[AttributeUsage(AttributeTargets.Method)]
    //public class FactAttributeAttribute : FactAttribute
    //{

    //    public FactAttributeAttribute(params Provider[] ignoreProviders)
    //    {
    //        IgnoredProviders = ignoreProviders;
    //    }

    //    public Provider[] IgnoredProviders { get; set; }
    //}


    //public class FactDiscoverer : IXunitTestCaseDiscoverer
    //{
    //    readonly IMessageSink diagnosticMessageSink;

    //    public FactDiscoverer(IMessageSink diagnosticMessageSink)
    //    {
    //        this.diagnosticMessageSink = diagnosticMessageSink;
    //    }

    //    public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
    //    {
    //        var ignoredProviders = factAttribute.GetNamedArgument<Provider[]>("IgnoredProviders");

    //        if (ignoredProviders.Any())
    //        {
    //            var traits = testMethod.TestClass.Class.GetCustomAttributes(typeof(TraitAttribute));

    //            var providerAtt = testMethod.TestClass.Class.GetCustomAttributes(typeof(ProviderAttribute)).SingleOrDefault();

    //            if (providerAtt != null)
    //            {
    //                if (ignoredProviders.Contains(providerAtt.GetNamedArgument<Provider>("Provider")))
    //                {
    //                    yield return new SkipTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod);
    //                }
    //            }
    //        }

    //        yield return new XunitTestCase(diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod);
    //    }
    //}

    //[Serializable]
    //public class SkipTestCase : XunitTestCase
    //{
    //    public SkipTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay testMethodDisplay, ITestMethod testMethod)
    //        : base(diagnosticMessageSink, testMethodDisplay, testMethod)
    //    {
    //        SkipReason = "Provider Skipped";
    //    }

    //}
}
