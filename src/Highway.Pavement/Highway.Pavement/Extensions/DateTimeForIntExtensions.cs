namespace System
{
    public static class DateTimeForIntExtensions
    {
        public static TimeSpan Days(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }
        
        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }
        
        public static TimeSpan Seconds(this int seconds)
        {
            return new TimeSpan(0,0,seconds);
        }

        public static TimeSpan Milliseconds(this int milliseconds)
        {
            return new TimeSpan(0,0,0,0,milliseconds);
        }
    }
}