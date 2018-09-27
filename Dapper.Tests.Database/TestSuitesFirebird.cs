﻿using System;
using System.IO;
using Dapper.Database;
using FirebirdSql.Data.FirebirdClient;
using Xunit;

namespace Dapper.Tests.Database
{
    [Trait("Provider", "Firebird")]
    public class FirebirdTestSuite : TestSuite
    {
        private static string DbName = "DBFiles\\Test.DB.fdb";
        private static string DbFile;

        public static string ConnectionString => $"DataSource=localhost;User=SYSDBA;Password=Password12!;Database={DbFile};";

        protected override void CheckSkip()
        {
            Skip.If(_skip, "Skipping Firebird Tests - no server.");
        }

        public override ISqlDatabase GetSqlDatabase()
        {
            CheckSkip();
            return new SqlDatabase(new StringConnectionService<FbConnection>(ConnectionString));
        }

        public override Provider GetProvider() => Provider.Firebird;

        private static readonly bool _skip;

        static FirebirdTestSuite()
        {
            DbFile = Directory.GetCurrentDirectory() + "\\DBFiles\\Test.DB.fdb";
            SqlDatabase.CacheQueries = false;

            ResetDapperTypes();
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());

            var init = false;

            //if (File.Exists(DbFile))
            //{
            //    File.Delete(DbFile);
            //}

            var commandText = string.Empty;

            try
            {
                using (var connection = new FbConnection(ConnectionString))
                {
                    connection.Open();

                    if (init)
                    {
                        var file = File.OpenText(".\\Scripts\\firebirdawlite.sql");
                        var line = string.Empty;

                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Equals("GO", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrEmpty(commandText))
                                    connection.Execute(commandText);
                                commandText = string.Empty;
                            }
                            else
                            {
                                commandText += "\r\n" + line;
                            }
                        }
                    }
                    connection.Execute("delete from Person");
                }

            }
            catch (FbException ex) when (ex.Message.Contains("Unable to complete network request"))
            {
                _skip = true;
            }
        }
    }
}
