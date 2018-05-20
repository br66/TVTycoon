using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vision
{
    public class WorldTime
    {
        private int TICKS_PER_DAY = 200;

        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Quarter { get; private set; }
        public int Year { get; private set; }

        private float ticks;
        private int scale = 45;

        public WorldTime(int year, int quarter, int month, int day)
        {
            Day = day;
            Month = month;
            Quarter = quarter;
            Year = year;
        }

        public WorldTime()
        {
            Day = 1;
            Month = 1;
            Quarter = 1;
            Year = 1;
        }

        ~WorldTime()
        {
            Day = Month = Quarter = Year = 0;
        }

        public void Update()
        {
            // New tick
            ticks += Time.deltaTime * scale;

            if (ticks < TICKS_PER_DAY) return;

            // New day
            ticks = 0;
            Day++;

            if (Day < 30) return;

            //New Month
            Day = 1;
            Month++;

            #region The Quarter stuff
            if (Month > 0 && Month < 4) Quarter = 1;
            if (Month > 4 && Month < 7) Quarter = 2;
            if (Month > 7 && Month < 10) Quarter = 3;
            if (Month > 10 && Month < 13) Quarter = 4;
            #endregion

            if (Month < 12) return;

            // New Year
            Month = 1;
            Year++;
        }

        #region when you shift the bits, you're compressing the number?
        UInt32 compressedDate;

        //compressedDate = (127 << (int)CompressedDateBits.YEAR_START_POS) | (12 << (int)CompressedDateBits.QUARTER_START_POS) | (31 << (int)CompressedDateBits.DAY_START_POS);

        //int beginning = ((int)compressedDate >> (int)CompressedDateBits.YEAR_START_POS);
        //int end = ((1 << (int)CompressedDateBits.YEAR_LENGTH) - 1);

        //int year = ((int)compressedDate >> (int)CompressedDateBits.YEAR_START_POS) & ((1 << (int)CompressedDateBits.YEAR_LENGTH) - 1);

        [Flags]
        enum CompressedDateBits
        {
            DAY_LENGTH = 5,
            QUARTER_LENGTH = 4,
            YEAR_LENGTH = 7, // 2^7 = 128

            DAY_START_POS = 0,
            QUARTER_START_POS = DAY_START_POS + DAY_LENGTH, // 5
            YEAR_START_POS = QUARTER_START_POS + QUARTER_LENGTH // 9
        };

        #endregion
    }
}