using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Raytracing_WPF.Annotations;
using RaytracingWPF.Command;

namespace Raytracing_WPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BitmapSource _bitmapSource;
        private int _progressBarValue;
        private string _textBoxInput;

        public MainViewModel()
        {
            ProgressBarValue = 0;
            RenderCommand = new RenderCommand(this);
            TextBoxInput = "512, 512\nsphere, 150, 200, -100, 50, 255, 0, 0\nsphere, 120, 200, 0, 0, 0, 255, 0\nsphere, 150, 200, 100, 50, 0, 0, 255";
        }

        public ICommand RenderCommand { get; set; }

        public int ProgressBarValue
        {
            get => _progressBarValue;
            set
            {
                if (value == _progressBarValue) return;
                _progressBarValue = value;
                OnPropertyChanged();
            }
        }

        public string TextBoxInput
        {
            get => _textBoxInput;
            set
            {
                if (value == _textBoxInput) return;
                _textBoxInput = value;
                OnPropertyChanged();
            }
        }

        public BitmapSource BitmapSource
        {
            get => _bitmapSource;
            set
            {
                if (Equals(value, _bitmapSource)) return;
                _bitmapSource = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}