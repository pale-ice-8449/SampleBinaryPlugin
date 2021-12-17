using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;

namespace SampleBinaryPlugin.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {

    #region Private Properties

    private string _summaryOfAvailablePlans;
    private Course _selectedCourse;

    #endregion


    #region Constructor

    public MainViewModel(List<Course> courses)
    {
      Courses = courses.OrderBy(x => x.Id).ToList();
      SetSelectedCourse();
    }

    #endregion


    #region Private Methods

    private void SetSelectedCourse()
    {
      SelectedCourse = null;
      if (Courses.Count > 0)
      {
        SelectedCourse = Courses[0];
      }
    }

    private void GetSummaryOfAvaliablePlans()
    {
      if (Courses.Count > 0 && SelectedCourse != null)
      {
        SummaryOfAvailablePlans = $"Plans in {SelectedCourse.Id}";
        foreach (var planSetup in SelectedCourse.PlanSetups)
        {
          SummaryOfAvailablePlans += GetPlanSummaryString(planSetup);
        }
      }
    }

    private string GetPlanSummaryString(PlanSetup planSetup)
    {
      // if the plan is not null, return a summary, if it is return an empty string
      return planSetup != null ?
        $"\n\n\t{planSetup.Id}\n\n" +
        $"\t\tDose:\t\t{planSetup.TotalDose}\n" +
        $"\t\tFx:\t\t{planSetup.NumberOfFractions}\n" +
        $"\t\tMax Dose:\t{planSetup.Dose?.DoseMax3D}\n" +
        $"\t\t# Tx Flds:\t\t{planSetup.Beams?.Where(x => x.IsSetupField == false).Count()}" : string.Empty;
    }

    #endregion


    #region Public Properties
    public List<Course> Courses { get; set; } = new List<Course>();
    public string SummaryOfAvailablePlans
    {
      get { return _summaryOfAvailablePlans; }
      set { _summaryOfAvailablePlans = value; OnPropertyChanged(); }
    }
    public Course SelectedCourse
    {
      get { return _selectedCourse; }
      set { _selectedCourse = value; OnPropertyChanged(); GetSummaryOfAvaliablePlans(); }
    }
    #endregion


    #region INotifyPropertyChanged

    // https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-property-change-notification?view=netframeworkdesktop-4.8

    // Declare the event
    public event PropertyChangedEventHandler PropertyChanged;
    // Create the OnPropertyChanged method to raise the event
    // The calling member's name will be used as the parameter.
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    #endregion

  }
}
