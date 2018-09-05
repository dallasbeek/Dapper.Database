using System;
using System.Data;
using System.Text;
using System.Linq;
#if !NETSTANDARD1_3 && !NETCOREAPP2_0 && !NETCOREAPP1_0
using Oracle.ManagedDataAccess.Client;
#endif

namespace Dapper.Tests.Database
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            if (value is string)
            {
                return new Guid(value as string);
            }

            if (value is byte[] b)
            {
                // Hack for Oracle to distinguish how to parse Guids
                // by setting the db type to raw(17)
                if (b.Length == 17)
                {
                    return new Guid(b.Skip(1).ToArray());
                }

                byte[] outVal = new byte[] { b[3], b[2], b[1], b[0], b[5], b[4], b[7], b[6], b[8], b[9], b[10], b[11], b[12], b[13], b[14], b[15] };
                return new Guid(outVal);
            }
            return (Guid)value;

        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
#if !NETSTANDARD1_3 && !NETCOREAPP1_0 && !NETCOREAPP2_0
            if (parameter is OracleParameter)
            {
                var oracleParameter = (OracleParameter)parameter;
                oracleParameter.OracleDbType = OracleDbType.Raw;
                // Hack for Oracle to distinguish how to parse Guids
                // by setting the db type to raw(17)
                var b = new byte[17];
                Array.Copy(value.ToByteArray(), 0, b, 1, 16);
                parameter.Value = b;
            }
            else
            {
                parameter.Value = value;
            }
#else
                parameter.Value = value;
#endif
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
