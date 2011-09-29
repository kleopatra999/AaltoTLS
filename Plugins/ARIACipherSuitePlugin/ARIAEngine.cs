//
// Copyright (C) 2010  Juho Vähä-Herttua <juhovh@iki.fi>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

//
// Based on RFC 5794, includes test vectors from the same document
//

using System;

public class ARIAEngine {
	static readonly int BLOCK_SIZE = 16;

	static readonly byte[][] SB = new byte[][] {
	new byte[] { // SB1
		0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76,
		0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0,
		0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15,
		0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75,
		0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84,
		0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf,
		0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8,
		0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2,
		0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73,
		0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb,
		0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79,
		0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08,
		0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a,
		0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e,
		0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf,
		0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16
	}, new byte[] { // SB2
		0xe2, 0x4e, 0x54, 0xfc, 0x94, 0xc2, 0x4a, 0xcc, 0x62, 0x0d, 0x6a, 0x46, 0x3c, 0x4d, 0x8b, 0xd1,
		0x5e, 0xfa, 0x64, 0xcb, 0xb4, 0x97, 0xbe, 0x2b, 0xbc, 0x77, 0x2e, 0x03, 0xd3, 0x19, 0x59, 0xc1,
		0x1d, 0x06, 0x41, 0x6b, 0x55, 0xf0, 0x99, 0x69, 0xea, 0x9c, 0x18, 0xae, 0x63, 0xdf, 0xe7, 0xbb,
		0x00, 0x73, 0x66, 0xfb, 0x96, 0x4c, 0x85, 0xe4, 0x3a, 0x09, 0x45, 0xaa, 0x0f, 0xee, 0x10, 0xeb,
		0x2d, 0x7f, 0xf4, 0x29, 0xac, 0xcf, 0xad, 0x91, 0x8d, 0x78, 0xc8, 0x95, 0xf9, 0x2f, 0xce, 0xcd,
		0x08, 0x7a, 0x88, 0x38, 0x5c, 0x83, 0x2a, 0x28, 0x47, 0xdb, 0xb8, 0xc7, 0x93, 0xa4, 0x12, 0x53,
		0xff, 0x87, 0x0e, 0x31, 0x36, 0x21, 0x58, 0x48, 0x01, 0x8e, 0x37, 0x74, 0x32, 0xca, 0xe9, 0xb1,
		0xb7, 0xab, 0x0c, 0xd7, 0xc4, 0x56, 0x42, 0x26, 0x07, 0x98, 0x60, 0xd9, 0xb6, 0xb9, 0x11, 0x40,
		0xec, 0x20, 0x8c, 0xbd, 0xa0, 0xc9, 0x84, 0x04, 0x49, 0x23, 0xf1, 0x4f, 0x50, 0x1f, 0x13, 0xdc,
		0xd8, 0xc0, 0x9e, 0x57, 0xe3, 0xc3, 0x7b, 0x65, 0x3b, 0x02, 0x8f, 0x3e, 0xe8, 0x25, 0x92, 0xe5,
		0x15, 0xdd, 0xfd, 0x17, 0xa9, 0xbf, 0xd4, 0x9a, 0x7e, 0xc5, 0x39, 0x67, 0xfe, 0x76, 0x9d, 0x43,
		0xa7, 0xe1, 0xd0, 0xf5, 0x68, 0xf2, 0x1b, 0x34, 0x70, 0x05, 0xa3, 0x8a, 0xd5, 0x79, 0x86, 0xa8,
		0x30, 0xc6, 0x51, 0x4b, 0x1e, 0xa6, 0x27, 0xf6, 0x35, 0xd2, 0x6e, 0x24, 0x16, 0x82, 0x5f, 0xda,
		0xe6, 0x75, 0xa2, 0xef, 0x2c, 0xb2, 0x1c, 0x9f, 0x5d, 0x6f, 0x80, 0x0a, 0x72, 0x44, 0x9b, 0x6c,
		0x90, 0x0b, 0x5b, 0x33, 0x7d, 0x5a, 0x52, 0xf3, 0x61, 0xa1, 0xf7, 0xb0, 0xd6, 0x3f, 0x7c, 0x6d,
		0xed, 0x14, 0xe0, 0xa5, 0x3d, 0x22, 0xb3, 0xf8, 0x89, 0xde, 0x71, 0x1a, 0xaf, 0xba, 0xb5, 0x81
	}, new byte[] { // SB3
		0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
		0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
		0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
		0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
		0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
		0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
		0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
		0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
		0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
		0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
		0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
		0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
		0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
		0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
		0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
		0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d
	}, new byte[] { // SB4
		0x30, 0x68, 0x99, 0x1b, 0x87, 0xb9, 0x21, 0x78, 0x50, 0x39, 0xdb, 0xe1, 0x72, 0x09, 0x62, 0x3c,
		0x3e, 0x7e, 0x5e, 0x8e, 0xf1, 0xa0, 0xcc, 0xa3, 0x2a, 0x1d, 0xfb, 0xb6, 0xd6, 0x20, 0xc4, 0x8d,
		0x81, 0x65, 0xf5, 0x89, 0xcb, 0x9d, 0x77, 0xc6, 0x57, 0x43, 0x56, 0x17, 0xd4, 0x40, 0x1a, 0x4d,
		0xc0, 0x63, 0x6c, 0xe3, 0xb7, 0xc8, 0x64, 0x6a, 0x53, 0xaa, 0x38, 0x98, 0x0c, 0xf4, 0x9b, 0xed,
		0x7f, 0x22, 0x76, 0xaf, 0xdd, 0x3a, 0x0b, 0x58, 0x67, 0x88, 0x06, 0xc3, 0x35, 0x0d, 0x01, 0x8b,
		0x8c, 0xc2, 0xe6, 0x5f, 0x02, 0x24, 0x75, 0x93, 0x66, 0x1e, 0xe5, 0xe2, 0x54, 0xd8, 0x10, 0xce,
		0x7a, 0xe8, 0x08, 0x2c, 0x12, 0x97, 0x32, 0xab, 0xb4, 0x27, 0x0a, 0x23, 0xdf, 0xef, 0xca, 0xd9,
		0xb8, 0xfa, 0xdc, 0x31, 0x6b, 0xd1, 0xad, 0x19, 0x49, 0xbd, 0x51, 0x96, 0xee, 0xe4, 0xa8, 0x41,
		0xda, 0xff, 0xcd, 0x55, 0x86, 0x36, 0xbe, 0x61, 0x52, 0xf8, 0xbb, 0x0e, 0x82, 0x48, 0x69, 0x9a,
		0xe0, 0x47, 0x9e, 0x5c, 0x04, 0x4b, 0x34, 0x15, 0x79, 0x26, 0xa7, 0xde, 0x29, 0xae, 0x92, 0xd7,
		0x84, 0xe9, 0xd2, 0xba, 0x5d, 0xf3, 0xc5, 0xb0, 0xbf, 0xa4, 0x3b, 0x71, 0x44, 0x46, 0x2b, 0xfc,
		0xeb, 0x6f, 0xd5, 0xf6, 0x14, 0xfe, 0x7c, 0x70, 0x5a, 0x7d, 0xfd, 0x2f, 0x18, 0x83, 0x16, 0xa5,
		0x91, 0x1f, 0x05, 0x95, 0x74, 0xa9, 0xc1, 0x5b, 0x4a, 0x85, 0x6d, 0x13, 0x07, 0x4f, 0x4e, 0x45,
		0xb2, 0x0f, 0xc9, 0x1c, 0xa6, 0xbc, 0xec, 0x73, 0x90, 0x7b, 0xcf, 0x59, 0x8f, 0xa1, 0xf9, 0x2d,
		0xf2, 0xb1, 0x00, 0x94, 0x37, 0x9f, 0xd0, 0x2e, 0x9c, 0x6e, 0x28, 0x3f, 0x80, 0xf0, 0x3d, 0xd3,
		0x25, 0x8a, 0xb5, 0xe7, 0x42, 0xb3, 0xc7, 0xea, 0xf7, 0x4c, 0x11, 0x33, 0x03, 0xa2, 0xac, 0x60
	}};

