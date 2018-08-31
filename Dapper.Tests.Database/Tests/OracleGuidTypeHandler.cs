using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if !NETSTANDARD1_3 && !NETCOREAPP2_0 && !NETCOREAPP1_0
using Oracle.ManagedDataAccess.Client;
#endif
namespace Dapper.Tests.Database
{
    public class OracleGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse( object value )
        {
            return new Guid((byte[]) value);
        }

        public override void SetValue( IDbDataParameter parameter, Guid value )
        {
#if !NETSTANDARD1_3 && !NETCOREAPP1_0 && !NETCOREAPP2_0
            if ( parameter is OracleParameter )
            {
                var oracleParameter = (OracleParameter) parameter;
                oracleParameter.OracleDbType = OracleDbType.Raw;
                parameter.Value = ((Guid) value);
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
}
