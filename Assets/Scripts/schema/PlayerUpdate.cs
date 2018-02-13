// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace schema
{

using global::System;
using global::FlatBuffers;

public struct PlayerUpdate : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static PlayerUpdate GetRootAsPlayerUpdate(ByteBuffer _bb) { return GetRootAsPlayerUpdate(_bb, new PlayerUpdate()); }
  public static PlayerUpdate GetRootAsPlayerUpdate(ByteBuffer _bb, PlayerUpdate obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public PlayerUpdate __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public PlayerStatus Status { get { int o = __p.__offset(4); return o != 0 ? (PlayerStatus)__p.bb.GetSbyte(o + __p.bb_pos) : PlayerStatus.Unknown; } }
  public PlayerInfo? Player { get { int o = __p.__offset(6); return o != 0 ? (PlayerInfo?)(new PlayerInfo()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<PlayerUpdate> CreatePlayerUpdate(FlatBufferBuilder builder,
      PlayerStatus status = PlayerStatus.Unknown,
      Offset<PlayerInfo> playerOffset = default(Offset<PlayerInfo>)) {
    builder.StartObject(2);
    PlayerUpdate.AddPlayer(builder, playerOffset);
    PlayerUpdate.AddStatus(builder, status);
    return PlayerUpdate.EndPlayerUpdate(builder);
  }

  public static void StartPlayerUpdate(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddStatus(FlatBufferBuilder builder, PlayerStatus status) { builder.AddSbyte(0, (sbyte)status, 0); }
  public static void AddPlayer(FlatBufferBuilder builder, Offset<PlayerInfo> playerOffset) { builder.AddOffset(1, playerOffset.Value, 0); }
  public static Offset<PlayerUpdate> EndPlayerUpdate(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<PlayerUpdate>(o);
  }
};


}