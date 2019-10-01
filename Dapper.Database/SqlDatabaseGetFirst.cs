using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region GetFirst Methods

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        T GetFirst<T>(string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        T GetFirst<T>(string sql, object parameters) where T : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 GetFirst<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 GetFirst<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 GetFirst<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 GetFirst<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 GetFirst<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        T1 GetFirst<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        #endregion

        #region GetFirstAsync Methods

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        Task<T> GetFirstAsync<T>(string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        Task<T> GetFirstAsync<T>(string sql, object parameters) where T : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetFirstAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetFirstAsync<T1, T2>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetFirstAsync<T1, T2, T3>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetFirstAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql,
            string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region GetFirst Methods

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        public T GetFirst<T>(string sql = null) where T : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        public T GetFirst<T>(string sql, object parameters) where T : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T>(sql, parameters, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 GetFirst<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class =>
            ExecuteInternal(() =>
                SharedConnection.GetFirst<T1, T2>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 GetFirst<T1, T2>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T1, T2>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 GetFirst<T1, T2, T3>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T1, T2, T3>(sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 GetFirst<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T1, T2, T3>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 GetFirst<T1, T2, T3, T4>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T1, T2, T3, T4>(sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public T1 GetFirst<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => ExecuteInternal(() =>
            SharedConnection.GetFirst<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.GetFirst(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.GetFirst(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.GetFirst(mapper, sql, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            ExecuteInternal(() => SharedConnection.GetFirst(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.GetFirst(mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters,
            string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.GetFirst(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region GetFirstAsync Methods

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetFirstAsync<T>(string sql = null) where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T>(sql, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T'.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<T> GetFirstAsync<T>(string sql, object parameters) where T : class =>
            await ExecuteInternalAsync(() =>
                SharedConnection.GetFirstAsync<T>(sql, parameters, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2>(string sql, string splitOn = null)
            where T1 : class where T2 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T1, T2>(sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T1, T2>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T1, T2, T3>(sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T1, T2, T3>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T1, T2, T3, T4>(sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync<T1, T2, T3, T4>(sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync(mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync(mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetFirstAsync(mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters,
                string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetFirstAsync(mapper, sql, parameters, splitOn,
                _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet>
            GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await ExecuteInternalAsync(() =>
                SharedConnection.GetFirstAsync(mapper, sql, splitOn, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns the first entity of type 'TRet'.
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
        ///     The first matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql,
            object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetFirstAsync(mapper, sql, parameters, splitOn,
                _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}
