/// Store Statistics information related to network usage 
public struct RakNetStatistics
{
	/// For each type in RNSPerSecondMetrics, what is the value over the last 1 second?
	internal unsafe fixed ulong valueOverLastSecond[7];

	public unsafe ulong GetStatsLastSecond(RNSPerSecondMetrics metrics)
    {
        unsafe
		{
			return valueOverLastSecond[(int)metrics];
		}
    }

	/// For each type in RNSPerSecondMetrics, what is the total value over the lifetime of the connection?
	internal unsafe fixed ulong runningTotal[7];

	public ulong GetStatsTotal(RNSPerSecondMetrics metrics)
	{
        unsafe
		{
			return runningTotal[(int)metrics];
		}
	}

	internal ulong connectionStartTime;

	/// When did the connection start?
	public ulong ConnectionStartTime()
    {
		return connectionStartTime;
    }
	
	internal ulong connectionTime;
	
	/// How much time has passed since connection?
	public ulong ConnectionTime()
    {
		return connectionTime;
    }

	internal bool isLimitedByCongestionControl;

	/// Is our current send rate throttled by congestion control?
	/// This value should be true if you send more data per second than your bandwidth capacity
	public bool IsCongestionLimited()
    {
		return isLimitedByCongestionControl;
    }

	internal ulong BPSLimitByCongestionControl;

	/// If \a isLimitedByCongestionControl is true, what is the limit, in bytes per second?
	public ulong CongestionLimit()
    {
		return BPSLimitByCongestionControl;
    }

	/// Is our current send rate throttled by a call to RakPeer::SetPerConnectionOutgoingBandwidthLimit()?
	internal bool isLimitedByOutgoingBandwidthLimit;

	/// Is our current send rate throttled by a call to RakPeer.SetLimitBandwidth()?
	public bool IsBandwidthLimited()
    {
		return isLimitedByOutgoingBandwidthLimit;
    }

	internal ulong BPSLimitByOutgoingBandwidthLimit;

	/// If \a isLimitedByOutgoingBandwidthLimit is true, what is the limit, in bytes per second?
	public ulong BandwidthLimit()
    {
		return BPSLimitByOutgoingBandwidthLimit;
    }

	internal unsafe fixed uint messageInSendBuffer[4];

	/// For each priority level, how many messages are waiting to be sent out?
	public double GetMessagesInSendBuffer(PacketPriority packetPriority)
	{
		unsafe
		{
			return messageInSendBuffer[(int)packetPriority];
		}
	}

	internal unsafe fixed double bytesInSendBuffer[4];

	/// For each priority level, how many bytes are waiting to be sent out?
	public double GetBytesInSendBuffer(PacketPriority packetPriority)
    {
        unsafe
		{
			return bytesInSendBuffer[(int)packetPriority];
		}
    }

	internal uint messagesInResendBuffer;

	/// How many messages are waiting in the resend buffer? This includes messages waiting for an ack, so should normally be a small value
	/// If the value is rising over time, you are exceeding the bandwidth capacity.
	public uint MessagesInResendBuffer()
    {
		return messagesInResendBuffer;
    }

	internal ulong bytesInResendBuffer;

	/// How many bytes are waiting in the resend buffer. See also messagesInResendBuffer
	public ulong BytesInResendBuffer()
    {
		return bytesInResendBuffer;
    }

	internal float packetlossLastSecond;

	//What was our packetloss?
	public float PacketLoss()
    {
		return packetlossLastSecond * 100f;
    }

	internal float packetlossTotal;

	/// What is the average total packetloss over the lifetime of the connection?
	public float PacketLossTotal()
    {
		return packetlossTotal * 100f;
    }
}