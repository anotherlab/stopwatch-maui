using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace StopwatchMaui.ViewModels
{
    public class StopWatchViewModel : BaseViewModel
    {
        readonly Stopwatch stopwatch = new();

        TimeSpan elapsed;
        public TimeSpan Elapsed
        {
            get { return elapsed; }
            set
            {
                SetProperty(ref elapsed, value);
            }            
        }

        private void Start()
        {
            stopwatch.Restart();

            IsRunning = true;

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ElapsedTime = stopwatch.Elapsed.ToString(@"mm\:ss\.fff");
                });
                return isRunning;
            });
        }

        private void Stop()
        {
            stopwatch.Stop();
            IsRunning = false;
        }

        private bool isRunning;
        public bool IsRunning
        {
            get {  return isRunning; }
            set { SetProperty(ref isRunning, value); }
        }

        private string elapsedTime;
        public string ElapsedTime
        {
            get {  return elapsedTime; }
            set {  SetProperty(ref elapsedTime, value);}
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public StopWatchViewModel()
        {
            ElapsedTime = "00:00.000";
            IsRunning = false;
            StartCommand = new Command(() => Start());
            StopCommand = new Command(() => Stop());
        }
    }
}
