using System;
using System.Collections.Generic;

public sealed class PooledBitStream : BitStream, IDisposable
{
    public override void Dispose()
    {
        BitStreamPool.Recycle(this);
    }
}

public class BitStreamPool
{
    static readonly Stack<PooledBitStream> stack_list = new Stack<PooledBitStream>();
    static PooledBitStream Take() => stack_list.Count > 0 ? stack_list.Pop() : new PooledBitStream();
    static void Return(PooledBitStream item) => stack_list.Push(item);
    public static int Count => stack_list.Count;
    public static PooledBitStream GetBitStream()
    {
        PooledBitStream writer = Take();
        writer.Reset();
        return writer;
    }
    public static void Recycle(PooledBitStream writer)
    {
        Return(writer);
    }
}