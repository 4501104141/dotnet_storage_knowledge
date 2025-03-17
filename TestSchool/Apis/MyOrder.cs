using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using TestSchool.Model;

namespace TestSchool.Apis;

public class MyOrder
{
    public string create(string code)
    {
        using (DataContext context = new DataContext())
        {
            SqlOrder? sql_order = context.orders!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
            if (sql_order != null)
            {
                return "Order is exist";
            }
            sql_order = new SqlOrder();
            sql_order.ID = DateTime.Now.Ticks;
            sql_order.code = code;
            context.orders!.Add(sql_order);
            context.SaveChanges();
            return string.Empty;
        }
    }

    public string delete(string code)
    {
        using (DataContext context = new DataContext())
        {
            SqlOrder? sql_order = context.orders!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
            if (sql_order == null)
            {
                return "Order is not exist";
            }
            context.orders!.Remove(sql_order);
            context.SaveChanges();
            return string.Empty;
        }
    }

    public string addOrderToOrder(string order, string orderChild)
    {
        using (DataContext context = new DataContext())
        {
            SqlOrder? sql_order = context.orders!.Where(s => s.code.CompareTo(order) == 0).Include(s => s.orders).FirstOrDefault();
            if (sql_order == null)
            {
                return "Order is not exist";
            }
            SqlOrder? sql_order_child = context.orders!.Where(s => s.code.CompareTo(orderChild) == 0).FirstOrDefault();
            if (sql_order_child == null)
            {
                return "Order is not exist";
            }
            foreach (SqlOrder f_order in sql_order.orders)
            {
                if (f_order == sql_order_child)
                {
                    return string.Empty;
                }
            }
            sql_order.orders.Add(sql_order_child);
            context.SaveChanges();
            return string.Empty;
        }
    }

    public string removeOrderFromOrder(string order, string orderChild)
    {
        using (DataContext context = new DataContext())
        {
            SqlOrder? sql_order = context.orders!.Where(s => s.code.CompareTo(order) == 0).Include(s => s.orders).FirstOrDefault();
            if (sql_order == null)
            {
                return "Order is not exist";
            }
            SqlOrder? sql_order_child = context.orders!.Where(s => s.code.CompareTo(orderChild) == 0).FirstOrDefault();
            if (sql_order_child == null)
            {
                return "Order is not exist";
            }
            foreach (SqlOrder f_order in sql_order.orders)
            {
                if (f_order == sql_order_child)
                {
                    sql_order.orders.Remove(f_order);
                    context.SaveChanges();
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }

    public class Item1Field
    {
        public string code { get; set; } = "";
    }

    public class ItemOrder
    {
        public string code { get; set; } = "";
        public Item1Field order { get; set; } = new Item1Field();
        public List<Item1Field> orders { get; set; } = new List<Item1Field>();
    }

    public List<ItemOrder> list()
    {
        using (DataContext context = new DataContext())
        {
            List<SqlOrder> sql_orders = context.orders!.Include(s => s.orders).Include(s => s.order).ToList();
            List<ItemOrder> respondes = new List<ItemOrder>();
            foreach (SqlOrder f_order in sql_orders)
            {
                ItemOrder item = new ItemOrder();
                item.code = f_order.code;
                if(f_order.order != null)
                {
                    Item1Field order = new Item1Field();
                    order.code = f_order.order.code;
                    item.order = order;
                }
                foreach (SqlOrder f_childOrder in f_order.orders)
                {
                    Item1Field itemChild = new Item1Field();
                    itemChild.code = f_childOrder.code;
                    item.orders.Add(itemChild);
                }
                respondes.Add(item);
            }
            return respondes;
        }
    }
}
