using Avalonia;
using Avalonia.Controls;
using System.Collections;
using System.Windows.Input;

namespace BSUIRSchedule.Controls;

public partial class GroupTeacherButtonPanel : UserControl
{
    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<GroupTeacherButtonPanel, IEnumerable>("ItemsSource");
    public IEnumerable ItemsSource
    {
        get { return GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly StyledProperty<ICommand> LoadCommandProperty =
        AvaloniaProperty.Register<GroupTeacherButtonPanel, ICommand>("LoadCommand");
    public ICommand LoadCommand
    {
        get { return GetValue(LoadCommandProperty); }
        set { SetValue(LoadCommandProperty, value); }
    }

    public GroupTeacherButtonPanel()
    {
        InitializeComponent();
    }
}