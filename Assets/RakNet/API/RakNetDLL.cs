public class RakNetDLL
{
    #if UNITY_64
    public const string DLL_NAME = "RakNet_x64";
    #endif
    #if !UNITY_64
    public const string DLL_NAME = "RakNet_x86";
    #endif
}
