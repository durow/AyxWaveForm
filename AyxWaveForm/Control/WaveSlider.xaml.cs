﻿using AyxWaveForm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AyxWaveForm.Control
{
    /// <summary>
    /// WaveSlider.xaml 的交互逻辑
    /// </summary>
    public partial class WaveSlider : UserControl
    {
        #region Properties
        public double Scale { get; private set; }
        public double StartPercent { get; private set; }
        public double MinScale { get; set; }
        #endregion

        #region DependencyProperties
        public SliderStyle SliderStyle
        {
            get { return (SliderStyle)GetValue(SliderStyleProperty); }
            set { SetValue(SliderStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SliderStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderStyleProperty =
            DependencyProperty.Register("SliderStyle", typeof(SliderStyle), typeof(WaveSlider), new PropertyMetadata(null));



        public double SliderValue
        {
            get { return (double)GetValue(SliderValueProperty); }
            set { SetValue(SliderValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SliderValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderValueProperty =
            DependencyProperty.Register("SliderValue", typeof(double), typeof(WaveSlider), new PropertyMetadata(0.0));


        #endregion

        #region Event
        public event EventHandler<SliderMovedEventArgs> SliderMoved;
        #endregion

        public WaveSlider()
        {
            InitializeComponent();
            SliderStyle = new SliderStyle();
            Scale = 1;
            SliderValue = 1;
        }

        public void SetScale(double delta, double mousePer)
        {
            var oldScale = Scale;
            if (delta < 0)
            {
                if (Scale == 1) return;
                Scale += 0.05;
                if (Scale > 1) Scale = 1;
            }
            else if (delta > 0)
            {
                if (Scale == MinScale) return;
                Scale -= 0.05;
                if (Scale < MinScale) Scale = MinScale;
            }
            if (Scale == 1)
                SliderValue = 1;
            else
                SliderValue = (StartPercent + (oldScale - Scale) * mousePer) / (1 - Scale);
            Self_SizeChanged(null, null);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SliderStyle.ThumbBackground = Brushes.CornflowerBlue;
        }

        private void MySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StartPercent = (1 - Scale) * MySlider.Value;
            if(SliderMoved != null)
                SliderMoved(this, new SliderMovedEventArgs(StartPercent));
        }

        private void Self_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SliderStyle == null) return;
            SliderStyle.ThumbWidth = this.ActualWidth * Scale;
        }
    }
}