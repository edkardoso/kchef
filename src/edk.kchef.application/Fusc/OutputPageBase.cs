using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Application.Fusc
{
    public abstract class OutputPageBase : IOutput
    {
        protected OutputPageBase(List<Notification> messages, int totalItems, int totalPage, int currentPage = 1)
        {
            Messages = messages ?? new List<Notification>();
            TotalItems = totalItems;
            TotalPage = totalPage;
            CurrentPage = currentPage;
        }

        public int CurrentPage { get; protected set; }
        public int TotalPage { get; protected set; }
        public int TotalItems { get; protected set; }
        public List<Notification> Messages { get; set; }


    }
}
