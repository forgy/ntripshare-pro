using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Model
{
   public class SatelliteInfo
    {
        /// 名称
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 仰角
        /// </summary>
        public double R = 0d;
        /// <summary>
        /// 仰角
        /// </summary>
        public double Elevation = 0d;
        /// <summary>
        /// 方位角
        /// </summary>
        public double Azimuth = 0d;
        /// <summary>
        /// 符号颜色
        /// </summary>
        public Color Color = Color.Red;

        public double X;
        public double Y;
        public double Z;
    }
}
