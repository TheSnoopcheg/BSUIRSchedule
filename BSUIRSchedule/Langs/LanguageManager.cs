using BSUIRSchedule.Classes;
using System.Diagnostics;

namespace BSUIRSchedule.Langs
{
    public static class LanguageManager
    {
        public static void SetLanguage(LanguageType languageType)
        {
            string ietfTag = languageType.GetIetfLanguageTag();
            if (string.IsNullOrEmpty(ietfTag))
            {
                return;
            }
            if(ietfTag == Config.Instance.CurrentLanguage)
            {
                return;
            }
            Config.Instance.CurrentLanguage = ietfTag;
            Config.Instance.Save();
            var currentExecutablePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(currentExecutablePath);
            //Application.Current.Shutdown();
        }
    }
}
