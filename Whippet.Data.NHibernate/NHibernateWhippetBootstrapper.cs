﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Athi.Whippet.Data.NHibernate
{
    /// <summary>
    /// Bootstrapper that provides methods for creating NHibernate database connections. This class cannot be inherited.
    /// </summary>
    public static class NHibernateWhippetBootstrapper
    {
        /// <summary>
        /// Creates an <see cref="ISessionFactory"/> object for creating connections to the database.
        /// </summary>
        /// <param name="connectionString">Connection string to assign to the internal session connection strings.</param>
        /// <param name="mappings">NHibernate Fluent mappings to apply for the session.</param>
        /// <param name="properties">Additional NHibernate properties to apply to the engine.</param>
        /// <returns><see cref="ISessionFactory"/> objects.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ISessionFactory CreateSessionFactory(string connectionString, IEnumerable<NHibernateBootstrapperMappingDelegate> mappings = null, NHibernatePropertyCollection properties = null)
        {
            return CreateSessionFactory(connectionString, mappings, properties, SupportedWhippetDatabaseTypes.MsSql2012); 
        }

        /// <summary>
        /// Creates an <see cref="ISessionFactory"/> object for creating connections to the database.
        /// </summary>
        /// <param name="connectionString">Connection string to assign to the internal session connection strings.</param>
        /// <param name="mappings">NHibernate Fluent mappings to apply for the session.</param>
        /// <param name="properties">Additional NHibernate properties to apply to the engine.</param>
        /// <param name="dbType">Type of database to create a session factory for.</param>
        /// <returns><see cref="ISessionFactory"/> objects.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ISessionFactory CreateSessionFactory(string connectionString, IEnumerable<NHibernateBootstrapperMappingDelegate> mappings, NHibernatePropertyCollection properties, SupportedWhippetDatabaseTypes dbType)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            else
            {
                NHibernateConfigurationOptions options = new NHibernateConfigurationOptions();

                if (mappings != null && mappings.Any())
                {
                    foreach (NHibernateBootstrapperMappingDelegate bmd in mappings)
                    {
                        bmd(options);
                    }
                }

                switch (dbType)
                {
                    case SupportedWhippetDatabaseTypes.MsSql2012:
                        NHibernateConfigurationHelper.ConfigureForSqlServerWithConnectionString(options, connectionString);
                        break;
                    case SupportedWhippetDatabaseTypes.Sqlite:
                        NHibernateConfigurationHelper.ConfigureForSqlite(options);
                        break;
                    case SupportedWhippetDatabaseTypes.MySql:
                        NHibernateConfigurationHelper.ConfigureForMySqlWithConnectionString(options, connectionString);
                        break;
                    case SupportedWhippetDatabaseTypes.PostgreSql:
                        NHibernateConfigurationHelper.ConfigureForPostgreSqlWithConnectionString(options, connectionString);
                        break;
                    default:
                        throw new InvalidEnumArgumentException(nameof(dbType), Convert.ToInt32(dbType), typeof(SupportedWhippetDatabaseTypes));
                }

                if (options.Properties == null)
                {
                    options.Properties = properties;
                }
                else if (properties != null && properties.Any())
                {
                    foreach (KeyValuePair<string, string> property in properties)
                    {
                        if (options.Properties.ContainsKey(property.Key))
                        {
                            options.Properties[property.Key] = property.Value;
                        }
                        else
                        {
                            options.Properties.Add(property.Key, property.Value);
                        }
                    }
                }

                return DefaultNHibernateSessionFactory.Create(options);
            }            
        }
    }
}
