using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using static RakNetDLL;


/// <summary>
/// We strongly do not recommend using methods from this class! MAY CAUSE THE APP TO CRASH
/// </summary>
public unsafe class BitStream_Native
{
    [DllImport(DLL_NAME)]
    public static extern IntPtr BitStream_Create1();

    [DllImport(DLL_NAME)]
    public static extern IntPtr BitStream_Create2(IntPtr packet_ptr);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_ReadPacket(IntPtr bitstream_pointer, IntPtr packet_ptr);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_Close(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_IgnoreBytes(IntPtr bitstream_pointer, int numberOfBytes);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_ResetWritePointer(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_SetWriteOffset(IntPtr bitstream_pointer, uint offset);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_ResetReadPointer(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_SetReadOffset(IntPtr bitstream_pointer, uint offset);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_Reset(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern IntPtr BitStream_GetData(IntPtr bitstream_pointer, ref int data_size);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_SetData(IntPtr bitstream_pointer, byte[] data, int data_size);

    /* BYTE */
    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteByte(IntPtr bitstream_pointer, byte value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteByteDelta(IntPtr bitstream_pointer, byte value, byte last_value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteByteCompressed(IntPtr bitstream_pointer, byte value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteByteCompressedDelta(IntPtr bitstream_pointer, byte value, byte last_value);

    [DllImport(DLL_NAME)]
    public static extern byte BitStream_ReadByte(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern byte BitStream_ReadByteDelta(IntPtr bitstream_pointer, byte last_value);

    [DllImport(DLL_NAME)]
    public static extern byte BitStream_ReadByteCompressed(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern byte BitStream_ReadByteCompressedDelta(IntPtr bitstream_pointer, byte last_value);

    /* SHORT */
    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteShort(IntPtr bitstream_pointer, short value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteShortDelta(IntPtr bitstream_pointer, short value, short last_value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteShortCompressed(IntPtr bitstream_pointer, short value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteShortCompressedDelta(IntPtr bitstream_pointer, short value, short last_value);

    [DllImport(DLL_NAME)]
    public static extern short BitStream_ReadShort(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern short BitStream_ReadShortDelta(IntPtr bitstream_pointer, short last_value);

    [DllImport(DLL_NAME)]
    public static extern short BitStream_ReadShortCompressed(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern short BitStream_ReadShortCompressedDelta(IntPtr bitstream_pointer, short last_value);

    /* INT */
    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteInt(IntPtr bitstream_pointer, int value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteIntDelta(IntPtr bitstream_pointer, int value, int last_value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteIntCompressed(IntPtr bitstream_pointer, int value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteIntCompressedDelta(IntPtr bitstream_pointer, int value, int last_value);

    [DllImport(DLL_NAME)]
    public static extern int BitStream_ReadInt(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern int BitStream_ReadIntDelta(IntPtr bitstream_pointer, int last_value);

    [DllImport(DLL_NAME)]
    public static extern int BitStream_ReadIntCompressed(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern int BitStream_ReadIntCompressedDelta(IntPtr bitstream_pointer, int last_value);

    /* FLOAT */
    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteFloat(IntPtr bitstream_pointer, float value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteFloatDelta(IntPtr bitstream_pointer, float value, float last_value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteFloatCompressed(IntPtr bitstream_pointer, float value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteFloatCompressedDelta(IntPtr bitstream_pointer, float value, float last_value);

    [DllImport(DLL_NAME)]
    public static extern float BitStream_ReadFloat(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern float BitStream_ReadFloatDelta(IntPtr bitstream_pointer, float last_value);

    [DllImport(DLL_NAME)]
    public static extern float BitStream_ReadFloatCompressed(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern float BitStream_ReadFloatCompressedDelta(IntPtr bitstream_pointer, float last_value);

    /* FLOAT16 */
    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteFloat16(IntPtr bitstream_pointer, float value, float min, float max);

    [DllImport(DLL_NAME)]
    public static extern float BitStream_ReadFloat16(IntPtr bitstream_pointer, float min, float max);

    /* LONG */
    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteLong(IntPtr bitstream_pointer, long value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteLongDelta(IntPtr bitstream_pointer, long value, long last_value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteLongCompressed(IntPtr bitstream_pointer, long value);

    [DllImport(DLL_NAME)]
    public static extern void BitStream_WriteLongCompressedDelta(IntPtr bitstream_pointer, long value, long last_value);

    [DllImport(DLL_NAME)]
    public static extern long BitStream_ReadLong(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern long BitStream_ReadLongDelta(IntPtr bitstream_pointer, long last_value);

    [DllImport(DLL_NAME)]
    public static extern long BitStream_ReadLongCompressed(IntPtr bitstream_pointer);

    [DllImport(DLL_NAME)]
    public static extern long BitStream_ReadLongCompressedDelta(IntPtr bitstream_pointer, long last_value);
}

/// <summary>
/// Bitstream - for reading data native network packets, it converts data arrays into data types that are convenient for you.
/// </summary>
public class BitStream : IDisposable
{
    public IntPtr pointer { get; private set; } = IntPtr.Zero;

    public BitStream()
    {
        pointer = BitStream_Native.BitStream_Create1();
        Reset();
    }

    internal BitStream(IntPtr packet_ptr)
    {
        pointer = BitStream_Native.BitStream_Create2(packet_ptr);
    }

    internal void ReadPacket(IntPtr packet_ptr)
    {
        if (pointer == IntPtr.Zero || packet_ptr == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_ReadPacket(pointer, packet_ptr);
    }

    #region Disposing
    private bool disposed = false;
    protected void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Close();
            }
            disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    ~BitStream()
    {
        Dispose(false);
    }
    #endregion

    public void Close()
    {
        BitStream_Native.BitStream_Close(pointer);
    }

    public void ResetWritePointer()
    {
        BitStream_Native.BitStream_ResetWritePointer(pointer);
    }

    public void SetWritePointer(uint offset)
    {
        BitStream_Native.BitStream_SetWriteOffset(pointer, offset);
    }

    public void ResetReadPointer()
    {
        BitStream_Native.BitStream_ResetReadPointer(pointer);
    }

    public void SetReadPointer(uint offset)
    {
        BitStream_Native.BitStream_SetReadOffset(pointer, offset);
    }

    public void IgnoreBytes(int numberOfBytes)
    {
        BitStream_Native.BitStream_IgnoreBytes(pointer, numberOfBytes);
    }

    public void Reset()
    {
        BitStream_Native.BitStream_Reset(pointer);
    }

    private byte[] GetData_Buffer = new byte[1];
    public byte[] GetData()
    {
        int data_size = 0;
        IntPtr data_ptr = BitStream_Native.BitStream_GetData(pointer, ref data_size);
        Array.Resize(ref GetData_Buffer, data_size);
        Marshal.Copy(data_ptr, GetData_Buffer, 0, data_size);
        return GetData_Buffer;
    }

    public void SetData(byte[] data)
    {
        BitStream_Native.BitStream_SetData(pointer, data, data.Length);
    }

    public void Write(byte value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteByte(pointer, value);
    }

    public void WriteDelta(byte value, byte last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteByteDelta(pointer, value, last_value);
    }
    public void WriteCompressed(byte value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteByteCompressed(pointer, value);
    }
    public void WriteCompressedDelta(byte value, byte last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteByteCompressedDelta(pointer, value, last_value);
    }

    public void Write(sbyte value)
    {
        Write((byte)value);
    }
    public void WriteDelta(sbyte value, sbyte last_value)
    {
        WriteDelta((byte)value, (byte)last_value);
    }
    public void WriteCompressed(sbyte value)
    {
        WriteCompressed((byte)value);
    }
    public void WriteCompressedDelta(sbyte value, sbyte last_value)
    {
        WriteCompressedDelta((byte)value, (byte)last_value);
    }
    public void Write(bool value)
    {
        Write(value ? (byte)1 : (byte)0);
    }

    public void Write(short value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteShort(pointer, value);
    }
    public void WriteDelta(short value, short last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteShortDelta(pointer, value, last_value);
    }
    public void WriteCompressed(short value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteShortCompressed(pointer, value);
    }
    public void WriteCompressedDelta(short value, short last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteShortCompressedDelta(pointer, value, last_value);
    }

    public void Write(ushort value)
    {
        Write((short)value);
    }
    public void WriteDelta(ushort value, ushort last_value)
    {
        WriteDelta((short)value, (short)last_value);
    }
    public void WriteCompressed(ushort value)
    {
        WriteCompressed((short)value);
    }
    public void WriteCompressedDelta(ushort value, ushort last_value)
    {
        WriteCompressedDelta((short)value, (short)last_value);
    }

    public void Write(int value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteInt(pointer, value);
    }
    public void WriteDelta(int value, int last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteIntDelta(pointer, value, last_value);
    }
    public void WriteCompressed(int value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteIntCompressed(pointer, value);
    }
    public void WriteCompressedDelta(int value, int last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteIntCompressedDelta(pointer, value, last_value);
    }

    public void Write(uint value)
    {
        Write((int)value);
    }
    public void WriteDelta(uint value, uint last_value)
    {
        WriteDelta((int)value, (int)last_value);
    }
    public void WriteCompressed(uint value)
    {
        WriteCompressed((int)value);
    }
    public void WriteCompressedDelta(uint value, uint last_value)
    {
        WriteCompressedDelta((int)value, (int)last_value);
    }

    public void Write(float value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteFloat(pointer, value);
    }
    public void WriteDelta(float value, float last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteFloatDelta(pointer, value, last_value);
    }
    public void WriteCompressed(float value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteFloatCompressed(pointer, value);
    }
    public void WriteCompressedDelta(float value, float last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteFloatCompressedDelta(pointer, value, last_value);
    }

    public void Write(float value, float min, float max)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteFloat16(pointer, value, min, max);
    }

    public void Write(long value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteLong(pointer, value);
    }
    public void WriteDelta(long value, long last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteLongDelta(pointer, value, last_value);
    }
    public void WriteCompressed(long value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteLongCompressed(pointer, value);
    }
    public void WriteCompressedDelta(long value, long last_value)
    {
        if (pointer == IntPtr.Zero)
            return;

        BitStream_Native.BitStream_WriteLongCompressedDelta(pointer, value, last_value);
    }

    public void Write(ulong value)
    {
        Write((long)value);
    }
    public void WriteDelta(ulong value, ulong last_value)
    {
        WriteDelta((long)value, (long)last_value);
    }
    public void WriteCompressed(ulong value)
    {
        WriteCompressed((long)value);
    }
    public void WriteCompressedDelta(ulong value, ulong last_value)
    {
        WriteCompressedDelta((long)value, (long)last_value);
    }

    public void Write(string value)
    {
        if (pointer == IntPtr.Zero)
            return;

        if (string.IsNullOrEmpty(value) || value.Length <= 0)
        {
            Write((ushort)0);
            return;
        }

        byte[] bytes = Encoding.UTF8.GetBytes(value);
        Write((ushort)bytes.Length);
        for (int i = 0; i < bytes.Length; i++)
        {
            Write(bytes[i]);
        }
    }

    public void Write(Vector2 value)
    {
        Write(value.x);
        Write(value.y);
    }

    public void Write(Vector3 value, bool compressed = false)
    {
        if (!compressed)
        {
            Write(value.x);
            Write(value.y);
            Write(value.z);
        }
        else
        {
            float m = Mathf.Sqrt((value.x * value.x) + (value.y * value.y) + (value.z * value.z));
            Write(m);
            WriteCompressed(value.x / m);
            WriteCompressed(value.y / m);
            WriteCompressed(value.z / m);
        }
    }

    public void Write(Vector4 value)
    {
        Write(value.x);
        Write(value.y);
        Write(value.z);
        Write(value.w);
    }

    public void Write(Quaternion value, bool compressed = false)
    {
        if (!compressed)
        {
            Write(value.eulerAngles);
        }
        else
        {
            Write(value.x, -1f, 1f);
            Write(value.y, -1f, 1f);
            Write(value.z, -1f, 1f);
            Write(value.w, -1f, 1f);
        }
    }

    public byte ReadByte()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadByte(pointer);
    }
    public byte ReadByteDelta(byte last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadByteDelta(pointer, last_value);
    }
    public byte ReadByteCompressed()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadByteCompressed(pointer);
    }
    public byte ReadByteCompressedDelta(byte last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadByteCompressedDelta(pointer, last_value);
    }

    public sbyte ReadSByte()
    {
        return (sbyte)ReadByte();
    }
    public sbyte ReadSByteDelta(sbyte last_value)
    {
        return (sbyte)ReadByteDelta((byte)last_value);
    }
    public sbyte ReadSByteCompressed()
    {
        return (sbyte)ReadByteCompressed();
    }
    public sbyte ReadSByteCompressedDelta(sbyte last_value)
    {
        return (sbyte)ReadByteCompressedDelta((byte)last_value);
    }
    public bool ReadBool()
    {
        return ReadByte() >= 1 ? true : false;
    }

    public short ReadShort()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadShort(pointer);
    }
    public short ReadShortDelta(short last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadShortDelta(pointer, last_value);
    }
    public short ReadShortCompressed()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadShortCompressed(pointer);
    }
    public short ReadShortCompressedDelta(short last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadShortCompressedDelta(pointer, last_value);
    }

    public ushort ReadUShort()
    {
        return (ushort)ReadShort();
    }
    public ushort ReadUShortDelta(ushort last_value)
    {
        return (ushort)ReadShortDelta((short)last_value);
    }
    public ushort ReadUShortCompressed()
    {
        return (ushort)ReadShortCompressed();
    }
    public ushort ReadUShortCompressedDelta(ushort last_value)
    {
        return (ushort)ReadShortCompressedDelta((short)last_value);
    }

    public int ReadInt()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadInt(pointer);
    }
    public int ReadIntDelta(int last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadIntDelta(pointer, last_value);
    }
    public int ReadIntCompressed()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadIntCompressed(pointer);
    }
    public int ReadIntCompressedDelta(int last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadIntCompressedDelta(pointer, last_value);
    }

    public uint ReadUInt()
    {
        return (uint)ReadInt();
    }
    public uint ReadUIntDelta(uint last_value)
    {
        return (uint)ReadIntDelta((int)last_value);
    }
    public uint ReadUIntCompressed()
    {
        return (uint)ReadIntCompressed();
    }
    public uint ReadUIntCompressedDelta(uint last_value)
    {
        return (uint)ReadIntCompressedDelta((int)last_value);
    }

    public float ReadFloat()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadFloat(pointer);
    }
    public float ReadFloatDelta(float last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadFloatDelta(pointer, last_value);
    }
    public float ReadFloatCompressed()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadFloatCompressed(pointer);
    }
    public float ReadFloatCompressedDelta(float last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadFloatCompressedDelta(pointer, last_value);
    }

    public float ReadFloat16(float min, float max)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadFloat16(pointer, min, max);
    }

    public long ReadLong()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadLong(pointer);
    }
    public long ReadLongDelta(long last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadLongDelta(pointer, last_value);
    }
    public long ReadLongCompressed()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadLongCompressed(pointer);
    }
    public long ReadLongCompressedDelta(long last_value)
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return BitStream_Native.BitStream_ReadLongCompressedDelta(pointer, last_value);
    }

    public ulong ReadULong()
    {
        return (ulong)ReadLong();
    }
    public ulong ReadULongDelta(ulong last_value)
    {
        return (ulong)ReadLongDelta((long)last_value);
    }
    public ulong ReadULongCompressed()
    {
        return (ulong)ReadLongCompressed();
    }
    public ulong ReadULongCompressedDelta(ulong last_value)
    {
        return (ulong)ReadLongCompressedDelta((long)last_value);
    }

    byte[] buffered_StringBytes = new byte[0];

    public string ReadString()
    {
        if (pointer == IntPtr.Zero)
            return string.Empty;

        ushort count = ReadUShort();

        if (count <= 0)
        {
            return string.Empty;
        }

        buffered_StringBytes = new byte[count];

        for (int i = 0; i < buffered_StringBytes.Length; i++)
        {
            buffered_StringBytes[i] = ReadByte();
        }

        return Encoding.UTF8.GetString(buffered_StringBytes);
    }

    public Vector2 ReadVector2()
    {
        float x = ReadFloat();
        float y = ReadFloat();
        return new Vector2(x, y);
    }

    public Vector3 ReadVector3(bool compressed = false)
    {
        if (!compressed)
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();

            return new Vector3(x, y, z);
        }
        else
        {
            float m = ReadFloat();
            float x = ReadFloatCompressed() * m;
            float y = ReadFloatCompressed() * m;
            float z = ReadFloatCompressed() * m;
            return new Vector3(x, y, z);
        }
    }

    public Vector4 ReadVector4()
    {
        float x = ReadFloat();
        float y = ReadFloat();
        float z = ReadFloat();
        float w = ReadFloat();
        return new Vector4(x, y, z, w);
    }

    public Quaternion ReadQuaternion(bool compressed = false)
    {
        if (!compressed)
        {
            return Quaternion.Euler(ReadVector3());
        }
        else
        {
            return new Quaternion(ReadFloat16(-1f, 1f), ReadFloat16(-1f, 1f), ReadFloat16(-1f, 1f), ReadFloat16(-1f, 1f));
        }
    }


    /* ARRAYS */

    //BYTE[]
    public void WriteArray(byte[] array)
    {
        Write((short)array.Length);
        for(int i = 0;i < array.Length; i++)
        {
            Write(array[i]);
        }
    }

    public void WriteArray(sbyte[] array) => WriteArray(array);

    public byte[] ReadBytes()
    {
        short count = ReadShort();
        byte[] array = new byte[count];

        for (int i = 0; i < count; i++)
        {
            array[i] = ReadByte();
        }

        return array;
    }

    public sbyte[] ReadSBytes()
    {
        short count = ReadShort();
        sbyte[] array = new sbyte[count];

        for (int i = 0; i < count; i++)
        {
            array[i] = ReadSByte();
        }

        return array;
    }

    //FLOAT[]
    public void WriteArray(float[] array)
    {
        Write((short)array.Length);
        for (int i = 0; i < array.Length; i++)
        {
            Write(array[i]);
        }
    }

    public float[] ReadFloats()
    {
        short count = ReadShort();
        float[] array = new float[count];

        for (int i = 0; i < count; i++)
        {
            array[i] = ReadFloat();
        }

        return array;
    }

}
