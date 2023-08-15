using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.CQRS
{
    public class QueryResponse : Response
    {
        public string Name { get; set; } = string.Empty;
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public List<object> Items { get; set; }

        public QueryResponse()
        {
            Items = new List<object>();
        }

        public void AddItem(object item)
        {
            Items.Add(item);
        }

        public void AddItems(List<object> items)
        {
            Items.AddRange(items);
        }

        public void SetItems(List<object> items)
        {
            Items = items;
        }

        public List<T> GetItems<T>()
        {
            var items = new List<T>();
            foreach (var item in Items)
            {
                items.Add((T)item);
            }
            return items;
        }
    }
}