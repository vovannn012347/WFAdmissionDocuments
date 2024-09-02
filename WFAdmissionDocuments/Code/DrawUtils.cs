using System;
using System.Drawing;
using System.Windows.Forms;

namespace WFAdmissionDocuments.Code
{
    public static class DrawUtils
    {
        private const int SelectionBorderWidth = 4;
        private const int SelectionBorderHalfWidth = SelectionBorderWidth / 2;

        private const int MovementBorderWidth = SelectionBorderWidth / 2;
        private const int MovementBorderHalfWidth = MovementBorderWidth / 2;

        public static void PaintBorder(this Control element, Graphics g, Color selectionColor)
        {
            using (Pen borderPen = new Pen(selectionColor, SelectionBorderWidth))
            {
                g.DrawRectangle(borderPen, element.ClientRectangle.X, element.ClientRectangle.Y,
                                          element.ClientRectangle.Width - SelectionBorderHalfWidth, element.ClientRectangle.Height - SelectionBorderHalfWidth);
            }
        }

        public static void PaintSquare(Point point, Size size, Graphics g, Color selectionColor)
        {
            using (Pen borderPen = new Pen(selectionColor, MovementBorderWidth))
            {
                g.DrawRectangle(borderPen, point.X, point.Y, size.Width, size.Height);
            }
        }

        public static Rectangle GetPaintRectangle(Point drawSquareCorner, Size drawSquareSize)
        {
            return new Rectangle(
                drawSquareCorner.X - MovementBorderHalfWidth,
                drawSquareCorner.Y - MovementBorderHalfWidth,
                drawSquareSize.Width + MovementBorderWidth,
                drawSquareSize.Height + MovementBorderWidth);
        }
    }
}
