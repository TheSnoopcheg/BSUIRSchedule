using Avalonia.Data.Converters;
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
    }
}
