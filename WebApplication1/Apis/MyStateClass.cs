using Serilog;
using WebApplication1.Model;

namespace WebApplication1.Apis;

public class MyStateClass
{
    public class Item2Field
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
    }

    public List<Item2Field> List()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                List<SqlStateClass> sql_stateClasses = context.stateClasses!.ToList();
                List<Item2Field> respondes = new List<Item2Field>();
                foreach (SqlStateClass f_state in sql_stateClasses)
                {
                    Item2Field item = new Item2Field();
                    item.code = f_state.code;
                    item.name = f_state.name;
                    respondes.Add(item);
                }
                return respondes;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<Item2Field>();
        }
    }

    public string Create(string code, string name)
    {
        if (string.IsNullOrEmpty(code))
        {
            return "Incomplete entry";
        }
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStateClass? sql_stateClass = context.stateClasses!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (sql_stateClass != null)
                {
                    return "StateClass is exist";
                }
                sql_stateClass = new SqlStateClass();
                sql_stateClass.ID = DateTime.Now.Ticks;
                sql_stateClass.code = code;
                sql_stateClass.name = name;
                context.stateClasses!.Add(sql_stateClass);
                context.SaveChanges();
                return "";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return "Error database";
        }
    }

    public string Edit(string code, string name)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStateClass? sql_stateClass = context.stateClasses!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (sql_stateClass == null)
                {
                    return "StateClass is not exist";
                }
                if (!string.IsNullOrEmpty(name))
                {
                    sql_stateClass.name = name;
                }
                context.SaveChanges();
                return "";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return "Error database";
        }
    }

    public string Delete(string code)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStateClass? sql_stateClass = context.stateClasses!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (sql_stateClass == null)
                {
                    return "StateClass is not exist";
                }
                sql_stateClass.isdeleted = true;
                context.SaveChanges();
                return "";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return "Error database";
        }
    }
}
