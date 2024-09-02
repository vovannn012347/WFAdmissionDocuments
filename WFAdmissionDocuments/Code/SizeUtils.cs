using System;
using System.Drawing;

namespace WFAdmissionDocuments.Code
{
    public static class SizeUtils
    {

        private static float? dpiX = null;
        public static float GetScreenDpi()
        {
            if (!dpiX.HasValue)
            {
                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    dpiX = g.DpiX;
                }
            }

            return dpiX.Value;
        }
        
        public static int MillimetersToPixels(float millimeters)
        {
            float dpi = GetScreenDpi();
            float inches = millimeters / 25.4f;
            return (int)(inches * dpi);
        }

        public static float PixelsToMilimiters(int pixels)
        {
            float dpi = GetScreenDpi();
            float inches = pixels / dpi;
            return inches * 25.4f;
        }

        private const float MmToPoints = 2.83465f;
        public static float GetMmToPoints(float milimiters) => MmToPoints * milimiters;
        public static float GetPointsToMM(float points) => points / MmToPoints;


        public static float PixelsToPoints(int pixels)
        {
            return (pixels / GetScreenDpi()) * 72;
        }

        public static int PointsToPixels(int points)
        {
            return (int)((points/72) * GetScreenDpi());
        }

        public static int Size(int points)
        {
            return (int)((points / 72) * GetScreenDpi());
        }
    }
}
