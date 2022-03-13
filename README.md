# Shadow.Client (Uncomplete and archived)

The goal of this project was to get near full remote access to an owned [shadow.tech](https://shadow.tech) virtual machine through **C#**, allowing to control the VM at a low level as well as control the mouse and keyboard etc.. Except I gave up on the project for lack of use, so it didn't go further than connecting and exchanging a few messages about each others version.

I still thought this might be useful to anyone who might need to automatize their Shadow VM, as this is still a solid base.

# Some additional informations

This client is entirely based off reversing their Android application (`com.blade.shadowcloudgaming`), which uses JNI to interact with a C++ native library named `libAwesome.so`, which does all the heavy work. This library is unpacked from the app data and then loaded in the app at runtime when you start interacting with the virtual machine. 

The connection to the **VM** runs over multiple channels (*aka. ports*) dispatched on two protocols: TCP and UDP.

Some of these channels are available on both protocols, and the application will switch to the prefered according to it's settings, but for most of them, it seems they're locked onto one.

Each channel has their own port offset, ranging from `01` to `32`.

The base port for each channel is the `port` field given by the `/shadow/vm/ip` endpoint ([replicated here](https://github.com/defnotcat/Shadow.Client/blob/master/Shadow.Client/Http/Gap/GapApiProvider.cs#L80)) + `7000` (For some fucking reason)


| Name | Data Type | Port Offset | Protocols
|-------------------|-------|---------|--|
| VmProxyChannel | Plain ASCII (`JSON`) | `1` | TCP
| SslCtrlChanV2 | ProtoBuff | `11` | TCP
| InputChannel | *Unknown, didn't go that far* | `12` | TCP, UDP
| VideoChannel | *Unknown, didn't go that far* | `10` | TCP, UDP
| AudioInUdpChannel | *Unknown, didn't go that far* | `32` | UDP
| AUdioOutChannel | *Unknown, didn't go that far* | `30` | TCP, UDP
| CursorTcpChannel | *Unknown, didn't go that far* | `20` | UDP
| GamepadChannel | *Unknown, didn't go that far* | `13` | UDP
| TcpClipboardChannel | *Unknown, didn't go that far* | `14` | TCP

I'm sure you can guess which channel does what from the name. I haven't investigated all the channels, only the ones present from the very beginning of the session: `VmProxyChannel` and `SslCtrlChanV2` both interacting with the VM backend to get status or establish session/encryption.

- The application uses [libuv](https://github.com/libuv/libuv) to handle it's networking, which has a very [complete documentation](http://docs.libuv.org/en/v1.x/) that can help you while reverse engineering the app.