using System;
using System.Threading.Tasks;
using Dapper.Database.Extensions;

namespace Dapper.Database
{
    public partial interface ISqlDatabase
    {
        #region GetPageList Methods

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql, object parameters) where T : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql,
            string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql,
            object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper,
            string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper,
            string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        #endregion

        #region GetPageListAsync Methods

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql = null) where T : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql, object parameters)
            where T : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql,
            string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters,
            string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql,
            string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql,
            object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper,
            string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper,
            string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize,
            Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize,
            Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where TRet : class;

        #endregion
    }

    public partial class SqlDatabase
    {
        #region GetPagedList Methods

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql = null) where T : class =>
            ExecuteInternal(() =>
                SharedConnection.GetPageList<T>(page, pageSize, sql, _transaction,
                    OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql, object parameters)
            where T : class => ExecuteInternal(() => SharedConnection.GetPageList<T>(page, pageSize, sql, parameters,
            _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class => ExecuteInternal(() =>
            SharedConnection.GetPageList<T1, T2>(page, pageSize, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public IPagedEnumerable<T1>
            GetPageList<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class => ExecuteInternal(() => SharedConnection.GetPageList<T1, T2>(page,
            pageSize, sql, parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => ExecuteInternal(() =>
            SharedConnection.GetPageList<T1, T2, T3>(page, pageSize, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public IPagedEnumerable<T1>
            GetPageList<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => ExecuteInternal(() =>
            SharedConnection.GetPageList<T1, T2, T3>(page, pageSize, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public IPagedEnumerable<T1>
            GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => ExecuteInternal(() =>
            SharedConnection.GetPageList<T1, T2, T3, T4>(page, pageSize, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public IPagedEnumerable<T1>
            GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => ExecuteInternal(() =>
            SharedConnection.GetPageList<T1, T2, T3, T4>(page, pageSize, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper,
            string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.GetPageList(page, pageSize, mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper,
            string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.GetPageList(page, pageSize, mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize,
            Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.GetPageList(page, pageSize, mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize,
            Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => ExecuteInternal(() =>
            SharedConnection.GetPageList(page, pageSize, mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.GetPageList(page, pageSize, mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class => ExecuteInternal(
            () => SharedConnection.GetPageList(page, pageSize, mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        #endregion

        #region GetPagedListAsync Methods

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql = null)
            where T : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync<T>(page, pageSize, sql, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T'.
        /// </summary>
        /// <typeparam name="T">The type of entity to retrieve.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T" />.
        /// </returns>
        public async Task<IPagedEnumerable<T>>
            GetPageListAsync<T>(int page, int pageSize, string sql, object parameters) where T : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetPageListAsync<T>(page, pageSize, sql, parameters,
                _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>>
            GetPageListAsync<T1, T2>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync<T1, T2>(page, pageSize, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>>
            GetPageListAsync<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync<T1, T2>(page, pageSize, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>>
            GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync<T1, T2, T3>(page, pageSize, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>>
            GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync<T1, T2, T3>(page, pageSize, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>>
            GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync<T1, T2, T3, T4>(page, pageSize, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'T1'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="T1" />.
        /// </returns>
        public async Task<IPagedEnumerable<T1>>
            GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters,
                string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetPageListAsync<T1, T2, T3, T4>(page, pageSize, sql,
                parameters, splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize,
            Func<T1, T2, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync(page, pageSize, mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize,
            Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync(page, pageSize, mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize,
            Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync(page, pageSize, mapper, sql, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize,
            Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where TRet : class => await ExecuteInternalAsync(() =>
            SharedConnection.GetPageListAsync(page, pageSize, mapper, sql, parameters, splitOn, _transaction,
                OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetPageListAsync(page, pageSize, mapper, sql, splitOn,
                _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        /// <summary>
        ///     Execute SQL that returns a page of matching records of type 'TRet'.
        /// </summary>
        /// <typeparam name="T1">The first type in the record set.</typeparam>
        /// <typeparam name="T2">The second type in the record set.</typeparam>
        /// <typeparam name="T3">The third type in the record set.</typeparam>
        /// <typeparam name="T4">The fourth type in the record set.</typeparam>
        /// <typeparam name="TRet">The combined type to return.</typeparam>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of records to return per page.</param>
        /// <param name="mapper">The function to map row types to the return type.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split the result on to return the next object.</param>
        /// <returns>
        ///     An IEnumerable list of matching entity of type <typeparamref name="TRet" />.
        /// </returns>
        public async Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize,
            Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null)
            where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class =>
            await ExecuteInternalAsync(() => SharedConnection.GetPageListAsync(page, pageSize, mapper, sql, parameters,
                splitOn, _transaction, OneTimeCommandTimeout ?? CommandTimeout));

        #endregion
    }
}
