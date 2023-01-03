using System.Collections.Generic;

namespace edk.Kchef.Domain.Ordes
{
    public class Menu
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<ItemMenu> Itens { get; set; }
    }
}
