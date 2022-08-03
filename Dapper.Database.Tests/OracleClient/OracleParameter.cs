using System;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Types;
using RealOracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;

namespace Dapper.Database.Tests.OracleClient;

public class OracleParameter : DbParameter
{
    /// <summary>
    ///     The nominal DbType.
    /// </summary>
    private DbType _dbType;

    internal OracleParameter(RealOracleParameter parameter) =>
        RealParameter = parameter ?? throw new ArgumentNullException(nameof(parameter));

    internal RealOracleParameter RealParameter { get; }

    public override DbType DbType
    {
        get => _dbType;
        set
        {
            if (_dbType == value) return;

            _dbType = value;
            switch (_dbType)
            {
                case DbType.Guid:
                    // Oracle does not support DbType.Guid.
                    // Convention is to use binary (endianness up to the TypeHandler).
                    RealParameter.DbType = DbType.Binary;
                    break;
                default:
                    // Let Oracle sort the rest out
                    RealParameter.DbType = _dbType;
                    break;
            }
        }
    }

    public override ParameterDirection Direction
    {
        get => RealParameter.Direction;
        set => RealParameter.Direction = value;
    }

    public override bool IsNullable
    {
        get => RealParameter.IsNullable;
        set => RealParameter.IsNullable = value;
    }

    public override string ParameterName
    {
        get => RealParameter.ParameterName;
        set => RealParameter.ParameterName = value;
    }

    public override string SourceColumn
    {
        get => RealParameter.SourceColumn;
        set => RealParameter.SourceColumn = value;
    }

    public override object Value
    {
        get
        {
            var realDbType = RealParameter.DbType;
            if (_dbType != DbType.Guid || realDbType != DbType.Binary) return RealParameter.Value;
            return RealParameter.Value switch
            {
                null => null,
                DBNull _ => null,
                byte[] b => new Guid(b),
                OracleBinary b => (b.IsNull ? null : new Guid(b.Value)),
                _ => RealParameter.Value
            };
        }
        set
        {
            RealParameter.Value = value switch
            {
                Guid guid =>
                    // Oracle does not like Guids.
                    guid.ToByteArray(),
                _ => value
            };
        }
    }

    public override bool SourceColumnNullMapping
    {
        get => RealParameter.SourceColumnNullMapping;
        set => RealParameter.SourceColumnNullMapping = value;
    }

    public override int Size
    {
        get => RealParameter.Size;
        set => RealParameter.Size = value;
    }

    public override byte Precision
    {
        get => RealParameter.Precision;
        set => RealParameter.Precision = value;
    }

    public override byte Scale
    {
        get => RealParameter.Scale;
        set => RealParameter.Scale = value;
    }

    public override DataRowVersion SourceVersion
    {
        get => RealParameter.SourceVersion;
        set => RealParameter.SourceVersion = value;
    }

    public override void ResetDbType() => RealParameter.ResetDbType();
}
