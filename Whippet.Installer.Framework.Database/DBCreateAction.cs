using System;
using System.Resources.NetStandard;
using SqlServer = Microsoft.SqlServer.Management.Smo.Server;
using Athi.Whippet.Data.Database.Microsoft;
using Athi.Whippet.Data.Database.PostgreSQL;
using Athi.Whippet.Data.Database.PostgreSQL.Extensions;
using Athi.Whippet.Installer.Framework.Database.ResourceFiles;
using Athi.Whippet.Localization;
using Athi.Whippet.Localization.Extensions;

namespace Athi.Whippet.Installer.Framework.Database
{
    /// <summary>
    /// Performs a database creation for the appropriate RDBMS. This class must be inherited.
    /// </summary>
    internal class DBCreateAction : InstallerActionBase, IInstallerAction
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
        /// Initializes a new instance of the <see cref="DBCreateAction"/> class with no arguments.
        /// </summary>
        protected DBCreateAction()
            : base("Creating Database")
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
        /// Represents a <see cref="DBCreateAction"/> for PostgreSQL database systems. This class cannot be inherited.
        /// </summary>
        internal sealed class PostgreSQL : DBCreateAction, IInstallerAction
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DBCreateAction.PostgreSQL"/> class with no arguments.
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
                
                string databaseName = String.Empty;

                ResXResourceReader resx = null;
                
                try
                {
                    VerifyParameters(typeof(WhippetPostgreSqlConnection), args);
                    VerifyParameters(typeof(string), args);

                    connection = (WhippetPostgreSqlConnection)(args.First(a => a is WhippetPostgreSqlConnection));
                    
                    databaseName = Convert.ToString(args.First(a => a is String));

                    if (!connection.DatabaseExists(null, databaseName))
                    {
                        resx = LocalizedStringResourceLoader.ReadResXFile(nameof(Scripts_PostgreSQL));
                        
                        command = connection.CreateCommand();
                        command.CommandText = resx.GetResource("DB_CREATE").Replace(
                                InstallerTokens.TOKEN_DBNAME,
                                databaseName,
                                StringComparison.InvariantCultureIgnoreCase);

                        connection.Open();

                        command.ExecuteNonQuery();
                    }

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
        
        /// <summary>
        /// Represents a <see cref="DBCreateAction"/> for Microsoft SQL Server database systems. This class cannot be inherited.
        /// </summary>
        internal sealed class MSSQL : DBCreateAction, IInstallerAction
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DBCreateAction.MSSQL"/> class with no arguments.
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
                    server.ConnectionContext.ExecuteNonQuery(Scripts_MSSQL.DB_CREATE.Replace(InstallerTokens.TOKEN_DBNAME, databaseName, StringComparison.InvariantCultureIgnoreCase));

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
    }
}
