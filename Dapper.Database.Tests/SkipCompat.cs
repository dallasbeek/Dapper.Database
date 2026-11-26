using System;
using System.Runtime.CompilerServices;

// Xunit.SkippableFact targets xunit v2. xunit v3 supports dynamic skipping natively
// (Assert.SkipWhen / Assert.SkipUnless), so these shims keep the existing
// [SkippableFact] and Skip.If(...) call sites working on top of it.
#pragma warning disable IDE0130
namespace Xunit;
#pragma warning restore IDE0130

/// <summary>A <see cref="FactAttribute" /> whose body may call <see cref="Skip" /> to skip itself at runtime.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class SkippableFactAttribute(
    [CallerFilePath] string sourceFilePath = null,
    [CallerLineNumber] int sourceLineNumber = -1) : FactAttribute(sourceFilePath, sourceLineNumber);

public static class Skip
{
    public static void If(bool condition, string reason) => Assert.SkipWhen(condition, reason);

    public static void IfNot(bool condition, string reason) => Assert.SkipUnless(condition, reason);
}
