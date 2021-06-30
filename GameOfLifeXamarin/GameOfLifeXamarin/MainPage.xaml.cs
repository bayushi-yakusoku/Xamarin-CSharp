using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace GameOfLifeXamarin
{
    public partial class MainPage : ContentPage
    {
        SKPaint blackFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black
        };

        SKPaint whiteStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true
        };

        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1f / 60), refreshCanvas);
        }

        private bool refreshCanvas()
        {
            canvasView.InvalidateSurface();
            return true;
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.CornflowerBlue);

            int width = e.Info.Width;
            int height = e.Info.Height;

            // Move to the center of the screen, so that (0,0) coordinate will be here
            canvas.Translate(width / 2, height / 2);
            
            // Scale size of canvas
            canvas.Scale(width / 210f);

            // Clock background
            canvas.DrawCircle(0, 0, 100, blackFillPaint);

            // Get date time
            DateTime dateTime = DateTime.Now;

            // Draw hours
            canvas.Save();
            canvas.RotateDegrees(dateTime.Hour * 30 + dateTime.Minute * 0.5f);
            whiteStrokePaint.StrokeWidth = 10;
            canvas.DrawLine(0, 0, 0, -50, whiteStrokePaint);
            canvas.Restore();

            // Draw minutes
            canvas.Save();
            canvas.RotateDegrees(dateTime.Minute * 6);
            whiteStrokePaint.StrokeWidth = 5;
            canvas.DrawLine(0, 0, 0, -70, whiteStrokePaint);
            canvas.Restore();

            // Draw seconds
            canvas.Save();
            canvas.RotateDegrees(dateTime.Second * 6 + (dateTime.Millisecond * 0.006f));
            whiteStrokePaint.StrokeWidth = 2;
            canvas.DrawLine(0, 0, 0, -90, whiteStrokePaint);
            canvas.Restore();

            // Draw milliseconds
            canvas.Save();
            float angleMilli = dateTime.Millisecond * 0.36f;
            canvas.RotateDegrees(angleMilli);
            whiteStrokePaint.StrokeWidth = 1;
            canvas.DrawLine(0, 0, 0, -90, whiteStrokePaint);
            canvas.Restore();
        }
    }
}
