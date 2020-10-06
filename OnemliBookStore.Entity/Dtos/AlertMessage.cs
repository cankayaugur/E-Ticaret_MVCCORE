using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class AlertMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string AlertType { get; set; }
    }
}
