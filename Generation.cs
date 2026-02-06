using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа
{
    class Generation
    {
        public TextBox tb;
        private bool _isActive = false;
        public SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Курсовая работа\\Курсовая работа\\DB.mdf;Integrated Security=True");
        public Generation(TextBox t)
        {

            tb = t;
            conn.Open();
        }
        public void Start()
        {
            _isActive = true;
            Task.Run(() => Generate());
            Task.Run(() => GenerateClients());
            Task.Run(() => GenerateFurniture());
        }
        public void Stop()
        {
            _isActive = false;
        }
        private void Generate()
        {
            while (_isActive)
            {
                Client c = PickClient();
                Order order = GenerateOrder(c);
                Checkout(order);
                Thread.Sleep(1000);
            }
        }
        public List<Client> clients = new List<Client>();
        public List<Order> orders = new List<Order>();
        public List<Furniture> furnitures = new List<Furniture>();
        public Buy buy = new Buy();

        public List<string> clientNames = new List<string> { "Покупатель"};
        public List<string> furnitureTypes = new List<string> { "эконом", "средний", "премиум"};
        public List<string> furnitureStyles = new List<string> { "барокко", "готика", "модерн" };


        public void GenerateClients()
        {
            while (_isActive)
            {
                Client c = new Client();
                Random random = new Random();
                c.Name = clientNames[random.Next(0, clientNames.Count)];
                clients.Add(c);
                Thread.Sleep(1000);
            }
        }
        public void GenerateFurniture()
        {
            while (_isActive)
            {
                Furniture f = new Furniture();
                Random random = new Random();
                f.Name = furnitureStyles[random.Next(0, furnitureTypes.Count)];
                f.Object = furnitureTypes[random.Next(0, furnitureStyles.Count)];
                if (f.Name == "барокко") f.Price = random.Next(1, 9999) + 10000;
                if (f.Name == "готика") f.Price = random.Next(1, 9999) + 10000;
                if (f.Name == "модерн") f.Price = random.Next(1, 9999) + 10000;
                furnitures.Add(f);
                Thread.Sleep(1000);
            }
        }
        public Client PickClient()
        {
            if (clients.Count > 0)
            {
                Random random = new Random();
                return clients[random.Next(0, clients.Count)];
            }
            return null;
        }

        public Order GenerateOrder(Client c)
        {
            if (c == null) return null;
            if (furnitures.Count <= 0) return null;
            Random random = new Random();
            Furniture f = furnitures[random.Next(0, furnitures.Count)];
            Order order = new Order() { Client = c, Furniture = f, Date = DateTime.Now };
            return order;
        }

        public void Checkout(Order o)
        {
            if (o == null) return;
            orders.Add(o);
            buy.Account += o.Furniture.Price;
            tb.Invoke(new Action(() => tb.Text = $"{o.Client.Name} купил {o.Furniture.Name} за {o.Furniture.Price} руб. Стиль: {o.Furniture.Object}.  Дата заказа: {o.Date.ToString("yyyy.MM.dd")}"));
            Insert(o);
            Thread.Sleep(500);

        }
        public void Insert(Order o)
        {
            string query = "INSERT INTO [Order] (clientName, furnitureType, furnitureStyle, date, price) VALUES (@clientName, @furnitureType, @furnitureStyle, @date, @price)";
            SqlCommand com = new SqlCommand(query, conn);
            com.Parameters.Add("@clientName", System.Data.SqlDbType.NVarChar);
            com.Parameters.Add("@furnitureType", System.Data.SqlDbType.NVarChar);
            com.Parameters.Add("@furnitureStyle", System.Data.SqlDbType.NVarChar);
            com.Parameters.Add("@date", System.Data.SqlDbType.Date);
            com.Parameters.Add("@price", System.Data.SqlDbType.Decimal);
            com.Parameters["@clientName"].Value = o.Client.Name;
            com.Parameters["@furnitureType"].Value = o.Furniture.Name;
            com.Parameters["@furnitureStyle"].Value = o.Furniture.Object;
            com.Parameters["@date"].Value = o.Date;
            com.Parameters["@price"].Value = o.Furniture.Price;
            com.ExecuteNonQuery();
        }
    }
}
