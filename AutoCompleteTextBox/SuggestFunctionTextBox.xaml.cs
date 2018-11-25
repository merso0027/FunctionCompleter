using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using FunctionComplete;
using FunctionComplete.Models;
using System.Linq;
using Repository;

namespace Controls
{
    public partial class SuggestFunctionTextBox : TextBox
    {
        private Popup Popup { get { return Template.FindName("PART_Popup", this) as Popup; } }
        private ListBox ItemList { get { return Template.FindName("PART_ItemList", this) as ListBox; } }
        private Grid Root { get { return Template.FindName("root", this) as Grid; } }
        private ScrollViewer Host { get { return Template.FindName("PART_ContentHost", this) as ScrollViewer; } }
        private UIElement TextBoxView { get { foreach (object o in LogicalTreeHelper.GetChildren(Host)) return o as UIElement; return null; } }

        private TokenCompleter _tokenCompleter;
        private Suggestions _suggestions;
        private ToolTip tooltipMethodSignatures;
        private FunctionRepository functionRepository;

        public SuggestFunctionTextBox()
        {
            functionRepository = new FunctionRepository();
            _tokenCompleter = new TokenCompleter(
                   functionRepository.GetRawFunctions(),
                   functionRepository.GetRawStructures(),
                   functionRepository.GetRawVariables()
                );
            InitializeComponent();
            InitializeTooltip();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            KeyDown += new KeyEventHandler(AutoCompleteTextBox_KeyDown);
            PreviewKeyDown += new KeyEventHandler(AutoCompleteTextBox_PreviewKeyDown);
            ItemList.PreviewMouseDown += new MouseButtonEventHandler(ItemList_PreviewMouseDown);
            ItemList.KeyDown += new KeyEventHandler(ItemList_KeyDown);
            Popup.CustomPopupPlacementCallback += new CustomPopupPlacementCallback(Repositioning);
        }

        /// <summary>
        /// Initialize tooltip.
        /// Tooltip will contain whole message signatures.
        /// </summary>
        private void InitializeTooltip()
        {
            tooltipMethodSignatures = new ToolTip();
            tooltipMethodSignatures.PlacementTarget = this;
            tooltipMethodSignatures.Placement = PlacementMode.Top;
            ToolTip = tooltipMethodSignatures;
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
                switch (e.Key)
                {
                    case Key.Enter:
                        Text = _suggestions.TokenToCurrent + (tb.Content as string).Substring(0, (tb.Content as string).Length - 1);
                        updateSource();
                        break;
                    case Key.Escape:
                        tooltipMethodSignatures.IsOpen = false;
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
                tooltipMethodSignatures.IsOpen = false;
                updateSource();
            }
            if (e.Key == Key.Escape)
            {
                Popup.IsOpen = false;
                tooltipMethodSignatures.IsOpen = false;
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
                TextBlock textBlock = e.OriginalSource as TextBlock;
                if (textBlock != null)
                {
                    Text = _suggestions.TokenToCurrent + (textBlock.Text as string).Substring(0, textBlock.Text.Length - 1);
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
            tooltipMethodSignatures.IsOpen = _suggestions.Signatures.Count > 0;
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

            tooltipMethodSignatures.Content = signatures;
            ItemList.Items.Clear();
            foreach (string value in _suggestions.CompleteStructures)
            {
                ItemList.Items.Add(value + "^");
            }

            foreach (string value in _suggestions.CompleteVariables)
            {
                ItemList.Items.Add(value + "#");
            }

            foreach (string value in _suggestions.CompleteFunctions)
            {
                ItemList.Items.Add(value + "(*");
            }
            Popup.IsOpen = ItemList.Items.Count > 0;
        }
    }
}