	private byte[][] _ek;
	private byte[][] _dk;

	public ARIAEngine(byte[] masterKey)
	{
		if (masterKey == null)
			throw new ArgumentNullException("masterKey");
		if (masterKey.Length != 16 && masterKey.Length != 24 && masterKey.Length != 32)
			throw new ArgumentException("masterKey");
		
		GenerateRoundKeys(masterKey);
	}

	public void Encrypt(byte[] inputBuffer, int inputOffset, byte[] outputBuffer, int outputOffset)
	{
		if (inputBuffer == null)
			throw new ArgumentNullException("inputBuffer");
		if (inputOffset < 0 || inputOffset > inputBuffer.Length+BLOCK_SIZE)
			throw new ArgumentOutOfRangeException("inputOffset");
		if (outputBuffer == null)
			throw new ArgumentNullException("outputBuffer");
		if (outputOffset < 0 || outputOffset > outputBuffer.Length+BLOCK_SIZE)
			throw new ArgumentOutOfRangeException("outputBuffer");
		
		byte[] state = new byte[BLOCK_SIZE];
		Buffer.BlockCopy(inputBuffer, inputOffset, state, 0, BLOCK_SIZE);

		for (int i=0; i<_ek.Length-1; i++) {
			F(i%2!=0, i==(_ek.Length-2), state, _ek[i], state);
		}

		XOR(state, _ek[_ek.Length-1], state);
		Buffer.BlockCopy(state, 0, outputBuffer, outputOffset, BLOCK_SIZE);
	}

