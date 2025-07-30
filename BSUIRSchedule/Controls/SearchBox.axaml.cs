using Avalonia;
using Avalonia.Data;
using Avalonia.Controls;
using System.Collections;
using Avalonia.Input;
using System;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace BSUIRSchedule.Controls;

public partial class SearchBox : UserControl
{

    #region MaxDropDownHeight

    public static readonly StyledProperty<double> MaxDropDownHeightProperty =
        AvaloniaProperty.Register<SearchBox, double>("MaxDropDownHeight", 0D);
    public double MaxDropDownHeight
    {
        get { return GetValue(MaxDropDownHeightProperty); }
        set { SetValue(MaxDropDownHeightProperty, value); }
    }

    #endregion

    #region ItemsSource

    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<SearchBox, IEnumerable>("ItemsSource");
    public IEnumerable ItemsSource
    {
        get { return GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    #endregion

    #region Text

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<SearchBox, string>("Text", string.Empty, defaultBindingMode: BindingMode.TwoWay);
    private static void OnTextChanged(SearchBox sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (sender is null) return;
        sender.RaiseEvent(new RoutedEventArgs { RoutedEvent = TextChangedEvent });
    }
    public string Text
    {
        get { return GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    #endregion

    #region IsDropDownOpen

    public static readonly StyledProperty<bool> IsDropDownOpenProperty =
        AvaloniaProperty.Register<SearchBox, bool>("IsDropDownOpen", false, defaultBindingMode: BindingMode.TwoWay);
    public bool IsDropDownOpen
    {
        get { return GetValue(IsDropDownOpenProperty); }
        set { SetValue(IsDropDownOpenProperty, value); }
    }

    #endregion

    #region SelectedIndex

    public static readonly StyledProperty<int> SelectedIndexProperty =
        AvaloniaProperty.Register<SearchBox, int>("SelectedIndex", -1);
    public int SelectedIndex
    {
        get { return GetValue(SelectedIndexProperty); }
        set { SetValue(SelectedIndexProperty, value); }
    }

    #endregion

    #region SelectedItem

    public static readonly StyledProperty<object?> SelectedItemProperty =
        AvaloniaProperty.Register<SearchBox, object?>("SelectedItem", null);
    public object? SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    #endregion

    static SearchBox()
    {
        TextProperty.Changed.AddClassHandler<SearchBox>(OnTextChanged);
    }
    public SearchBox()
    {
        InitializeComponent();
        listBox.AddHandler(InputElement.KeyDownEvent, listBox_OnPreviewKeyDown, RoutingStrategies.Tunnel);
        listBox.AddHandler(InputElement.GotFocusEvent, listBox_OnGotFocus, RoutingStrategies.Bubble);
        this.AddHandler(InputElement.KeyDownEvent, SearchBox_OnPreviewKeyDown, RoutingStrategies.Tunnel);

        searchTextBox.AttachedToVisualTree += TextBox_OnAttachedToVisualTree;
    }

    private void TextBox_OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        searchTextBox.Focus();
        searchTextBox.AttachedToVisualTree -= TextBox_OnAttachedToVisualTree;
    }

    bool listFocused = false;

    private void SearchBox_OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Up || e.Key == Key.Down)
        {
            if (!listFocused)
            {
                listBox.Focus();
                listFocused = true;
            }
        }
        else if(e.Key == Key.Enter)
        {
            return;
        }
        else
        {
            if (listFocused)
            {
                searchTextBox.Focus();
                listFocused = false;
            }
        }
    }
    private void listBox_OnGotFocus(object? sender, EventArgs e)
    {
        listBox.SelectedIndex = 0;    
    }
    private void listBox_OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if(e.Key == Key.Down)
        {
            if (listBox.SelectedIndex < listBox.Items.Count - 1)
                listBox.SelectedIndex++;
            else if (listBox.SelectedIndex == listBox.Items.Count - 1)
                listBox.SelectedIndex = 0;
        }
        else if(e.Key == Key.Up)
        {
            if(listBox.SelectedIndex > 0)
                listBox.SelectedIndex--;
            else if(listBox.SelectedIndex == 0)
                listBox.SelectedIndex = listBox.Items.Count - 1;
        }
        else if (e.Key == Key.Enter)
        {
            this.RaiseEvent(new RoutedEventArgs { RoutedEvent = ItemSelectedEvent });
        }
        else
        {
            return;
        }
        e.Handled = true;
    }

    private void ListBoxItem_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Properties.IsLeftButtonPressed)
        {
            if (e.Source is not InputElement element) return;
            var item = element.FindAncestorOfType<ListBoxItem>();
            if (item is null) return;
            SelectedItem = item.DataContext;
            this.RaiseEvent(new RoutedEventArgs { RoutedEvent = ItemSelectedEvent });
            e.Handled = true;
        }
    }

    #region TextChangedEvent

    public static readonly RoutedEvent<RoutedEventArgs> TextChangedEvent =
        RoutedEvent.Register<SearchBox, RoutedEventArgs>("TextChanged", RoutingStrategies.Bubble);
    public event EventHandler<RoutedEventArgs> TextChanged
    {
        add => AddHandler(TextChangedEvent, value);
        remove => RemoveHandler(TextChangedEvent, value);
    }

    #endregion

    #region ItemSelectedEvent

    public static readonly RoutedEvent<RoutedEventArgs> ItemSelectedEvent =
        RoutedEvent.Register<SearchBox, RoutedEventArgs>("ItemSelected", RoutingStrategies.Bubble);
    public event EventHandler<RoutedEventArgs> ItemSelected
    {
        add => AddHandler(ItemSelectedEvent, value);
        remove => RemoveHandler(ItemSelectedEvent, value);
    }

    #endregion
}