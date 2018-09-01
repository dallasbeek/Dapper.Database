using System;
using System.Data;
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
                case byte[] b:
                    switch (b.Length)
                    {
                        case 16:
                            return new Guid(b);
                        case 36:
                            // It's probably a string stored as binary.
                            // Because UTF-8, Latin1, etc. all use ASCII as a base, and only ASCII characters are involved,
                            // convert it from ASCII.
                            return new Guid(Encoding.ASCII.GetString(b));
                        default:
                            // ??
                            throw new ArgumentException($"Cannot parse byte array of length {b.Length} as a Guid.", nameof(value));
                    }
                default:
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
