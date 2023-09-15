namespace HassounaAcademy;

public partial class PageAllTools : ContentPage
{
    private CourseType courseType = CourseType.Anybody;
    private List<Course> courses = DataTools.courses;

    public PageAllTools()
    {
        InitializeComponent();
    }

    private async void btnOtherCourses_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageShowCourses { type = CourseType.Other });
    }

    private async void btnAllCourses_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageShowCourses { type = CourseType.All });
    }

    private void RadioButton_For_Copy_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is not RadioButton) return;
        if (((RadioButton)sender).IsChecked)
        {
            if (!int.TryParse(((RadioButton)sender).Content.ToString(), out _)) return;
            courseType = (CourseType)Convert.ToInt32(((RadioButton)sender).Content.ToString().Trim());
            if (btnCopyLinks != null) btnCopyLinks.Text = $"Copy {courseType} Links";
            if (btnCopyLinksAsHTML != null) btnCopyLinksAsHTML.Text = $"Copy {courseType} Links As HTML";
            if (courseType == CourseType.All) courses = DataTools.courses;
            else courses = DataTools.courses.Where(c => c.type == courseType).ToList();
        }
    }

    private async void btnCopyLinks_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(await DataTools.GetCoursesLinksAsync(courses));
        await Shell.Current.DisplayAlert(DataTools.MyAppName, "Links copied to clipboard", "OK");
    }

    private async void btnCopyLinksAsHTML_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(await DataTools.GetCoursesLinksAsHTMLAsync(courses));
        await Shell.Current.DisplayAlert(DataTools.MyAppName, "Links copied to clipboard as HTML", "OK");
    }

    private async void btnSearch_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageShowCourses { searchText = txtSearch.Text });
    }

}