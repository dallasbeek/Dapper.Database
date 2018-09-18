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
        /// Execute SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        public async Task<int> ExecuteAsync(string fullSql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteAsync(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string fullSql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteAsync(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region ExecuteScalar Methods

        /// <summary>
        /// Execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullSql">The SQL to execute for this Query</param>
        /// <returns>
        /// The first cell selected as <see cref="object" />.
        /// </returns>
        public async Task<T> ExecuteScalarAsync<T>(string fullSql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<T>(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// The first cell selected as <typeparamref name="T" />.
        /// </returns>
        public async Task<T> ExecuteScalarAsync<T>(string fullSql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<T>(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }
  
        #endregion

        #region GetMultiple Methods

        /// <summary>
        /// Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        /// A GridReader
        /// </returns>
        public async Task<GridReader> GetMultipleAsync(string fullSql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.QueryMultipleAsync(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// A GridReader
        /// </returns>
        public async Task<GridReader> GetMultipleAsync(string fullSql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.QueryMultipleAsync(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Count Methods

        /// <summary>
        /// Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        /// Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync(string fullSql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync(fullSql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the number of matching records.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync(string fullSql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        /// Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the number of matching records.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// Total Count of matching records.
        /// </returns>
        public async Task<int> CountAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.CountAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Exist Methods

        /// <summary>
        /// Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <returns>
        /// True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync(string fullSql)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<bool>(fullSql, null, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that checks if record(s) exist.
        /// </summary>
        /// <param name="fullSql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync(string fullSql, object parameters)
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExecuteScalarAsync<bool>(fullSql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entityToCheck">Entity to check for existence.</param>
        /// <returns>
        /// True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(T entityToCheck) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(entityToCheck, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="primaryKey">A single primary key to check.</param>
        /// <returns>
        /// True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(object primaryKey) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>
        /// True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that checks if an entity exists.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL clause to check for existence.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// True if record is found.
        /// </returns>
        public async Task<bool> ExistsAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.ExistsAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="entityToGet">An entity with primary key(s) populated.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T"/>.
        /// </returns>
        public async Task<T> GetAsync<T>(T entityToGet) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T>(entityToGet, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="primaryKey">A Single primary key value to retrieve. </param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T"/>.
        /// </returns>
        public async Task<T> GetAsync<T>(object primaryKey) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T"/>.
        /// </returns>
        public async Task<T> GetAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T1"/>.
        /// </returns>        
        public async Task<T1> GetAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="T1"/>.
        /// </returns>        
        public async Task<T1> GetAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// A Single entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetAsync<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region GetFirst Methods

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetFirstAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetFirstAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// The first matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetFirstAsync<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region GetList Methods

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<IEnumerable<T>> GetListAsync<T>(string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1"/>.
        /// </returns>
        public async Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4, TRet>(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns all matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet"/>.
        /// </returns>
        public async Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetListAsync<T1, T2, T3, T4, TRet>(mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region GetPagedList Methods

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql = null) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T>(page, pageSize, sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql, object parameters) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T>(page, pageSize, sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4>(page, pageSize, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4>(page, pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4, TRet>(page, pageSize, mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the recordset.</typeparam>
        /// <typeparam name="T2">The second type in the recordset.</typeparam>
        /// <typeparam name="T3">The third type in the recordset.</typeparam>
        /// <typeparam name="T4">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retreive.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        /// An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.GetPageListAsync<T1, T2, T3, T4, TRet>(page, pageSize, mapper, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region Insert Methods

        /// <summary>
        /// Inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entityToInsert">The Entity to insert.</param>
        /// <returns>
        /// True if the record is inserted.
        /// </returns>
        public async Task<bool> InsertAsync<T>(T entityToInsert) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.InsertAsync<T>(entityToInsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region InsertList Methods

        /// <summary>
        /// Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to insert.</typeparam>
        /// <param name="entitiesToInsert">The IEnumerable list of Entity to insert.</param>
        /// <returns>
        /// True if records are inserted.
        /// </returns>
        public async Task<bool> InsertListAsync<T>(IEnumerable<T> entitiesToInsert) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.InsertListAsync<T>(entitiesToInsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }

        #endregion

        #region Update Methods

        /// <summary>
        /// Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <returns>
        /// True if the record is updated.
        /// </returns>
        public async Task<bool> UpdateAsync<T>(T entityToUpdate) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpdateAsync<T>(entityToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entityToUpdate">The Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        /// True if the record is updated.
        /// </returns>
        public async Task<bool> UpdateAsync<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpdateAsync<T>(entityToUpdate, columnsToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region UpdateList Methods

        /// <summary>
        /// Updates a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <returns>
        /// True if records are updated.
        /// </returns>
        public async Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpdateListAsync<T>(entitiesToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }

        /// <summary>
        /// Inserts a list of entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <param name="entitiesToUpdate">The IEnumerable list of Entity to update.</param>
        /// <param name="columnsToUpdate">The list of columns to updates.</param>
        /// <returns>
        /// True if records are updated.
        /// </returns>
        public async Task<bool> UpdateListAsync<T>(IEnumerable<T> entitiesToUpdate, IEnumerable<string> columnsToUpdate) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpdateListAsync<T>(entitiesToUpdate, columnsToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }

        #endregion

        #region Upsert Methods

        /// <summary>
        /// Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <returns>
        /// True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        /// True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsert, columnsToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        /// True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsert, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Updates or inserts an entity and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entityToUpsert">The Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        /// True if the record is updated or inserted.
        /// </returns>
        public async Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertAsync<T>(entityToUpsert, columnsToUpdate, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion

        #region UpsertList Methods

        /// <summary>
        /// Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <returns>
        /// True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertListAsync<T>(entitiesToUpsert, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }

        /// <summary>
        /// Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <returns>
        /// True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertListAsync<T>(entitiesToUpsert, columnsToUpdate, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }

        /// <summary>
        /// Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        /// True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertListAsync<T>(entitiesToUpsert, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }

        /// <summary>
        /// Updates or inserts a list of entities and returns true if successful.
        /// </summary>
        /// <typeparam name="T">The type of entity to update or insert.</typeparam>
        /// <param name="entitiesToUpsert">The list of Entity to update or insert.</param>
        /// <param name="columnsToUpdate">The columns to update if the record exists.</param>
        /// <param name="insertAction">A callback function before the record is inserted.</param>
        /// <param name="updateAction">A callback function before the record is updated.</param>
        /// <returns>
        /// True if the records are updated or inserted.
        /// </returns>
        public async Task<bool> UpsertListAsync<T>(IEnumerable<T> entitiesToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.UpsertListAsync<T>(entitiesToUpsert, columnsToUpdate, insertAction, updateAction, _transaction, OneTimeCommandTimeout ?? CommandTimeout), true);
        }
     
        #endregion

        #region Delete Methods

        /// <summary>
        /// Delete entity in table "Ts" that match the key values of the entity (T) passed in
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">Entity to delete. If Keys are specified, they will be used as the WHERE condition to delete.</param>
        /// <returns>
        /// True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(T entityToDelete) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(entityToDelete, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Delete entity in table "Ts" by a primary key value specified on (T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKeyValue">a Single primary key to delete</param>
        /// <returns>
        /// True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(object primaryKeyValue) where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(primaryKeyValue, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Delete entity in table "Ts" by an unparameterized WHERE clause.
        /// If you want to Delete All of the data, call the DeleteAll() command
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereClause">The where clause to use to bound a delete, cannot be null, empty, or whitespace</param>
        /// <returns>
        /// True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(string whereClause) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            }
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(whereClause, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Delete entity(s).
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <param name="whereClause">The where clause.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        /// True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAsync<T>(string whereClause, object parameters) where T : class
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                throw new ArgumentNullException(nameof(whereClause), "Must specify a where clause for deletion.");
            }
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAsync<T>(whereClause, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        /// <summary>
        /// Delete ALL entities.
        /// </summary>
        /// <typeparam name="T">The type of entity to delete.</typeparam>
        /// <returns>
        /// True if deleted, false if not found.
        /// </returns>
        public async Task<bool> DeleteAllAsync<T>() where T : class
        {
            return await ExecuteInternalAsync(() => _sharedConnection.DeleteAllAsync<T>(_transaction, OneTimeCommandTimeout ?? CommandTimeout));
        }

        #endregion
    }
}
