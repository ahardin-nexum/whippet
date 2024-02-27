using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.ComponentModel;
using Npgsql;

namespace Athi.Whippet.Data.Database.PostgreSQL
{
    /// <summary>
    /// Provides a simple way to create and mange the contents of connection string used by the <see cref="WhippetPostgreSqlConnection"/> class. This class cannot be inherited.
    /// </summary>
    public sealed class WhippetPostgreSqlConnectionStringBuilder : DbConnectionStringBuilder, IDictionary<string, object>, IDictionary, ICustomTypeDescriptor, ICollection<KeyValuePair<string, object>>, IEnumerable, IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Gets or sets the internal <see cref="NpgsqlConnectionStringBuilder"/> object. 
        /// </summary>
        private NpgsqlConnectionStringBuilder InternalBuilder
        { get; set; }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="keyword">The key of the item to get or set.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        [Browsable(false)]
        public override object this[string keyword]
        {
            get
            {
                return InternalBuilder[keyword];
            }
            set
            {
                InternalBuilder[keyword] = value;
            }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="keyword">The key of the element to get or set.</param>
        object IDictionary.this[object keyword]
        {
            get
            {
                return ((IDictionary)(InternalBuilder))[keyword];
            }
            set
            {
                ((IDictionary)(InternalBuilder))[keyword] = value;
            }
        }

        /// <summary>
        /// Indicates whether access to the <see cref="ICollection"/> is thread safe. This property is read-only.
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection)(InternalBuilder)).IsSynchronized;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ICollection"/>. This property is read-only.
        /// </summary>
        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)(InternalBuilder)).SyncRoot;
            }
        }
        
        /// <summary>
        /// Gets an <see cref="ICollection"/> that contains the keys in the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public override ICollection Keys
        {
            get
            {
                return ((IDictionary)(InternalBuilder)).Keys;
            }
        }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> that contains the keys in the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>. This property is read-only.
        /// </summary>
        ICollection<string> IDictionary<string, object>.Keys
        {
            get
            {
                return ((IDictionary<string, object>)(InternalBuilder)).Keys;
            }
        }

        /// <summary>
        /// Gets an <see cref="ICollection"/> that contains the values in the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public override ICollection Values
        {
            get
            {
                return ((IDictionary)(InternalBuilder)).Values;
            }
        }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> that contains the values in the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>. This property is read-only.
        /// </summary>
        [Browsable(false)]
        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return ((IDictionary<string, object>)(InternalBuilder)).Values;
            }
        }
        
        /// <summary>
        /// Specifies whether the <see cref="ConnectionString"/> property is visible in Visual Studio designers.
        /// </summary>
        [Browsable(false)]
        public new bool BrowsableConnectionString
        {
            get
            {
                return InternalBuilder.BrowsableConnectionString;
            }
            set
            {
                InternalBuilder.BrowsableConnectionString = value;
            }
        }

        /// <summary>
        /// Gets or sets the connection string associated with the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public new string ConnectionString
        {
            get
            {
                return InternalBuilder.ConnectionString;
            }
            set
            {
                InternalBuilder.ConnectionString = value;
            }
        }

        /// <summary>
        /// Gets the current number of keys that are contained within the <see cref="ConnectionString"/> property. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public override int Count
        {
            get
            {
                return InternalBuilder.Count;
            }
        }

        /// <summary>
        /// Indicates whether the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> has a fixed size. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public override bool IsFixedSize
        {
            get
            {
                return InternalBuilder.IsFixedSize;
            }
        }
        
        /// <summary>
        /// Indicates whether the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> is read-only. This property is read-only.
        /// </summary>
        [Browsable(false)]
        public new bool IsReadOnly
        {
            get
            {
                return InternalBuilder.IsReadOnly;
            }
        }

        /// <summary>
        /// The hostname or IP address of the PostgreSQL server to connect to.
        /// </summary>
        public string Host
        {
            get
            {
                return InternalBuilder.Host;
            }
            set
            {
                InternalBuilder.Host = value;
            }
        }

        /// <summary>
        /// Gets or sets the TCP/IP port of the PostgreSQL server.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Port
        {
            get
            {
                return InternalBuilder.Port;
            }
            set
            {
                InternalBuilder.Port = value;
            }
        }

        /// <summary>
        /// Gets or sets the PostgreSQL database to connect to.
        /// </summary>
        public string Database
        {
            get
            {
                return InternalBuilder.Database;
            }
            set
            {
                InternalBuilder.Database = value;
            }
        }

        /// <summary>
        /// Gets or sets the username to connect with.
        /// </summary>
        public string Username
        {
            get
            {
                return InternalBuilder.Username;
            }
            set
            {
                InternalBuilder.Username = value;
            }
        }

        /// <summary>
        /// Gets or sets the password to connect with.
        /// </summary>
        public string Password
        {
            get
            {
                return InternalBuilder.Password;
            }
            set
            {
                InternalBuilder.Password = value;
            }
        }

        /// <summary>
        /// Gets or sets the path to a PostgreSQL password file (<b>PGPASSFILE</b>) from which the password should be taken.
        /// </summary>
        public string Passfile
        {
            get
            {
                return InternalBuilder.Passfile;
            }
            set
            {
                InternalBuilder.Passfile = value;
            }
        }

        /// <summary>
        /// Gets or sets the optional application name parameter to be sent to the backend during connection initiation.
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return InternalBuilder.ApplicationName;
            }
            set
            {
                InternalBuilder.ApplicationName = value;
            }
        }

        /// <summary>
        /// Specifies whether to enlist in an ambient transaction scope.
        /// </summary>
        public bool Enlist
        {
            get
            {
                return InternalBuilder.Enlist;
            }
            set
            {
                InternalBuilder.Enlist = value;
            }
        }

        /// <summary>
        /// Gets or sets the schema search path.
        /// </summary>
        public string SearchPath
        {
            get
            {
                return InternalBuilder.SearchPath;
            }
            set
            {
                InternalBuilder.SearchPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the <b>client_encoding</b> parameter.
        /// </summary>
        public string ClientEncoding
        {
            get
            {
                return InternalBuilder.ClientEncoding;
            }
            set
            {
                InternalBuilder.ClientEncoding = value;
            }
        }

        /// <summary>
        /// Gets or sets the .NET encoding that will be used to encode/decode PostgreSQL string data.
        /// </summary>
        public string Encoding
        {
            get
            {
                return InternalBuilder.Encoding;
            }
            set
            {
                InternalBuilder.Encoding = value;
            }
        }

        /// <summary>
        /// Gets or sets the PostgreSQL session timezone in Olson/IANA database format.
        /// </summary>
        public string Timezone
        {
            get
            {
                return InternalBuilder.Timezone;
            }
            set
            {
                InternalBuilder.Timezone = value;
            }
        }

        /// <summary>
        /// Specifies whether SSL is required, disabled, or preferred, depending on server support.
        /// </summary>
        public SslMode SslMode
        {
            get
            {
                return InternalBuilder.SslMode;
            }
            set
            {
                InternalBuilder.SslMode = value;
            }
        }

        /// <summary>
        /// Specifies the location of a client certificate to be sent to the server.
        /// </summary>
        public string SslCertificate
        {
            get
            {
                return InternalBuilder.SslCertificate;
            }
            set
            {
                InternalBuilder.SslCertificate = value;
            }
        }

        /// <summary>
        /// Gets or sets the location of a client key for a client certificate to be sent to the server.
        /// </summary>
        public string SslKey
        {
            get
            {
                return InternalBuilder.SslKey;
            }
            set
            {
                InternalBuilder.SslKey = value;
            }
        }

        /// <summary>
        /// Gets or sets the password for a key for a client certificate.
        /// </summary>
        public string SslPassword
        {
            get
            {
                return InternalBuilder.SslPassword;
            }
            set
            {
                InternalBuilder.SslPassword = value;
            }
        }

        /// <summary>
        /// Gets or sets the location of a CA certificate used to validate the server certificate.
        /// </summary>
        public string RootCertificate
        {
            get
            {
                return InternalBuilder.RootCertificate;
            }
            set
            {
                InternalBuilder.RootCertificate = value;
            }
        }

        /// <summary>
        /// Specifies whether to check the certificate revocation list during authentication.
        /// </summary>
        public bool CheckCertificateRevocation
        {
            get
            {
                return InternalBuilder.CheckCertificateRevocation;
            }
            set
            {
                InternalBuilder.CheckCertificateRevocation = value;
            }
        }

        /// <summary>
        /// Gets or sets the Kerberos service name to be used for authentication.
        /// </summary>
        public string KerberosServiceName
        {
            get
            {
                return InternalBuilder.KerberosServiceName;
            }
            set
            {
                InternalBuilder.KerberosServiceName = value;
            }
        }

        /// <summary>
        /// Gets or sets the Kerberos realm to be used for authentication.
        /// </summary>
        public bool IncludeRealm
        {
            get
            {
                return InternalBuilder.IncludeRealm;
            }
            set
            {
                InternalBuilder.IncludeRealm = value;
            }
        }

        /// <summary>
        /// Specifies whether security-sensitive information is not returned as part of the connection if the connection is open or has ever been in an open state.
        /// </summary>
        public bool PersistSecurityInfo
        {
            get
            {
                return InternalBuilder.PersistSecurityInfo;
            }
            set
            {
                InternalBuilder.PersistSecurityInfo = value;
            }
        }

        /// <summary>
        /// Specifies whether parameter values are to be logged when commands are executed.
        /// </summary>
        public bool LogParameters
        {
            get
            {
                return InternalBuilder.LogParameters;
            }
            set
            {
                InternalBuilder.LogParameters = value;
            }
        }

        /// <summary>
        /// Specifies whether PostgreSQL error details are included on <see cref="PostgresException.Detail"/> and <see cref="PostgresNotice.Detail"/>. Note that these can contain sensitive data.
        /// </summary>
        public bool IncludeErrorDetail
        {
            get
            {
                return InternalBuilder.IncludeErrorDetail;
            }
            set
            {
                InternalBuilder.IncludeErrorDetail = value;
            }
        }

        /// <summary>
        /// Controls whether channel binding is required, disabled, or preferred, depending on server support.
        /// </summary>
        public ChannelBinding ChannelBinding
        {
            get
            {
                return InternalBuilder.ChannelBinding;
            }
            set
            {
                InternalBuilder.ChannelBinding = value;
            }
        }

        /// <summary>
        /// Specifies whether connection pooling should be used.
        /// </summary>
        public bool Pooling
        {
            get
            {
                return InternalBuilder.Pooling;
            }
            set
            {
                InternalBuilder.Pooling = value;
            }
        }

        /// <summary>
        /// Specifies the minimum connection pool size.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int MinPoolSize
        {
            get
            {
                return InternalBuilder.MinPoolSize;
            }
            set
            {
                InternalBuilder.MinPoolSize = value;
            }
        }

        /// <summary>
        /// Specifies the maximum connection pool size.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int MaxPoolSize
        {
            get
            {
                return InternalBuilder.MaxPoolSize;
            }
            set
            {
                InternalBuilder.MaxPoolSize = value;
            }
        }

        /// <summary>
        /// Specifies the time to wait before closing idle connections in the pool if the count of all connections exceeds <see cref="MinPoolSize"/>.
        /// </summary>
        public int ConnectionIdleLifetime
        {
            get
            {
                return InternalBuilder.ConnectionIdleLifetime;
            }
            set
            {
                InternalBuilder.ConnectionIdleLifetime = value;
            }
        }

        /// <summary>
        /// Specifies the number of seconds the pool waits before attempting to prune idle connections that are beyond idle lifetime.
        /// </summary>
        public int ConnectionPruningInterval
        {
            get
            {
                return InternalBuilder.ConnectionPruningInterval;
            }
            set
            {
                InternalBuilder.ConnectionPruningInterval = value;
            }
        }

        /// <summary>
        /// Specifies the total maximum lifetime of connections (in seconds). Connections which have exceeded this value will be 
        /// destroyed instead of returned from the pool. This is useful in clustered configurations to force load balancing between a
        /// running server and a server just brought online.
        /// </summary>
        public int ConnectionLifeTime
        {
            get
            {
                return InternalBuilder.ConnectionLifetime;
            }
            set
            {
                InternalBuilder.ConnectionLifetime = value;
            }
        }

        /// <summary>
        /// Specifies the time to wait (in seconds) while trying to establish a connection before terminating the attempt and generating an error. 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Timeout
        {
            get
            {
                return InternalBuilder.Timeout;
            }
        }

        /// <summary>
        /// Specifies the time to wait (in seconds) while trying to execute a command before terminating the attempt and generating an error.
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return InternalBuilder.CommandTimeout;
            }
            set
            {
                InternalBuilder.CommandTimeout = value;
            }
        }

        /// <summary>
        /// Specifies the time to wait (in milliseconds) while trying to read a response for a cancellation request for a timed out or cancelled query, before terminating the attempt and generating an error. Use zero (0) for infinity and -1 to skip the wait.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int CancellationTimeout
        {
            get
            {
                return InternalBuilder.CancellationTimeout;
            }
            set
            {
                InternalBuilder.CancellationTimeout = value;
            }
        }

        /// <summary>
        /// Specifies the preferred PostgreSQL target server type.
        /// </summary>
        public TargetSessionAttributes? TargetSessionAttributes
        {
            get
            {
                TargetSessionAttributes? attribs = null;

                if (!String.IsNullOrWhiteSpace(InternalBuilder.TargetSessionAttributes))
                {
                    attribs = Enum.Parse<TargetSessionAttributes>(InternalBuilder.TargetSessionAttributes);
                }

                return attribs;
            }
            set
            {
                string attrib = null;

                if (value.HasValue)
                {
                    if (value.Value == Npgsql.TargetSessionAttributes.PreferPrimary)
                    {
                        attrib = "prefer-primary";
                    }
                    else if (value.Value == Npgsql.TargetSessionAttributes.PreferStandby)
                    {
                        attrib = "prefer-standby";
                    }
                    else if (value.Value == Npgsql.TargetSessionAttributes.ReadWrite)
                    {
                        attrib = "read-write";
                    }
                    else if (value.Value == Npgsql.TargetSessionAttributes.ReadOnly)
                    {
                        attrib = "read-only";
                    }
                    else
                    {
                        attrib = value.Value.ToString().ToLower();
                    }

                    InternalBuilder.TargetSessionAttributes = attrib;
                }
            }
        }

        /// <summary>
        /// Enables or disables balancing between multiple hosts by round-robin.
        /// </summary>
        public bool LoadBalanceHosts
        {
            get
            {
                return InternalBuilder.LoadBalanceHosts;
            }
            set
            {
                InternalBuilder.LoadBalanceHosts = value;
            }
        }

        /// <summary>
        /// Specifies how long the host's cached state will be considered as valid.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public int HostRecheckSeconds
        {
            get
            {
                return InternalBuilder.HostRecheckSeconds;
            }
            set
            {
                InternalBuilder.HostRecheckSeconds = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of seconds of connection inactivity before a keepalive query is sent. If zero (0), the keepalive is disabled.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int KeepAlive
        {
            get
            {
                return InternalBuilder.KeepAlive;
            }
            set
            {
                InternalBuilder.KeepAlive = value;
            }
        }

        /// <summary>
        /// Specifies whether to use TCP keepalive with system defaults if overrides aren't specified.
        /// </summary>
        public bool TcpKeepAlive
        {
            get
            {
                return InternalBuilder.TcpKeepAlive;
            }
            set
            {
                InternalBuilder.TcpKeepAlive = value;
            }
        }

        /// <summary>
        /// Specifies the number of seconds of connection inactivity before a TCP keepalive query is sent. Use of this option is discouraged, use <see cref="KeepAlive"/> instead if possible. 
        /// Set to zero (0) (the default) to disable.
        /// </summary>
        public int TcpKeepAliveTime
        {
            get
            {
                return InternalBuilder.TcpKeepAliveTime;
            }
            set
            {
                InternalBuilder.TcpKeepAliveTime = value;
            }
        }

        /// <summary>
        /// Specifies the interval, in seconds, between when successive keep-alive packets are sent if no acknowledgement is received.
        /// Defaults to the value of <see cref="TcpKeepAliveTime"/>. <see cref="TcpKeepAliveTime"/> must be non-zero as well.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int TcpKeepAliveInterval
        {
            get
            {
                return InternalBuilder.TcpKeepAliveInterval;
            }
            set
            {
                InternalBuilder.TcpKeepAliveInterval = value;
            }
        }

        /// <summary>
        /// Specifies the size of the internal buffer PostgreSQL uses when reading. Increasing may improve performance if transferring large values from the database.
        /// </summary>
        public int ReadBufferSize
        {
            get
            {
                return InternalBuilder.ReadBufferSize;
            }
            set
            {
                InternalBuilder.ReadBufferSize = value;
            }
        }

        /// <summary>
        /// Determines the size of the internal buffer PostgreSQL uses when writing. Increasing may improve performance if transferring large values to the database.
        /// </summary>
        public int WriteBufferSize
        {
            get
            {
                return InternalBuilder.WriteBufferSize;
            }
            set
            {
                InternalBuilder.WriteBufferSize = value;
            }
        }

        /// <summary>
        /// Specifies the size of socket read buffer.
        /// </summary>
        public int SocketReceiveBufferSize
        {
            get
            {
                return InternalBuilder.SocketReceiveBufferSize;
            }
            set
            {
                InternalBuilder.SocketReceiveBufferSize = value;
            }
        }

        /// <summary>
        /// Specifies the size of socket send buffer.
        /// </summary>
        public int SocketSendBufferSize
        {
            get
            {
                return InternalBuilder.SocketSendBufferSize;
            }
            set
            {
                InternalBuilder.SocketSendBufferSize = value;
            }
        }

        /// <summary>
        /// Specifies the maximum number SQL statements that can be automatically prepared at any given point. Beyond this number the least-recently-used statement will be recycled. 
        /// Zero (0) (the default) disables automatic preparation.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int MaxAutoPrepare
        {
            get
            {
                return InternalBuilder.MaxAutoPrepare;
            }
            set
            {
                InternalBuilder.MaxAutoPrepare = value;
            }
        }

        /// <summary>
        /// Specifies the minimum number of usages an SQL statement is used before it's automatically prepared.
        /// </summary>
        public int AutoPrepareMinUsages
        {
            get
            {
                return InternalBuilder.AutoPrepareMinUsages;
            }
            set
            {
                InternalBuilder.AutoPrepareMinUsages = value;
            }
        }
        
        /// <summary>
        /// If <see langword="true"/>, a pool connection's state won't be reset when it is closed (improves performance).
        /// </summary>
        /// <remarks>Do not specify this unless you know what you're doing.</remarks>
        public bool NoResetOnClose
        {
            get
            {
                return InternalBuilder.NoResetOnClose;
            }
            set
            {
                InternalBuilder.NoResetOnClose = value;
            }
        }

        /// <summary>
        /// Specifies whether table composite type definitions are loaded (not just free-standing composite types).
        /// </summary>
        public bool LoadTableComposites
        {
            get
            {
                return InternalBuilder.LoadTableComposites;
            }
            set
            {
                InternalBuilder.LoadTableComposites = value;
            }
        }

        /// <summary>
        /// Specifies PostgreSQL's configuration parameter default values for the connection.
        /// </summary>
        public string Options
        {
            get
            {
                return InternalBuilder.Options;
            }
            set
            {
                InternalBuilder.Options = value;
            }
        }

        /// <summary>
        /// Specifies the way arrays of value types are returned when requested as object instances.
        /// </summary>
        public ArrayNullabilityMode ArrayNullabilityMode
        {
            get
            {
                return InternalBuilder.ArrayNullabilityMode;
            }
            set
            {
                InternalBuilder.ArrayNullabilityMode = value;
            }
        }

        /// <summary>
        /// Specifies whether multiplexing (allows more efficient use of connections) should be enabled.
        /// </summary>
        public bool Multiplexing
        {
            get
            {
                return InternalBuilder.Multiplexing;
            }
            set
            {
                InternalBuilder.Multiplexing = value;
            }
        }

        /// <summary>
        /// When multiplexing is enabled, specifies the maximum number of outgoing bytes to buffer before flushing to the network.
        /// </summary>
        public int WriteCoalescingBufferThresholdBytes
        {
            get
            {
                return InternalBuilder.WriteCoalescingBufferThresholdBytes;
            }
            set
            {
                InternalBuilder.WriteCoalescingBufferThresholdBytes = value;
            }
        }

        /// <summary>
        /// Specifies the compatibility mode for special PostgreSQL server types.
        /// </summary>
        public ServerCompatibilityMode ServerCompatibilityMode
        {
            get
            {
                return InternalBuilder.ServerCompatibilityMode;
            }
            set
            {
                InternalBuilder.ServerCompatibilityMode = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> class with no arguments.
        /// </summary>
        public WhippetPostgreSqlConnectionStringBuilder()
            : this(new NpgsqlConnectionStringBuilder())
        { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="useOdbcRules">If <see langword="true"/>, will use curly braces to delimit fields; otherwise, quotation marks will be used.</param>
        public WhippetPostgreSqlConnectionStringBuilder(bool useOdbcRules)
            : this(new NpgsqlConnectionStringBuilder(useOdbcRules))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="connectionString">Base connection string to use or modify.</param>
        /// <exception cref="ArgumentException"></exception>
        public WhippetPostgreSqlConnectionStringBuilder(string connectionString)
            : this(new NpgsqlConnectionStringBuilder(connectionString))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="builder"><see cref="NpgsqlConnectionStringBuilder"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WhippetPostgreSqlConnectionStringBuilder(NpgsqlConnectionStringBuilder builder)
            : base()
        {
            ArgumentNullException.ThrowIfNull(builder);
            InternalBuilder = builder;
        }
        
        /// <summary>
        /// Adds an entry with the specified key and value into the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.
        /// </summary>
        /// <param name="keyword">The key to add to the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.</param>
        /// <param name="value">The value for the specified key.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public new void Add(string keyword, object value)
        {
            InternalBuilder.Add(keyword, value);
        }

        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item"><see cref="KeyValuePair{TKey,TValue}"/> that contains the key and value to add.</param>
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }
        
        /// <summary>
        /// Clears the contents of the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> instance.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public override void Clear()
        {
            InternalBuilder.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> contains a specific key.
        /// </summary>
        /// <param name="keyword">The key to locate in the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> contains an entry with the specified key; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override bool ContainsKey(string keyword)
        {
            return InternalBuilder.ContainsKey(keyword);
        }

        /// <summary>
        /// Determines whether the collection contains the specified entry.
        /// </summary>
        /// <param name="item"><see cref="KeyValuePair{TKey,TValue}"/> struct to look for.</param>
        /// <returns><see langword="true"/> if the collection contains an entry with the specified key; otherwise, <see langword="false"/>.</returns>
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)(InternalBuilder)).Contains(item);
        }

        /// <summary>
        /// Compares the connection information in this <see cref="WhippetPostgreSqlConnectionStringBuilder"/> object with the connection information in the supplied object.
        /// </summary>
        /// <param name="connectionStringBuilder">The <see cref="DbConnectionStringBuilder"/> to be compared with this <see cref="WhippetPostgreSqlConnectionStringBuilder"/> object.</param>
        /// <returns><see langword="true"/> if the connection information in both of the <see cref="DbConnectionStringBuilder"/> objects causes an equivalent connection string; otherwise, <see langword="false"/>.</returns>
        public override bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder)
        {
            return InternalBuilder.EquivalentTo(connectionStringBuilder);
        }

        /// <summary>
        /// Removes the entry with the specified key from the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> instance.
        /// </summary>
        /// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.</param>
        /// <returns><see langword="true"/> if the key existed within the connection string and was removed; <see langword="false"/> if the key did not exist.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public override bool Remove(string keyword)
        {
            return InternalBuilder.Remove(keyword);
        }

        /// <summary>
        /// Removes an entry from the collection.
        /// </summary>
        /// <param name="item"><see cref="KeyValuePair{TKey,TValue}"/> struct to remove.</param>
        /// <returns><see langword="true"/> if the item existed within the collection and was removed; <see langword="false"/> if the item did not exist.</returns>
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)(InternalBuilder)).Remove(item);
        }
        
        /// <summary>
        /// Indicates whether the specified key exists in this <see cref="WhippetPostgreSqlConnectionStringBuilder"/> instance.
        /// </summary>
        /// <param name="keyword">The key to locate in the <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="WhippetPostgreSqlConnectionStringBuilder"/> contains an entry with the specified key; otherwise, <see langword="false"/>.</returns>
        public override bool ShouldSerialize(string keyword)
        {
            return InternalBuilder.ShouldSerialize(keyword);
        }

        /// <summary>
        /// Returns the connection string associated with this <see cref="WhippetPostgreSqlConnectionStringBuilder"/>.
        /// </summary>
        /// <returns>The current <see cref="ConnectionString"/> property.</returns>
        public override string ToString()
        {
            return InternalBuilder.ToString();
        }

        /// <summary>
        /// Retrieves the value corresponding to the supplied key from this <see cref="WhippetPostgreSqlConnectionStringBuilder"/>. 
        /// </summary>
        /// <param name="keyword">The key of the item to retrieve.</param>
        /// <param name="value">The value corresponding to the <paramref name="keyword"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="keyword"/> was found within the connection string; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override bool TryGetValue(string keyword, out object value)
        {
            return InternalBuilder.TryGetValue(keyword, out value);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection"/>.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)(InternalBuilder)).CopyTo(array, index);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/> to an array, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="ICollection{T}"/>.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int index)
        {
            ((ICollection<KeyValuePair<string, object>>)(InternalBuilder)).CopyTo(array, index);
        }
        
        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IDictionary"/> object.
        /// </summary>
        /// <param name="keyword">The <see cref="object"/> to use as the key of the element to add.</param>
        /// <param name="value">The <see cref="object"/> to use as the value of the element to add.</param>
        void IDictionary.Add(object keyword, object value)
        {
            ((IDictionary)(InternalBuilder)).Add(keyword, value);
        }

        /// <summary>
        /// Determines whether the <see cref="IDictionary"/> object contains an element with the specified key.
        /// </summary>
        /// <param name="keyword">The key to locate in the <see cref="IDictionary"/> object.</param>
        /// <returns><see langword="true"/> if the <see cref="IDictionary"/> contains an element with the key; otherwise, <see langword="false"/>.</returns>
        bool IDictionary.Contains(object keyword)
        {
            return ((IDictionary)(InternalBuilder)).Contains(keyword);
        }

        /// <summary>
        /// Returns an <see cref="IDictionaryEnumerator"/> object for the <see cref="IDictionary"/> object.
        /// </summary>
        /// <returns>An <see cref="IDictionaryEnumerator"/> object for the <see cref="IDictionary"/> object.</returns>
        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)(InternalBuilder)).GetEnumerator();
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IDictionary"/> object.
        /// </summary>
        /// <param name="keyword">The key of the element to remove.</param>
        void IDictionary.Remove(object keyword)
        {
            ((IDictionary)(InternalBuilder)).Remove(keyword);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)(InternalBuilder)).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.</returns>
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)(InternalBuilder)).GetEnumerator();
        }

        /// <summary>
        /// Returns a collection of custom attributes for this instance of a component.
        /// </summary>
        /// <returns>An <see cref="AttributeCollection"/> containing the attributes for this object.</returns>
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetAttributes();
        }

        /// <summary>
        /// Returns the class name of this instance of a component.
        /// </summary>
        /// <returns>The class name of the object, or <see langword="null"/> if the class does not have a name.</returns>
        string ICustomTypeDescriptor.GetClassName()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetClassName();
        }

        /// <summary>
        /// Returns the name of this instance of a component.
        /// </summary>
        /// <returns>The name of the object, or <see langword="null"/> if the class does not have a name.</returns>
        string ICustomTypeDescriptor.GetComponentName()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetComponentName();
        }

        /// <summary>
        /// Returns a type converter for this instance of a component.
        /// </summary>
        /// <returns>A <see cref="TypeConverter"/> that is the converter for this object or <see langword="null"/> if there is no <see cref="TypeConverter"/> for this object.</returns>
        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetConverter();
        }

        /// <summary>
        /// Returns the default event for this instance of a component.
        /// </summary>
        /// <returns>An <see cref="EventDescriptor"/> that represents the default event for this object or <see langword="null"/> if this object does not have events.</returns>
        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetDefaultEvent();
        }

        /// <summary>
        /// Returns the default property for this instance of a component.
        /// </summary>
        /// <returns>A <see cref="PropertyDescriptor"/> that represents the default property for this object or <see langword="null"/> if this object does not have properties.</returns>
        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetDefaultProperty();
        }

        /// <summary>
        /// Returns an editor of the specified type for this instance of a component.
        /// </summary>
        /// <param name="editorBaseType">A <see cref="Type"/> that represents the editor for this object.</param>
        /// <returns>An <see cref="object"/> of the specified type that is the editor of this object or <see langword="null"/> if the editor cannot be found.</returns>
        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetEditor(editorBaseType);
        }

        /// <summary>
        /// Returns the events for this instance of a component.
        /// </summary>
        /// <returns>An <see cref="EventDescriptorCollection"/> that represents the events for this component instance.</returns>
        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetEvents();
        }
        
        /// <summary>
        /// Returns the events for this instance of a component using the specified attribute array as a filter.
        /// </summary>
        /// <param name="attributes">An array of type <see cref="Attribute"/> that is used as a filter.</param>
        /// <returns>An <see cref="EventDescriptorCollection"/> that represents the filtered events for this component instance.</returns>
        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetEvents(attributes);
        }
        
        /// <summary>
        /// Returns the events for this instance of a component.
        /// </summary>
        /// <returns>A <see cref="PropertyDescriptorCollection"/> that represents the properties for this component instance.</returns>
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetProperties();
        }
        
        /// <summary>
        /// Returns the properties for this instance of a component using the specified attribute array as a filter.
        /// </summary>
        /// <param name="attributes">An array of type <see cref="Attribute"/> that is used as a filter.</param>
        /// <returns>A <see cref="PropertyDescriptorCollection"/> that represents the properties for this component instance.</returns>
        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetProperties(attributes);
        }

        /// <summary>
        /// Returns an object that contains the property described by the specified property descriptor.
        /// </summary>
        /// <param name="pd">A <see cref="PropertyDescriptor"/> that represents the property whose owner is to be found.</param>
        /// <returns>An <see cref="Object"/> that represents the owner of the specified property.</returns>
        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return ((ICustomTypeDescriptor)(InternalBuilder)).GetPropertyOwner(pd);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">Object to compare against.</param>
        /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return InternalBuilder.Equals(obj);
        }

        /// <summary>
        /// Gets the hash code for the current instance.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return InternalBuilder.GetHashCode();
        }

        public static implicit operator NpgsqlConnectionStringBuilder(WhippetPostgreSqlConnectionStringBuilder builder)
        {
            return (builder == null) ? null : builder.InternalBuilder;
        }

        public static implicit operator WhippetPostgreSqlConnectionStringBuilder(NpgsqlConnectionStringBuilder builder)
        {
            return (builder == null) ? null : new WhippetPostgreSqlConnectionStringBuilder(builder);
        }
    }
}
