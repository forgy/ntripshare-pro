// Copyright 2007 - Morten Nielsen
//
// This file is part of SharpGps.
// SharpGps is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpGps is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpGps; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGPS.NMEA
{
    /// <summary>
    /// Global Positioning System Fix Data
    /// </summary>
    public class GPGGA
    {
        /// <summary>
        /// Initializes the NMEA Global Positioning System Fix Data
        /// </summary>
        public GPGGA()
        {
            _position = new Coordinate();
        }


        public static string GenerateGPGGAcode(double lon, double lat)
        {
            double posnum = 0;
            double minutes = 0;

            DateTime UTCTime = DateTime.UtcNow;

            //$GPGGA,052158,4158.7333,N,09147.4277,W,2,08,3.1,260.4,M,-32.6,M,,*79

            string mycode = "GPGGA,";
            if (UTCTime.Hour < 10)
            {
                mycode = mycode + "0";
            }
            mycode = mycode + System.Convert.ToString(UTCTime.Hour);
            if (UTCTime.Minute < 10)
            {
                mycode = mycode + "0";
            }
            mycode = mycode + System.Convert.ToString(UTCTime.Minute);
            if (UTCTime.Second < 10)
            {
                mycode = mycode + "0";
            }
            mycode = mycode + System.Convert.ToString(UTCTime.Second);
            mycode = mycode + ",";

            posnum = (double)(Math.Abs(lat));
            minutes = posnum % 1;
            posnum = posnum - minutes;
            minutes = minutes * 60;
            posnum = (posnum * 100) + minutes;
            mycode = mycode + posnum.ToString("0000.00####");

            if (lat > 0)
            {
                mycode = mycode + ",N,";
            }
            else
            {
                mycode = mycode + ",S,";
            }

            posnum = (double)(Math.Abs(lon));
            minutes = posnum % 1;
            posnum = posnum - minutes;
            minutes = minutes * 60;
            posnum = (posnum * 100) + minutes;
            mycode = mycode + posnum.ToString("00000.00####");

            if (lon > 0)
            {
                mycode = mycode + ",E,";
            }
            else
            {
                mycode = mycode + ",W,";
            }


            mycode = mycode + "4,10,1,200,M,1,M,";

            mycode = mycode + ((DateTime.Now.Second % 6) + 3) + ",0";
            mycode = "$" + mycode + "*" + CalculateChecksum(mycode); //Add checksum data
            return mycode;
        }

        private static string CalculateChecksum(string sentence)
        {
            // Calculates the checksum for a sentence
            // Loop through all chars to get a checksum
            char Character = '\0';
            int Checksum = 0;
            foreach (char tempLoopVar_Character in sentence)
            {
                Character = tempLoopVar_Character;
                switch (Character)
                {
                    case '$':
                        break;
                    // Ignore the dollar sign
                    case '*':
                        // Stop processing before the asterisk
                        goto endOfForLoop;
                    default:
                        // Is this the first value for the checksum
                        if (Checksum == 0)
                        {
                            // Yes. Set the checksum to the value
                            Checksum = Convert.ToByte(Character);
                        }
                        else
                        {
                            // No. XOR the checksum with this character's value
                            Checksum = Checksum ^ Convert.ToByte(Character);
                        }
                        break;
                }
            }
        endOfForLoop:
            // Return the checksum formatted as a two-character hexadecimal
            return Checksum.ToString("X2");
        }


        /// <summary>
        /// Initializes the NMEA Global Positioning System Fix Data and parses an NMEA sentence
        /// </summary>
        /// <param name="NMEAsentence"></param>
        public GPGGA(string NMEAsentence)
        {
            try
            {
                if (NMEAsentence.IndexOf('*') > 0)
                    NMEAsentence = NMEAsentence.Substring(0, NMEAsentence.IndexOf('*'));
                //Split into an array of strings.
                string[] split = NMEAsentence.Split(new Char[] { ',' });
                if (split[1].Length >= 6)
                {
                    TimeSpan t = new TimeSpan(GPSHandler.intTryParse(split[1].Substring(0, 2)),
                        GPSHandler.intTryParse(split[1].Substring(2, 2)), GPSHandler.intTryParse(split[1].Substring(4, 2)));
                    DateTime nowutc = DateTime.UtcNow;
                    nowutc = nowutc.Add(-nowutc.TimeOfDay);
                    _timeOfFix = nowutc.Add(t);

                }

                _position = new Coordinate(GPSHandler.GPSToDecimalDegrees(split[4], split[5]),
                                           GPSHandler.GPSToDecimalDegrees(split[2], split[3]));
                if (split[6] == "1")
                    _fixQuality = FixQualityEnum.GPS;
                else if (split[6] == "2")
                    _fixQuality = FixQualityEnum.DGPS;
                else
                    _fixQuality = FixQualityEnum.Invalid;
                _noOfSats = Convert.ToByte(split[7]);
                GPSHandler.dblTryParse(split[8], out _dilution);
                GPSHandler.dblTryParse(split[9], out _altitude);
                _altitudeUnits = split[10][0];
                GPSHandler.dblTryParse(split[11], out _heightOfGeoid);
                GPSHandler.intTryParse(split[13], out _dGPSUpdate);
                _dGPSStationID = split[14];
            }
            catch { }
        }

        /// <summary>
        /// Enum for the GGA Fix Quality.
        /// </summary>
        public enum FixQualityEnum
        {
            /// <summary>
            /// Invalid fix
            /// </summary>
            Invalid = 0,
            /// <summary>
            /// GPS fix
            /// </summary>
            GPS = 1,
            /// <summary>
            /// DGPS fix
            /// </summary>
            DGPS = 2
        }

        #region Properties

        private DateTime _timeOfFix;
        private Coordinate _position;
        private FixQualityEnum _fixQuality;
        private byte _noOfSats;
        private double _altitude;
        private char _altitudeUnits;
        private double _dilution;
        private double _heightOfGeoid;
        private int _dGPSUpdate;
        private string _dGPSStationID;

        /// <summary>
        /// time of fix (hhmmss).
        /// </summary>
        public DateTime TimeOfFix
        {
            get { return _timeOfFix; }
            //set { _timeOfFix = value; }
        }

        /// <summary>
        /// Coordinate of recieved position
        /// </summary>
        public Coordinate Position
        {
            get { return _position; }
            //set { _position = value; }
        }

        /// <summary>
        /// Fix quality (0=invalid, 1=GPS fix, 2=DGPS fix)
        /// </summary>
        public FixQualityEnum FixQuality
        {
            get { return _fixQuality; }
            internal set { _fixQuality = value; }
        }

        /// <summary>
        /// number of satellites being tracked.
        /// </summary>
        public byte NoOfSats
        {
            get { return _noOfSats; }
            //set { _noOfSats = value; }
        }

        /// <summary>
        /// Altitude above sea level.
        /// </summary>
        public double Altitude
        {
            get { return _altitude; }
            //set { _altitude = value; }
        }

        /// <summary>
        /// Altitude Units - M (meters).
        /// </summary>
        public char AltitudeUnits
        {
            get { return _altitudeUnits; }
            //set { _altitudeUnits = value; }
        }

        /// <summary>
        /// Horizontal dilution of position (HDOP).
        /// </summary>
        public double Dilution
        {
            get { return _dilution; }
            //set { _dilution = value; }
        }

        /// <summary>
        /// Height of geoid (mean sea level) above WGS84 ellipsoid.
        /// </summary>
        public double HeightOfGeoid
        {
            get { return _heightOfGeoid; }
            //set { _heightOfGeoid = value; }
        }

        /// <summary>
        /// Time in seconds since last DGPS update.
        /// </summary>
        public int DGPSUpdate
        {
            get { return _dGPSUpdate; }
            //set { _dGPSUpdate = value; }
        }

        /// <summary>
        /// DGPS station ID number.
        /// </summary>
        public string DGPSStationID
        {
            get { return _dGPSStationID; }
            //set { _dGPSStationID = value; }
        }
        #endregion
    }
}