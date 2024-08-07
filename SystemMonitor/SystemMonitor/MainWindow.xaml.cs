using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SystemMonitor
{
    public partial class MainWindow : Window
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter memoryCounter;
        private PerformanceCounter diskCounter;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            InitializePerformanceCounters();
            InitializeTimer();
        }

        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdatePerformanceData();
        }

        private void UpdatePerformanceData()
        {
            float cpuUsage = cpuCounter.NextValue();
            float memoryUsage = memoryCounter.NextValue();
            float diskUsage = diskCounter.NextValue();

            CpuProgressBar.Value = cpuUsage;
            MemoryProgressBar.Value = memoryUsage;
            DiskProgressBar.Value = diskUsage;

            CpuUsageTextBlock.Text = $"{cpuUsage:F1}%";
            MemoryUsageTextBlock.Text = $"{memoryUsage:F1}%";
            DiskUsageTextBlock.Text = $"{diskUsage:F1}%";
        }
    }
}