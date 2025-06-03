using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Interactivity;
using System;
using System.Threading;
using System.Windows.Input;

namespace BSUIRSchedule.Controls;

public class RoundButton : RadioButton
{
    protected override Type StyleKeyOverride => typeof(RoundButton);
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private bool _isAnimationRunning = false;

    #region Properties

    #region DeleteCommand

    public static readonly StyledProperty<ICommand> DeleteCommandProperty =
        AvaloniaProperty.Register<RoundButton, ICommand>("DeleteCommand", null, defaultBindingMode: BindingMode.OneWay);

    public ICommand DeleteCommand
    {
        get { return GetValue(DeleteCommandProperty); }
        set { SetValue(DeleteCommandProperty, value); }
    }

    #endregion

    #endregion

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        _cancellationTokenSource.Dispose();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _dashEllipse = e.NameScope.Find<Ellipse>(PART_DashEllipse);

        if(this.TryFindResource("RoundButtonDashEllipseAnimation", out var resource) && resource is Animation animation)
        {
            _dashEllipseAnimation = animation;
        }
    }
    protected override async void OnClick()
    {
        base.OnClick();
        if (this.Classes.Contains("static"))
            return;
        if (_isAnimationRunning)
        {
            _cancellationTokenSource.Cancel();
            _isAnimationRunning = false;
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
        if (_dashEllipseAnimation != null && _dashEllipse != null)
        {
            _isAnimationRunning = true;
            await _dashEllipseAnimation.RunAsync(_dashEllipse, _cancellationTokenSource.Token);
            _isAnimationRunning = false;
        }
    }

    private const string PART_DashEllipse = "PART_DashEllipse";

    private Animation _dashEllipseAnimation;
    private Ellipse _dashEllipse;
}