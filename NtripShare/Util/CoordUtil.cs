using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Util
{
   public  class CoordUtil
    {
        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        public static double[] Mercator2LonLat(double X, double Y)
        {
            double[] d = new double[2];
            double x = X / 20037508.34 * 180;
            double y = Y / 20037508.34 * 180;
            y = 180 / Math.PI * (2 * Math.Atan(Math.Exp(y * Math.PI / 180)) - Math.PI / 2);
            d[0] = x;
            d[1] = y;
            return d;
        }

        public static double[] LonLat2Mercator(double X, double Y)
        {
            double[] d = new double[2];
            double x = X * 20037508.34 / 180;
            double y = Math.Log(Math.Tan((90 + Y) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
            d[0] = x;
            d[1] = y;
            return d;
        }

        /// <summary>
        /// 判断经纬度是否在中国范围内
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static bool isInChina(double lat,double lon) {
            if (lat >= 3.86 && lat <= 53.55 && lon >= 73.66 && lon <= 135.05) {
                return true;
            }
            return false;
        }
    }
}
