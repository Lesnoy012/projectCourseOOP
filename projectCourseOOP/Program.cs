namespace projectCourseOOP
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new WelcomeForm());
            Application.Run(new MainForm());
        }
    }
}