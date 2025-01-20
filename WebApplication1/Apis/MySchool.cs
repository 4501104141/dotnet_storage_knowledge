using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using WebApplication1.Model;
using static WebApplication1.Apis.MyStudent;

namespace WebApplication1.Apis;

public class MySchool
{
    public class ItemCommon
    {
        public string code { get; set; } = "";
    }
    public class ItemSchool
    {
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        public string des { get; set; } = "";
        public List<ItemCommon> students { get; set; } = new List<ItemCommon>();
        public List<ItemCommon> teachers { get; set; } = new List<ItemCommon>();
    }
    public List<ItemSchool> List()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                //bị circle
                //List<SqlSchool> sql_schools = context.schools!.Include(s => s.students).ThenInclude(s => s.school).Include(s => s.teachers).AsNoTracking().ToList();
                //Log: The Include path 'classs->students' results in a cycle. Cycles are not allowed in no-tracking queries; either use a tracking query or remove the cycle.
                //List<SqlSchool> sql_schools = context.schools!.Include(s => s.students).ThenInclude(s=>s.classs!.students).Include(s => s.teachers).AsNoTracking().ToList();
                List<SqlSchool> sql_schools = context.schools!.Include(s => s.students).Include(s => s.teachers).AsNoTracking().ToList();
                List<SqlSchool> copies = new List<SqlSchool>();
                //Vì class là tham chiếu và efcore theo dõi object gốc nên phải ánh xạ qua object khác để object gốc không bị thay đổi. 
                foreach (SqlSchool i in sql_schools)
                {
                    SqlSchool item = new SqlSchool();
                    item.code = i.code;
                    item.name = i.name;
                    item.des = i.des;
                    foreach (SqlStudent j in i.students)
                    {
                        SqlStudent jtem = new SqlStudent();
                        jtem.code = j.code;
                        jtem.school = jtem.school;
                        item.students.Add(jtem);
                    }
                    foreach (SqlTeacher j in i.teachers)
                    {
                        SqlTeacher jtem = new SqlTeacher();
                        jtem.code = j.code;
                        jtem.school = j.school;
                        item.teachers.Add(jtem);
                    }
                    copies.Add(item);
                }

