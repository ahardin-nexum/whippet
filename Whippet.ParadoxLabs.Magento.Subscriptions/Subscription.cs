﻿using System;
using NodaTime;
using Athi.Whippet.Extensions;
using Athi.Whippet.Adobe.Magento;
using Athi.Whippet.Adobe.Magento.Data;
using Athi.Whippet.ParadoxLabs.Magento.Data;
using Athi.Whippet.Adobe.Magento.Customer;
using Athi.Whippet.Adobe.Magento.Store;
using Athi.Whippet.Adobe.Magento.Sales;
using Athi.Whippet.Adobe.Magento.Sales.Extensions;
using Athi.Whippet.Adobe.Magento.Store.Extensions;
using Athi.Whippet.Adobe.Magento.Customer.Extensions;

namespace Athi.Whippet.ParadoxLabs.Magento.Subscriptions
{
    /// <summary>
    /// Represents a subscription of a product or service in Magento.
    /// </summary>
    public class Subscription : ParadoxLabsMagentoRestEntity<ParadoxLabsSubscriptionInterface>, ISubscription, IEqualityComparer<ISubscription>,  IParadoxLabsMagentoRestEntity, IParadoxLabsMagentoRestEntity<ParadoxLabsSubscriptionInterface>, IMagentoAuditableEntity
    {
        private SalesOrder _order;
        private SalesOrderItem _item;
        private Customer _customer;
        private Store _store;
        
        /// <summary>
        /// Gets or sets the parent <see cref="SalesOrder"/> of the subscription.
        /// </summary>
        public virtual SalesOrder Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new SalesOrder();
                }

