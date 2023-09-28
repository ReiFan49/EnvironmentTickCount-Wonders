require 'json'
require 'ffi'

module Tick
  extend FFI::Library

  ffi_lib "kernel32.dll"
  ffi_convention :stdcall

  attach_function :count, :GetTickCount, [ ], :int
  attach_function :count64, :GetTickCount, [ ], :int64_t

  class << self
    alias count32 count
    def count32_p
      Process.clock_gettime(Process::CLOCK_MONOTONIC, :millisecond) % (1 << 32)
    end
    def count64_p
      Process.clock_gettime(Process::CLOCK_MONOTONIC, :millisecond) % (1 << 64)
    end
  end
end

output = ->() { {tick_count: Tick.count32, tick_count_64: Tick.count64} }

if ARGV.any? do |arg| arg == '/test' || arg == '--test' end then
  Time.now.tap do |ctime|
    tick = Tick.count32
    begin
      next_tick = Tick.count32
    end until tick != next_tick
    puts "A: %.3fsec (%d -> %d)" % [(Time.now - ctime), tick, next_tick]
  end
  Time.now.tap do |ctime|
    tick = Tick.count32_p
    begin
      next_tick = Tick.count32_p
    end until tick != next_tick
    puts "B: %.3fsec (%d -> %d)" % [(Time.now - ctime), tick, next_tick]
  end
  exit
end

puts JSON.dump(output.call)
