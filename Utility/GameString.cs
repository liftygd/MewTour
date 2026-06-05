using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MewTour.Utility;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe struct GameString
{
    [FieldOffset(0)]
    public fixed byte inlineData[16];
    
    [FieldOffset(16)]
    public long size;
    
    [FieldOffset(24)]
    public long capacity;
    
    public static GameString FromManaged(string str)
    {
        GameString gs = new GameString();
        int length = Math.Min(str.Length, 15);
        
        // Write inline data
        byte[] bytes = Encoding.ASCII.GetBytes(str);
        for (int i = 0; i < bytes.Length && i < 16; i++)
        {
            gs.inlineData[i] = bytes[i];
        }
        
        gs.size = length;
        gs.capacity = 15;  // SSO capacity
        
        return gs;
    }
}