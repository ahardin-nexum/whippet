using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Security;
using System.Text;
using System.Transactions;
using MySql.Data.MySqlClient;
using Npgsql;
using IsolationLevel = System.Data.IsolationLevel;

namespace Athi.Whippet.Data.Database.PostgreSQL
{
    /// <summary>
    /// Represents a connection to a PostgreSQL database. This class cannot be inherited.
    /// </summary>
    public sealed class WhippetPostgreSqlConnection : WhippetDatabaseConnection<NpgsqlConnection>, ICloneable, IWhippetCloneable
    {
        /// <summary>
        /// This event is unsupported by <see cref="WhippetPostgreSqlConnection"/>. Use <see cref="DbConnection.StateChange"/> instead.
        /// </summary>
        public new event EventHandler Disposed
        {
            add
            {
                InternalConnection.Disposed += value;
            }
            remove
            {
                InternalConnection.Disposed -= value;
            }
        }
        
        /// <summary>
        /// Fires when PostgreSQL notices are received from PostgreSQL.
        /// </summary>
        public event NoticeEventHandler Notice
        {
            add
            {
                InternalConnection.Notice += value;
            }
            remove
            {
                InternalConnection.Notice -= value;
            }
        }

        /// <summary>
        /// Fires when PostgreSQL notifications are received from PostgreSQL.
        /// </summary>
        public event NotificationEventHandler Notification
        {
            add
            {
                InternalConnection.Notification += value;
            }
            remove
            {
                InternalConnection.Notification -= value;
            }
        }

        /// <summary>
        /// Selects the local Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        public ProvideClientCertificatesCallback ProvideClientCertificatesCallback
        {
            get
            {
                return InternalConnection.ProvideClientCertificatesCallback;
            }
            set
            {
                InternalConnection.ProvideClientCertificatesCallback = value;
            }
        }

        /// <summary>
        /// When using SSL/TLS, this is a callback that allows customizing how the PostgreSQL-provided certificate is verified. This is an advanced API; consider using <see cref="SslMode.VerifyFull"/> or <see cref="SslMode.VerifyCA"/> instead.
        /// </summary>
        public RemoteCertificateValidationCallback UserCertificateValidationCallback
        {
            get
            {
                return InternalConnection.UserCertificateValidationCallback;
            }
            set
            {
                InternalConnection.UserCertificateValidationCallback = value;
            }
        }

        /// <summary>
        /// Gets the version of the PostgreSQL server currently connected to. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public Version PostgreSqlVersion
        {
            get
            {
                return InternalConnection.PostgreSqlVersion;
            }
        }
        
        /// <summary>
        /// The name of the current database or the name of the database to be used after a connection is opened. This property is read-only.
        /// </summary>
        public override string Database
        {
            get
            {
                return InternalConnection.Database;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ISite"/> of the component.
        /// </summary>
        public override ISite Site
        {
            get
            {
                return InternalConnection.Site;
            }
            set
            {
                InternalConnection.Site = value;
            }
        }

        /// <summary>
        /// Indicates the current connection state of the current instance. This property is read-only.
        /// </summary>
        public override ConnectionState State
        {
            get
            {
                return InternalConnection.State;
            }
        }

        /// <summary>
        /// Gets or sets the connection string to the data source.
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return InternalConnection.ConnectionString;
            }
            set
            {
                InternalConnection.ConnectionString = value;
            }
        }

        /// <summary>
        /// Gets the time (in seconds) to wait while trying to establish a connection before terminating the attempt and generating an error. This property is read-only.
        /// </summary>
        public override int ConnectionTimeout
        {
            get
            {
                return InternalConnection.ConnectionTimeout;
            }
        }

        /// <summary>
        /// Gets the string identifying the database server. This property is read-only.
        /// </summary>
        public override string DataSource
        {
            get
            {
                return InternalConnection.DataSource;
            }
        }

        /// <summary>
        /// The PostgreSQL server version as returned by the <b>server_version</b> option. This property is read-only.
        /// </summary>
        public override string ServerVersion
        {
            get
            {
                return InternalConnection.ServerVersion;
            }
        }

