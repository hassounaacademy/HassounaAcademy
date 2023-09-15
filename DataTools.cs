namespace HassounaAcademy
{
    internal class DataTools
    {
        public static string MyAppName = "Hassouna Academy";

        internal static List<Course> courses = CoursesData.courses;

        public static Task<string> GetCoursesLinksAsync(List<Course> courses)
        {
            string strLinks = "";
            foreach (var item in courses) strLinks += $"{item.name_en} {Environment.NewLine}{item.course_url} {Environment.NewLine}{Environment.NewLine}";
            return Task.FromResult(strLinks);
        }

        public static Task<string> GetCoursesLinksAsHTMLAsync(List<Course> courses)
        {
            string strLinks = "";
            foreach (var item in courses) strLinks += $"<a href=\"{item.course_url}\">{item.name_en}</a><br>{Environment.NewLine}";
            return Task.FromResult(strLinks);
        }

    }

    public enum CourseType { Anybody = 1, Knowledge = 2, Database = 3, Great = 4, Other = 5, All = 6, Unknown = 0 }

    public class Course
    {
        private static int _nextID = 0;
        public int id { get; set; }
        public required string name_en { get; set; }
        public required string name_ar { get; set; }
        public CourseType type { get; set; } = CourseType.Other;
        public required string list_id { get; set; }
        public required string video_id_for_image { get; set; }
        public string image_url => $"https://img.youtube.com/vi/{video_id_for_image}/default.jpg";
        public string course_url => $"https://www.youtube.com/{(string.IsNullOrEmpty(video_id) ? "playlist?" : $"watch?v={video_id}&")}list={list_id}";
        public string video_id { get; set; }
        public Command open => new Command(async () => { await Launcher.Default.OpenAsync(new Uri(course_url)); });
        public Command copy => new Command(async () => { await Clipboard.SetTextAsync(course_url); await Shell.Current.DisplayAlert(DataTools.MyAppName, "Link copied to clipboard", "OK"); });
        public Course(){ id = ++_nextID; }
    }

}
