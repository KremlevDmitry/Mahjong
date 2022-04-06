namespace Timer
{
    public struct Time
    {
        public static Time Zero => new Time(0, 0, 0, 0);


        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public int MiliSeconds { get; private set; }


        public Time(int hours, int minutes, int seconds, int miliSeconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            MiliSeconds = miliSeconds;
        }


        public void AddSeconds(int seconds)
        {
            Seconds += seconds;
            if (Seconds >= 60)
            {
                Seconds = 0;
                Minutes++;
                if (Minutes >= 60)
                {
                    Minutes = 0;
                    Hours++;
                    if (Hours >= 24)
                    {
                        Hours = 0;
                    }
                }
            }
        }
        public void AddSecond() => AddSeconds(1);

        public override string ToString()
        {
            string GetString(int number)
            {
                if (number < 10)
                {
                    return "0" + number.ToString();
                }
                return number.ToString();
            }

            return GetString(Hours) + ":" + GetString(Minutes) + ":" + GetString(Seconds) + "." + GetString(MiliSeconds);
        }
    }
}