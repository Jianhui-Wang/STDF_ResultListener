// Responsible: TEAM (johansen)
// Copyright:   Copyright 2015 Keysight Technologies.  All rights reserved. No 
//              part of this program may be photocopied, reproduced or translated 
//              to another program language without the prior written consent of 
//              Keysight Technologies.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

using System.Windows.Media;
using Keysight.Tap.ResultsViewer;

namespace TapPlugin.ResultViewer.STDF
{
    public class WaferPlot : CustomResultsViewerPlugin
    {
        private Grid Control = new Grid { };
        private TextBlock titleText = new TextBlock { HorizontalAlignment = System.Windows.HorizontalAlignment.Center, FontSize = 22, Text = "Wafer plot" };
        private Viewbox viewbox = new Viewbox { };
        private Canvas canvas = new Canvas { };

        public WaferPlot()
        {
            Control.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            Control.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Control.Background = System.Windows.Media.Brushes.White;

            Control.Children.Add(titleText);

            Control.Children.Add(viewbox);
            viewbox.SetValue(Grid.RowProperty, 1);

            viewbox.Child = canvas;
            viewbox.Stretch = System.Windows.Media.Stretch.Uniform;
        }

        private void GenerateWafer(double Radius)
        {
            canvas.Width = 2 * Radius;
            canvas.Height = 2 * Radius;

            var rad = new Ellipse();
            Canvas.SetLeft(rad, 0);
            Canvas.SetTop(rad, 0);

            rad.Width =2 * Radius;
            rad.Height = 2 * Radius;

            rad.Stroke = null;
            rad.Fill = System.Windows.Media.Brushes.Silver;

            canvas.Background = System.Windows.Media.Brushes.White;

            canvas.Children.Add(rad);
        }

        public override FrameworkElement UserControl
        {
            get
            {
                return Control;
            }
        }

        public override void Clear()
        {
            canvas.Children.Clear();
        }

        public override List<AxisGroup> GetAxisGroups()
        {
            return new List<AxisGroup>();
        }

        public override bool HasLimits()
        {
            return false;
        }

        public override void Invalidate()
        {
            Control.InvalidateArrange();
            Control.InvalidateVisual();
        }

        public override bool NeedAllAxes()
        {
            return true;
        }

        private void AddPart(double X, double Y, double Radius, double Width, double Height, Brush Color)
        {
            Rectangle r = new Rectangle();
            r.Width = Width * 0.95;
            r.Height = Height * 0.95;

            Canvas.SetLeft(r, Radius + X * Width - r.Width / 2);
            Canvas.SetTop(r, Radius + Y * Height - r.Height / 2);

            r.Stroke = null;
            r.Fill = Color;
            r.Opacity = 0.5;

            Canvas.SetZIndex(r, 1);

            canvas.Children.Add(r);
        }

        private bool FindColumn(string Name, List<AxisData> Columns, out AxisData Col)
        {
            Col = null;
            var RadC = Columns.FirstOrDefault(col => (col.Name == Name));
            if (RadC == null || (RadC.DoubleData.Count() <= 0)) return false;
            Col = RadC;
            return true;
        }

        private bool FindColumnValue(string Name, List<AxisData> Columns, out double Val)
        {
            Val = 0;
            var RadC = Columns.FirstOrDefault(col => (col.Name == Name) && (col.AxisKind.HasFlag(ColumnKind.TestPlanParam)));
            if (RadC == null || (RadC.DoubleData.Count() <= 0)) return false;
            Val = RadC.DoubleData[0];
            return true;
        }

        public override void RedrawPlot(string Title, List<AxisData> AxisData, List<LimitData> LimitData, List<PlotSeries> PlotSeries)
        {
            this.titleText.Text = Title;

            Clear();

            // Find radius
            double XOffset, YOffset, XMin, YMin, Radius, DieWidth, DieHeight;
            bool HasInfo = true;

            if (!FindColumnValue("Wafer Radius", AxisData, out Radius)) HasInfo = false;
            if (!FindColumnValue("Die Width", AxisData, out DieWidth)) HasInfo = false;
            if (!FindColumnValue("Die Height", AxisData, out DieHeight)) HasInfo = false;

            AxisData XCoord, YCoord, Verdict;

            if (!FindColumn("X-Coordinate", AxisData, out XCoord)) return;
            if (!FindColumn("Y-Coordinate", AxisData, out YCoord)) return;
            if (!FindColumn("Verdict", AxisData, out Verdict)) return;

            if (!HasInfo)
            {
                XMin = XCoord.DoubleData.Min();
                YMin = YCoord.DoubleData.Min();

                XOffset = (XCoord.DoubleData.Max() - XMin) / 2;
                YOffset = (YCoord.DoubleData.Max() - YMin) / 2;

                /*if (XOffset >= YOffset)
                    YMin -= (XOffset - YOffset) / 2;
                else
                    XMin -= (YOffset - XOffset) / 2;*/

                //XMin -= XOffset;
                //YMin -= YOffset;

                Radius = Math.Max(Math.Abs(XOffset + 1), Math.Abs(YOffset + 1)) * 1.5;

                DieWidth = 1;
                DieHeight = 1;
            }
            else
            {
                XMin = 0;
                YMin = 0;

                XOffset = Radius;
                YOffset = Radius;
            }

            GenerateWafer(Radius);

            foreach (var Series in PlotSeries)
                foreach (var Idx in Series.Indices)
                    AddPart((XCoord.DoubleData[Idx] - XMin - XOffset), (YCoord.DoubleData[Idx] - YMin - YOffset), Radius, DieWidth, DieHeight, Verdict.StringData[Idx] == "Fail" ? Brushes.Red : Brushes.Green);
        }
    }
}
