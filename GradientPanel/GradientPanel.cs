using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GradientPanel
{
    public class GradientPanel: Panel
    {

        #region Round corners

        private int _borderRadius = 33;
        public int BorderRadius
        {
            get => _borderRadius;

            set 
            { 
                _borderRadius = value;
                this.Invalidate();    
            }
        }

        #endregion Round corners


        #region Gradient

        private Color _gradientTopColor = Color.FromArgb(61, 61, 61);
        public Color GradientTopColor
        {
            get => _gradientTopColor;
            set 
            { 
                _gradientTopColor = value;
                this.Invalidate();    
            }
        }
        
        private Color _gradientBottomColor = Color.FromArgb(21, 21, 21);
        public Color GradientBottomColor
        {
            get => _gradientBottomColor;
            set
            { 
                _gradientBottomColor = value;
                this.Invalidate();
            }
        }

        private float _gradientAngle = 60;
        public float GradientAngle
        {
            get => _gradientAngle;
            set
            {
                _gradientAngle = value;
                this.Invalidate();
            }
        }

        #endregion Gradien

        // Constructor
        public GradientPanel()
        {
            BackColor = Color.FromArgb(11, 11, 11) ;
        }


        #region Methods
        
        
        private GraphicsPath GetPath(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicPath = new GraphicsPath();
            graphicPath.StartFigure();
            graphicPath.AddArc(rectangle.Width - radius, rectangle.Height - radius, radius, radius, 0, 90);
            graphicPath.AddArc(rectangle.X, rectangle.Height - radius, radius, radius, 90, 90);
            graphicPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            graphicPath.AddArc(rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            graphicPath.CloseFigure();
            return graphicPath;
        }
        

        /*
        private static GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        */

        #endregion Methods


        #region Paint

        
        protected override void OnPaint(PaintEventArgs e)
        {           
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Gradient
            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, _gradientTopColor, _gradientBottomColor, _gradientAngle);            
            e.Graphics.FillRectangle(brush, this.ClientRectangle);

                       
            // Border radius
            RectangleF rectangleF = new RectangleF(0, 0, this.Width, this.Height);
            if (_borderRadius > 2)
            {
                using (GraphicsPath graphicpath = GetPath(rectangleF, _borderRadius))
                using (Pen pen = new Pen(this.Parent.BackColor, 2))
                {
                    this.Region = new Region(graphicpath);
                    e.Graphics.DrawPath(pen, graphicpath);                    
                }

            }
            else this.Region = new Region(rectangleF);
            

            base.OnPaint(e);
        }
        

        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, _borderRadius))
            {
                this.Region = new Region(path);
                using (Brush brush = new LinearGradientBrush(this.ClientRectangle, _gradientTopColor, _gradientBottomColor, _gradientAngle))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }
            base.OnPaint(e);
        }
        */

        #endregion Paint
    }
}