        /// <summary>
        /// Gets the backend server host name. This property is read-only.
        /// </summary>
        [Browsable(true)]
        public string Host
        {
            get
            {
                return InternalConnection.Host;
            }
        }

        /// <summary>
        /// Gets the backend server port. This property is read-only.
        /// </summary>
        [Browsable(true)]
        public int Port
        {
            get
            {
                return InternalConnection.Port;
            }
        }

        /// <summary>
        /// Gets the time (in seconds) to wait while trying to execute a command before terminating the attempt and generating an error. This property is read-only.
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return InternalConnection.CommandTimeout;
            }
        }

        /// <summary>
        /// Gets the user name of the current session. This property is read-only.
        /// </summary>
        public string Username
        {
            get
            {
                return InternalConnection.UserName;
            }
        }

        /// <summary>
        /// Gets the current state of the connection. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public ConnectionState FullState
        {
            get
            {
                return InternalConnection.FullState;
            }
        }

        /// <summary>
        /// Indicates whether this <see cref="WhippetPostgreSqlCommand"/> instance supports the <see cref="WhippetPostgreSqlBatch"/> class. This property is read-only.
        /// </summary>
        public override bool CanCreateBatch
        {
            get
            {
                return InternalConnection.CanCreateBatch;
            }
        }

        /// <summary>
        /// Gets the process ID of the backend server. This property is read-only.
        /// </summary>
        public int ProcessID
        {
            get
            {
                return InternalConnection.ProcessID;
            }
        }

        /// <summary>
        /// Reports whether the backend uses the newer integer timestamp representation. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public bool HasIntegerDateTimes
        {
            get
            {
                return InternalConnection.HasIntegerDateTimes;
            }
        }

        /// <summary>
        /// Gets the connection's timezone as reported by PostgreSQL, in the IANA/Olson database format. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public string Timezone
        {
            get
            {
                return InternalConnection.Timezone;
            }
        }

        /// <summary>
        /// Holds all PostgreSQL parameters received for this connection. This property is read-only.
        /// </summary>
        public IReadOnlyDictionary<string, string> PostgresParameters
        {
            get
            {
                return InternalConnection.PostgresParameters;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnection"/> class with no arguments.
        /// </summary>
        public WhippetPostgreSqlConnection()
            : base(new NpgsqlConnection())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnection"/> class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">Connection string used to open a connection to the MySQL database.</param>
        public WhippetPostgreSqlConnection(string connectionString)
            : base(new NpgsqlConnection(connectionString))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnection"/> class with the specified <see cref="MySqlConnection"/>.
        /// </summary>
        /// <param name="connection"><see cref="WhippetPostgreSqlConnection"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public WhippetPostgreSqlConnection(NpgsqlConnection connection)
            : base(connection)
        { }

        /// <summary>
        /// Creates and returns a <see cref="WhippetPostgreSqlCommand"/> object associated with the <see cref="WhippetPostgreSqlConnection"/>.
        /// </summary>
        /// <returns><see cref="WhippetPostgreSqlCommand"/> object.</returns>
        public new WhippetPostgreSqlCommand CreateCommand()
        {
            return new WhippetPostgreSqlCommand(InternalConnection.CreateCommand());
        }

        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="WhippetPostgreSqlBatch"/> class.
        /// </summary>
        /// <returns>A new instance of <see cref="WhippetPostgreSqlBatch"/>.</returns>
        public new WhippetPostgreSqlBatch CreateBatch()
        {
            return InternalConnection.CreateBatch();
        }

        /// <summary>
        /// Begins a database transaction.
        /// </summary>
        /// <returns>A <see cref="WhippetPostgreSqlTransaction"/> object representing a new transaction.</returns>
        public new WhippetPostgreSqlTransaction BeginTransaction()
        {
            return InternalConnection.BeginTransaction();
        }
        
        /// <summary>
        /// Begins a database transaction.
        /// </summary>
        /// <param name="level">The isolation level under which the transaction should run.</param>
        /// <returns>A <see cref="WhippetPostgreSqlTransaction"/> object representing a new transaction.</returns>
        public new WhippetPostgreSqlTransaction BeginTransaction(IsolationLevel level)
        {
            return InternalConnection.BeginTransaction(level);
        }

        /// <summary>
        /// Asynchronously begins a database transaction.
        /// </summary>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="ValueTask{TResult}"/> containing the created <see cref="WhippetPostgreSqlTransaction"/> object.</returns>
        public new async ValueTask<WhippetPostgreSqlTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            NpgsqlTransaction trans = await InternalConnection.BeginTransactionAsync(cancellationToken);
            return trans;
        }

        /// <summary>
        /// Asynchronously begins a database transaction.
        /// </summary>
        /// <param name="level">The isolation level under which the transaction should run.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="ValueTask{TResult}"/> containing the created <see cref="WhippetPostgreSqlTransaction"/> object.</returns>
        public new async ValueTask<WhippetPostgreSqlTransaction> BeginTransactionAsync(IsolationLevel level, CancellationToken cancellationToken = default)
        {
            NpgsqlTransaction trans = await InternalConnection.BeginTransactionAsync(level, cancellationToken);
            return trans;
        }

        /// <summary>
        /// Enlists in the specified transaction. 
        /// </summary>
        /// <param name="transaction">Transaction to enlist.</param>
        public override void EnlistTransaction(Transaction transaction)
        {
            InternalConnection.EnlistTransaction(transaction);
        }

        /// <summary>
        /// Releases the connection. If the connection is pooled, it will be returned to the pool and made available for re-use. If it is non-pooled, the physical connection will be closed.
        /// </summary>
        public override void Close()
        {
            InternalConnection.Close();
        }

        /// <summary>
        /// Asynchronously releases the connection. If the connection is pooled, it will be returned to the pool and made available for re-use. If it is non-pooled, the physical connection will be closed.
        /// </summary>
        /// <returns><see cref="Task"/> object.</returns>
        public override Task CloseAsync()
        {
            return InternalConnection.CloseAsync();
        }

        /// <summary>
        /// Disposes of the current object and releases its resources from memory.
        /// </summary>
        public new void Dispose()
        {
            InternalConnection.Dispose();
        }
        
        /// <summary>
        /// Asynchronously disposes of the current object and releases its resources from memory.
        /// </summary>
        /// <returns><see cref="ValueTask"/> object.</returns>
        public override ValueTask DisposeAsync()
        {
            return InternalConnection.DisposeAsync();
        }

        /// <summary>
        /// Creates a duplicate instance of the current object.
        /// </summary>
        /// <returns>Duplicate instance of the current object.</returns>
        object ICloneable.Clone()
        {
            return ((ICloneable)(InternalConnection)).Clone();
        }

        /// <summary>
        /// Creates a duplicate instance of the current object with the specified connection string.
        /// </summary>
        /// <param name="connectionString">Connection string to apply to the new instance.</param>
        /// <returns><see cref="WhippetPostgreSqlConnection"/> object.</returns>
        public WhippetPostgreSqlConnection Clone(string connectionString)
        {
            return InternalConnection.CloneWith(connectionString);
        }
        
        /// <summary>
        /// Creates a duplicate instance of the current object.
        /// </summary>
        /// <typeparam name="TObject">Type of object to return.</typeparam>
        /// <returns>Duplicate instance of the current object.</returns>
        TObject IWhippetCloneable.Clone<TObject>(Guid? createdBy)
        {
            return (TObject)(((ICloneable)(this)).Clone());
        }

        /// <summary>
        /// Changes the current database by disconnecting from the actual database and connecting to the specified.
        /// </summary>
        /// <param name="databaseName">The name of the database to use in place of the current database.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public override void ChangeDatabase(string databaseName)
        {
            InternalConnection.ChangeDatabase(databaseName);
        }

        /// <summary>
        /// Changes the current database by disconnecting from the actual database and connecting to the specified.
        /// </summary>
        /// <param name="databaseName">The name of the database to use in place of the current database.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected override void ChangeConnectionStringDatabase(string databaseName)
        {
            if (String.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }
            else if (!String.IsNullOrWhiteSpace(ConnectionString))
            {
                StringBuilder builder = new StringBuilder();
                string[] pieces = ConnectionString.Split(new char[] { ';' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (pieces != null && pieces.Length > 0)
                {
                    for (int i = 0; i < pieces.Length; i++)
                    {
                        if (!pieces[i].StartsWith("database", StringComparison.InvariantCultureIgnoreCase))
                        {
                            builder.Append(pieces[i] + ';');
                        }
                        else
                        {
                            builder.Append("database=" + databaseName.ToLowerInvariant() + ';');
                        }
                    }
                }

                ConnectionString = builder.ToString();
            }
        }

        /// <summary>
        /// Begins a binary <b>COPY FROM STDIN</b> operation, a high-performance data import mechanism to a PostgreSQL table.
        /// </summary>
        /// <param name="copyFromCommand">A <b>COPY FROM STDIN</b> database command.</param>
        /// <returns><see cref="WhippetPostgreSqlBinaryImporter"/> object which can be used to write rows and columns.</returns>
        public WhippetPostgreSqlBinaryImporter BeginBinaryImport(string copyFromCommand)
        {
            return InternalConnection.BeginBinaryImport(copyFromCommand);
        }

        /// <summary>
        /// Begins an asynchronous binary <b>COPY FROM STDIN</b> operation, a high-performance data import mechanism to a PostgreSQL table.
        /// </summary>
        /// <param name="copyFromCommand">A <b>COPY FROM STDIN</b> database command.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="Task{TResult}"/> object.</returns>
        public async Task<WhippetPostgreSqlBinaryImporter> BeginBinaryImportAsync(string copyFromCommand, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.BeginBinaryImportAsync(copyFromCommand, cancellationToken);
        }

        /// <summary>
        /// Begins a binary <b>COPY TO STDOUT</b> operation, a high-performance data export mechanism from a PostgreSQL table. 
        /// </summary>
        /// <param name="copyToCommand">A <b>COPY TO STDOUT</b> SQL command.</param>
        /// <returns><see cref="WhippetPostgreSqlBinaryExporter"/> object which can be used to read rows and columns.</returns>
        public WhippetPostgreSqlBinaryExporter BeginBinaryExport(string copyToCommand)
        {
            return InternalConnection.BeginBinaryExport(copyToCommand);
        }

        /// <summary>
        /// Asynchronously begins a binary <b>COPY TO STDOUT</b> operation, a high-performance data export mechanism from a PostgreSQL table. 
        /// </summary>
        /// <param name="copyToCommand">A <b>COPY TO STDOUT</b> SQL command.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="Task{TResult}"/> object.</returns>
        public async Task<WhippetPostgreSqlBinaryExporter> BeginBinaryExportAsync(string copyToCommand, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.BeginBinaryExportAsync(copyToCommand, cancellationToken);
        }

        /// <summary>
        /// Begins a textual <b>COPY FROM STDIN</b> operation, a data import mechanism to a PostgreSQL table.
        /// </summary>
        /// <param name="copyFromCommand">A <b>COPY FROM STDIN</b> SQL command.</param>
        /// <returns><see cref="TextWriter"/> object that can be used to send textual data.</returns>
        public TextWriter BeginTextImport(string copyFromCommand)
        {
            return InternalConnection.BeginTextImport(copyFromCommand);
        }

        /// <summary>
        /// Asynchronously begins a textual <b>COPY FROM STDIN</b> operation, a data import mechanism to a PostgreSQL table.
        /// </summary>
        /// <param name="copyFromCommand">A <b>COPY FROM STDIN</b> SQL command.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="Task{TResult}"/> object.</returns>
        public async Task<TextWriter> BeginTextImportAsync(string copyFromCommand, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.BeginTextImportAsync(copyFromCommand, cancellationToken);
        }

        /// <summary>
        /// Begins a textual <b>COPY TO STDOUT</b> operation, a data import mechanism to a PostgreSQL table.
        /// </summary>
        /// <param name="copyToCommand">A <b>COPY TO STDOUT</b> SQL command.</param>
        /// <returns><see cref="TextReader"/> object that can be used to read textual data.</returns>
        public TextReader BeginTextExport(string copyToCommand)
        {
            return InternalConnection.BeginTextExport(copyToCommand);
        }

        /// <summary>
        /// Asynchronously begins a textual <b>COPY TO STDOUT</b> operation, a data import mechanism to a PostgreSQL table.
        /// </summary>
        /// <param name="copyToCommand">A <b>COPY TO STDOUT</b> SQL command.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="Task{TResult}"/> object.</returns>
        public async Task<TextReader> BeginTextExportAsync(string copyToCommand, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.BeginTextExportAsync(copyToCommand, cancellationToken);
        }

        /// <summary>
        /// Begins a raw binary COPY operation (TO STDOUT or FROM STDIN), a high-performance data export/import mechanism to a PostgreSQL table.
        /// Note that unlike the other COPY API methods, <see cref="BeginRawBinaryCopy(string)"/> doesn't implement any encoding/decoding
        /// and is unsuitable for structured import/export operation. It is useful mainly for exporting a table as an opaque
        /// blob, for the purpose of importing it back later.
        /// </summary>
        /// <param name="copyCommand">A <b>COPY TO STDOUT</b> or <b>COPY FROM STDIN</b> SQL command.</param>
        /// <returns>A <see cref="WhippetPostgreSQLRawCopyStream"/> that can be used to read or write raw binary data.</returns>
        public WhippetPostgreSQLRawCopyStream BeginRawBinaryCopy(string copyCommand)
        {
            return InternalConnection.BeginRawBinaryCopy(copyCommand);
        }

        /// <summary>
        /// Begins an asynchronous raw binary COPY operation (TO STDOUT or FROM STDIN), a high-performance data export/import mechanism to a PostgreSQL table.
        /// Note that unlike the other COPY API methods, <see cref="BeginRawBinaryCopyAsync(string, CancellationToken)"/> doesn't implement any encoding/decoding
        /// and is unsuitable for structured import/export operation. It is useful mainly for exporting a table as an opaque
        /// blob, for the purpose of importing it back later.
        /// </summary>
        /// <param name="copyCommand">A <b>COPY TO STDOUT</b> or <b>COPY FROM STDIN</b> SQL command.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="Task{TResult}"/> object.</returns>
        public async Task<WhippetPostgreSQLRawCopyStream> BeginRawBinaryCopyAsync(string copyCommand, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.BeginRawBinaryCopyAsync(copyCommand, cancellationToken);
        }

        /// <summary>
        /// Waits until an asynchronous PostgreSQL messages (e.g. a notification) arrives, and exits immediately. The asynchronous message is delivered via the normal events (<see cref="Notification"/>, <see cref="Notice"/>).
        /// </summary>
        /// <param name="timeout">The time-out value, in milliseconds, passed to <see cref="System.Net.Sockets.Socket.ReceiveTimeout"/>. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</param>
        /// <returns><see langword="true"/> if an asynchronous message was received, <see langword="false"/> if timed out.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public bool Wait(int timeout)
        {
            return InternalConnection.Wait(timeout);
        }

        /// <summary>
        /// Waits until an asynchronous PostgreSQL messages (e.g. a notification) arrives, and exits immediately. The asynchronous message is delivered via the normal events (<see cref="Notification"/>, <see cref="Notice"/>).
        /// </summary>
        /// <param name="timeout">The timeout value that is passed to <see cref="System.Net.Sockets.Socket.ReceiveTimeout"/>.</param>
        /// <returns><see langword="true"/> if an asynchronous message was received, <see langword="false"/> if timed out.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public bool Wait(TimeSpan timeout)
        {
            return InternalConnection.Wait(timeout);
        }

        /// <summary>
        /// Waits until an asynchronous PostgreSQL messages (e.g. a notification) arrives, and exits immediately. The asynchronous message is delivered via the normal events (<see cref="Notification"/>, <see cref="Notice"/>).
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public void Wait()
        {
            InternalConnection.Wait();
        }

        /// <summary>
        /// Waits asynchronously until an asynchronous PostgreSQL messages (e.g. a notification) arrives, and exits immediately. The asynchronous message is delivered via the normal events (<see cref="Notification"/>, <see cref="Notice"/>).
        /// </summary>
        /// <param name="timeout">The timeout value (in milliseconds). Any value less than or equal to zero (0) indicates an indefinite timeout period.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see langword="true"/> if an asynchronous message was received, <see langword="false"/> if timed out.</returns>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<bool> WaitAsync(int timeout, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.WaitAsync(timeout, cancellationToken);
        }
        
        /// <summary>
        /// Waits asynchronously until an asynchronous PostgreSQL messages (e.g. a notification) arrives, and exits immediately. The asynchronous message is delivered via the normal events (<see cref="Notification"/>, <see cref="Notice"/>).
        /// </summary>
        /// <param name="timeout"><see cref="TimeSpan"/> struct containing the timeout value.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see langword="true"/> if an asynchronous message was received, <see langword="false"/> if timed out.</returns>
        public async Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.WaitAsync(timeout, cancellationToken);
        }

        /// <summary>
        /// Waits asynchronously until an asynchronous PostgreSQL messages (e.g. a notification) arrives, and exits immediately. The asynchronous message is delivered via the normal events (<see cref="Notification"/>, <see cref="Notice"/>).
        /// </summary>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns><see cref="Task"/> object.</returns>
        public Task WaitAsync(CancellationToken cancellationToken = default)
        {
            return InternalConnection.WaitAsync(cancellationToken);
        }

        /// <summary>
        /// Returns schema information for the data source of this <see cref="WhippetPostgreSqlConnection"/>.
        /// </summary>
        /// <returns>A <see cref="DataTable"/> that contains schema information.</returns>
        public override DataTable GetSchema()
        {
            return InternalConnection.GetSchema();
        }

        /// <summary>
        /// Returns the schema collection specified by the collection name filtered by the restrictions.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <param name="restrictions">The restriction values to filter the results by.</param>
        /// <returns>A <see cref="DataTable"/> that contains schema information.</returns>
        public override DataTable GetSchema(string collectionName, string[] restrictions)
        {
            return InternalConnection.GetSchema(collectionName, restrictions);
        }

        /// <summary>
        /// Asynchronously returns schema information for the data source of this <see cref="WhippetPostgreSqlConnection"/>.
        /// </summary>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="DataTable"/> that contains schema information.</returns>
        public override Task<DataTable> GetSchemaAsync(CancellationToken cancellationToken = default)
        {
            return InternalConnection.GetSchemaAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns schema information for the data source of this <see cref="WhippetPostgreSqlConnection"/>.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        /// <returns>A <see cref="DataTable"/> that contains schema information.</returns>
        public override Task<DataTable> GetSchemaAsync(string collectionName, CancellationToken cancellationToken = default)
        {
            return InternalConnection.GetSchemaAsync(collectionName, cancellationToken);
        }
        
        /// <summary>
        /// Asynchronously returns the schema collection specified by the collection name filtered by the restrictions.
        /// </summary>
        /// <param name="collectionName">The collection name.</param>
        /// <param name="restrictions">The restriction values to filter the results by.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation.</param>
        public new async Task<DataTable> GetSchemaAsync(string collectionName, string[] restrictions, CancellationToken cancellationToken = default)
        {
            return await InternalConnection.GetSchemaAsync(collectionName, restrictions, cancellationToken);
        }

        /// <summary>
        /// Clears the connection pool. All idle physical connections in the pool of the given connection are immediately closed, and any busy connections which were opened before <see cref="ClearPool"/> was called
        /// will be closed when returned to the pool.
        /// </summary>
        /// <param name="connection"><see cref="WhippetPostgreSqlConnection"/> to the data source to clear pools for.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ClearPool(WhippetPostgreSqlConnection connection)
        {
            ArgumentNullException.ThrowIfNull(connection);
            NpgsqlConnection.ClearPool(connection);
        }

        /// <summary>
        /// Clear all connection pools. All idle physical connections in all pools are immediately closed, and any busy connections which were opened before <see cref="ClearAllPools"/> was called will be closed when returned to their pool.
        /// </summary>
        public static void ClearAllPools()
        {
            NpgsqlConnection.ClearAllPools();
        }

        /// <summary>
        /// Resets all prepared statements on this connection.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public void UnprepareAll()
        {
            InternalConnection.UnprepareAll();
        }

        /// <summary>
        /// Flushes the type cache for this connection's connection string and reloads the types for this connection only. Type changes will appear for other connections only after they are re-opened from the pool.
        /// </summary>
        public void ReloadTypes()
        {
            InternalConnection.ReloadTypes();
        }

        /// <summary>
        /// Asynchronously flushes the type cache for this connection's connection string and reloads the types for this connection only. Type changes will appear for other connections only after they are re-opened from the pool.
        /// </summary>
        public async Task ReloadTypesAsync()
        {
            await InternalConnection.ReloadTypesAsync();
        }

        /// <summary>
        /// Changes the username and password on the connection string.
        /// </summary>
        /// <param name="username">Username to use when connecting to the data source.</param>
        /// <param name="password">Password to use when connecting to the data source.</param>
        public override void UpdateCredentials(string username, string password)
        {
            if (!String.IsNullOrWhiteSpace(ConnectionString))
            {
                StringBuilder builder = new StringBuilder();
                string[] pieces = ConnectionString.Split(new char[] { ';' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                NpgsqlConnection newConnection = null;

                bool foundUsername = false;
                bool foundPassword = false;
                
                if (pieces != null && pieces.Length > 0)
                {
                    for (int i = 0; i < pieces.Length; i++)
                    {
                        if (!pieces[i].StartsWith("user id", StringComparison.InvariantCultureIgnoreCase) 
                                && !pieces[i].StartsWith("password", StringComparison.InvariantCultureIgnoreCase)
                                && !pieces[i].StartsWith("username", StringComparison.InvariantCultureIgnoreCase)
                            )
                        {
                            builder.Append(pieces[i] + ';');
                        }
                        else
                        {
                            if (pieces[i].StartsWith("user id", StringComparison.InvariantCultureIgnoreCase)
                                || pieces[i].StartsWith("username", StringComparison.InvariantCultureIgnoreCase))
                            {
                                foundUsername = true;
                                
                                if (!String.IsNullOrWhiteSpace(username))
                                {
                                    builder.Append("User ID=" + username + ';');
                                }
                            }
                            else
                            {
                                foundPassword = true;
                                
                                if (!String.IsNullOrWhiteSpace(password))
                                {
                                    builder.Append("Password=" + password + ';');
                                }
                            }
                        }
                    }
                }

                if (!builder.ToString().EndsWith(';'))
                {
                    builder.Append(';');
                }

                if (!foundUsername && !String.IsNullOrWhiteSpace(username))
                {
                    builder.Append("User ID=" + username + ';');
                }

                if (!foundPassword && !String.IsNullOrWhiteSpace(password))
                {
                    builder.Append("Password=" + password + ';');
                }
                
                if (InternalConnection.State != ConnectionState.Closed)
                {
                    InternalConnection.Close();
                }
                
                newConnection = InternalConnection.CloneWith(builder.ToString());

                Dispose();

                ResetConnection(newConnection);
            }
        }

        public static implicit operator NpgsqlConnection(WhippetPostgreSqlConnection connection)
        {
            return (connection == null) ? null : connection.InternalConnection;
        }

        public static implicit operator WhippetPostgreSqlConnection(NpgsqlConnection connection)
        {
            return (connection == null) ? null : new WhippetPostgreSqlConnection(connection);
        }
    }
}
