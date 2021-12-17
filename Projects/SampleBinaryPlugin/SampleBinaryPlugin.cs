using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using SampleBinaryPlugin.Views;
using SampleBinaryPlugin.ViewModels;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
  public class Script
  {
    public Script()
    {
    }

    #region Private Properties

    private List<Course> _availableCourses = new List<Course>();

    #endregion


    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Execute(ScriptContext context , System.Windows.Window window/*, ScriptEnvironment environment*/)
    {
      // TODO : Add here the code that is called when the script is launched from Eclipse.
      // get list of courses
      GetAvailableCourses(context);

      // create main view and set window content
      InitializeViewsAndSetWindowContent(window);
    }

   

    #region Private Methods

    private void GetAvailableCourses(ScriptContext context)
    {
      if (context.Patient.Courses.Count() > 0)
      {
        _availableCourses = context.Patient.Courses.ToList();
      }
    }

    private void InitializeViewsAndSetWindowContent(Window window)
    {
      // create instance of main view
      MainView mainView = new MainView { DataContext = new MainViewModel(_availableCourses) };

      // set window settings
      window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      //window.WindowState = WindowState.Maximized;

      // set content
      window.Content = mainView;
    }

    #endregion

  }
}
