using System;
using System.Data;
using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Dapper.Database.Tests;

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        return value switch
        {
            string s => new Guid(s),
            Guid g => g,
            byte[] { Length: 16 } b => new Guid(b),
            byte[] { Length: 17 } b =>
                // Hack for Oracle to distinguish how to parse Guids
                // by setting the db type to raw(17)
                new Guid(b.Skip(1).ToArray()),
            byte[] { Length: 36 } b =>
                // It's probably a string stored as binary.
                // Because UTF-8, Latin1, etc. all use ASCII as a base, and only ASCII characters are involved,
                // convert it from ASCII.
                new Guid(Encoding.ASCII.GetString(b)),
            // ReSharper disable once PossibleInvalidCastException
            _ => (Guid)value
        };
    }

    public override void SetValue(IDbDataParameter parameter, Guid value) => parameter.Value = value;
}

public class NumericTypeHandler : SqlMapper.TypeHandler<decimal>
{
    public override decimal Parse(object value) => Convert.ToDecimal(value);

    public override void SetValue(IDbDataParameter parameter, decimal value) => parameter.Value = value;
}
