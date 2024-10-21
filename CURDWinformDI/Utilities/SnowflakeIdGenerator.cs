using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURDWinformDI
{
    public class SnowflakeIdGenerator
    {
        private static readonly DateTime StartTime = new DateTime(2024, 10, 17, 0, 0, 0, DateTimeKind.Utc);
        private static readonly object SyncLock = new object();
        private static long lastTimestamp = -1L;
        private static long sequence = 0L;
        private const long NodeId = 1L;

        public static long GenerateId()
        {
            lock (SyncLock)
            {
                long timestamp = (long)(DateTime.UtcNow - StartTime).TotalMilliseconds;

                if (timestamp == lastTimestamp)
                {
                    sequence = (sequence + 1) & 4095;
                    if (sequence == 0)
                    {
                        while (timestamp <= lastTimestamp)
                        {
                            timestamp = (long)(DateTime.UtcNow - StartTime).TotalMilliseconds;
                        }
                    }
                }
                else
                {
                    sequence = 0L;
                }

                lastTimestamp = timestamp;

                return ((timestamp << 22) | (NodeId << 12) | sequence);
            }
        }
    }
}