                List<ItemSchool> responses = new List<ItemSchool>();
                foreach (SqlSchool i in copies)
                {
                    ItemSchool item = new ItemSchool();
                    item.code = i.code;
                    item.name = i.name;
                    item.des = i.des;
                    foreach (SqlStudent j in i.students)
                    {
                        ItemCommon jtem = new ItemCommon();
                        jtem.code = j.code;
                        item.students.Add(jtem);
                    }
                    foreach (SqlTeacher j in i.teachers)
                    {
                        ItemCommon jtem = new ItemCommon();
                        jtem.code = j.code;
                        item.teachers.Add(jtem);
                    }
                    responses.Add(item);
                }
                return responses;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemSchool>();
        }
    }

    //* Bài Where trong include
    public List<ItemSchool> ListIncludeWhere()
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                // Đây là where trong Include và hoạt động bình thường
                // Truy vấn sẽ phức tạp hơn, giảm hiệu suất truy vấn
                List<SqlSchool> sql_schools = context.schools!.Include(s => s.students.Where(s => s.isdeleted == false)).Include(s => s.teachers).Where(s => s.isdeleted == false).AsNoTracking().ToList();
                List<ItemSchool> itemSchools = new List<ItemSchool>();
                foreach (SqlSchool f_school in sql_schools)
                {
                    ItemSchool itemSchool = new ItemSchool();
                    itemSchool.code = f_school.code;
                    itemSchool.name = f_school.name;
                    itemSchool.des = f_school.des;
                    foreach (SqlStudent f_student in f_school.students)
                    {
                        ItemCommon itemCommon = new ItemCommon();
                        itemCommon.code = f_student.code;
                        itemSchool.students.Add(itemCommon);
                    }
                    foreach (SqlTeacher f_teacher in f_school.teachers)
                    {
                        ItemCommon itemCommon = new ItemCommon();
                        itemCommon.code = f_teacher.code;
                        itemSchool.teachers.Add(itemCommon);
                    }
                    itemSchools.Add(itemSchool);
                }
                return itemSchools;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return new List<ItemSchool>();
        }
    }

    public int Create(string school, string name, string des)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).AsNoTracking().FirstOrDefault();
                if (sql_school != null)
                {
                    return -1;
                }
                sql_school = new SqlSchool();
                sql_school.ID = DateTime.Now.Ticks;
                sql_school.code = school;
                sql_school.name = name;
                sql_school.des = des;
                context.schools!.Add(sql_school);
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

    public int Edit(string school, string name, string des)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).FirstOrDefault();
                if (sql_school == null)
                {
                    return -1;
                }
                if (string.IsNullOrEmpty(name) == false)
                {
                    sql_school.name = name;
                }
                if (string.IsNullOrEmpty(des) == false)
                {
                    sql_school.des = des;
                }
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

    public int Delete(string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).AsNoTracking().FirstOrDefault();
                if (sql_school == null)
                {
                    return -1;
                }
                sql_school.isdeleted = true;
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

    public int AddStudentToSchool(string student, string? school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                //lý do tại sao AsNoTraking entity này lại saveChange được
                SqlStudent? sql_student = context.students!.Include(s => s.school).Where(s => s.code.CompareTo(student) == 0).FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                //lý do tại sao as AsNoTracking mà vẫn savechanges được (*)?
                SqlSchool? sql_school = context.schools!.Include(s => s.students).Where(s => s.code.CompareTo(school) == 0).FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                if (sql_school.students.Select(s => s.code).Contains(sql_student.code))
                {
                    return 0;
                }
                sql_school.students.Add(sql_student);
                //sql_student.school = sql_school;// (*)
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

    public int RemoveStudentFromSchool(string student, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                //lý do tại sao AsNoTraking entity này lại không thể savechange?
                SqlStudent? sql_student = context.students!.Include(s => s.school).Where(s => s.code.CompareTo(student) == 0).FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                SqlSchool? sql_school = context.schools!.Include(s => s.students).Where(s => s.code.CompareTo(school) == 0).FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                if (sql_school.students.Select(s => s.code).Contains(sql_student.code) == false)
                {
                    return 0;
                }
                sql_school.students.Remove(sql_student);
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

    public int AddTeacherToSchool(string teacher, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlTeacher? sql_teacher = context.teachers!.Where(s => s.code.CompareTo(teacher) == 0).FirstOrDefault();
                if (sql_teacher == null)
                {
                    return -1;
                }
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).Include(s => s.students).FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                if (sql_school.teachers.Contains(sql_teacher))
                {
                    return -3;
                }
                sql_school.teachers.Add(sql_teacher);
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

    public int RemoveTeacherFromSchool(string teacher, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlTeacher? sql_teacher = context.teachers!.Where(s => s.code.CompareTo(teacher) == 0).FirstOrDefault();
                if (sql_teacher == null)
                {
                    return -1;
                }
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).Include(s => s.students).FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                if (sql_school.teachers.Contains(sql_teacher) == false)
                {
                    return -3;
                }
                sql_school.teachers.Add(sql_teacher);
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

    public int CompareStudentWithSchool(string student, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).Include(s => s.school).FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                if (sql_student.school == sql_school)
                {
                    return 0;
                }
                else
                {
                    return -3;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return -9;
        }
    }

    public int CompareTeacherWithSchool(string teacher, string school)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlTeacher? sql_teacher = context.teachers!.Where(s => s.code.CompareTo(teacher) == 0).Include(s => s.school).FirstOrDefault();
                if (sql_teacher == null)
                {
                    return -1;
                }
                SqlSchool? sql_school = context.schools!.Where(s => s.code.CompareTo(school) == 0).FirstOrDefault();
                if (sql_school == null)
                {
                    return -2;
                }
                if (sql_teacher.school == sql_school)
                {
                    return 0;
                }
                else
                {
                    return -3;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return -9;
        }
    }

    public int CompareStudentWithTeacher(string student, string teacher)
    {
        try
        {
            using (DataContext context = new DataContext())
            {
                SqlStudent? sql_student = context.students!.Where(s => s.code.CompareTo(student) == 0).Include(s => s.school).FirstOrDefault();
                if (sql_student == null)
                {
                    return -1;
                }
                SqlTeacher? sql_teacher = context.teachers!.Where(s => s.code.CompareTo(teacher) == 0).Include(s => s.school).FirstOrDefault();
                if (sql_teacher == null)
                {
                    return -1;
                }
                if (sql_student.school == sql_teacher.school)
                {
                    return 0;
                }
                else
                {
                    return -3;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return -9;
        }
    }
}
