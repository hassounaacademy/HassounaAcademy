namespace HassounaAcademy;

public partial class PageShowCourses : ContentPage
{
    public CourseType type { get; set; } = CourseType.Anybody;
    public string searchText { get; set; } = "";

    public PageShowCourses()
	{
		InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        List<Course> courses;
        if (!string.IsNullOrEmpty(searchText))
        {
            courses = DataTools.courses.Where(c => c.name_ar.ToLower().Contains(searchText.ToLower()) || c.name_en.ToLower().Contains(searchText.ToLower())).ToList();
            Title = $"Courses containts {searchText} ({courses.Count})";
        }
        else if (type == CourseType.All)
        {
            courses = DataTools.courses;
            Title = $"All Courses ({courses.Count})";
        }
        else
        {
            courses = DataTools.courses.Where(c => c.type == type).ToList();
            Title = $"{type} Courses ({courses.Count})";
        }
        cv.ItemsSource = courses;
    }
}