using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace RPG_Noelf
{
    /// <summary>
    /// A página do menu.
    /// </summary>
    public sealed partial class Menu : Page
    {
        private const string username = "root";
        private const string password = "123";

        private const bool IsServerOn = false;

        public Menu()
        {
            this.InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size(860, 640));
        }

        private void PlayBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if(!IsServerOn)
            {
                if(UsernameBox.Text == username && PasswordBox.Text == password)
                {
                    OpenPlayerInterface();
                }
            }
        }

        private void QuitBtn_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private async void OpenPlayerInterface()
        {
            var viewId = 0;
            var newView = CoreApplication.CreateNewView();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(CharacterCreation));
                Window.Current.Content = frame;

                viewId = ApplicationView.GetForCurrentView().Id;
                
                Window.Current.Activate();
            });
            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }
    }
}
