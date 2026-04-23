using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Reminder
{
    public class StyledNumericUpDown : NumericUpDown
    {
        private bool _isFocused = false;
        private Color _normalBorderColor = Color.FromArgb(0xD0, 0xD5, 0xDD);
        private Color _focusedBorderColor = Color.FromArgb(0x00, 0xA8, 0x96);
        private int _borderRadius = 6;

        public StyledNumericUpDown()
        {
            this.GotFocus += OnGotFocus;
            this.LostFocus += OnLostFocus;
        }

        private void OnGotFocus(object sender, EventArgs e)
        {
            _isFocused = true;
            Invalidate();
        }

        private void OnLostFocus(object sender, EventArgs e)
        {
            _isFocused = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath path = CreateRoundedRectPath(bounds, _borderRadius))
            {
                Color borderColor = _isFocused ? _focusedBorderColor : _normalBorderColor;
                int borderWidth = _isFocused ? 2 : 1;

                using (Pen borderPen = new Pen(borderColor, borderWidth))
                {
                    g.DrawPath(borderPen, path);
                }
            }
        }

        private GraphicsPath CreateRoundedRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}