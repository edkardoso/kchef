using edk.Fusc.Contracts;
using edk.Kchef.Application.Features.Users.Create;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Ordes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace edk.Kchef.Application.Common
{
    public class ResultApi
    {
        public ResultApi(IEnumerable<string> notifications)
            :this(null, notifications){}

        public ResultApi(object value, IEnumerable<string> notifications)
        {
            Id = Guid.NewGuid();
            Result = value;
            Notifications = notifications;
        }

        public ResultApi(object value, IReadOnlyCollection<INotification> notifications)
            :this(value, notifications.ToStringList()){}

        public ResultApi(object value, List<Exception> exceptions)
            :this(value, exceptions.ToStringList()) {}

       

        [JsonProperty(Order = 1)]
        public Guid Id { get;  }

        [JsonProperty(Order = 2)]
        public object Result { get;  }
        [JsonProperty(Order = 3)]
        public IEnumerable<string> Notifications { get;  }
        [JsonProperty(Order = 4)]
        public List<Link> Links { get; private set; } = new List<Link>();

        public void AddLink(string linkName, string linkUrl) 
            => Links.Add(new Link(linkName, linkUrl));


    }
}
