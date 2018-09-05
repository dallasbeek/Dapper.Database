using System;
using System.Collections.Generic;
using System.Data;
using Dapper.Database.Extensions;
using static Dapper.SqlMapper;

namespace Dapper.Database
{
    public partial class SqlDatabase : ISqlDatabase, IDisposable
    {
        #region Execute Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public int Execute(string sql)
        {
            return ExecuteInternal(() => _sharedConnection.Execute(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        public int Execute(string sql, object parameters)
        {
            return ExecuteInternal(() => _sharedConnection.Execute(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region Execute Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public T ExecuteScaler<T>(string sql)
        {
            return ExecuteInternal(() => _sharedConnection.ExecuteScalar<T>(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        public T ExecuteScaler<T>(string sql, object parameters)
        {
            return ExecuteInternal(() => _sharedConnection.ExecuteScalar<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region GetDataTable Methods
#if !NETSTANDARD1_3 && !NETCOREAPP1_0
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public DataTable GetDataTable(string sql)
        {
            return ExecuteInternal(() =>
            {
                var dt = new DataTable();
                dt.Load(_sharedConnection.ExecuteReader(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
                return dt;
            });
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        public DataTable GetDataTable(string sql, object parameters)
        {
            return ExecuteInternal(() =>
            {
                var dt = new DataTable();
                dt.Load(_sharedConnection.ExecuteReader(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
                return dt;
            });
        }
#endif
        #endregion

        #region GetMultiple Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public GridReader GetMultiple(string sql)
        {
            return ExecuteInternal(() => _sharedConnection.QueryMultiple(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        public GridReader GetMultiple(string sql, object parameters)
        {
            return ExecuteInternal(() => _sharedConnection.QueryMultiple(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region Count Methods
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public int Count(string sql)
        {
            return ExecuteInternal(() => _sharedConnection.ExecuteScalar<int>(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        public int Count(string sql, object parameters)
        {
            return ExecuteInternal(() => _sharedConnection.ExecuteScalar<int>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        public int Count<T>(string sql = null) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Count<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        public int Count<T>(string sql, object parameters) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Count<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Exists Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Exists(string sql)
        {
            return ExecuteInternal(() => _sharedConnection.ExecuteScalar<bool>(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool Exists(string sql, object parameters)
        {
            return ExecuteInternal(() => _sharedConnection.ExecuteScalar<bool>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToExists"></param>
        /// <returns></returns>
        public bool Exists<T>(T entityToExists) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Exists<T>(entityToExists, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists<T>(object primaryKey) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Exists<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Exists<T>(string sql = null) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Exists<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool Exists<T>(string sql, object parameters) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Exists<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <returns>the entity, else null</returns>
        public T Get<T>(T entityToGet) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T>(entityToGet, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <returns>the entity, else null</returns>
        public T Get<T>(object primaryKey) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <returns>the entity, else null</returns>
        public T Get<T>(string sql, object parameters) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 Get<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 Get<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 Get<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 Get<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 Get<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.Get<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region GetFirst Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        public T GetFirst<T>(string sql = null) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        public T GetFirst<T>(string sql, object parameters) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 GetFirst<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 GetFirst<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 GetFirst<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 GetFirst<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 GetFirst<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public T1 GetFirst<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns> 
        public TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetFirst<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region GetList Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        public IEnumerable<T> GetList<T>(string sql = null) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T> GetList<T>(string sql, object parameters) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T1> GetList<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T1> GetList<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T1> GetList<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T1> GetList<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T1> GetList<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<T1> GetList<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<TRet> GetList<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<TRet> GetList<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<TRet> GetList<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<TRet> GetList<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetList<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region GetPagedList Queries
        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        public IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql = null) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T>(page, pageSize, sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql, object parameters) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T>(page, pageSize, sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3, T4>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3, T4>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns> 
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3, T4, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return ExecuteInternal(() => _sharedConnection.GetPageList<T1, T2, T3, T4, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Insert Methods
        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="entityToInsert">Entity to insert, can be list of entities</param>
        /// <returns>the entity to insert or the list of entities</returns>
        public bool Insert<T>(T entityToInsert) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Insert<T>(entityToInsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Update Queries
        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public bool Update<T>(T entityToUpdate) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Update<T>(entityToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public bool Update<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Update<T>(entityToUpdate, columnsToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Upsert Queries

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public bool Upsert<T>(T entityToUpsert) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Upsert<T>(entityToUpsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Upsert<T>(entityToUpsert, columnsToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public bool Upsert<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Upsert<T>(entityToUpsert, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Upsert<T>(entityToUpsert, columnsToUpdate, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        }
        #endregion

        #region Delete Methods
        /// <inheritdoc />
        public bool Delete<T>(T entityToDelete) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Delete<T>(entityToDelete, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public bool Delete<T>(object primaryKeyValue) where T : class
        {
            return ExecuteInternal(() => _sharedConnection.Delete<T>(primaryKeyValue, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public bool Delete<T>(string whereClause) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            }
            return ExecuteInternal(() => _sharedConnection.Delete<T>(whereClause, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public bool Delete<T>(string whereClause, object parameters) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            }
            return ExecuteInternal(() => _sharedConnection.Delete<T>(whereClause, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public bool DeleteAll<T>() where T : class
        {
            return ExecuteInternal(() => _sharedConnection.DeleteAll<T>(_transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion
    }
}
