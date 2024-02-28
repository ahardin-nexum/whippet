using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Athi.Whippet.Data.NHibernate
{
    /// <summary>
    /// Base class for all factories used for creating NHibernate objects that are used in an installation process or application initialization. This class must be inherited.
    /// </summary>
    public abstract class NHibernateFactoryBase
    {
        /// <summary>
        /// Gets a singleton instance of the current object. This property is read-only.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        protected static NHibernateFactoryBase Instance
        {
            get
            {
                throw new InvalidOperationException("This property must be overridden in a derived class.");
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateFactoryBase"/> class with no arguments.
        /// </summary>
        protected NHibernateFactoryBase()
        { }
        
        /// <summary>
        /// Creates a new <see cref="NHibernateConfigurationOptions"/> instance with no configuration preloaded.
        /// </summary>
        /// <returns><see cref="NHibernateConfigurationOptions"/> instance.</returns>
        public virtual NHibernateConfigurationOptions CreateConfigurationOptions()
        {
            return new NHibernateConfigurationOptions();
        }

        /// <summary>
        /// Configures Fluent mappings for the specified assemblies.
        /// </summary>
        /// <param name="options"><see cref="NHibernateConfigurationOptions"/> object to configure.</param>
        /// <param name="assemblies"><see cref="Assembly"/> objects containing the mappings to load.</param>
        public virtual void ConfigureMappings(NHibernateConfigurationOptions options, IEnumerable<Assembly> assemblies)
        {
            ArgumentNullException.ThrowIfNull(assemblies);

            if (assemblies.Any())
            {
                foreach (Assembly asm in assemblies)
                {
                    options.MappingConfiguration = new Action<MappingConfiguration>(mapping =>
                        {
                            mapping.FluentMappings.AddFromAssembly(asm);
                        }
                    );
                }
            }
        }

        /// <summary>
        /// Configures Fluent mappings for the specified types.
        /// </summary>
        /// <param name="options"><see cref="NHibernateConfigurationOptions"/> object to configure.</param>
        /// <param name="types"><see cref="Type"/> objects containing the mappings to load.</param>
        public virtual void ConfigureMappings(NHibernateConfigurationOptions options, IEnumerable<Type> types)
        {
            ArgumentNullException.ThrowIfNull(types);

            if (types.Any())
            {
                ConfigureMappings(options, types.Select(t => t.Assembly));
            }
        }
    }
}
