using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Raytracing_WPF.Annotations;
using Raytracing_WPF.ViewModel;
using RaytracingWPF.Render;

namespace RaytracingWPF.Command
{
    public class RenderCommand : ICommand
    {
        private readonly bool _isExecutable;
        private readonly MainViewModel _mainViewModel;
        private RenderMain _renderMain;

        public RenderCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _isExecutable = true;

            byte[] pixels = new byte[400];

            SaveToBmp(10, 10, pixels);
        }

        public bool CanExecute(object parameter)
        {
            // should return false while render is active
            return _isExecutable;
        }

        public void Execute(object parameter)
        {
            Console.Clear();
            _mainViewModel.ProgressBarValue = 0;
            _renderMain = new RenderMain(this, _mainViewModel.TextBoxInput);
            _mainViewModel.ProgressBarValue = 100;
        }

        public event EventHandler CanExecuteChanged;

        public void SaveToBmp(int width, int height, [NotNull] byte[] pixelData)
        {
            if (pixelData == null) throw new ArgumentNullException(nameof(pixelData));
            //PixelFormat.Bgr32 ----> [blue 8bit] [green 8bit] [red 8bit] [alpha 8bit]
            _mainViewModel.BitmapSource = BitmapSource.Create(width, height, 300, 300, PixelFormats.Bgr32,
                null, pixelData, width*4);
        }

        public void UpdateProgressBar(int n)
        {
            if (Dispatcher.CurrentDispatcher.CheckAccess())
                _mainViewModel.ProgressBarValue = n;
            else
                Dispatcher.CurrentDispatcher.Invoke(new Action(() => _mainViewModel.ProgressBarValue = n), null);
        }
    }
}