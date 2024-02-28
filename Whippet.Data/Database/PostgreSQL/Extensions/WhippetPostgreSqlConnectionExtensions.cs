using System;
using System.Data;

namespace Athi.Whippet.Data.Database.PostgreSQL.Extensions
{
    /// <summary>
    /// Provides extension methods to <see cref="WhippetPostgreSqlConnection"/> objects. This class cannot be inherited.
    /// </summary>
    public static class WhippetPostgreSqlConnectionExtensions
    {
        private const string DB_EXISTS_SCRIPT = @"SELECT 1 FROM pg_catalog.pg_database WHERE datname = '{0}'";
        private const string DB_USER_EXISTS_SCRIPT = @"SELECT 1 FROM pg_roles WHERE rolname='{0}'";

        /// <summary>
        /// Determines if the specified user exists.
        /// </summary>
        /// <param name="baseConnection"><see cref="WhippetPostgreSqlConnection"/> to use.</param>
        /// <param name="user">User name to search for.</param>
        /// <returns><see langword="true"/> if the user exists; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool UserExists(this WhippetPostgreSqlConnection baseConnection, string user)
        {
            if (baseConnection == null)
            {
                throw new ArgumentNullException();
            }
            else if (String.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException(nameof(user));
            }
            else
            {
                WhippetPostgreSqlConnection connection = null;
                WhippetPostgreSqlCommand command = null;
                WhippetPostgreSqlDataReader reader = null;

                bool exists = false;
                
                try
                {
                    connection = baseConnection.Clone(baseConnection.ConnectionString);
                    command = new WhippetPostgreSqlCommand(String.Format(DB_USER_EXISTS_SCRIPT, user), connection);

                    connection.Open();

                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader[0] == DBNull.Value || Convert.ToInt32(reader[0]) <= 0)
                        {
                            exists = false;
                        }
                        else
                        {
                            exists = true;
                        }
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                    
                    if (command != null)
                    {
                        command.Dispose();
                        command = null;
                    }

                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                        connection = null;
                    }
                }

                return exists;                
            }
        }
        
        /// <summary>
        /// Checks the PostgreSQL instance to see if the specified database exists.
        /// </summary>
        /// <param name="baseConnection">Base <see cref="WhippetPostgreSqlConnection"/> to use if <paramref name="connectionString"/> is <see langword="null"/> or <see cref="String.Empty"/>.</param>
        /// <param name="connectionString">Connection string using an existing login (such as "postgres") used to connect to the PostgreSQL instance or <see langword="null"/> to use the parent connection.</param>
        /// <param name="dbName">Name of the database to search for.</param>
        /// <returns><see langword="true"/> if the database exists; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool DatabaseExists(this WhippetPostgreSqlConnection baseConnection, string connectionString, string dbName)
        {
            if (String.IsNullOrWhiteSpace(connectionString) && ((baseConnection == null) || String.IsNullOrWhiteSpace(baseConnection.ConnectionString)))
            {
                throw new ArgumentNullException(connectionString);
            }
            else if (String.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException(nameof(dbName));
            }
            else
            {
                WhippetPostgreSqlConnection connection = null;
                WhippetPostgreSqlCommand command = null;
                WhippetPostgreSqlDataReader reader = null;
                
                object returnValue = null;

                bool useBaseConnection = String.IsNullOrWhiteSpace(connectionString);
                
                try
                {
                    connection = useBaseConnection ? baseConnection.Clone(baseConnection.ConnectionString) : new WhippetPostgreSqlConnection(connectionString);
                    command = new WhippetPostgreSqlCommand(String.Format(DB_EXISTS_SCRIPT, dbName.ToLowerInvariant()), connection);

                    connection.Open();

                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        returnValue = Convert.ToInt32(reader[0]);
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                    
                    if (command != null)
                    {
                        command.Dispose();
                        command = null;
                    }

                    if (connection != null)
                    {
                        connection.Close();
                        connection.Dispose();
                        connection = null;
                    }
                }

                return (returnValue != null) && (Convert.ToInt32(returnValue) != 0);
            }
        }
    }
}
