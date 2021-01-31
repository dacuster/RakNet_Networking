# RakNet For Unity3D
This is an re-writted version of the wrapper for the native library of the RakNet network engine.

## How to use it
In order to use The raknet network engine in your project, I recommend that you study the test client and server.
See SampleClient.cs and SampleServer.cs

## What's new?
- Updated and optimized native (C++) code
- Added: Bitstream with many variations of writing and reading data (compression, delta, compressed-delta)
- Added: Password for the server
- Added: Data encryption
- Fixed: Setting bandwidth limit
- Added: Getting specific statistics data
- Removed: Unsafe parts of the code

### The old version of RakNet is not compatible with the current version 
Connection attempts will fail because the current version has traffic encryption and a different protocol version
