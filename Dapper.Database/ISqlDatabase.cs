using System;
using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace Dapper.Database
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface ISqlDatabase : IDisposable
    {
        #region Execute Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        int Execute(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        int Execute(string sql, object parameters);

        #endregion

        #region ExecuteScaler Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        T ExecuteScaler<T>(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        T ExecuteScaler<T>(string sql, object parameters);

        #endregion

        #region GetDataTable Methods
#if !NETSTANDARD1_3 && !NETCOREAPP1_0
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        DataTable GetDataTable(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        DataTable GetDataTable(string sql, object parameters);
#endif
        #endregion

        #region GetMultiple Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        GridReader GetMultiple(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        GridReader GetMultiple(string sql, object parameters);
        #endregion

        #region Count Methods
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        int Count(string sql);

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        int Count(string sql, object parameters);

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        int Count<T>(string sql = null) where T : class;

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        int Count<T>(string sql, object parameters) where T : class;

        #endregion

        #region Exists Methods
        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>true if record is found</returns>
        bool Exists(string sql = null);

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if record is found</returns>
        bool Exists(string sql, object parameters);

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToExists">Entity to delete</param>
        /// <returns>true if record is found</returns>
        bool Exists<T>(T entityToExists) where T : class;

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="primaryKey">a Single primary key to check</param>
        /// <returns>true if record is found</returns>
        bool Exists<T>(object primaryKey) where T : class;

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>true if record is found</returns>
        bool Exists<T>(string sql = null) where T : class;

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if record is found</returns>
        bool Exists<T>(string sql, object parameters) where T : class;

        #endregion

        #region Get Methods
        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <returns>the entity, else null</returns>
        T Get<T>(T entityToGet) where T : class;

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <returns>the entity, else null</returns>
        T Get<T>(object primaryKey) where T : class;

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <returns>the entity, else null</returns>
        T Get<T>(string sql, object parameters) where T : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 Get<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 Get<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 Get<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 Get<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 Get<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet Get<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;
        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet Get<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;


        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet Get<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        #endregion

        #region GetFirst Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        T GetFirst<T>(string sql = null) where T : class;

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        T GetFirst<T>(string sql, object parameters) where T : class;


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 GetFirst<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 GetFirst<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 GetFirst<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 GetFirst<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 GetFirst<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        T1 GetFirst<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet GetFirst<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet GetFirst<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        TRet GetFirst<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;
        #endregion

        #region GetList Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        IEnumerable<T> GetList<T>(string sql = null) where T : class;

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T> GetList<T>(string sql, object parameters) where T : class;


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T1> GetList<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T1> GetList<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T1> GetList<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T1> GetList<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T1> GetList<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<T1> GetList<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<TRet> GetList<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<TRet> GetList<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<TRet> GetList<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<TRet> GetList<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IEnumerable<TRet> GetList<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;
        #endregion

        #region Get Paged List Methods
        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql = null) where T : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T> GetPageList<T>(int page, int pageSize, string sql, object parameters) where T : class;


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T1> GetPageList<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<T1> GetPageList<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

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
        IPagedEnumerable<TRet> GetPageList<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

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
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

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
        IPagedEnumerable<TRet> GetPageList<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        #endregion

        #region Insert Methods

        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="entityToInsert">Entity to insert, can be list of entities</param>
        /// <returns>the entity to insert or the list of entities</returns>
        bool Insert<T>(T entityToInsert) where T : class;

        #endregion

        #region Update Queries
        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Update<T>(T entityToUpdate) where T : class;


        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Update<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class;

        #endregion

        #region Upsert Queries

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Upsert<T>(T entityToUpsert) where T : class;

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class;

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Upsert<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class;

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Upsert<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction) where T : class;
        #endregion

        #region Delete Methods
        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        bool Delete<T>(T entityToDelete) where T : class;

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        bool Delete<T>(object primaryKey) where T : class;

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        bool Delete<T>(string sql = null) where T : class;

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        bool Delete<T>(string sql, object parameters) where T : class;

        #endregion

        #region Transaction
        /// <summary>
        /// Get a transaction
        /// </summary>
        /// <param name="isolationlevel"></param>
        /// <returns></returns>
        ITransaction GetTransaction(IsolationLevel isolationlevel = IsolationLevel.ReadCommitted);

        #endregion

        #region Timeout Settings

        /// <summary>
        /// Sets the Database timeout for all transactions
        /// </summary>
        int? CommandTimeout { get; set; }

        /// <summary>
        /// Sets the timeout value for the next (and only next) SQL statement
        /// </summary>
        int? OneTimeCommandTimeout { get; set; }

        #endregion
    }
}
