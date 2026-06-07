using System;
using System.Runtime.InteropServices;

namespace MewTour.Utility;

public static class SafeMemoryReader
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        [Out] out IntPtr lpBuffer,
        int nSize,
        out int lpNumberOfBytesRead);

    // Use -1 (GetCurrentProcess) to read from your own app's memory space
    private static readonly IntPtr CurrentProcessHandle = new IntPtr(-1);

    public static IntPtr ReadIntPtrSafe(IntPtr address)
    {
        if (address == IntPtr.Zero) 
            return IntPtr.Zero;

        // Try to read the pointer (4 bytes on 32-bit, 8 bytes on 64-bit)
        int size = IntPtr.Size; 
        if (ReadProcessMemory(CurrentProcessHandle, address, out IntPtr buffer, size, out _))
            return buffer;

        // If it fails (e.g., invalid address), return IntPtr.Zero instead of crashing
        return IntPtr.Zero;
    }
}