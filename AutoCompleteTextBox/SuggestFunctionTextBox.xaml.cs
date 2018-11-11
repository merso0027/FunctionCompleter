using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls.Primitives;
using FunctionComplete;

namespace QuickZip.Controls
{
    /// <summary>
    /// Interaction logic for SelectFolderTextBox.xaml
    /// </summary>
    public partial class SuggestFunctionTextBox : TextBox
    {
        Popup Popup { get { return this.Template.FindName("PART_Popup", this) as Popup; } }
        ListBox ItemList { get { return this.Template.FindName("PART_ItemList", this) as ListBox; } }
        Grid Root { get { return this.Template.FindName("root", this) as Grid; } }
        ScrollViewer Host { get { return this.Template.FindName("PART_ContentHost", this) as ScrollViewer; } }
        UIElement TextBoxView { get { foreach (object o in LogicalTreeHelper.GetChildren(Host)) return o as UIElement; return null; } }

        private bool _loaded = false;
        string lastPath;

        public SuggestFunctionTextBox()
        {
            InitializeComponent();
        }


        private bool prevState = false;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _loaded = true;
            this.KeyDown += new KeyEventHandler(AutoCompleteTextBox_KeyDown);
            this.PreviewKeyDown += new KeyEventHandler(AutoCompleteTextBox_PreviewKeyDown);
            ItemList.PreviewMouseDown += new MouseButtonEventHandler(ItemList_PreviewMouseDown);
            ItemList.KeyDown += new KeyEventHandler(ItemList_KeyDown);
            Popup.CustomPopupPlacementCallback += new CustomPopupPlacementCallback(Repositioning);


            Window parentWindow = getParentWindow();
            if (parentWindow != null)
            {
                parentWindow.Deactivated += delegate { prevState = Popup.IsOpen; Popup.IsOpen = false; };
                parentWindow.Activated += delegate { Popup.IsOpen = prevState; };
            }


        }

        private Window getParentWindow()
        {
            DependencyObject d = this;
            while (d != null && !(d is Window))
                d = LogicalTreeHelper.GetParent(d);
            return d as Window;
        }

        private CustomPopupPlacement[] Repositioning(Size popupSize, Size targetSize, Point offset)
        {
            return new CustomPopupPlacement[] {
                new CustomPopupPlacement(new Point((0.01 - offset.X), (Root.ActualHeight - offset.Y)), PopupPrimaryAxis.None) };
        }

        void TempVisual_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string text = Text;
            ItemList.SelectedIndex = -1;
            Text = text;
            Popup.IsOpen = false;
        }

        void AutoCompleteTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (ItemList.Items.Count > 0 && !(e.OriginalSource is ListBoxItem))
                switch (e.Key)
                {
                    case Key.Up:
                    case Key.Down:
                    case Key.Prior:
                    case Key.Next:
                        ItemList.Focus();
                        ItemList.SelectedIndex = 0;
                        ListBoxItem lbi = ItemList.ItemContainerGenerator.ContainerFromIndex(ItemList.SelectedIndex) as ListBoxItem;
                        lbi.Focus();
                        e.Handled = true;
                        break;

                }
        }


        void ItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {

                ListBoxItem tb = e.OriginalSource as ListBoxItem;

                e.Handled = true;
                var d = Text;
                switch (e.Key)
                {
                    case Key.Enter:
                        Text = "damir" + (tb.Content as string) + "(";
                        updateSource();
                        break;
                    default:
                        e.Handled = false;
                        break;
                }
                //12-25-08 - Force focus back the control after selected.
                if (e.Handled)
                {
                    Keyboard.Focus(this);
                    Popup.IsOpen = false;
                    this.Select(Text.Length, 0); //Select last char
                }
            }
        }


        void AutoCompleteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Popup.IsOpen = false;
                updateSource();
            }
        }

        void updateSource()
        {
            if (this.GetBindingExpression(TextBox.TextProperty) != null)
                this.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        void ItemList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TextBlock tb = e.OriginalSource as TextBlock;
                if (tb != null)
                {
                    Text = tb.Text;
                    updateSource();
                    Popup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (_loaded)
            {
                try
                {
                    {
                        Tuple<List<string>, List<string>> tokenCompleter = new TokenCompleter().Complete(this.Text);
                        var complete = tokenCompleter.Item1;

                        ItemList.Items.Clear();
                        foreach (string value in complete)
                            if (!(String.Equals(value, this.Text, StringComparison.CurrentCultureIgnoreCase)))
                                ItemList.Items.Add(value);
                    }


                    Popup.IsOpen = ItemList.Items.Count > 0;
                }
                catch
                {

                }
            }
        }


        private string[] Lookup(string path)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    DirectoryInfo lookupFolder = new DirectoryInfo(Path.GetDirectoryName(path));
                    if (lookupFolder != null)
                    {
                        DirectoryInfo[] AllItems = lookupFolder.GetDirectories();
                        return (from di in AllItems where di.FullName.StartsWith(path, StringComparison.CurrentCultureIgnoreCase) select di.FullName).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return new string[0];
        }
    }
}
