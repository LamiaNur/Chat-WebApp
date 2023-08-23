using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Chat.Framework.Database.Models
{
    public class FieldOrder
    {
        public string FieldKey { get; set; } = string.Empty;
        public SortDirection SortDirection { get; set; }
    }
}
