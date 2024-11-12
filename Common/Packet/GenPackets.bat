START ../../CrazyArcade_Server/PacketGenerator/bin/Debug/PacketGenerator.exe ../../CrazyArcade_Server/PacketGenerator/PDL.xml
XCOPY /Y GenPackets.cs "../../CrazyArcade_Client/Assets/@Scripts/Packet"
XCOPY /Y GenPackets.cs "../../CrazyArcade_Server/CrazyArcade_Server/Packet"

XCOPY /Y ClientPacketManager.cs "../../CrazyArcade_Client/Assets/@Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../CrazyArcade_Server/CrazyArcade_Server/Packet"