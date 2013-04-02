using System;

namespace Domain
{
    //abstract the system clock so that you can unit test through time!
    public static class SystemTime
    {
        private static DateTime _now = DateTime.MinValue;

        public static void Clear()
        {
            _now = DateTime.MinValue;
        }

        public static void SetTime(DateTime now)
        {
            _now = now;
        }

        public static DateTime Now 
        {
            get
            {
                if (_now == DateTime.MinValue)
                    return DateTime.Now;

                return _now;
            }
        }
    }
}
