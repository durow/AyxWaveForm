﻿using AyxWaveForm.Format;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != true) return;
            try
            {
                var file = WavFile.Read(ofd.FileName);
                var type = file.GetType();
                var result = "";
                foreach (var prop in type.GetProperties())
                {
                    result += prop.Name + ":" + prop.GetValue(file) + "\n";
                }
                ResultText.Text = result;
                //WaveImage.Source = file.DrawChannel(0, 1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ResultText.Text = "";
            }
        }

        private void TestScroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //ScrollInfo.Text = TestScroll.Value + "\n";
        }

        private void TestSlider_SliderMoved(object sender, AyxWaveForm.Model.SliderMovedEventArgs e)
        {
            ScrollInfo.Text = 
                                      "StartPer: "+ e.StartPercent + "\n" +
                                      "Scale: " + e.Scale;
        }

        private void DockPanel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var p = e.GetPosition(this).X/this.ActualWidth;
            TestSlider.SetScale(e.Delta, p);
        }
    }
}
