from json import dumps
from uptime import uptime

TICK_RESOLUTION = (15, 625, 0)
def GetTickCount():
  return int(uptime() * 1000) % (1 << 32)

def GetTickCount64():
  return int(uptime() * 1000)

def ResolveTick(tick):
  resolution = 0
  for i in range(len(TICK_RESOLUTION)):
    resolution = resolution + TICK_RESOLUTION[i] / (10 ** (i * 3))
  if not resolution:
    resolution = 1

  return int((tick * resolution) / resolution) 

if __name__ == "__main__":
  print(dumps({'tick_count': GetTickCount(), 'tick_count_64': GetTickCount64()}))