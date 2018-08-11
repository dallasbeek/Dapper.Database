using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (value is byte[])
            {
                var inVal = (byte[])value;
                byte[] outVal = new byte[] { inVal[3], inVal[2], inVal[1], inVal[0], inVal[5], inVal[4], inVal[7], inVal[6], inVal[8], inVal[9], inVal[10], inVal[11], inVal[12], inVal[13], inVal[14], inVal[15] };
                return new Guid(outVal);
            }
            return (Guid)value;
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
