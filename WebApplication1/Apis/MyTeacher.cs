using Serilog;
using static WebApplication1.Apis.MySchool;
using WebApplication1.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Apis;

public class MyTeacher
{
    public class ItemTeacher
    {
        public string code { get; set; } = "";
        public string school { get; set; } = "";
    }
    public List<ItemTeacher> List()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                List<SqlTeacher> sql_teachers = context.teachers!.Include(s => s.school).AsNoTracking().ToList();
                List<ItemTeacher> responses = new List<ItemTeacher>();
                foreach (SqlTeacher i in sql_teachers)
                {
                    ItemTeacher item = new ItemTeacher();
                    item.code = i.code;
                    if (i.school != null)
                    {
                        item.school = i.school.code;
                    }
                    responses.Add(item);
                }
                return responses;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemTeacher>();
        }
    }

    public int Create(string teacher)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlTeacher? sql_teacher = context.teachers!.Where(s => s.code.CompareTo(teacher) == 0).AsNoTracking().FirstOrDefault();
                if (sql_teacher != null)
                {
                    return -1;
                }

                sql_teacher = new SqlTeacher();
                sql_teacher.ID = DateTime.Now.Ticks;
                sql_teacher.code = teacher;
                context.teachers!.Add(sql_teacher);
                context.SaveChanges();
                return 0;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return -9;
        }
    }

    public int Delete(string teacher)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlTeacher? sql_teacher = context.teachers!.Where(s => s.code.CompareTo(teacher) == 0).AsNoTracking().FirstOrDefault();
                if (sql_teacher == null)
                {
                    return -1;
                }
                sql_teacher.isdeleted = true;
                context.SaveChanges();
                return 0;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return -9;
        }
    }
}
