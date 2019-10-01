using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region Get Methods

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="entityToGet">An entity with primary key(s) populated.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        T Get<T>(T entityToGet) where T : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="primaryKey">A Single primary key value to retrieve. </param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        T Get<T>(object primaryKey) where T : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        T Get<T>(string sql, object parameters) where T : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 Get<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 Get<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 Get<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 Get<T1, T2, T3, T4>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 Get<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        #endregion

        #region GetAsync Methods

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="entityToGet">An entity with primary key(s) populated.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        Task<T> GetAsync<T>(T entityToGet) where T : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="primaryKey">A Single primary key value to retrieve. </param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        Task<T> GetAsync<T>(object primaryKey) where T : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        Task<T> GetAsync<T>(string sql, object parameters) where T : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetAsync<T1, T2>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetAsync<T1, T2, T3>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region Get Methods

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="entityToGet">An entity with primary key(s) populated.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        public T Get<T>(T entityToGet) where T : class => ExecuteInternal(() =>
            SharedConnection.Get(entityToGet, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="primaryKey">A Single primary key value to retrieve. </param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        public T Get<T>(object primaryKey) where T : class => ExecuteInternal(() =>
            SharedConnection.Get<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        public T Get<T>(string sql, object parameters) where T : class => ExecuteInternal(() =>
            SharedConnection.Get<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 Get<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class =>
            ExecuteInternal(() =>
                SharedConnection.Get<T1, T2>(sql, parameters, splitOn, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 Get<T1, T2, T3>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => ExecuteInternal(() =>
            SharedConnection.Get<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 Get<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => ExecuteInternal(() =>
            SharedConnection.Get<T1, T2, T3>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 Get<T1, T2, T3, T4>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => ExecuteInternal(() =>
            SharedConnection.Get<T1, T2, T3, T4>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 Get<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => ExecuteInternal(() =>
            SharedConnection.Get<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to record set.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.Get(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.Get(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.Get(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            ExecuteInternal(() => SharedConnection.Get(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.Get(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
            string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.Get(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region GetAsync Methods

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="entityToGet">An entity with primary key(s) populated.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetAsync<T>(T entityToGet) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync(entityToGet, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="primaryKey">A Single primary key value to retrieve. </param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetAsync<T>(object primaryKey) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync<T>(primaryKey, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetAsync<T>(string sql, object parameters) where T : class => await ExecuteInternalAsync(
            () => SharedConnection.GetAsync<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync<T1, T2>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2, T3>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2, T3, T4>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync<T1, T2, T3, T4>(sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetAsync(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
                string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetAsync(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await ExecuteInternalAsync(() =>
                SharedConnection.GetAsync(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a single entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     A Single entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
                string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetAsync(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}
