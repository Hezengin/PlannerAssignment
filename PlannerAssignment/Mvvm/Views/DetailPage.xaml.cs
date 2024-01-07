using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Utils;
using System.Diagnostics;

namespace PlannerAssignment.Mvvm.Views;

public partial class DetailPage : ContentPage
{
    RequestManager RequestManager { get; set; }
    DetailViewModel DetailViewModel { get; set; }

    public DetailPage(DetailViewModel detailViewModel)
    {
        InitializeComponent();
        DetailViewModel = detailViewModel;
        BindingContext = DetailViewModel;

        switchIsOn.Toggled += (s, e) => DetailViewModel.SwitchToggleCommand.Execute(e.Value);

    }

    private void lampTapped(object sender, TappedEventArgs e)
    {

    }

    private void refreshButtonClicked(object sender, EventArgs e)
    {

    }

    private void SliderBrightness_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DetailViewModel.UpdateBrightnessCommand.Execute((int)e.NewValue);
        int briValue = (int)e.NewValue;
        briLabel.Text = briValue.ToString();
    }
    private void SliderSaturation_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DetailViewModel.UpdateSaturationCommand.Execute((int)e.NewValue);
        int satValue = (int)e.NewValue;
        satLabel.Text = satValue.ToString();
    }

    private void SliderHue_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DetailViewModel.UpdateHueCommand.Execute((int)e.NewValue);
        int hueValue = (int)e.NewValue;
        hueLabel.Text = hueValue.ToString();
    }
}