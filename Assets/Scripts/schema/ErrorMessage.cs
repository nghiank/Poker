// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace schema
{

using global::System;
using global::FlatBuffers;

public struct ErrorMessage : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static ErrorMessage GetRootAsErrorMessage(ByteBuffer _bb) { return GetRootAsErrorMessage(_bb, new ErrorMessage()); }
  public static ErrorMessage GetRootAsErrorMessage(ByteBuffer _bb, ErrorMessage obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public ErrorMessage __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Error { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
  public ArraySegment<byte>? GetErrorBytes() { return __p.__vector_as_arraysegment(4); }

  public static Offset<ErrorMessage> CreateErrorMessage(FlatBufferBuilder builder,
      StringOffset errorOffset = default(StringOffset)) {
    builder.StartObject(1);
    ErrorMessage.AddError(builder, errorOffset);
    return ErrorMessage.EndErrorMessage(builder);
  }

  public static void StartErrorMessage(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddError(FlatBufferBuilder builder, StringOffset errorOffset) { builder.AddOffset(0, errorOffset.Value, 0); }
  public static Offset<ErrorMessage> EndErrorMessage(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    builder.Required(o, 4);  // error
    return new Offset<ErrorMessage>(o);
  }
};


}