	public void Decrypt(byte[] inputBuffer, int inputOffset, byte[] outputBuffer, int outputOffset)
	{
		if (inputBuffer == null)
			throw new ArgumentNullException("inputBuffer");
		if (inputOffset < 0 || inputOffset > inputBuffer.Length+BLOCK_SIZE)
			throw new ArgumentOutOfRangeException("inputOffset");
		if (outputBuffer == null)
			throw new ArgumentNullException("outputBuffer");
		if (outputOffset < 0 || outputOffset > outputBuffer.Length+BLOCK_SIZE)
			throw new ArgumentOutOfRangeException("outputBuffer");
		
		byte[] state = new byte[BLOCK_SIZE];
		Buffer.BlockCopy(inputBuffer, inputOffset, state, 0, BLOCK_SIZE);

		for (int i=0; i<_dk.Length-1; i++) {
			F(i%2!=0, i==(_dk.Length-2), state, _dk[i], state);
		}

		XOR(state, _dk[_dk.Length-1], state);
		Buffer.BlockCopy(state, 0, outputBuffer, outputOffset, BLOCK_SIZE);
	}


	private void Substitute(bool even, byte[] x, byte[] y)
	{
		int sbox = even ? 2 : 0;
		for (int i=0; i<BLOCK_SIZE; i++) {
			y[i] = SB[sbox++%4][x[i]];
		}
	}

