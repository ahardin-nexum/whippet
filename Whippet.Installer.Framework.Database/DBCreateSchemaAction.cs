using System;
using System.Resources.NetStandard;
using SqlServer = Microsoft.SqlServer.Management.Smo.Server;
using Athi.Whippet.Data.Database.Microsoft;
using Athi.Whippet.Data.Database.PostgreSQL;
using Athi.Whippet.Installer.Framework.Database.ResourceFiles;
using Athi.Whippet.Localization.Extensions;
using Athi.Whippet.Localization;

namespace Athi.Whippet.Installer.Framework.Database
{
    /// <summary>
    /// Performs a database schema creation for the appropriate RDBMS. This class must be inherited.
    /// </summary>
    internal class DBCreateSchemaAction : InstallerActionBase, IInstallerAction
    {
        /// <summary>
        /// Indicates whether the action supports asynchronous execution. This property is read-only.
        /// </summary>
        public override bool SupportsAsync
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBCreateSchemaAction"/> class with no arguments.
        /// </summary>
        protected DBCreateSchemaAction()
            : base("Creating Database Schema")
        { }

        /// <summary>
        /// Executes the current action with the specified parameters.
        /// </summary>
        /// <param name="args">Parameters to pass to the action (if any).</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> containing the result of the operation.</returns>
        public override WhippetResultContainer<object> Execute(params object[] args)
        {
            // method needs to be overridden by child
            return new WhippetResultContainer<object>(WhippetResult.Success, null);
        }

        /// <summary>
        /// Represents a <see cref="DBCreateSchemaAction"/> for Microsoft SQL Server database systems. This class cannot be inherited.
        /// </summary>
        internal sealed class MSSQL : DBCreateSchemaAction, IInstallerAction
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DBCreateSchemaAction.MSSQL"/> class with no arguments.
            /// </summary>
            public MSSQL()
                : base()
            { }
            
            /// <summary>
            /// Executes the current action with the specified parameters.
            /// </summary>
            /// <param name="args">Parameters to pass to the action (if any).</param>
            /// <returns><see cref="WhippetResultContainer{T}"/> containing the result of the operation.</returns>
            public override WhippetResultContainer<object> Execute(params object[] args)
            {
                WhippetResultContainer<object> result = null;
                
                WhippetSqlServerConnection connection = null;
                SqlServer server = null;

                string databaseName = String.Empty;
                
                try
                {
                    VerifyParameters(typeof(WhippetSqlServerConnection), args);
                    VerifyParameters(typeof(string), args);
                    
                    connection = (WhippetSqlServerConnection)(args.First(a => a is WhippetSqlServerConnection));
                    databaseName = Convert.ToString(args.First(a => a is String));
                    
                    server = connection.CreateServerInstance();
                    server.ConnectionContext.ExecuteNonQuery(Scripts_MSSQL.DB_SCHEMA.Replace(InstallerTokens.TOKEN_DBNAME, databaseName, StringComparison.InvariantCultureIgnoreCase));

                    result = new WhippetResultContainer<object>(WhippetResult.Success, null);
                }
                catch (Exception e)
                {
                    result = new WhippetResultContainer<object>(e);
                }
                finally
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
                
                return result;
            }
        }
        
                /// <summary>
        /// Represents a <see cref="DBCreateSchemaAction"/> for PostgreSQL database systems. This class cannot be inherited.
        /// </summary>
        internal sealed class PostgreSQL : DBCreateSchemaAction, IInstallerAction
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DBCreateSchemaAction.PostgreSQL"/> class with no arguments.
            /// </summary>
            public PostgreSQL()
                : base()
            { }
            
            /// <summary>
            /// Executes the current action with the specified parameters.
            /// </summary>
            /// <param name="args">Parameters to pass to the action (if any).</param>
            /// <returns><see cref="WhippetResultContainer{T}"/> containing the result of the operation.</returns>
            public override WhippetResultContainer<object> Execute(params object[] args)
            {
                WhippetResultContainer<object> result = null;
                
                WhippetPostgreSqlConnection connection = null;
                WhippetPostgreSqlCommand command = null;

                ResXResourceReader resx = null;

                string databaseName = null;

                try
                {
                    VerifyParameters(typeof(WhippetPostgreSqlConnection), args);
                    VerifyParameters(typeof(string), args, 2);
                    
                    connection = (WhippetPostgreSqlConnection)(args.First(a => a is WhippetPostgreSqlConnection));
                    databaseName = Convert.ToString(args.First(a => a is String)).ToLowerInvariant();
                    
                    connection.Open();
                    connection.ChangeDatabase(databaseName);
                    
                    resx = LocalizedStringResourceLoader.ReadResXFile(nameof(Scripts_PostgreSQL));
                    
                    command = connection.CreateCommand();
                    command.CommandText = resx.GetResource("DB_SCHEMA");

                    command.ExecuteNonQuery();
                    
                    result = new WhippetResultContainer<object>(WhippetResult.Success, null);
                }
                catch (Exception e)
                {
                    result = new WhippetResultContainer<object>(e);
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                        command = null;
                    }
                    
                    if (connection != null)
                    {
                        connection.Close();
                    }

                    if (resx != null)
                    {
                        resx.Close();
                    }
                }
                
                return result;
            }
        }
    }
}
