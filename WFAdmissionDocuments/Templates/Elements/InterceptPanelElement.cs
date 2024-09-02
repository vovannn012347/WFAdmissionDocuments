using System.Drawing;
using System.Windows.Forms;
using WFAdmissionDocuments.Code;

namespace WFAdmissionDocuments.Templates.TemplateElements
{
    public class TransparentPanelElement : Panel
    {
        bool DrawSquare = false;
        Point DrawSquareCorner;
        Size DrawSquareSize;

        public Point ResultPosition => DrawSquareCorner;
        public Size ResultSize => DrawSquareSize;

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            if (DrawSquare)
            {
                DrawUtils.PaintSquare(DrawSquareCorner, DrawSquareSize, pe.Graphics, Constants.SelectionColor);
            }
        }

        public void StopDraw()
        {
            DrawSquare = false;
        }

        
        public void SetDrawSquare(Point location, Size size)
        {
            if (DrawSquare)
            {
                Invalidate(DrawUtils.GetPaintRectangle(DrawSquareCorner, DrawSquareSize));
            }

            DrawSquare = true;

            DrawSquareCorner = location;
            DrawSquareSize = size;
            Invalidate(DrawUtils.GetPaintRectangle(DrawSquareCorner, DrawSquareSize));
        }

        public void SetDrawSquare(Point location1, Point location2)
        {
            if (DrawSquare)
            {
                Invalidate(DrawUtils.GetPaintRectangle(DrawSquareCorner, DrawSquareSize));
            }

            DrawSquare = true;

            int x = 0, y = 0;
            if (location2.X < location1.X) x = 1;
            if (location2.Y < location1.Y) y = 1;

            DrawSquareCorner = location1;
            DrawSquareSize = new Size(x, y);
            Invalidate(DrawUtils.GetPaintRectangle(DrawSquareCorner, DrawSquareSize));
        }
    }
}
