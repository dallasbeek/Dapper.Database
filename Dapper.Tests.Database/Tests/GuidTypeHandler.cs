using System;
using System.Data;
using System.Linq;
using System.Text;

namespace Dapper.Tests.Database
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            switch (value)
            {
                case string s:
                    return new Guid(s);
                case Guid g:
                    return g;
                case byte[] b when b.Length == 16:
                    return new Guid(b);
                case byte[] b when b.Length == 17:
                    // Hack for Oracle to distinguish how to parse Guids
                    // by setting the db type to raw(17)
                    return new Guid(b.Skip(1).ToArray());
                case byte[] b when b.Length == 36:
                    // It's probably a string stored as binary.
                    // Because UTF-8, Latin1, etc. all use ASCII as a base, and only ASCII characters are involved,
                    // convert it from ASCII.
                    return new Guid(Encoding.ASCII.GetString(b));
                default:
                    // ReSharper disable once PossibleInvalidCastException - use the built-in message.
                    return (Guid)value;
            }
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value;
        }
    }


    public class NumericTypeHandler : SqlMapper.TypeHandler<decimal>
    {
        public override decimal Parse(object value)
        {
            return Convert.ToDecimal(value);
        }

        public override void SetValue(IDbDataParameter parameter, decimal value)
        {
            parameter.Value = value;
        }
    }
}
