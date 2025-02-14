using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Xml.Linq;
using TestSchool.Model;

namespace TestSchool.Apis;

public class MyClass
{
    public class ItemClass
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
    }

    public List<ItemClass> ListClasses()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                List<SqlClass> sql_classes = context.classes!.ToList();
                List<ItemClass> respondes = new List<ItemClass>();
                foreach (SqlClass f_class in sql_classes)
                {
                    ItemClass item = new ItemClass();
                    item.code = f_class.code;
                    item.name = f_class.name;
                    respondes.Add(item);
                }
                return respondes;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemClass>();
        }
    }

    public string Create(string code, string name)
    {
        try
        {
            if (string.IsNullOrEmpty(code))
            {
                return "Incomplete entry";
            }
            using (DataContext context = new DataContext())
            {
                SqlClass? sql_class = context.classes!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (sql_class != null)
                {
                    return "Class is exist";
                }
                sql_class = new SqlClass();
                sql_class.ID = DateTime.Now.Ticks;
                sql_class.code = code;
                sql_class.name = name;
                context.classes!.Add(sql_class);
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
            if (string.IsNullOrEmpty(code))
            {
                return "Incomplete entry";
            }
            using (DataContext context = new DataContext())
            {
                SqlClass? sql_class = context.classes!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (sql_class == null)
                {
                    return "Class is not exist";
                }
                if (!string.IsNullOrEmpty(name))
                {
                    sql_class.name = name;
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
            if (string.IsNullOrEmpty(code))
            {
                return "Incomplete entry";
            }
            using (DataContext context = new DataContext())
            {
                SqlClass? sql_class = context.classes!.Where(s => s.code.CompareTo(code) == 0).FirstOrDefault();
                if (sql_class == null)
                {
                    return "Class is not exist";
                }
                sql_class.isdeleted = true;
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

    public string AddStudentToClass(string student, string classs)
    {
        try
        {
            if (string.IsNullOrEmpty(student) || string.IsNullOrEmpty(classs))
            {
                return "Incomplete entry";
            }
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).FirstOrDefault();
                if (sql_student == null)
                {
                    return "Student is not exist";
                }
                SqlClass? sql_class = context.classes!.Include(s => s.students).Where(s => s.code.CompareTo(classs) == 0).FirstOrDefault();
                if (sql_class == null)
                {
                    return "Class is not exist";
                }
                if (sql_class.students.Contains(sql_student))
                {
                    return "";
                }
                sql_class.students.Add(sql_student);
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

    public string RemoveStudentFromClass(string student, string classs)
    {
        try
        {
            if (string.IsNullOrEmpty(student) || string.IsNullOrEmpty(classs))
            {
                return "Incomplete entry";
            }
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).FirstOrDefault();
                if (sql_student == null)
                {
                    return "Student is not exist";
                }
                SqlClass? sql_class = context.classes!.Include(s => s.students).Where(s => s.code.CompareTo(classs) == 0).FirstOrDefault();
                if (sql_class == null)
                {
                    return "Class is not exist";
                }
                if (!sql_class.students.Contains(sql_student))
                {
                    return "";
                }
                sql_class.students.Remove(sql_student);
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

    public string SetState(string classs, string state)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlClass? sql_class = context.classes!.Where(s => s.code.CompareTo(classs) == 0).FirstOrDefault();
                if (sql_class == null)
                {
                    return "Class is not exist";
                }
                SqlStateClass? sql_stateClass = context.stateClasses!.Where(s => s.code.CompareTo(state) == 0).FirstOrDefault();
                if (sql_stateClass == null)
                {
                    return "StateClass is not exist";
                }
                sql_class.state = sql_stateClass;
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
