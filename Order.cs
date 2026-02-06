using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_работа
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Furniture Furniture { get; set; }
    }
}
