using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FunicularControl
{
    public partial class MainWindow : Window
    {
        private readonly FunicularSystem funicularSystem;

        public MainWindow()
        {
            InitializeComponent();
            funicularSystem = new FunicularSystem(
            new List<Rectangle> { F1Rect, F2Rect }, this);
        }

        private void NextState_Click(object sender, RoutedEventArgs e)
        {
            funicularSystem.NextState();
        }

        private void StartAuto_Click(object sender, RoutedEventArgs e)
        {
            funicularSystem.StartAuto();
        }

        private void StopAuto_Click(object sender, RoutedEventArgs e)
        {
            funicularSystem.StopAuto();
        }
    }
}