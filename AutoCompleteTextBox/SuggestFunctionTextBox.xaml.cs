using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using FunctionComplete;
using FunctionComplete.Models;
using System.Linq;

namespace Controls
{
    public partial class SuggestFunctionTextBox : TextBox
    {
        private Popup Popup { get { return this.Template.FindName("PART_Popup", this) as Popup; } }
        private ListBox ItemList { get { return this.Template.FindName("PART_ItemList", this) as ListBox; } }
        private Grid Root { get { return this.Template.FindName("root", this) as Grid; } }
        private ScrollViewer Host { get { return this.Template.FindName("PART_ContentHost", this) as ScrollViewer; } }
        private UIElement TextBoxView { get { foreach (object o in LogicalTreeHelper.GetChildren(Host)) return o as UIElement; return null; } }

        private TokenCompleter _tokenCompleter;
        private Suggestions _suggestions;
        private ToolTip tooltipMethodDescriptions;
        public SuggestFunctionTextBox()
        {
            _tokenCompleter = new TokenCompleter();
            InitializeComponent();
            InitializeTooltip();
        }

        private void InitializeTooltip()
        {
            tooltipMethodDescriptions = new ToolTip();
            tooltipMethodDescriptions.PlacementTarget = this;
            tooltipMethodDescriptions.Placement = PlacementMode.Top;
            this.ToolTip = tooltipMethodDescriptions;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.KeyDown += new KeyEventHandler(AutoCompleteTextBox_KeyDown);
            this.PreviewKeyDown += new KeyEventHandler(AutoCompleteTextBox_PreviewKeyDown);
            ItemList.PreviewMouseDown += new MouseButtonEventHandler(ItemList_PreviewMouseDown);
            ItemList.KeyDown += new KeyEventHandler(ItemList_KeyDown);
            Popup.CustomPopupPlacementCallback += new CustomPopupPlacementCallback(Repositioning);
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
                        Text = _suggestions.TokenToCurrent + (tb.Content as string) + "(";
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
                    Text = _suggestions.TokenToCurrent + (tb.Text as string) + "(";
                    updateSource();
                    SelectionStart = Text.Length;
                    SelectionLength = 0;
                    Popup.IsOpen = false;
                    e.Handled = true;
                }
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            _suggestions = _tokenCompleter.Run(this.Text);
            tooltipMethodDescriptions.IsOpen = _suggestions.Signatures.Count > 0;
            string signatures = string.Empty;
            foreach (var item in _suggestions.Signatures)
            {
                if (_suggestions.Signatures.LastOrDefault().Equals(item))
                {
                    signatures += item;
                    break;
                }
                signatures += item + Environment.NewLine;
            }

            tooltipMethodDescriptions.Content = signatures;
            ItemList.Items.Clear();
            foreach (string value in _suggestions.Complete)
            {
                ItemList.Items.Add(value);
            }
            Popup.IsOpen = ItemList.Items.Count > 0;
        }
    }
}
