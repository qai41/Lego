﻿using System;
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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// DebugWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DebugWindow : Window
    {
        private readonly ParkingSolver solver;
        private double spaceInterval;

        public DebugWindow()
        {
            InitializeComponent();

        }

        internal DebugWindow(ParkingSolver solver)
            : this()
        {
            this.solver = solver;

            int layerCount = (int)Math.Ceiling(360 / solver.OrientationTolerance / 2);

            layerScrollBar.Maximum = layerCount - 1;
            //layerScrollBar.LargeChange =Math.Ceiling( layerCount/10.0);

            //layerScrollBar.ViewportSize = layerCount;

            for (int i = 0; i < layerCount; i++)
            {
                Color c = Gqqnbig.Drawing.ColorConversion.HslToRgb((i * 120) % 360, 1, 0.5);
                Brush brush = new SolidColorBrush(c);
                GridLayer l = new GridLayer(new Pen(brush, 0.5), solver.Map.Size, solver.DistanceTolerance);



                l.Visibility = System.Windows.Visibility.Collapsed;
                l.Width = solver.Map.Size.Width;
                l.Height = solver.Map.Size.Height;
                l.VerticalAlignment = VerticalAlignment.Top;
                l.HorizontalAlignment = HorizontalAlignment.Left;
                grid.Children.Add(l);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                GridLayer gridLayer = (GridLayer)grid.Children[i];
                gridLayer.RenderTransform = new SkewTransform(-30, 0, 0, solver.Map.Size.Height);
            }
            //layerScrollBar.Track.Thumb.Height = 50;
            //layerScrollBar.Track.Thumb.Height = layerScrollBar.Track.ActualHeight / layerScrollBar.Maximum * 10;
            //if (layerScrollBar.Track.Thumb.Height < 10)
            //    layerScrollBar.Track.Thumb.Height = 10;

            //layerCanvas.Width = solver.Map.Size.Width;
            //layerCanvas.Height = solver.Map.Size.Height;

            //for (double i = 0; i < layerCanvas.Width; i+=solver.DistanceTolerance)
            //{
            //    Line l = new Line();
            //    l.Stroke = Brushes.Black;
            //    l.StrokeThickness = 0.1;
            //    l.X1 = i;
            //    l.Y1 = 0;
            //    l.X2 = i;
            //    l.Y2 = layerCanvas.Height;
            //    layerCanvas.Children.Add(l);
            //}

            //for (double j = 0; j < layerCanvas.Height; j += solver.DistanceTolerance)
            //{
            //    Line l = new Line();
            //    l.Stroke = Brushes.Black;
            //    l.StrokeThickness = 0.1;
            //    l.X1 = 0;
            //    l.Y1 = j;
            //    l.X2 = layerCanvas.Width;
            //    l.Y2 = j;
            //    layerCanvas.Children.Add(l);
            //}


        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged == false)
                return;


            spaceInterval = (grid.ActualHeight - solver.Map.Size.Height) / 2;
            ShowLayer((int)layerScrollBar.Value);
            //fro
            // grid.Children[0].Visibility

            //double spaceInterval = (grid.ActualHeight - ((FrameworkElement)grid.Children[0]).ActualHeight) / 2;
            //for (int i = 0; i < grid.Children.Count; i++)
            //{
            //    FrameworkElement gridLayer = (FrameworkElement)grid.Children[i];
            //    gridLayer.Margin = new Thickness(0, i * spaceInterval, 0, 0);
            //}
        }

        private void layerScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (UIElement child in grid.Children)
            {
                child.Visibility = System.Windows.Visibility.Collapsed;
            }


            ShowLayer((int)e.NewValue);

            //int start = n - 1;
            //if (start >= 0)
            //{
            //    grid.Children[start].Opacity = 70;
            //    grid.Children[start].Visibility = Visibility.Visible;
            //    ((FrameworkElement)grid.Children[start]).Margin = new Thickness(0, marginTop, 0, 0);
            //    marginTop += spaceInterval;
            //}

            //grid.Children[n].Opacity = 70;
            //grid.Children[n].Visibility = Visibility.Visible;
            //((FrameworkElement)grid.Children[n]).Margin = new Thickness(0, marginTop, 0, 0);
            //marginTop += spaceInterval;


            //int end = n + 1;
            //if (end < grid.Children.Count)
            //{
            //    grid.Children[end].Opacity = 70;
            //    grid.Children[end].Visibility = Visibility.Visible;
            //}
        }

        private void ShowLayer(int centerIndex)
        {
            //设置不透明度后性能会很慢！！

            if (centerIndex == 0)
            {
                //grid.Children[0].Opacity =0.7;
                grid.Children[0].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[0]).Margin = new Thickness(0, 0, 0, 0);

                //grid.Children[1].Opacity = 0.7;
                grid.Children[1].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[1]).Margin = new Thickness(0, spaceInterval, 0, 0);
            }
            else if (centerIndex == grid.Children.Count - 1)
            {
                //grid.Children[centerIndex - 1].Opacity = 0.7;
                grid.Children[centerIndex - 1].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[centerIndex - 1]).Margin = new Thickness(0, spaceInterval, 0, 0);

                grid.Children[centerIndex].Opacity = 0.7;
                grid.Children[centerIndex].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[0]).Margin = new Thickness(0, 2 * spaceInterval, 0, 0);
            }
            else
            {
                //grid.Children[centerIndex - 1].Opacity = 0.7;
                grid.Children[centerIndex - 1].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[centerIndex - 1]).Margin = new Thickness(0, 0, 0, 0);

                //grid.Children[centerIndex].Opacity = 0.7;
                grid.Children[centerIndex].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[centerIndex]).Margin = new Thickness(0, spaceInterval, 0, 0);

                //grid.Children[centerIndex + 1].Opacity = 0.7;
                grid.Children[centerIndex + 1].Visibility = Visibility.Visible;
                ((FrameworkElement)grid.Children[centerIndex + 1]).Margin = new Thickness(0, 2 * spaceInterval, 0, 0);
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (pauseButton.Content.Equals("暂停"))
            {
                solver.RequirePause();
                pauseButton.Content = "继续";

                ReadState();
            }
            else
            {
                solver.Resume();
                pauseButton.Content = "暂停";
            }

        }

        void ReadState()
        {
            foreach (GridLayer layer in grid.Children)
            {
                layer.InitDrawing();


                //layer.SetPoint(100, 300);
            }



            int x = (int)((int)(solver.EndPoint.X / solver.DistanceTolerance) * solver.DistanceTolerance);
            int y = (int)((int)(solver.EndPoint.Y / solver.DistanceTolerance) * solver.DistanceTolerance);
            int z = (int)(solver.EndOrientation / solver.OrientationTolerance / 2);

            ((GridLayer)grid.Children[z]).SetPoint(x, y, false);

            foreach (ParkingSolver.Node node in solver.Open_List)
            {
                x = (int)((int)(node.Position.X / solver.DistanceTolerance) * solver.DistanceTolerance);
                y = (int)((int)(node.Position.Y / solver.DistanceTolerance) * solver.DistanceTolerance);
                z = (int)(node.Position.Z / solver.OrientationTolerance / 2);

                GridLayer layer = (GridLayer)grid.Children[z];
                layer.SetPoint(x, y);
            }

            foreach (List<ParkingSolver.Node> sublist in solver.Close_List)
            {
                foreach (ParkingSolver.Node node in sublist)
                {
                    x = (int)((int)(node.Position.X / solver.DistanceTolerance) * solver.DistanceTolerance);
                    y = (int)((int)(node.Position.Y / solver.DistanceTolerance) * solver.DistanceTolerance);
                    z = (int)(node.Position.Z / solver.OrientationTolerance / 2);

                    GridLayer layer = (GridLayer)grid.Children[z];
                    layer.SetPoint(x, y,false);
                }
            }



            foreach (GridLayer layer in grid.Children)
            {
                layer.EndDrawing();
            }


        }
    }
}
