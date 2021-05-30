using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.Projections;

namespace GMapProvidersExt.TianDitu
{
    public abstract class TiandituProviderBase : GMapProvider
    {
        // Fields
        public static readonly int maxServer;
        private GMapProvider[] overlays;
        public static string UrlFormat;

        // Methods
        static TiandituProviderBase()
        {
            maxServer = 5;
            //UrlFormat = "http://t{0}.tianditu.com/{1}/wmts?SERVICE=WMTS&REQUEST=GetTile&VERSION=1.0.0&LAYER={2}&TILEMATRIXSET={3}&TILEMATRIX={4}&TILEROW={5}&TILECOL={6}&FORMAT=tiles";
            //UrlFormat = "http://t{0}.tianditu.com/DataServer?T={1}&x={2}&y={3}&l={4}";
            UrlFormat = "https://t{0}.tianditu.gov.cn/DataServer?T={1}&x={2}&y={3}&l={4}&tk=37ddd4a2946d1d090e1530823f0152a5";
        }

        public TiandituProviderBase()
        {
            base.MaxZoom = 0x12;
            base.MinZoom = 1;
            base.RefererUrl = "http://www.tianditu.com";
        }

        // Properties
        public override GMapProvider[] Overlays
        {
            get
            {
                if (this.overlays == null)
                {
                    this.overlays = new GMapProvider[] { this };
                }
                return this.overlays;
            }
        }

        public override PureProjection Projection
        {
            get
            {
                return MercatorProjection.Instance;
            }
        }
    }


}
