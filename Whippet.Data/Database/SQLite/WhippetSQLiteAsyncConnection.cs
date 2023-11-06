﻿using System;
using System.Threading.Tasks;
using System.Data;
using SQLite;

namespace Athi.Whippet.Data.Database.SQLite
{
    /// <summary>
    /// Represents a pooled asynchronous connection to a SQLite database.
    /// </summary>
    public class WhippetSQLiteAsyncConnection : SQLiteAsyncConnection, IDbConnection
    {
        /// <summary>
        /// Gets the default file extension for SQLite database files.
        /// </summary>
        public const string DEFAULT_FILE_EXT = ".sqlite";

        /// <summary>
        /// Gets or sets the connection string. <see cref="WhippetSQLiteAsyncConnection"/> objects are immutable; attempting to set this property will throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        string IDbConnection.ConnectionString
        {
            get
            {
                return ConnectionString;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the connection string (database path) used for the <see cref="WhippetSQLiteConnection"/>. This property is read-only.
        /// </summary>
        public string ConnectionString
        { get; private set; }

        /// <summary>
        /// Gets the total time (in seconds) to establish a connection to the database before throwing an exception. This property is read-only.
        /// </summary>
        public int ConnectionTimeout
        {
            get
            {
                return GetBusyTimeout().Seconds;
            }
        }

        /// <summary>
        /// Gets the database path used for the <see cref="WhippetSQLiteConnection"/>. This property is read-only.
        /// </summary>
        string IDbConnection.Database
        {
            get
            {
                return ConnectionString;
            }
        }

        /// <summary>
        /// Gets the current state of the <see cref="WhippetSQLiteConnection"/>. Until disposed, the state will always be <see cref="ConnectionState.Open"/>. This property is read-only.
        /// </summary>
        ConnectionState IDbConnection.State
        {
            get
            {
                return ConnectionState.Open;
            }
        }

        /// <summary>
        /// Constructs a new <see cref="WhippetSQLiteAsyncConnection"/> and opens a SQLite database specified by <paramref name="databasePath"/>.
        /// </summary>
        /// <param name="databasePath">Specifies the path to the database file.</param>
        /// <param name="storeDateTimeAsTicks">Specifies whether to store DateTime properties as ticks (true) or strings (false). You absolutely do want to store them as Ticks in all new projects. The value of false is only here for backwards compatibility. There is a *significant* speed advantage, with no down sides, when setting storeDateTimeAsTicks = true. If you use DateTimeOffset properties, it will be always stored as ticks regardingless the storeDateTimeAsTicks parameter.</param>
        public WhippetSQLiteAsyncConnection(string databasePath, bool storeDateTimeAsTicks = true)
            : base(databasePath, storeDateTimeAsTicks)
        {
            ConnectionString = databasePath;
        }

        /// <summary>
        /// Constructs a new <see cref="WhippetSQLiteAsyncConnection"/> and opens a SQLite database specified by <paramref name="databasePath"/>.
        /// </summary>
        /// <param name="databasePath">Specifies the path to the database file.</param>
        /// <param name="openFlags">Flags controlling how the connection should be opened.</param>
        /// <param name="storeDateTimeAsTicks">Specifies whether to store DateTime properties as ticks (true) or strings (false). You absolutely do want to store them as Ticks in all new projects. The value of false is only here for backwards compatibility. There is a *significant* speed advantage, with no down sides, when setting storeDateTimeAsTicks = true. If you use DateTimeOffset properties, it will be always stored as ticks regardingless the storeDateTimeAsTicks parameter.</param>
        public WhippetSQLiteAsyncConnection(string databasePath, SQLiteOpenFlags openFlags, bool storeDateTimeAsTicks = true)
            : base(databasePath, openFlags, storeDateTimeAsTicks)
        {
            ConnectionString = databasePath;
        }

        /// <summary>
        /// Constructs a new SQLiteAsyncConnection and opens a SQLite database.
        /// </summary>
        /// <param name="connectionString">Details on how to find and open the database.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        public WhippetSQLiteAsyncConnection(WhippetSQLiteConnectionString connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString.DatabasePath;
        }

        /// <summary>
        /// This method is not supported in this instance.
        /// </summary>
        /// <returns><see cref="IDbTransaction"/> object.</returns>
        IDbTransaction IDbConnection.BeginTransaction()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported in this instance.
        /// </summary>
        /// <param name="il">Isolation levels are not supported on SQLite databases. This parameter is ignored.</param>
        /// <returns><see cref="IDbTransaction"/> object.</returns>
        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel il)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// <see cref="WhippetSQLiteConnection"/> objects are immutable and encapsulate a single connection to a single SQLite instance per object. This method is not supported.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        /// <exception cref="NotSupportedException"></exception>
        void IDbConnection.ChangeDatabase(string databaseName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This method is not supported at this time.
        /// </summary>
        /// <returns><see cref="IDbCommand"/> object.</returns>
        /// <exception cref="NotSupportedException"></exception>
        IDbCommand IDbConnection.CreateCommand()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The <see cref="WhippetSQLiteConnection"/> is intrinsically opened upon instantiation. This method is only implemented per the interface contract.
        /// </summary>
        void IDbConnection.Open()
        { }

        /// <summary>
        /// Closes any pooled connections used by the database.
        /// </summary>
        public virtual void Close()
        {
            Task.Run(() => CloseAsync());
        }

        /// <summary>
        /// Disposes of the current object and releases its resources from memory.
        /// </summary>
        public virtual void Dispose()
        {
            Close();
        }
    }
}
