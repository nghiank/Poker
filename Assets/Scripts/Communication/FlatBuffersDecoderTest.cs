using System;
using NUnit.Framework;


public class FlatBuffersDecoderTest
{
	[Test]
	public void FlatBuffersDecoderTest_LengthComeSlowly ()
	{
		FlatBuffersDecoder decoder = new FlatBuffersDecoder();
		Assert.AreEqual(decoder.getState(), FlatBuffersDecoder.ReadState.INITIAL);
		byte[] b = new byte[1];
		b[0] = 0;
		decoder.Fetch(b, 1);
		Assert.AreEqual(decoder.getState(), FlatBuffersDecoder.ReadState.READING_LENGTH);

		byte[] b1 = new byte[1];
		b1 [0] = 3;
		decoder.Fetch (b1, 1);
		Assert.AreEqual(decoder.getState(), FlatBuffersDecoder.ReadState.READING_DATA);

		byte[] b2 = new byte[3];
		b2[0] = 1;
		b2[1] = 2;
		b2[2] = 3;
		decoder.Fetch (b2, 3);
		Assert.AreEqual(decoder.getState(), FlatBuffersDecoder.ReadState.DONE);

		byte[] res = decoder.getData ();
		Assert.AreEqual (res.Length, 3);
		Assert.AreEqual (res [0], 1);
		Assert.AreEqual (res [1], 2);
		Assert.AreEqual (res [2], 3);
	}

	[Test]
	public void FlatBuffersDecoderTest_AllIn() {
		byte[] b = new byte[6];
		b [0] = 0;
		b [1] = 4;
		b [2] = 1;
		b [3] = 2;
		b [4] = 3;
		b [5] = 4;
		FlatBuffersDecoder decoder = new FlatBuffersDecoder();
		decoder.Fetch (b, 6);
		Assert.AreEqual(decoder.getState(), FlatBuffersDecoder.ReadState.DONE);
		byte[] res = decoder.getData ();
		Assert.AreEqual (res.Length, 4);
		Assert.AreEqual (res [0], 1);
		Assert.AreEqual (res [1], 2);
		Assert.AreEqual (res [2], 3);
		Assert.AreEqual (res [3], 4);
	}
}


