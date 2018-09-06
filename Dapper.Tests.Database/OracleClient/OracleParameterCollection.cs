#if ORACLE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Diagnostics;
using RealOracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;
using RealOracleParameterCollection = Oracle.ManagedDataAccess.Client.OracleParameterCollection;

namespace Dapper.Tests.Database.OracleClient
{
    public class OracleParameterCollection : DbParameterCollection
    {
        private readonly RealOracleParameterCollection _realParameters;
        private readonly List<OracleParameter> _parameters;

        internal OracleParameterCollection(RealOracleParameterCollection parameters)
        {
            _realParameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _parameters = _realParameters.Cast<RealOracleParameter>().Select(p => new OracleParameter(p)).ToList();
            AssertValid();
        }

        [Conditional("DEBUG")]
        private void AssertValid()
        {
            Debug.Assert(_realParameters.Count == _parameters.Count, "parameter counts do not match");
            for (int i = 0; i < Count; i++)
            {
                Debug.Assert(ReferenceEquals(_parameters[i].RealParameter, _realParameters[i]), $"parameter {i} does not match");
            }
        }

        private int AddInternal(OracleParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            // If we successfully add, it will be at index == original count
            var index = Count;
            _parameters.Add(parameter);
            _realParameters.Add(parameter.RealParameter);
            AssertValid();
            return index;
        }

        public void Add(OracleParameter parameter) => AddInternal(parameter);


        public override int Add(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            switch (value)
            {
                case OracleParameter parameter:
                    return AddInternal(parameter);

                case RealOracleParameter realParameter:
                    return AddInternal(new OracleParameter(realParameter));

                default:
                    throw new InvalidCastException($"Cannot cast connection of type {value.GetType()} to {typeof(OracleParameter)}.");
            }
        }

        public override bool Contains(object value) => _parameters.Contains((OracleParameter)value);

        public override void Clear()
        {
            _parameters.Clear();
            _realParameters.Clear();
            AssertValid();
        }

        public override int IndexOf(object value) => _parameters.IndexOf((OracleParameter)value);

        private void InsertInternal(int index, OracleParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            _parameters.Insert(index, parameter);
            _realParameters.Insert(index, parameter.RealParameter);
            AssertValid();
        }

        public void Insert(int index, OracleParameter parameter) => InsertInternal(index, parameter);

        public override void Insert(int index, object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            switch (value)
            {
                case OracleParameter parameter:
                    InsertInternal(index, parameter);
                    break;

                case RealOracleParameter realParameter:
                    InsertInternal(index, new OracleParameter(realParameter));
                    break;

                default:
                    throw new InvalidCastException($"Cannot cast connection of type {value.GetType()} to {typeof(OracleParameter)}.");
            }
        }

        private void RemoveInternal(OracleParameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            _parameters.Remove(parameter);
            _realParameters.Remove(parameter.RealParameter);
            AssertValid();
        }

        public override void Remove(object value) => RemoveInternal((OracleParameter)value);

        private void RemoveAtInternal(int index)
        {
            _parameters.RemoveAt(index);
            _realParameters.RemoveAt(index);
            AssertValid();
        }

        public override void RemoveAt(int index) => RemoveAtInternal(index);

        public override void RemoveAt(string parameterName)
        {
            RemoveAtInternal(IndexOf(parameterName));
        }

        private void SetParameterInternal(int index, OracleParameter parameter)
        {
            _parameters[index] = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _realParameters[index] = parameter.RealParameter;
            AssertValid();
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            SetParameterInternal(index, (OracleParameter)value);
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            SetParameterInternal(IndexOf(parameterName), (OracleParameter)value);
        }

        public override int Count => _realParameters.Count;

        public override object SyncRoot => _realParameters.SyncRoot;

        public override int IndexOf(string parameterName) => _realParameters.IndexOf(parameterName);

        public override IEnumerator GetEnumerator() => _parameters.GetEnumerator();

        protected override DbParameter GetParameter(int index) => _parameters[index];

        protected override DbParameter GetParameter(string parameterName) => GetParameter(IndexOf(parameterName));

        public override bool Contains(string value) => _realParameters.Contains(value);

        public override void CopyTo(Array array, int index) => _parameters.CopyTo((OracleParameter[])array, index);

        public override void AddRange(Array values)
        {
            foreach (var obj in values)
                Add(obj);
        }
    }
}
#endif