	private void Diffuse(byte[] x, byte[] y)
	{
		y[0]  = (byte) (x[3] ^ x[4] ^ x[6] ^ x[8]  ^ x[9]  ^ x[13] ^ x[14]);
		y[1]  = (byte) (x[2] ^ x[5] ^ x[7] ^ x[8]  ^ x[9]  ^ x[12] ^ x[15]);
		y[2]  = (byte) (x[1] ^ x[4] ^ x[6] ^ x[10] ^ x[11] ^ x[12] ^ x[15]);
		y[3]  = (byte) (x[0] ^ x[5] ^ x[7] ^ x[10] ^ x[11] ^ x[13] ^ x[14]);
		y[4]  = (byte) (x[0] ^ x[2] ^ x[5] ^ x[8]  ^ x[11] ^ x[14] ^ x[15]);
		y[5]  = (byte) (x[1] ^ x[3] ^ x[4] ^ x[9]  ^ x[10] ^ x[14] ^ x[15]);
		y[6]  = (byte) (x[0] ^ x[2] ^ x[7] ^ x[9]  ^ x[10] ^ x[12] ^ x[13]);
		y[7]  = (byte) (x[1] ^ x[3] ^ x[6] ^ x[8]  ^ x[11] ^ x[12] ^ x[13]);
		y[8]  = (byte) (x[0] ^ x[1] ^ x[4] ^ x[7]  ^ x[10] ^ x[13] ^ x[15]);
		y[9]  = (byte) (x[0] ^ x[1] ^ x[5] ^ x[6]  ^ x[11] ^ x[12] ^ x[14]);
		y[10] = (byte) (x[2] ^ x[3] ^ x[5] ^ x[6]  ^ x[8]  ^ x[13] ^ x[15]);
		y[11] = (byte) (x[2] ^ x[3] ^ x[4] ^ x[7]  ^ x[9]  ^ x[12] ^ x[14]);
		y[12] = (byte) (x[1] ^ x[2] ^ x[6] ^ x[7]  ^ x[9]  ^ x[11] ^ x[12]);
		y[13] = (byte) (x[0] ^ x[3] ^ x[6] ^ x[7]  ^ x[8]  ^ x[10] ^ x[13]);
		y[14] = (byte) (x[0] ^ x[3] ^ x[4] ^ x[5]  ^ x[9]  ^ x[11] ^ x[14]);
		y[15] = (byte) (x[1] ^ x[2] ^ x[4] ^ x[5]  ^ x[8]  ^ x[10] ^ x[15]);
	}

	private void XOR(byte[] x1, byte[] x2, byte[] y)
	{
		for (int i=0; i<BLOCK_SIZE; i++) {
			y[i] = (byte) (x1[i] ^ x2[i]);
		}
	}

	private byte[] Ftmp = new byte[BLOCK_SIZE];
	private void F(bool even, bool final, byte[] input, byte[] key, byte[] output)
	{
		XOR(input, key, Ftmp);
		Substitute(even, Ftmp, Ftmp);
		if (final) {
			Buffer.BlockCopy(Ftmp, 0, output, 0, BLOCK_SIZE);
		} else {
			Diffuse(Ftmp, output);
		}
	}

	private byte[] CircularShiftLeft(byte[] x, int c)
	{
		int mask = 0;
		for (int i=0; i<(8-c%8); i++)
			mask = mask*2+1;

		byte[] y = new byte[x.Length];
		for (int i=0; i<y.Length; i++) {
			y[i]  = (byte) (((x[(i+c/8)%x.Length] & mask) << c%8) |
			                ((x[(i+c/8+1)%x.Length] & ~mask) >> 8-c%8));
		}
		return y;
	}

