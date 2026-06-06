using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MewTour.Utility;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct GameString
{
    [FieldOffset(0)]
    public nint heapPtr;
    
    [FieldOffset(0)]
    public fixed byte inlineData[16];
    
    [FieldOffset(16)]
    public long size;
    
    [FieldOffset(24)]
    public long capacity;
    
    public static GameString FromManaged(string str)
    {
        GameString gs = default;
        byte[] bytes = Encoding.ASCII.GetBytes(str);
        gs.size = str.Length;
        
        if (str.Length <= 15)
        {
            for (int i = 0; i < bytes.Length; i++)
                gs.inlineData[i] = bytes[i];
            gs.capacity = 15;
        }
        else
        {
            gs.heapPtr = Marshal.AllocHGlobal(bytes.Length + 1);
            Marshal.Copy(bytes, 0, gs.heapPtr, bytes.Length);
            Marshal.WriteByte(gs.heapPtr, bytes.Length, 0);
            gs.capacity = bytes.Length;
        }
        
        return gs;
    }
    
    public static void Free(ref GameString gs)
    {
        if (gs.capacity <= 15)
            return;
        
        Marshal.FreeHGlobal(gs.heapPtr);
    }
}