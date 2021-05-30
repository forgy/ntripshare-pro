﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMapPositionFix;

namespace GMapUtil
{
    public static class PolygonUtils
    {
        public static GMapPolygon CreateCircle(PointLatLng center, double radius, string name = "")
        {
            double lng;
            double lat;
            CoordinateTransform.ConvertWgs84ToGcj02(center.Lng, center.Lat,out lng, out lat);
            List<PointLatLng> pList = new List<PointLatLng>();
            int segments = 500;
            double seg = 2 * Math.PI / segments;
            for (int i = 0; i < segments; ++i)
            {
                double theta = i * seg;
                double a = lng + Math.Sin(theta) * radius;
                double b = lat + Math.Cos(theta) * radius;
                double lng2;
                double lat2;
                CoordinateTransform.ConvertGcj02ToWgs84(a, b, out lng2, out lat2);
                pList.Add(new PointLatLng(lat2, lng2));
            }
            GMapPolygon circle = new GMapPolygon(pList, name);
            circle.Stroke = new Pen(Brushes.Red, 1);
            return circle;
        }


        public static GMapRoute CreateCircleRoute(PointLatLng center, double radius, string name = "")
        {
            double lng;
            double lat;
            CoordinateTransform.LonLat2Mercator(center.Lng, center.Lat, out lng, out lat);
            List<PointLatLng> pList = new List<PointLatLng>();
            int segments = 500;
            double seg = 2 * Math.PI / segments;
            for (int i = 0; i < segments; ++i)
            {
                double theta = i * seg;
                double a = lng + Math.Sin(theta) * radius;
                double b = lat + Math.Cos(theta) * radius;
                double lng2;
                double lat2;
                CoordinateTransform.Mercator2lonLat(a, b, out lng2, out lat2);
                pList.Add(new PointLatLng(lat2, lng2));
            }
            GMapRoute circle = new GMapRoute(pList, name);
            circle.Stroke = new Pen(Brushes.Red, 2);
            return circle;
        }

        public static GMapPolygon CreateSector(PointLatLng center, double radius, double start, double end, string name = "")
        {
            List<PointLatLng> pList = new List<PointLatLng>();
            pList.Add(center);
            int segments = 100000;
            double seg = 2 * Math.PI / segments;
            for (double theta = ToRadians(start); theta < ToRadians(end); theta += seg)
            {
                double a = center.Lat + Math.Sin(theta) * radius;
                double b = center.Lng + Math.Cos(theta) * radius;
                pList.Add(new PointLatLng(a, b));
            }
            GMapPolygon sector = new GMapPolygon(pList, name);
            sector.Stroke = new Pen(Brushes.Red, 1);
            return sector;
        }

        internal static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180d);
        }

        public static RectLatLng GetRegionMaxRect(GMapPolygon polygon)
        {
            double latMin = 90;
            double latMax = -90;
            double lngMin = 180;
            double lngMax = -180;
            foreach (var point in polygon.Points)
            {
                if (point.Lat < latMin)
                {
                    latMin = point.Lat;
                }
                if (point.Lat > latMax)
                {
                    latMax = point.Lat;
                }
                if (point.Lng < lngMin)
                {
                    lngMin = point.Lng;
                }
                if (point.Lng > lngMax)
                {
                    lngMax = point.Lng;
                }
            }

            return new RectLatLng(latMax, lngMin, lngMax - lngMin, latMax - latMin);
        }
    }
}
