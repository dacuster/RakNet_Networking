public enum CustomIDs
{
    //Packet numbers from 0 to 133 are reserved! If you specify the packet IDs in this range, the receiving party will ignore these packets
    CLIENT_DATA = DefaultMessageIDTypes.ID_USER_PACKET_ENUM,
    CLIENT_DATA_ACCEPTED
}