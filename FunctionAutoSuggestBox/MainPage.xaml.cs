using FunctionComplete;
using SignatureRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FunctionAutoSuggestBox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            var signatureFactory = new SignatureFactory();
            Task.Run(() => new TokenCompleter(signatureFactory.GetSignatureService()))
                .ContinueWith((res) =>
                  {
                      _tokenCompleter = res.Result;
                  }
                );
            this.InitializeComponent();
        }

        private TokenCompleter _tokenCompleter;
        private Suggestions _suggestions;
        private ToolTip tooltipMethodSignatures;

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
        
                _suggestions = _tokenCompleter.Run(sender.Text);
                //tooltipMethodSignatures.IsOpen = _suggestions.Signatures.Count > 0;
                //string signatures = string.Empty;
                //foreach (var item in _suggestions.Signatures)
                //{
                //    if (_suggestions.Signatures.LastOrDefault().Equals(item))
                //    {
                //        signatures += item;
                //        break;
                //    }
                //    signatures += item + Environment.NewLine;
                //}

                //tooltipMethodSignatures.Content = signatures;
                //ItemList.Items.Clear();
                var res = new List<string>();
                foreach (string value in _suggestions.CompleteStructures)
                {
                    res.Add("ω:" + value);
                }

                foreach (string value in _suggestions.CompleteVariables)
                {
                    res.Add("ω:" + value);
                }

                foreach (string value in _suggestions.CompleteFunctions)
                {
                    res.Add("ƒ:" + value + "(");
                }
                //Popup.IsOpen = ItemList.Items.Count > 0;

                
                //Set the ItemsSource to be your filtered dataset
                sender.ItemsSource = res;
            }
            else {
               // args.Hendle
                
            }
        }

        private void Autosbox_ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
        {

            if (args.Key == VirtualKey.Down || args.Key == VirtualKey.Up)
            {
                args.Handled = true;
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem != null)
            {
               // sender.ha
                sender.Text = string.Format("{0} *** ({1})", "a", "ddd");
                // functionSuggestBox.Text = "damir galas";
                // User selected an item from the suggestion list, take an action on it here.
            }
        }


        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                //sender.Text = sender.Text;
                var chosen = (args.ChosenSuggestion as string);
                sender.Text = _suggestions.TokenToCurrent + chosen.Substring(2, chosen.Length - 2);
                sender.IsSuggestionListOpen = false;
               // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }
    }
}
