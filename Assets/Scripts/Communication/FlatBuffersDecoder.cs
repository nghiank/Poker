﻿using System;

// The bytebuf is big endian 2-bytes length prepdended.
public class FlatBuffersDecoder
{
	public enum ReadState { INITIAL, READING_LENGTH, READING_DATA, DONE };
	//public const int LENGTH_SIZE = 2; // 2 bytes for the length;

	private ReadState state;
	private byte[] buf;
	private int bufLen;
	private int pos;
	public FlatBuffersDecoder ()
	{
		state = ReadState.INITIAL;
		bufLen = 0;
		pos = 0;
	}

	public ReadState getState() {
		return state;
	}

	public byte[] getData() {
		return buf;
	}

	public void Fetch(byte[] buffer, int len) {
		switch (state) {
		case ReadState.INITIAL:
			if (buffer.Length == 1) {
				bufLen = buffer [0] << 8; 
				state = ReadState.READING_LENGTH;
			} else if (len > 1) {
				bufLen = ((int)buffer [0] << 8) + (int)buffer [1];
				buf = new byte[bufLen];
				if (len > 2) {
					Buffer.BlockCopy (buffer, 2, buf, pos, len - 2);
					pos += len - 2;
				}
				state = ReadState.READING_DATA;
			}
			break;
		case ReadState.READING_LENGTH:
			bufLen += buffer [0];
			buf = new byte[bufLen];
			state = ReadState.READING_DATA;
			if (len > 1) {
				Buffer.BlockCopy (buffer, 1, buf, pos, len - 1);
				pos += len - 1;
			}
			break;
		case ReadState.READING_DATA:
			Buffer.BlockCopy (buffer, 0, buf, pos, len);
			pos += len;
			break;
		}

		if (state == ReadState.READING_DATA && pos == bufLen) {
			state = ReadState.DONE;
		}
	}
}