	private void GenerateRoundKeys(byte[] MK)
	{
		/* Constant values derived from the fractional part of 1/PI */
		byte[][] C = new byte[][] {
			new byte[] { 0x51, 0x7c, 0xc1, 0xb7, 0x27, 0x22, 0x0a, 0x94,
			             0xfe, 0x13, 0xab, 0xe8, 0xfa, 0x9a, 0x6e, 0xe0 },
			new byte[] { 0x6d, 0xb1, 0x4a, 0xcc, 0x9e, 0x21, 0xc8, 0x20,
			             0xff, 0x28, 0xb1, 0xd5, 0xef, 0x5d, 0xe2, 0xb0 },
			new byte[] { 0xdb, 0x92, 0x37, 0x1d, 0x21, 0x26, 0xe9, 0x70,
			             0x03, 0x24, 0x97, 0x75, 0x04, 0xe8, 0xc9, 0x0e }
		};

		int cIndex, nRounds;
		if (MK.Length == 16) {
			cIndex = 0;
			nRounds = 12;
		} else if (MK.Length == 24) {
			cIndex = 1;
			nRounds = 14;
		} else if (MK.Length == 32) {
			cIndex = 2;
			nRounds = 16;
		} else {
			throw new Exception("Invalid key length");
		}

		/* Left and right parts of the key */
		byte[] KL = new byte[16];
		byte[] KR = new byte[16];
		Buffer.BlockCopy(MK, 0, KL, 0, 16);
		Buffer.BlockCopy(MK, 16, KR, 0, MK.Length-16);

		/* Allocate key generators */
		byte[][] W = new byte[4][];
		for (int i=0; i<W.Length; i++)
			W[i] = new byte[BLOCK_SIZE];

		/* Calculate key generators */
		Buffer.BlockCopy(KL, 0, W[0], 0, BLOCK_SIZE);
		F(false, false, W[0], C[(cIndex)%3], W[1]);
		XOR(W[1], KR, W[1]);
		F(true, false, W[1], C[(cIndex+1)%3], W[2]);
		XOR(W[2], W[0], W[2]);
		F(false, false, W[2], C[(cIndex+2)%3], W[3]);
		XOR(W[3], W[1], W[3]);

		/* Allocate encrypt and decrypt round keys */
		_ek = new byte[nRounds+1][];
		_dk = new byte[nRounds+1][];
		for (int i=0; i<=nRounds; i++) {
			_ek[i] = new byte[BLOCK_SIZE];
			_dk[i] = new byte[BLOCK_SIZE];
		}

		/* Generate round keys */
		byte[] shifts = new byte[] { 109, 97, 61, 31, 19 };
		for (int i=0; i<=nRounds; i++) {
			XOR(W[i%4], CircularShiftLeft(W[(i+1)%4], shifts[i/4]), _ek[i]);
		}
		Buffer.BlockCopy(_ek[0], 0, _dk[nRounds], 0, BLOCK_SIZE);
		for (int i=1; i<nRounds; i++) {
			Diffuse(_ek[nRounds-i], _dk[i]);
		}
		Buffer.BlockCopy(_ek[nRounds], 0, _dk[0], 0, BLOCK_SIZE);
	}




/*
	private static byte[] StringToByteArray(String str)
	{
		if ((str.Length % 2) != 0) {
			throw new ArgumentException("Invalid string length");
		}

		byte[] bytes = new byte[str.Length / 2];
		for (int i = 0; i<str.Length; i+=2) {
			bytes[i/2] = Convert.ToByte(str.Substring(i, 2), 16);
		}
		return bytes;
	}

	private static bool ValidateTestVector(string key,
	                                       string plaintext,
	                                       string ciphertext)
	{
		byte[] K = StringToByteArray(key);
		byte[] P = StringToByteArray(plaintext);
		byte[] C = StringToByteArray(ciphertext);

		byte[] encrypted = new byte[BLOCK_SIZE];
		byte[] decrypted = new byte[BLOCK_SIZE];

		ARIAEngine aria = new ARIAEngine(K);
		aria.Encrypt(P, 0, encrypted, 0);
		aria.Decrypt(encrypted, 0, decrypted, 0);

		bool encryptOk = (BitConverter.ToString(encrypted) == BitConverter.ToString(C));
		bool decryptOk = (BitConverter.ToString(decrypted) == BitConverter.ToString(P));
		return (encryptOk && decryptOk);
	}

	public static void Main(string[] args)
	{
		bool vector1 = ValidateTestVector("000102030405060708090a0b0c0d0e0f",
		                                  "00112233445566778899aabbccddeeff",
		                                  "d718fbd6ab644c739da95f3be6451778");
		bool vector2 = ValidateTestVector("000102030405060708090a0b0c0d0e0f" +
		                                  "1011121314151617",
		                                  "00112233445566778899aabbccddeeff",
		                                  "26449c1805dbe7aa25a468ce263a9e79");
		bool vector3 = ValidateTestVector("000102030405060708090a0b0c0d0e0f" +
		                                  "101112131415161718191a1b1c1d1e1f",
		                                  "00112233445566778899aabbccddeeff",
		                                  "f92bd7c79fb72e2f2b8f80c1972d24fc");

		Console.WriteLine("Test vector 1 validated: " + vector1);
		Console.WriteLine("Test vector 2 validated: " + vector2);
		Console.WriteLine("Test vector 3 validated: " + vector3);
	}
*/
}
