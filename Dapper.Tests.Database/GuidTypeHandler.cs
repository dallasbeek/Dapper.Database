using System;
using System.Collections.Generic;
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
            return (Guid)value;
        }

        public override void SetValue(System.Data.IDbDataParameter parameter, Guid value)
        {
            var inVal = value.ToByteArray();
            byte[] outVal = new byte[] { inVal[3], inVal[2], inVal[1], inVal[0], inVal[5], inVal[4], inVal[7], inVal[6], inVal[8], inVal[9], inVal[10], inVal[11], inVal[12], inVal[13], inVal[14], inVal[15] };
            parameter.Value = outVal;
        }
    }
}
