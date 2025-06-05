using BSUIRSchedule.Classes;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BSUIRSchedule.Services;

public static class ConvertersHelper
{
    public static string GetTeacherDescription(Employee e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(e.GetFullName());
        bool dRes = string.IsNullOrEmpty(e.degreeAbbrev);
        bool rRes = string.IsNullOrEmpty(e.rank);
        if (!dRes || !rRes)
        {
            sb.Append("\n(");
            sb.Append(e.degreeAbbrev);
            if (!dRes && !rRes)
                sb.Append(", ");
            sb.Append(e.rank);
            sb.Append(")");
        }
        return sb.ToString();
    }
    public static string GetStudentGroupDescription(StudentGroup s)
    {
        return $"{s.specialityName} ({s.numberOfStudents} {Langs.Language.Students})";
    }

    public static IEnumerable<GroupTeacher>? GetGroupTeacherCollectionFromLesson(Lesson lesson)
    {
        return lesson switch
        {
            { employees: not null } => lesson.employees.Select(e => new GroupTeacher
            {
                VisibleName = e.ToString(),
                PhotoLink = e.photoLink,
                Description = GetTeacherDescription(e),
                Url = e.urlId!
            }),
            { studentGroups: not null } => lesson.studentGroups.OrderBy(g => g.name).Select(g => new GroupTeacher
            {
                VisibleName = g.ToString(),
                Description = GetStudentGroupDescription(g),
                Url = g.urlId!
            }),
            _ => null
        };
    }
    public static IEnumerable<GroupTeacher>? GetGroupTeacherCollectionFromAnn(Announcement announcement, bool isEmpl)
    {
        return isEmpl switch
        {
            true when announcement.studentGroups != null => announcement.studentGroups.OrderBy(g => g.name).Select(g => new GroupTeacher
            {
                VisibleName = g.ToString(),
                Url = g.urlId!
            }),
            false when !string.IsNullOrEmpty(announcement.employee) => new List<GroupTeacher>
            {
                new GroupTeacher
                {
                    VisibleName = announcement.employee,
                    Url = announcement.urlId!,
                }
            },
            _ => null
        };
    }
}
