using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<int> ExecuteAsync(string sql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteAsync(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<int> ExecuteAsync(string sql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteAsync(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region Execute Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<T> ExecuteScalerAsync<T>(string sql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<T>(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<T> ExecuteScalerAsync<T>(string sql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        //        #region GetDataTable Methods
        //#if !NETSTANDARD1_3 && !NETCOREAPP1_0
        //        /// <summary>
        //        /// Execute a query
        //        /// </summary>
        //        /// <param name="sql">The sql clause to count</param>
        //        /// <returns>Return Total CountAsync of matching records</returns>
        //        public async Task<DataTable> GetDataTableAsync(string sql)
        //        {
        //            return await ExecuteInternalAsync(() =>
        //            {
        //                var dt = new DataTable();
        //                dt.Load(_sharedConnection.ExecuteReaderAsync(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        //                return dt;
        //            });
        //        }

        //        /// <summary>
        //        /// Execute a query
        //        /// </summary>
        //        /// <param name="sql">The sql clause to count</param>
        //        /// <param name="parameters">The parameters of the clause</param>
        //        /// <returns>Return Total CountAsync of matching records</returns>
        //        public async Task<DataTable> GetDataTableAsync(string sql, object parameters)
        //        {
        //            return await ExecuteInternalAsync(() =>
        //            {
        //                var dt = new DataTable();
        //                dt.Load(await _sharedConnection.ExecuteReaderAsync(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        //                return dt;
        //            });
        //        }
        //#endif
        //        #endregion

        #region GetMultiple Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<GridReader> GetMultipleAsync(string sql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.QueryMultipleAsync(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<GridReader> GetMultipleAsync(string sql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.QueryMultipleAsync(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region CountAsync Methods
        /// <summary>
        /// CountAsync of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<int> CountAsync(string sql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<int>(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// CountAsync of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<int> CountAsync(string sql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<int>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// CountAsync of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<int> CountAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// CountAsync of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total CountAsync of matching records</returns>
        public async Task<int> CountAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region ExistsAsync Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string sql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<bool>(sql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string sql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<bool>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityToExistsAsync"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync<T>(T entityToExistsAsync) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(entityToExistsAsync, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync<T>(object primaryKey) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <returns>the entity, else null</returns>
        public async Task<T> GetAsync<T>(T entityToGet) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T>(entityToGet, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <returns>the entity, else null</returns>
        public async Task<T> GetAsync<T>(object primaryKey) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <returns>the entity, else null</returns>
        public async Task<T> GetAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region GetFirstAsync Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        public async Task<T> GetFirstAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T> GetFirstAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetFirstAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetFirstAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
        #endregion

        #region GetListAsync Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T>(page, pageSize, sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T>(page, pageSize, sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

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
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
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
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region InsertAsync Methods
        /// <summary>
        /// InsertAsyncs an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="entityToInsertAsync">Entity to insert, can be list of entities</param>
        /// <returns>the entity to insert or the list of entities</returns>
        public async Task<bool> InsertAsync<T>(T entityToInsertAsync) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.InsertAsync<T>(entityToInsertAsync, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region UpdateAsync Queries
        /// <summary>
        /// UpdateAsyncs entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdateAsync">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async Task<bool> UpdateAsync<T>(T entityToUpdateAsync) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpdateAsync<T>(entityToUpdateAsync, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// UpdateAsyncs entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdateAsync">Entity to be updated</param>
        /// <param name="columnsToUpdateAsync">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async Task<bool> UpdateAsync<T>(T entityToUpdateAsync, IEnumerable<string> columnsToUpdateAsync) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpdateAsync<T>(entityToUpdateAsync, columnsToUpdateAsync, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region UpsertAsync Queries

        /// <summary>
        /// UpdateAsyncs entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsertAsync">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsertAsync) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsertAsync, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// UpdateAsyncs entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsertAsync">Entity to be updated</param>
        /// <param name="columnsToUpdateAsync">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsertAsync, IEnumerable<string> columnsToUpdateAsync) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsertAsync, columnsToUpdateAsync, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// UpdateAsyncs entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsertAsync">Entity to be inserted or updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">UpdateAsync action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsertAsync, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsertAsync, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// UpdateAsyncs entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsertAsync">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdateAsync">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">UpdateAsync action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsertAsync, IEnumerable<string> columnsToUpdateAsync, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsertAsync, columnsToUpdateAsync, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        }
        #endregion

        #region Delete Methods
        /// <inheritdoc />
        public async Task<bool> DeleteAsync<T>(T entityToDelete) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(entityToDelete, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync<T>(object primaryKeyValue) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(primaryKeyValue, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync<T>(string whereClause) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            }
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(whereClause, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync<T>(string whereClause, object parameters) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            }
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(whereClause, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAllAsync<T>() where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAllAsync<T>(_transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion
    }
}
