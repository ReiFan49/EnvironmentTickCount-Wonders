using System;

namespace TickCount {
    class ProgramTest {
        static void untilNextTick() {
            int startTick, nextTick;
            startTick = Environment.TickCount;
            for(nextTick = startTick; startTick == nextTick; nextTick = Environment.TickCount);
            Console.WriteLine(String.Format("NT: {0,10} -> {1,10}", startTick, nextTick));
        }

        const int OneRoundValue = 1000;
        const int PerfectRoundValue = 10000;
        static void untilOneRound() {
            int startTick, lastTick, nextTick, nextCount;
            for(
              startTick = Environment.TickCount,
              lastTick = startTick,
              nextTick = startTick;
              (nextTick % OneRoundValue != 0) && (nextTick < startTick + PerfectRoundValue);
              lastTick = nextTick,
              nextTick = Environment.TickCount
            );
            if(nextTick % OneRoundValue != 0)
              throw new Exception(String.Format("1R: {0,10} -> {1,10} (Failed Rounding)", startTick, nextTick));
            for(
              startTick = nextTick,
              lastTick = nextTick,
              nextCount = 0;
              nextTick < startTick + OneRoundValue;
              lastTick = nextTick,
              nextTick = Environment.TickCount,
              nextCount = nextCount + (lastTick == nextTick ? 0 : 1)
            );
            Console.WriteLine(String.Format("1R: {0,10} -> {1,10} ({2}x)", startTick, nextTick, nextCount));
        }

        public static void PerformTest() {
            untilNextTick();
            untilOneRound();
        }
    }
}
