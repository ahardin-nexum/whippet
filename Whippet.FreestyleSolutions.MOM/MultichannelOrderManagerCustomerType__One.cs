﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Athi.Whippet.Extensions.Primitives;
using Athi.Whippet.Data;
using Athi.Whippet.Data.Extensions;
using Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.Extensions;
using Athi.Whippet.Json;
using Athi.Whippet.Json.Newtonsoft.Extensions;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager
{
    /// <summary>
    /// Represents the first customer categorization type in M.O.M.
    /// </summary>
    public class MultichannelOrderManagerCustomerType__One : MultichannelOrderManagerEntity, IWhippetEntity, IWhippetEntityExternalDataRowImportMapper, IMultichannelOrderManagerCustomerType__One, IEqualityComparer<IMultichannelOrderManagerCustomerType__One>, IMultichannelOrderManagerEntity
    {
        private string _desc;

        /// <summary>
        /// Gets or sets the unique ID of the object.
        /// </summary>
        public new long ID
        {
            get
            {
                return MomObjectID;
            }
            set
            {
                MomObjectID = value;
            }
        }

        /// <summary>
        /// Gets the external table name or <see langword="null"/> if the data source is not stored in a database. This property is read-only.
        /// </summary>
        protected override string ExternalTableName
        {
            get
            {
                return MultichannelOrderManagerDatabaseConstants.Tables.CTYPE1;
            }
        }


        /// <summary>
        /// Gets or sets the parent <see cref="IMultichannelOrderManagerServer"/> object that the <see cref="IMultichannelOrderManagerCustomerType__One"/> is registered with.
        /// </summary>
        IMultichannelOrderManagerServer IMultichannelOrderManagerEntity.Server
        {
            get
            {
                return Server;
            }
            set
            {
                Server = value?.ToMultichannelOrderManagerServer();
            }
        }

        /// <summary>
        /// Single-character (or digit) code that represents the customer type.
        /// </summary>
        public virtual char CustomerType
        { get; set; }

        /// <summary>
        /// Description of the customer type.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException" />
        public virtual string Description
        {
            get
            {
                return _desc;
            }
            set
            {
                this.CheckLengthRequirement(value, ImportMap[nameof(Description)].Column);
                _desc = value;
            }
        }

        /// <summary>
        /// Specifies the M.O.M. type ID of the type.
        /// </summary>
        public virtual long TypeId
        { get; set; }

        /// <summary>
        /// Gets the external table name or <see langword="null"/> if the data source is not stored in a database. This property is read-only.
        /// </summary>
        string IWhippetEntityExternalDataRowImportMapper.ExternalTableName
        {
            get
            {
                return MultichannelOrderManagerDatabaseConstants.Tables.CTYPE1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultichannelOrderManagerCustomerType__One"/> class with no arguments.
        /// </summary>
        public MultichannelOrderManagerCustomerType__One()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultichannelOrderManagerCustomerType__One"/> with the specified ID.
        /// </summary>
        /// <param name="id">Unique ID of the entity.</param>
        public MultichannelOrderManagerCustomerType__One(Guid id)
            : base(id)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultichannelOrderManagerCustomerType__One"/> with the specified parameters.
        /// </summary>
        /// <param name="id">Unique ID of the entity.</param>
        /// <param name="customerType">Customer type code.</param>
        /// <param name="description">Description of the type.</param>
        /// <param name="typeId">Type ID assigned by the external database.</param>
        public MultichannelOrderManagerCustomerType__One(Guid id, char customerType, string description, long typeId)
            : this(id)
        {
            CustomerType = customerType;
            Description = description;
            TypeId = typeId;
        }

        /// <summary>
        /// Creates a <see cref="WhippetDataRowImportMap"/> object that contains a mapping for the current entity.
        /// </summary>
        /// <returns><see cref="WhippetDataRowImportMap"/> object.</returns>
        public override WhippetDataRowImportMap CreateImportMap()
        {
            WhippetDataRowImportMapEntry ctype = new WhippetDataRowImportMapEntry(nameof(CustomerType), MultichannelOrderManagerDatabaseConstants.Columns.CTYPE);
            WhippetDataRowImportMapEntry desc1 = new WhippetDataRowImportMapEntry(nameof(Description), MultichannelOrderManagerDatabaseConstants.Columns.DESC1);
            WhippetDataRowImportMapEntry ctypeId = new WhippetDataRowImportMapEntry(nameof(TypeId), MultichannelOrderManagerDatabaseConstants.Columns.CTYPE_ID);

            return new WhippetDataRowImportMap(new[] { ctype, desc1, ctypeId });
        }

        /// <summary>
        /// Imports the specified <see cref="DataRow"/> containing the information needed to populate the <see cref="IWhippetEntity"/>. This method must be overridden.
        /// </summary>
        /// <param name="dataRow"><see cref="DataRow"/> containing the data to import.</param>
        /// <param name="importMap">External <see cref="WhippetDataRowImportMap"/>. If <see langword="null"/>, then the one generated by <see cref="CreateImportMap"/> will be used.</param>
        /// <exception cref="ArgumentNullException" />
        public override void ImportDataRow(DataRow dataRow, WhippetDataRowImportMap importMap = null)
        {
            if (dataRow == null)
            {
                throw new ArgumentNullException(nameof(dataRow));
            }
            else
            {
                WhippetDataRowImportMap map = (importMap == null ? CreateImportMap() : importMap);

                CustomerType = dataRow.Field<char>(map[nameof(CustomerType)].Column);
                Description = dataRow.Field<string>(map[nameof(Description)].Column);
                TypeId = dataRow.Field<long>(map[nameof(TypeId)].Column);
            }
        }

        /// <summary>
        /// Creates a <see cref="DataTable"/> that represents the database table of the current entity.
        /// </summary>
        /// <returns><see cref="DataTable"/> containing the columns and respective definitions of the associated external database table for the current entity.</returns>
        public override DataTable CreateDataTable()
        {
            WhippetDataRowImportMap map = CreateImportMap();
            DataTable table = new DataTable();

            DataColumn ctype = DataColumnFactory.CreateDataColumn(map[nameof(CustomerType)].Column, typeof(char), false, 1);
            DataColumn desc = DataColumnFactory.CreateDataColumn(map[nameof(Description)].Column, typeof(string), false, 30);
            DataColumn typeId = DataColumnFactory.CreateDataColumn(map[nameof(TypeId)].Column, typeof(decimal), false);

            table.Columns.AddRange(new[] { ctype, desc, typeId });

            return table;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IMultichannelOrderManagerCustomerType__One);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
        public virtual bool Equals(IMultichannelOrderManagerCustomerType__One obj)
        {
            return Equals(this, obj);
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="a">The first object of type <see cref="IMultichannelOrderManagerCustomerType__One"/> to compare.</param>
        /// <param name="b">The second object of type <see cref="IMultichannelOrderManagerCustomerType__One"/> to compare.</param>
        /// <returns><see langword="true"/> if the specified objects are equal; otherwise, <see langword="false"/>.</returns>
        public virtual bool Equals(IMultichannelOrderManagerCustomerType__One a, IMultichannelOrderManagerCustomerType__One b)
        {
            bool equals = (a == null && b == null);

            if (!equals && (a != null) && (b != null))
            {
                equals =
                    a.Server.Equals(b.Server)
                        && a.CustomerType.Equals(b.CustomerType)
                        && String.Equals(a.Description, b.Description, StringComparison.InvariantCultureIgnoreCase)
                        && a.TypeId.Equals(b.TypeId);
            }

            return equals;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="ArgumentNullException" />
        public virtual int GetHashCode(IMultichannelOrderManagerCustomerType__One obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            else
            {
                return obj.GetHashCode();
            }
        }

        /// <summary>
        /// Gets the string representation of the current object.
        /// </summary>
        /// <returns>String representation of the current object.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(CustomerType);

            if (!String.IsNullOrWhiteSpace(Description))
            {
                builder.Append(" ");
                builder.Append("(");
                builder.Append(Description);
                builder.Append(")");
            }

            return builder.ToString();
        }

    }
}
