using Avalonia.Data.Converters;
using BSUIRSchedule.Classes;
using BSUIRSchedule.Services;
using System.Collections.Generic;
using System.Text;

namespace BSUIRSchedule.Converters
{
    public static class FuncConverters
    {
        public static FuncValueConverter<string, string> ShortNameConverter { get; }
            = new FuncValueConverter<string, string>(name =>
            {
                if (int.TryParse(name, out int numVal))
                {
                    return name;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    string[] parts = name!.Split(' ');
                    stringBuilder.Append(parts[0], 0, 3);
                    stringBuilder.Append("\n");
                    stringBuilder.Append(parts[1]);
                    stringBuilder.Append(parts[2]);
                    return stringBuilder.ToString();
                }
            });

        public static FuncValueConverter<object, IEnumerable<GroupTeacher>?> GroupTeacherConverter { get; }
            = new FuncValueConverter<object, IEnumerable<GroupTeacher>?>(obj =>
            {
                return obj switch
                {
                    Lesson l => ConvertersHelper.GetGroupTeacherCollectionFromLesson(l),
                    _ => null
                };
            });

        public static FuncMultiValueConverter<object?, IEnumerable<GroupTeacher>?> GroupTeacherMultiConverter { get; }
            = new FuncMultiValueConverter<object?, IEnumerable<GroupTeacher>?>(objs =>
            {
                if (objs is not List<object> collection || collection.Count < 2) return null;
                if (collection[0] is not Announcement ann) return null;
                if (collection[1] is not bool isEmpl) return null;
                return ConvertersHelper.GetGroupTeacherCollectionFromAnn(ann, isEmpl);
            });
    }
}
