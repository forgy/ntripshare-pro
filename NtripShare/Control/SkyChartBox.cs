using DevComponents.DotNetBar;
using NtripShare.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NtripShare.MControl
{
    public partial class SkyChartBox : PanelEx
    {
       public SatelliteInfo[] stlArray = new SatelliteInfo[0];    //卫星信息数组

        public SkyChartBox()
        {
            base.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //取正方形
            Rectangle myRect = new Rectangle();
            myRect.Width = Math.Min(pe.ClipRectangle.Width, pe.ClipRectangle.Height);
            myRect.Height = myRect.Width;
            myRect.X = (pe.ClipRectangle.Width - myRect.Width) / 2;
            myRect.Y = (pe.ClipRectangle.Height - myRect.Height) / 2;
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //绘制圆圈
            int pad = 20;
            Pen myPen = new Pen(base.ForeColor, 1f);
            pe.Graphics.DrawEllipse(myPen, pad + myRect.X, pad + myRect.Y, myRect.Width - pad * 2, myRect.Height - pad * 2);
            pe.Graphics.DrawEllipse(myPen, pad + myRect.X + (myRect.Width - pad * 2) / 6, pad + myRect.Y + (myRect.Height - pad * 2) / 6, (myRect.Width - pad * 2) / 3 * 2, (myRect.Height - pad * 2) / 3 * 2);
            pe.Graphics.DrawEllipse(myPen, pad + myRect.X + (myRect.Width - pad * 2) / 6 * 2, pad + myRect.Y + (myRect.Height - pad * 2) / 6 * 2, (myRect.Width - pad * 2) / 3, (myRect.Height - pad * 2) / 3);
            //绘制线条
            pe.Graphics.DrawLine(myPen, myRect.X + pad, myRect.Y + myRect.Height / 2, myRect.Right - pad, myRect.Y + myRect.Height / 2);
            pe.Graphics.DrawLine(myPen, myRect.X + myRect.Width / 2, myRect.Y + pad, myRect.X + myRect.Width / 2, myRect.Bottom - pad);
            //绘制注记
            SolidBrush myBrush = new SolidBrush(base.ForeColor);
            pe.Graphics.DrawString("0°", base.Font, myBrush, myRect.X + myRect.Width / 2 + myPen.Width, myRect.Y + pad + myPen.Width);
            pe.Graphics.DrawString("30°", base.Font, myBrush, myRect.X + myRect.Width / 2 + myPen.Width, myRect.Y + pad + (myRect.Height - pad * 2) / 6 + myPen.Width);
            pe.Graphics.DrawString("60°", base.Font, myBrush, myRect.X + myRect.Width / 2 + myPen.Width, myRect.Y + pad + (myRect.Height - pad * 2) / 6 * 2 + myPen.Width);
            pe.Graphics.DrawString("N", base.Font, myBrush, myRect.X + myRect.Width / 2 - base.Font.SizeInPoints / 2, myRect.Y + pad - base.Font.Height);
            pe.Graphics.DrawString("S", base.Font, myBrush, myRect.X + myRect.Width / 2 - base.Font.SizeInPoints / 2, myRect.Bottom - pad);
            pe.Graphics.DrawString("W", base.Font, myBrush, myRect.Left + pad - base.Font.SizeInPoints, myRect.Y + myRect.Height / 2 - base.Font.Height / 2);
            pe.Graphics.DrawString("E", base.Font, myBrush, myRect.Right - pad, myRect.Y + myRect.Height / 2 - base.Font.Height / 2);
            //绘制卫星
            int stlSize = 10;
            for (int i = 0; i < stlArray.Length; i++)
            {
                Point stlPoint = new Point();
                stlPoint.X = (int)(Math.Sin(stlArray[i].Azimuth * Math.PI / 180) * stlArray[i].Elevation / 90d * (myRect.Width / 2 - pad) + myRect.X + myRect.Width / 2);
                stlPoint.Y = (int)(myRect.Y + myRect.Height / 2 - Math.Cos(stlArray[i].Azimuth * Math.PI / 180) * stlArray[i].Elevation / 90 * (myRect.Width / 2 - pad));
                pe.Graphics.FillEllipse(new SolidBrush(stlArray[i].Color), stlPoint.X - stlSize / 2, stlPoint.Y - stlSize / 2, stlSize, stlSize);
                pe.Graphics.DrawString(stlArray[i].Name, base.Font, new SolidBrush(stlArray[i].Color), stlPoint.X + stlSize / 2, stlPoint.Y - base.Font.Height / 2);
            }
            myPen.Dispose();
            myBrush.Dispose();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Refresh();
        }

        /// <summary>
        /// 获取和设置卫星信息
        /// </summary>
        public SatelliteInfo[] StlInfo
        {
            get { return this.stlArray; }
            set
            {
                this.stlArray = value;
                this.Refresh();
            }
        }
    }
}