                return _order;
            }
            set
            {
                _order = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent <see cref="ISalesOrder"/> of the subscription.
        /// </summary>
        ISalesOrder ISubscription.Order
        {
            get
            {
                return Order;
            }
            set
            {
                Order = value.ToSalesOrder();
            }
        }
        
        /// <summary>
        /// Gets or sets the <see cref="SalesOrderItem"/> that is being subscribed to.
        /// </summary>
        public virtual SalesOrderItem Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new SalesOrderItem();
                    _item.Store = Store;
                    _item.OrderID = Order.ID;
                }

                return _item;
            }
            set
            {
                _item = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ISalesOrderItem"/> that is being subscribed to.
        /// </summary>
        ISalesOrderItem ISubscription.Item
        {
            get
            {
                return Item;
            }
            set
            {
                Item = value.ToSalesOrderItem();
            }
        }
        
        /// <summary>
        /// Gets or sets the <see cref="Customer"/> that is subscribing to <see cref="Item"/>. 
        /// </summary>
        public virtual Customer Subscriber
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new Customer();
                }

                return _customer;
            }
            set
            {
                _customer = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ICustomer"/> that is subscribing to <see cref="Item"/>. 
        /// </summary>
        ICustomer ISubscription.Subscriber
        {
            get
            {
                return Subscriber;
            }
            set
            {
                Subscriber = value.ToCustomer();
            }
        }
        
        /// <summary>
        /// Gets or sets the date and time the entity was created.
        /// </summary>
        public virtual Instant CreatedTimestamp
        { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was last updated (if any).
        /// </summary>
        public virtual Instant? UpdatedTimestamp
        { get; set; }
        
        /// <summary>
        /// Gets or sets the parent store that houses the subscription.
        /// </summary>
        public virtual Store Store
        {
            get
            {
                if (_store == null)
                {
                    _store = new Store();
                }

                return _store;
            }
            set
            {
                _store = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent store that houses the subscription.
        /// </summary>
        IStore ISubscription.Store
        {
            get
            {
                return Store;
            }
            set
            {
                Store = value.ToStore();
            }
        }
        
        /// <summary>
        /// Gets or sets the date and time of the next subscription charge interval.
        /// </summary>
        public virtual Instant? NextRun
        { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time of the last subscription charge interval.
        /// </summary>
        public virtual Instant? LastRun
        { get; set; }
        
        /// <summary>
        /// Gets or sets the subscription item subtotal.
        /// </summary>
        public virtual decimal Subtotal
        { get; set; }
        
        /// <summary>
        /// Specifies whether the subscription has completed its required number of runs and is now expired.
        /// </summary>
        public virtual bool IsComplete
        { get; set; }
        
        /// <summary>
        /// Gets or sets the total number of runs the subscription should execute. 
        /// </summary>
        public virtual int Length
        { get; set; }
        
        /// <summary>
        /// Gets or sets the total number of times the subscription has ran.
        /// </summary>
        public virtual int RunCount
        { get; set; }

        /// <summary>
        /// Gets or sets the subscription status.
        /// </summary>
        public virtual string Status
        { get; set; }
        
        /// <summary>
        /// Gets or sets the frequency of the subscription charges during the specified <see cref="FrequencyInterval"/>.
        /// </summary>
        public virtual int Frequency
        { get; set; }

        /// <summary>
        /// Gets or sets the frequency interval.
        /// </summary>
        public virtual string FrequencyInterval
        { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class with no arguments.
        /// </summary>
        public Subscription()
            : base()
        { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class with the specified ID.
        /// </summary>
        /// <param name="entityId">ID to assign the <see cref="MagentoEntity"/> object.</param>
        /// <param name="server"><see cref="MagentoServer"/> the entity resides on.</param>
        /// <param name="restEndpoint"><see cref="MagentoRestEndpoint"/> the entity resides on.</param>
        public Subscription(uint entityId, MagentoServer server = null, MagentoRestEndpoint restEndpoint = null)
            : base(entityId, server, restEndpoint)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Subscription"/> class with the specified <see cref="IExtensionInterface"/> object.
        /// </summary>
        /// <param name="model"><see cref="IExtensionInterface"/> object to initialize a new instance of the class with.</param>
        /// <param name="server"><see cref="MagentoServer"/> the entity resides on.</param>
        /// <param name="restEndpoint"><see cref="MagentoRestEndpoint"/> the entity resides on.</param>
        public Subscription(ParadoxLabsSubscriptionInterface model, MagentoServer server = null, MagentoRestEndpoint restEndpoint = null)
            : base(model, server, restEndpoint)
        { }

        /// <summary>
        /// Compares the current instance to the specified object for equality.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return ((obj == null) || !(obj is ISubscription)) ? false : Equals((ISubscription)(obj));
        }

        /// <summary>
        /// Compares the current instance to the specified object for equality.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
        public virtual bool Equals(ISubscription obj)
        {
            return (obj == null) ? false : Equals(this, obj);
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="x">First object to compare.</param>
        /// <param name="y">Second object to compare.</param>
        /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
        public virtual bool Equals(ISubscription x, ISubscription y)
        {
            bool equals = (x == null) && (y == null);

            if (!equals && (x != null) && (y != null))
            {
                equals = (((x.Order == null) && (y.Order == null)) || ((x.Order != null) && x.Order.Equals(y.Order)))
                         && (((x.Item == null) && (y.Item == null)) || ((x.Item != null) && x.Item.Equals(y.Item)))
                         && (((x.Subscriber == null) && (y.Subscriber == null)) || ((x.Subscriber != null) && x.Subscriber.Equals(y.Subscriber)))
                         && (((x.Store == null) && (y.Store == null)) || ((x.Store != null) && x.Store.Equals(y.Store)))
                         && x.NextRun.GetValueOrDefault().Equals(y.NextRun.GetValueOrDefault())
                         && x.LastRun.GetValueOrDefault().Equals(y.LastRun.GetValueOrDefault())
                         && x.Subtotal == y.Subtotal
                         && x.IsComplete == y.IsComplete
                         && x.RunCount == y.RunCount
                         && String.Equals(x.Status?.Trim(), y.Status?.Trim(), StringComparison.InvariantCultureIgnoreCase)
                         && x.Frequency == y.Frequency
                         && String.Equals(x.FrequencyInterval?.Trim(), y.FrequencyInterval?.Trim(), StringComparison.InvariantCultureIgnoreCase)
                         && (((x.Server == null) && (y.Server == null)) || ((x.Server != null) && x.Server.Equals(y.Server)))
                         && (((x.RestEndpoint == null) && (y.RestEndpoint == null)) || ((x.RestEndpoint != null) && x.RestEndpoint.Equals(y.RestEndpoint)))
                         && x.Length == y.Length;
            }

            return equals;
        }

        /// <summary>
        /// Converts the current instance to an <see cref="IExtensionInterface"/> of type <see cref="ParadoxLabsSubscriptionInterface"/>.
        /// </summary>
        /// <returns><see cref="IExtensionInterface"/> object of type <see cref="ParadoxLabsSubscriptionInterface"/>.</returns>
        public override ParadoxLabsSubscriptionInterface ToInterface()
        {
            ParadoxLabsSubscriptionInterface subInterface = new ParadoxLabsSubscriptionInterface();

            subInterface.ID = ID;
            subInterface.QuoteID = Order.QuoteID;
            subInterface.Description = Item.Description;
            subInterface.CustomerID = Subscriber.ID;
            subInterface.CreatedAt = CreatedTimestamp.ToDateTimeUtc().ToString();
            subInterface.UpdatedAt = UpdatedTimestamp.HasValue ? UpdatedTimestamp.Value.ToDateTimeUtc().ToString() : String.Empty;
            subInterface.StoreID = Store.ID;
            subInterface.NextRun = NextRun.HasValue ? NextRun.Value.ToDateTimeUtc().ToString() : String.Empty;
            subInterface.LastRun = LastRun.HasValue ? LastRun.Value.ToDateTimeUtc().ToString() : String.Empty;
            subInterface.Subtotal = Subtotal;
            subInterface.Complete = IsComplete;
            subInterface.Length = Length;
            subInterface.RunCount = RunCount;
            subInterface.Status = Status;
            subInterface.FrequencyCount = Frequency;
            subInterface.FrequencyUnit = FrequencyInterval;
            
            return subInterface;
        }

        /// <summary>
        /// Creates a duplicate instance of the current object.
        /// </summary>
        /// <returns>Duplicate instance of the current object.</returns>
        public override object Clone()
        {
            Subscription subscription = new Subscription();

            subscription.ID = ID;
            subscription.Order = Order.Clone<SalesOrder>();
            subscription.Item = Item.Clone<SalesOrderItem>();
            subscription.CreatedTimestamp = CreatedTimestamp;
            subscription.UpdatedTimestamp = UpdatedTimestamp;
            subscription.Store = Store.Clone<Store>();
            subscription.NextRun = NextRun;
            subscription.LastRun = LastRun;
            subscription.Subtotal = Subtotal;
            subscription.IsComplete = IsComplete;
            subscription.Length = Length;
            subscription.RunCount = RunCount;
            subscription.Status = Status;
            subscription.Frequency = Frequency;
            subscription.FrequencyInterval = FrequencyInterval;
            
            return subscription;
        }

        /// <summary>
        /// Gets the hash code of the current instance.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();

            hash.Add(ID);
            hash.Add(Order);
            hash.Add(Item);
            hash.Add(CreatedTimestamp);
            hash.Add(UpdatedTimestamp);
            hash.Add(Store);
            hash.Add(NextRun);
            hash.Add(LastRun);
            hash.Add(Subtotal);
            hash.Add(IsComplete);
            hash.Add(Length);
            hash.Add(RunCount);
            hash.Add(Status);
            hash.Add(Frequency);
            hash.Add(FrequencyInterval);
            
            return hash.ToHashCode();
        }

        /// <summary>
        /// Constructs the current instance with the specified <see cref="IExtensionInterface"/> object.
        /// </summary>
        /// <param name="model"><see cref="IExtensionInterface"/> object to construct the current instance from.</param>
        protected override void ImportFromModel(ParadoxLabsSubscriptionInterface model)
        {
            if (model != null)
            {
                ID = model.ID;
                Order = new SalesOrder(default(uint)) { QuoteID = model.QuoteID };
                Item = new SalesOrderItem(Convert.ToUInt32(model.ID));

                if (!String.IsNullOrWhiteSpace(model.CreatedAt))
                {
                    CreatedTimestamp = Instant.FromDateTimeUtc(DateTime.Parse(model.CreatedAt).ToUniversalTime(true));
                }

                if (!String.IsNullOrWhiteSpace(model.CreatedAt))
                {
                    CreatedTimestamp = Instant.FromDateTimeUtc(DateTime.Parse(model.CreatedAt).ToUniversalTime(true));
                }

                UpdatedTimestamp = !String.IsNullOrWhiteSpace(model.UpdatedAt) ? Instant.FromDateTimeUtc(DateTime.Parse(model.UpdatedAt).ToUniversalTime(true)) : null;
                Store = new Store(Convert.ToUInt32(model.StoreID));
                NextRun = !String.IsNullOrWhiteSpace(model.NextRun) ? Instant.FromDateTimeUtc(DateTime.Parse(model.NextRun).ToUniversalTime(true)) : null;
                LastRun = !String.IsNullOrWhiteSpace(model.LastRun) ? Instant.FromDateTimeUtc(DateTime.Parse(model.LastRun).ToUniversalTime(true)) : null;
                Subtotal = Convert.ToDecimal(model.Subtotal);
                IsComplete = Convert.ToBoolean(model.Complete);
                Length = Convert.ToInt32(model.Length);
                RunCount = Convert.ToInt32(model.RunCount);
                Status = model.Status;
                Frequency = Convert.ToInt32(model.FrequencyCount);
                FrequencyInterval = model.FrequencyUnit;
            }
        }

        /// <summary>
        /// Gets the hash code of the specified object.
        /// </summary>
        /// <param name="subscription"><see cref="ISubscription"/> object to get hash code for.</param>
        /// <returns>Hash code.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual int GetHashCode(ISubscription subscription)
        {
            ArgumentNullException.ThrowIfNull(subscription);
            return subscription.GetHashCode();
        }
    }
}
