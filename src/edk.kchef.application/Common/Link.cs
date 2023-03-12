namespace edk.Kchef.Application.Common
{
    public class Link
    {
        public Link(string rel, string href) {
            Rel = rel;
            Href = href;
        }

        public string Rel { get; }
        public string Href { get; }
    }
}
