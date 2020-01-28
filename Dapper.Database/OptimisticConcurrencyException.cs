using System;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace Dapper.Database
{
    /// <summary>
    /// The exception that is thrown when an optimistic concurrency violation occurs.
    /// </summary>
    [Serializable]
    public class OptimisticConcurrencyException : DataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptimisticConcurrencyException"/> class.
        /// </summary>
        public OptimisticConcurrencyException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimisticConcurrencyException"/> class with the specified message.
        /// </summary>
        /// <param name="message"></param>
        public OptimisticConcurrencyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimisticConcurrencyException"/> class with the specified message
        /// and a reference to the inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public OptimisticConcurrencyException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimisticConcurrencyException"/> class from serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected OptimisticConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimisticConcurrencyException"/> class with the specified entity in conflict.
        /// </summary>
        /// <param name="tableInfo">The <see cref="TableInfo"/> associated with the exception.</param>
        /// <param name="entity">The entity that failed its concurrency check.</param>
        public OptimisticConcurrencyException(TableInfo tableInfo, object entity)
            : base(BuildMessage(tableInfo))
        {
            TableInfo = tableInfo ?? throw new ArgumentNullException(nameof(tableInfo));
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        private static string BuildMessage(TableInfo tableInfo)
        {
            if (tableInfo == null) throw new ArgumentNullException(nameof(tableInfo));

            return $"Conflicts were detected for instance of entity type '{tableInfo.ClassType}' on the concurrency token properties {string.Join(", ", tableInfo.ConcurrencyCheckColumns.Select(c => c.PropertyName))}.";
        }

        /// <summary>
        /// Information about the table associated with the exception.
        /// </summary>
        public TableInfo TableInfo { get; }

        /// <summary>
        /// The entity that failed its concurrency check.
        /// </summary>
        public object Entity { get; }
    }
}
