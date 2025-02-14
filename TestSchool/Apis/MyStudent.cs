using Microsoft.EntityFrameworkCore;
using Serilog;
using TestSchool.Model;
using static TestSchool.Apis.MySchool;
using static TestSchool.Controller.StateClassController;

namespace TestSchool.Apis;

public class MyStudent
{
    public class ItemClass
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public ItemStateClass states { get; set; } = new ItemStateClass();
    }
    public class ItemStudent
    {
        public string code { get; set; } = "";
        public string school { get; set; } = "";
        public ItemClass classs { get; set; } = new ItemClass();
    }
    public List<ItemStudent> List()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                //**** Bị circle khi include vòng lại chính context hoặc chính include đang theninclude
                //**** Circle phân biệt được List và Object
                //**** Mỗi include là riêng biệt

                //bị circle
                //List<SqlStudent> sql_students = context.students!.Include(s => s.school).ThenInclude(s=>s!.students).ThenInclude(s=>s.school).ToList();
                //không bị circle
                //List<SqlStudent> sql_students = context.students!.Include(s => s.school).ThenInclude(s=>s!.students).ToList();
                //Include dạng thế này vẫn đầu đủ
                //List<SqlStudent> sql_students = context.students!.Include(s => s.school).Include(s => s.classs!.state).ToList();
                List<SqlStudent> sql_students = context.students!.Include(s => s.school).Include(s => s.classs).ThenInclude(s => s!.state).ToList();
                List<ItemStudent> responses = new List<ItemStudent>();
                foreach (SqlStudent i in sql_students)
                {
                    ItemStudent item = new ItemStudent();
                    item.code = i.code;
                    if (i.school != null)
                    {
                        item.school = i.school.code;
                    }
                    if (i.classs != null)
                    {
                        item.classs.code = i.classs.code;
                        item.classs.name = i.classs.name;
                        if (i.classs.state != null)
                        {
                            item.classs.states.code = i.classs.state.code;
                            item.classs.states.name = i.classs.state.name;
                        }
                    }
                    responses.Add(item);
                }
                return responses;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemStudent>();
        }
    }

    public class ItemStudent2
    {
        public string code { get; set; } = "";
        public ItemSchool school { get; set; } = new ItemSchool();
        public ItemClass classs { get; set; } = new ItemClass();
    }

    public List<ItemStudent2> ListTestInclude()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                List<SqlStudent> sql_students = context.students!
                    .Include(s => s.school)//.ThenInclude(s => s!.students)
                    .Include(s => s.school).ThenInclude(s => s!.teachers)
                    .Include(s => s.classs).ThenInclude(s => s!.state)
                    //.AsNoTracking()
                    .ToList();
                List<ItemStudent2> responses = new List<ItemStudent2>();
                foreach (SqlStudent i in sql_students)
                {
                    ItemStudent2 itemStudent = new ItemStudent2();
                    itemStudent.code = i.code;
                    if (i.school != null)
                    {
                        itemStudent.school.code = i.school.code;
                        itemStudent.school.name = i.school.name;
                        itemStudent.school.des = i.school.des;
                        foreach (SqlStudent f_student in i.school.students)
                        {
                            ItemCommon itemCommon = new ItemCommon();
                            itemCommon.code = f_student.code;
                            itemStudent.school.students.Add(itemCommon);
                        }
                        foreach (SqlTeacher f_teacher in i.school.teachers)
                        {
                            ItemCommon itemCommon = new ItemCommon();
                            itemCommon.code = f_teacher.code;
                            itemStudent.school.teachers.Add(itemCommon);
                        }
                    }
                    if (i.classs != null)
                    {
                        itemStudent.classs.code = i.classs.code;
                        itemStudent.classs.name = i.classs.name;
                        if (i.classs.state != null)
                        {
                            itemStudent.classs.states.code = i.classs.state.code;
                            itemStudent.classs.states.name = i.classs.state.name;
                        }
                    }
                    responses.Add(itemStudent);
                }
                return responses;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemStudent2>();
        }
    }

    public List<ItemStudent> GetStudentInSchool(string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                //Efcore truy vấn và lọc được nếu record đó có chứa school là null
                List<SqlStudent>? sql_students = context.students!.Include(s => s.school).Where(s => s.school!.code.CompareTo(school) == 0).ToList();
                if (sql_students == null)
                {
                    return new List<ItemStudent>();
                }
                List<ItemStudent> respondes = new List<ItemStudent>();
                foreach (SqlStudent sql_student in sql_students)
                {
                    ItemStudent respond = new ItemStudent();
                    respond.code = sql_student.code;
                    respond.school = sql_student.school!.code; // vẫn loại bỏ được school là null
                    respondes.Add(respond);
                }
                return respondes;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemStudent>();
        }
    }

    public int Create(string student, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).AsNoTracking().FirstOrDefault();
                if (sql_student != null)
                {
                    return -1;
                }
                SqlSchool? sql_school = null;
                //khi AsNoTracking thì không Add được?
                if (!string.IsNullOrEmpty(school))
                {
                    sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).FirstOrDefault();
                    if (sql_school == null)
                    {
                        return -2;
                    }
                }
                sql_student = new SqlStudent();
                sql_student.ID = DateTime.Now.Ticks;
                sql_student.code = student;
                sql_student.school = sql_school;
                context.students!.Add(sql_student);
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

    public int Edit(string student, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).AsNoTracking().FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                sql_student.school = sql_school;
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

    public int Delete(string student)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                sql_student.isdeleted = true;
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

    public int DeleteStudentBySchool(string student, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Include(s => s.school)
                    .Where(s => s.code.CompareTo(student) == 0 && s.school!.code.CompareTo(school) == 0)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                context.students!.Remove(sql_student);
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
