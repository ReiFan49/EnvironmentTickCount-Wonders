#include <stdio.h>
#include <stdint.h>
#include <time.h>

#if defined(_WIN32) || defined(WIN32)
#include <sysinfoapi.h>
#else
int32_t GetTickCount() {
  struct timespec ts;
  clock_gettime(CLOCK_MONOTONIC, &ts);
  return (int32_t)(ts.tv_sec * 1000) + (int32_t)(ts.tv_nsec / 1000000);
}

int64_t GetTickCount64() {
  struct timespec ts;
  clock_gettime(CLOCK_MONOTONIC, &ts);
  return (int64_t)(ts.tv_sec * 1000LL) + (int64_t)(ts.tv_nsec / 1000000LL);
}
#endif

void outputJSON() {
  _printf_p(
    "{\"%1$s\": %3$d, \"%2$s\": %4$ld}\n",
    "tick_count",
    "tick_count_64",
    GetTickCount(),
    GetTickCount64()
  );
}

void writeThisAndNextTick() {
  int32_t tick, nextTick;
  tick = GetTickCount();
  do { nextTick = GetTickCount(); } while (tick == nextTick);
  printf("%d -> %d %d\n", CLOCKS_PER_SEC, tick, nextTick);
}

int main() {
  outputJSON();
}