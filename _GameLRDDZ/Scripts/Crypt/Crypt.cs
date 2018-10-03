﻿using UnityEngine;
using System.Collections;
using System;

public class Crypt {


    //----------------------------------------------------------------------------------------------------------------------------------------
    /* the eight DES S-boxes */
    static UInt32[] SB1 = new UInt32[]{
	    0x01010400, 0x00000000, 0x00010000, 0x01010404,
	    0x01010004, 0x00010404, 0x00000004, 0x00010000,
	    0x00000400, 0x01010400, 0x01010404, 0x00000400,
	    0x01000404, 0x01010004, 0x01000000, 0x00000004,
	    0x00000404, 0x01000400, 0x01000400, 0x00010400,
	    0x00010400, 0x01010000, 0x01010000, 0x01000404,
	    0x00010004, 0x01000004, 0x01000004, 0x00010004,
	    0x00000000, 0x00000404, 0x00010404, 0x01000000,
	    0x00010000, 0x01010404, 0x00000004, 0x01010000,
	    0x01010400, 0x01000000, 0x01000000, 0x00000400,
	    0x01010004, 0x00010000, 0x00010400, 0x01000004,
	    0x00000400, 0x00000004, 0x01000404, 0x00010404,
	    0x01010404, 0x00010004, 0x01010000, 0x01000404,
	    0x01000004, 0x00000404, 0x00010404, 0x01010400,
	    0x00000404, 0x01000400, 0x01000400, 0x00000000,
	    0x00010004, 0x00010400, 0x00000000, 0x01010004
    };

    static UInt32[] SB2 = new UInt32[]{
	    0x80108020, 0x80008000, 0x00008000, 0x00108020,
	    0x00100000, 0x00000020, 0x80100020, 0x80008020,
	    0x80000020, 0x80108020, 0x80108000, 0x80000000,
	    0x80008000, 0x00100000, 0x00000020, 0x80100020,
	    0x00108000, 0x00100020, 0x80008020, 0x00000000,
	    0x80000000, 0x00008000, 0x00108020, 0x80100000,
	    0x00100020, 0x80000020, 0x00000000, 0x00108000,
	    0x00008020, 0x80108000, 0x80100000, 0x00008020,
	    0x00000000, 0x00108020, 0x80100020, 0x00100000,
	    0x80008020, 0x80100000, 0x80108000, 0x00008000,
	    0x80100000, 0x80008000, 0x00000020, 0x80108020,
	    0x00108020, 0x00000020, 0x00008000, 0x80000000,
	    0x00008020, 0x80108000, 0x00100000, 0x80000020,
	    0x00100020, 0x80008020, 0x80000020, 0x00100020,
	    0x00108000, 0x00000000, 0x80008000, 0x00008020,
	    0x80000000, 0x80100020, 0x80108020, 0x00108000
    };

    static UInt32[] SB3 = new UInt32[]{
	    0x00000208, 0x08020200, 0x00000000, 0x08020008,
	    0x08000200, 0x00000000, 0x00020208, 0x08000200,
	    0x00020008, 0x08000008, 0x08000008, 0x00020000,
	    0x08020208, 0x00020008, 0x08020000, 0x00000208,
	    0x08000000, 0x00000008, 0x08020200, 0x00000200,
	    0x00020200, 0x08020000, 0x08020008, 0x00020208,
	    0x08000208, 0x00020200, 0x00020000, 0x08000208,
	    0x00000008, 0x08020208, 0x00000200, 0x08000000,
	    0x08020200, 0x08000000, 0x00020008, 0x00000208,
	    0x00020000, 0x08020200, 0x08000200, 0x00000000,
	    0x00000200, 0x00020008, 0x08020208, 0x08000200,
	    0x08000008, 0x00000200, 0x00000000, 0x08020008,
	    0x08000208, 0x00020000, 0x08000000, 0x08020208,
	    0x00000008, 0x00020208, 0x00020200, 0x08000008,
	    0x08020000, 0x08000208, 0x00000208, 0x08020000,
	    0x00020208, 0x00000008, 0x08020008, 0x00020200
    };

    static UInt32[] SB4 = new UInt32[]{
	    0x00802001, 0x00002081, 0x00002081, 0x00000080,
	    0x00802080, 0x00800081, 0x00800001, 0x00002001,
	    0x00000000, 0x00802000, 0x00802000, 0x00802081,
	    0x00000081, 0x00000000, 0x00800080, 0x00800001,
	    0x00000001, 0x00002000, 0x00800000, 0x00802001,
	    0x00000080, 0x00800000, 0x00002001, 0x00002080,
	    0x00800081, 0x00000001, 0x00002080, 0x00800080,
	    0x00002000, 0x00802080, 0x00802081, 0x00000081,
	    0x00800080, 0x00800001, 0x00802000, 0x00802081,
	    0x00000081, 0x00000000, 0x00000000, 0x00802000,
	    0x00002080, 0x00800080, 0x00800081, 0x00000001,
	    0x00802001, 0x00002081, 0x00002081, 0x00000080,
	    0x00802081, 0x00000081, 0x00000001, 0x00002000,
	    0x00800001, 0x00002001, 0x00802080, 0x00800081,
	    0x00002001, 0x00002080, 0x00800000, 0x00802001,
	    0x00000080, 0x00800000, 0x00002000, 0x00802080
    };

    static UInt32[] SB5 = new UInt32[]{
	    0x00000100, 0x02080100, 0x02080000, 0x42000100,
	    0x00080000, 0x00000100, 0x40000000, 0x02080000,
	    0x40080100, 0x00080000, 0x02000100, 0x40080100,
	    0x42000100, 0x42080000, 0x00080100, 0x40000000,
	    0x02000000, 0x40080000, 0x40080000, 0x00000000,
	    0x40000100, 0x42080100, 0x42080100, 0x02000100,
	    0x42080000, 0x40000100, 0x00000000, 0x42000000,
	    0x02080100, 0x02000000, 0x42000000, 0x00080100,
	    0x00080000, 0x42000100, 0x00000100, 0x02000000,
	    0x40000000, 0x02080000, 0x42000100, 0x40080100,
	    0x02000100, 0x40000000, 0x42080000, 0x02080100,
	    0x40080100, 0x00000100, 0x02000000, 0x42080000,
	    0x42080100, 0x00080100, 0x42000000, 0x42080100,
	    0x02080000, 0x00000000, 0x40080000, 0x42000000,
	    0x00080100, 0x02000100, 0x40000100, 0x00080000,
	    0x00000000, 0x40080000, 0x02080100, 0x40000100
    };

    static UInt32[] SB6 = new UInt32[]{
	    0x20000010, 0x20400000, 0x00004000, 0x20404010,
	    0x20400000, 0x00000010, 0x20404010, 0x00400000,
	    0x20004000, 0x00404010, 0x00400000, 0x20000010,
	    0x00400010, 0x20004000, 0x20000000, 0x00004010,
	    0x00000000, 0x00400010, 0x20004010, 0x00004000,
	    0x00404000, 0x20004010, 0x00000010, 0x20400010,
	    0x20400010, 0x00000000, 0x00404010, 0x20404000,
	    0x00004010, 0x00404000, 0x20404000, 0x20000000,
	    0x20004000, 0x00000010, 0x20400010, 0x00404000,
	    0x20404010, 0x00400000, 0x00004010, 0x20000010,
	    0x00400000, 0x20004000, 0x20000000, 0x00004010,
	    0x20000010, 0x20404010, 0x00404000, 0x20400000,
	    0x00404010, 0x20404000, 0x00000000, 0x20400010,
	    0x00000010, 0x00004000, 0x20400000, 0x00404010,
	    0x00004000, 0x00400010, 0x20004010, 0x00000000,
	    0x20404000, 0x20000000, 0x00400010, 0x20004010
    };

    static UInt32[] SB7 = new UInt32[]{
	    0x00200000, 0x04200002, 0x04000802, 0x00000000,
	    0x00000800, 0x04000802, 0x00200802, 0x04200800,
	    0x04200802, 0x00200000, 0x00000000, 0x04000002,
	    0x00000002, 0x04000000, 0x04200002, 0x00000802,
	    0x04000800, 0x00200802, 0x00200002, 0x04000800,
	    0x04000002, 0x04200000, 0x04200800, 0x00200002,
	    0x04200000, 0x00000800, 0x00000802, 0x04200802,
	    0x00200800, 0x00000002, 0x04000000, 0x00200800,
	    0x04000000, 0x00200800, 0x00200000, 0x04000802,
	    0x04000802, 0x04200002, 0x04200002, 0x00000002,
	    0x00200002, 0x04000000, 0x04000800, 0x00200000,
	    0x04200800, 0x00000802, 0x00200802, 0x04200800,
	    0x00000802, 0x04000002, 0x04200802, 0x04200000,
	    0x00200800, 0x00000000, 0x00000002, 0x04200802,
	    0x00000000, 0x00200802, 0x04200000, 0x00000800,
	    0x04000002, 0x04000800, 0x00000800, 0x00200002
    };

    static UInt32[] SB8 = new UInt32[]{
	    0x10001040, 0x00001000, 0x00040000, 0x10041040,
	    0x10000000, 0x10001040, 0x00000040, 0x10000000,
	    0x00040040, 0x10040000, 0x10041040, 0x00041000,
	    0x10041000, 0x00041040, 0x00001000, 0x00000040,
	    0x10040000, 0x10000040, 0x10001000, 0x00001040,
	    0x00041000, 0x00040040, 0x10040040, 0x10041000,
	    0x00001040, 0x00000000, 0x00000000, 0x10040040,
	    0x10000040, 0x10001000, 0x00041040, 0x00040000,
	    0x00041040, 0x00040000, 0x10041000, 0x00001000,
	    0x00000040, 0x10040040, 0x00001000, 0x00041040,
	    0x10001000, 0x00000040, 0x10000040, 0x10040000,
	    0x10040040, 0x10000000, 0x00040000, 0x10001040,
	    0x00000000, 0x10041040, 0x00040040, 0x10000040,
	    0x10040000, 0x10001000, 0x10001040, 0x00000000,
	    0x10041040, 0x00041000, 0x00041000, 0x00001040,
	    0x00001040, 0x00040040, 0x10000000, 0x10041000
    };

    // Constants are the integer part of the sines of integers (in radians) * 2^32.
    static UInt32[] k = new UInt32[]{
	    0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
	    0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
	    0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
	    0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
	    0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
	    0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
	    0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
	    0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
	    0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
	    0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
	    0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
	    0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
	    0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
	    0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
	    0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
	    0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391 
    };

    // r specifies the per-round shift amounts
    static UInt32[] r = new UInt32[]{ 
        7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
        5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
        4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
        6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21 
    };

    /* PC1: left and right halves bit-swap */
    static UInt32[] LHs = new UInt32[]{
	    0x00000000, 0x00000001, 0x00000100, 0x00000101,
	    0x00010000, 0x00010001, 0x00010100, 0x00010101,
	    0x01000000, 0x01000001, 0x01000100, 0x01000101,
	    0x01010000, 0x01010001, 0x01010100, 0x01010101
    };

    static UInt32[] RHs = new UInt32[]{
	    0x00000000, 0x01000000, 0x00010000, 0x01010000,
	    0x00000100, 0x01000100, 0x00010100, 0x01010100,
	    0x00000001, 0x01000001, 0x00010001, 0x01010001,
	    0x00000101, 0x01000101, 0x00010101, 0x01010101,
    };


    // leftrotate function definition
    static UInt32 LEFTROTATE(UInt32 x, UInt32 c)
    {
        //return (UInt32)((((Int32)x) << ((Int32)c)) | ((((Int32)x)) >> ((Int32)(32 - (c)))));
        return (((x) << (Int32)(c)) | ((x) >> (Int32)(32 - (c))));
    }


    static void hmac(UInt32[] x, UInt32[] y, UInt32[] result) 
    {
        UInt32[] w = new UInt32[16];
        UInt32 a, b, c, d, f, g, temp;
        UInt32 i;

	    a = 0x67452301u;
	    b = 0xefcdab89u;
	    c = 0x98badcfeu;
	    d = 0x10325476u;
	    for (i = 0; i < 16; i += 4) {
		    w[i] = x[1];
		    w[i + 1] = x[0];
		    w[i + 2] = y[1];
		    w[i + 3] = y[0];
	    }
	    for (i = 0; i < 64; i++) {
		    if (i < 16) {
			    f = (b & c) | ((~b) & d);
			    g = i;
		    }
		    else if (i < 32) {
			    f = (d & b) | ((~d) & c);
			    g = (5 * i + 1) % 16;
		    }
		    else if (i < 48) {
			    f = b ^ c ^ d;
			    g = (3 * i + 5) % 16;
		    }
		    else {
			    f = c ^ (b | (~d));
			    g = (7 * i) % 16;
		    }

		    temp = d;
		    d = c;
		    c = b;
		    b = b + LEFTROTATE((a + f + k[i] + w[g]), r[i]);
		    a = temp;

	    }
	    result[0] = c^d;
	    result[1] = a^b;
    }

    static void Hash(Byte[] str, int sz, Byte[] key) 
    {
        UInt32 djb_hash = 5381;
        UInt32 js_hash = 1315423911;

	    int i;
	    for (i = 0; i < sz; i++) {
            Byte c = (Byte)str[i];
		    djb_hash += (djb_hash << 5) + c;
		    js_hash ^= ((js_hash << 5) + c + (js_hash >> 2));
	    }

        key[0] = (Byte)(djb_hash & 0xff);
        key[1] = (Byte)((djb_hash >> 8) & 0xff);
        key[2] = (Byte)((djb_hash >> 16) & 0xff);
        key[3] = (Byte)((djb_hash >> 24) & 0xff);

        key[4] = (Byte)(js_hash & 0xff);
        key[5] = (Byte)((js_hash >> 8) & 0xff);
        key[6] = (Byte)((js_hash >> 16) & 0xff);
        key[7] = (Byte)((js_hash >> 24) & 0xff);
    }

    static void lhmac64(Byte[] one_text, int one_size, Byte[] two_text, int two_size, Byte[] buffer)
    {
        UInt32[] result = new UInt32[2];
        UInt32[] x = new UInt32[2];
        UInt32[] y = new UInt32[2];
	    read64(one_text, one_size, two_text, two_size, x, y);
	    hmac(x, y, result);
	    pushqword(buffer, result);
    }

    /*
    8bytes key
    string text
    */
    void lhmac_hash(Byte[] x, int x_size, Byte[] text, int text_size, Byte[] buffer) 
    {
        UInt32[] key = new UInt32[2];
	    //size_t sz = 0;
	    //const uint8_t *x = (const uint8_t *)luaL_checklstring(L, 1, &sz);
	    if (x_size != 8) {
		    //luaL_error(L, "Invalid uint64 key");
		    return;
	    }

	    key[0] = ((UInt32)x[0]) | ((UInt32)x[1]) << 8 | ((UInt32)x[2]) << 16 | ((UInt32)x[3]) << 24;
	    key[1] = ((UInt32)x[4]) | ((UInt32)x[5]) << 8 | ((UInt32)x[6]) << 16 | ((UInt32)x[7]) << 24;
	    //const char * text = luaL_checklstring(L, 2, &sz);
        Byte[] h = new Byte[8];
	    Hash(text, text_size, h);
        UInt32[] htext = new UInt32[2];
	    htext[0] = (UInt32)h[0] | (UInt32)(h[1] << 8) | (UInt32)(h[2] << 16) | (UInt32)(h[3] << 24);
	    htext[1] = (UInt32)h[4] | (UInt32)(h[5] << 8) | (UInt32)(h[6] << 16) | (UInt32)(h[7] << 24);
        UInt32[] result = new UInt32[2];
	    hmac(htext, key, result);
	    pushqword(buffer, result);
    }



    public const UInt64 P = 0xffffffffffffffc5;

    static UInt64 mul_mod_p(UInt64 a, UInt64 b)
    {
        UInt64 m = 0;
        while (b != 0)
        {
            if ((b & 1) != 0)
            {
                UInt64 t = P - a;
                if (m >= t)
                {
                    m -= t;
                }
                else
                {
                    m += a;
                }
            }
            if (a >= P - a)
            {
                a = a * 2 - P;
            }
            else
            {
                a = a * 2;
            }
            b >>= 1;
        }
        return m;
    }

    static UInt64 pow_mod_p(UInt64 a, UInt64 b)
    {
        UInt64 t;
	    if (b == 1) {
		    return a;
	    }
	    t = pow_mod_p(a, b >> 1);
	    t = mul_mod_p(t, t);
	    if ((b % 2) != 0) {
		    t = mul_mod_p(t, a);
	    }
	    return t;
    }

    static UInt64 powmodp(UInt64 a, UInt64 b)
    {
        if (a > P)
            a %= P;
        return pow_mod_p(a, b);
    }

    static void push64(Byte[] tmp, UInt64 r) 
    {
	    //uint8_t tmp[8];
        tmp[0] = (Byte)(r & 0xff);
        tmp[1] = (Byte)((r >> 8) & 0xff);
        tmp[2] = (Byte)((r >> 16) & 0xff);
        tmp[3] = (Byte)((r >> 24) & 0xff);
        tmp[4] = (Byte)((r >> 32) & 0xff);
        tmp[5] = (Byte)((r >> 40) & 0xff);
        tmp[6] = (Byte)((r >> 48) & 0xff);
        tmp[7] = (Byte)((r >> 56) & 0xff);
	    //lua_pushlstring(L, (const char *)tmp, 8);
    }

    private static void ldhexchange(Byte[] x, int sz, Byte[] buffer) 
    {
	    //size_t sz = 0;
	    //const uint8_t *x = (const uint8_t *)luaL_checklstring(L, 1, &sz);
        UInt32[] xx = new UInt32[2];
	    UInt64 r;
	    if (sz != 8) {
		    //luaL_error(L, "Invalid dh uint64 key");
		    return;
	    }

        xx[0] = ((UInt32)x[0]) | ((UInt32)x[1]) << 8 | ((UInt32)x[2]) << 16 | ((UInt32)x[3]) << 24;

        xx[1] = ((UInt32)x[4]) | ((UInt32)x[5]) << 8 | ((UInt32)x[6]) << 16 | ((UInt32)x[7]) << 24;

        UInt64 x64 = (UInt64)xx[0] | (UInt64)xx[1] << 32;
	    if (x64 == 0){
		    return;
	    }

	    r = powmodp(5, x64);
	    push64(buffer, r);
	    //return 1;
    }


    static void read64(Byte[] x, int xsize, Byte[] y, int ysize, UInt32[] xx, UInt32[] yy) 
    {
	    //size_t sz = 0;
	    //const uint8_t *x = (const uint8_t *)luaL_checklstring(L, 1, &sz);
	    if (xsize != 8) {
		    return;//luaL_error(L, "Invalid uint64 x");
	    }
	    //const uint8_t *y = (const uint8_t *)luaL_checklstring(L, 2, &sz);
	    if (ysize != 8) {
		    return;//luaL_error(L, "Invalid uint64 y");
	    }
        xx[0] = ((UInt32)x[0]) | ((UInt32)x[1]) << 8 | ((UInt32)x[2]) << 16 | ((UInt32)x[3]) << 24;
        xx[1] = ((UInt32)x[4]) | ((UInt32)x[5]) << 8 | ((UInt32)x[6]) << 16 | ((UInt32)x[7]) << 24;
        yy[0] = ((UInt32)y[0]) | ((UInt32)y[1]) << 8 | ((UInt32)y[2]) << 16 | ((UInt32)y[3]) << 24;
        yy[1] = ((UInt32)y[4]) | ((UInt32)y[5]) << 8 | ((UInt32)y[6]) << 16 | ((UInt32)y[7]) << 24;
    }

    static void pushqword(Byte[] tmp, UInt32[] result) 
    {
	    //uint8_t tmp[8];
        tmp[0] = (Byte)(result[0] & 0xff);
        tmp[1] = (Byte)((result[0] >> 8) & 0xff);
        tmp[2] = (Byte)((result[0] >> 16) & 0xff);
        tmp[3] = (Byte)((result[0] >> 24) & 0xff);
        tmp[4] = (Byte)(result[1] & 0xff);
        tmp[5] = (Byte)((result[1] >> 8) & 0xff);
        tmp[6] = (Byte)((result[1] >> 16) & 0xff);
        tmp[7] = (Byte)((result[1] >> 24) & 0xff);

	    //lua_pushlstring(L, (const char *)tmp, 8);
	    //return 1;
    }

    private static void ldhsecret(Byte[] one_text, int one_size, Byte[] two_text, int two_size, Byte[] buffer)
    {
        UInt32[] x = new UInt32[2];
        UInt32[] y = new UInt32[2];
        UInt64 r;

	    read64(one_text, one_size, two_text, two_size, x, y);

        UInt64 xx = (UInt64)x[0] | ((UInt64)x[1]) << 32;
        UInt64 yy = (UInt64)y[0] | ((UInt64)y[1]) << 32;
	    if (xx == 0 || yy == 0){
		    return;
	    }
	    r = powmodp(xx, yy);

	    push64(buffer, r);
    }


    /* platform-independant 32-bit integer manipulation macros */
    static void GET_UINT32(out UInt32 n, Byte[] b, Int32 i)
    {
        n = (((UInt32)b[i]) << 24) | (((UInt32)b[i + 1]) << 16) | (((UInt32)b[i + 2]) << 8) | (((UInt32)b[i + 3]));
    }

    static void PUT_UINT32(UInt32 n, Byte[] b, Int32 i)
    {
        (b)[(i)] = (Byte)((n) >> 24);
        (b)[(i) + 1] = (Byte)((n) >> 16);
        (b)[(i) + 2] = (Byte)((n) >> 8);
        (b)[(i) + 3] = (Byte)((n));
    }

    /* Initial Permutation macro */
    static void DES_IP(out UInt32 X, out UInt32 Y, out UInt32 T, UInt32 XX, UInt32 YY)
    {
        X = XX;
        Y = YY;
        T = ((X >> 4) ^ Y) & 0x0F0F0F0F; Y ^= T; X ^= (T << 4);
        T = ((X >> 16) ^ Y) & 0x0000FFFF; Y ^= T; X ^= (T << 16);
        T = ((Y >> 2) ^ X) & 0x33333333; X ^= T; Y ^= (T << 2);
        T = ((Y >> 8) ^ X) & 0x00FF00FF; X ^= T; Y ^= (T << 8);
        Y = ((Y << 1) | (Y >> 31)) & 0xFFFFFFFF;
        T = (X ^ Y) & 0xAAAAAAAA; Y ^= T; X ^= T;
        X = ((X << 1) | (X >> 31)) & 0xFFFFFFFF;
    }

    /* Final Permutation macro */
    static void DES_FP(out UInt32 X, out UInt32 Y, out UInt32 T, UInt32 XX, UInt32 YY)
    {
        X = XX;
        Y = YY;
        X = ((X << 31) | (X >> 1)) & 0xFFFFFFFF;
        T = (X ^ Y) & 0xAAAAAAAA; X ^= T; Y ^= T;
        Y = ((Y << 31) | (Y >> 1)) & 0xFFFFFFFF;
        T = ((Y >> 8) ^ X) & 0x00FF00FF; X ^= T; Y ^= (T << 8);
        T = ((Y >> 2) ^ X) & 0x33333333; X ^= T; Y ^= (T << 2);
        T = ((X >> 16) ^ Y) & 0x0000FFFF; Y ^= T; X ^= (T << 16);
        T = ((X >> 4) ^ Y) & 0x0F0F0F0F; Y ^= T; X ^= (T << 4);
    }

    /* DES round macro */
    static void DES_ROUND(out UInt32 X, out UInt32 Y, out UInt32 T, UInt32[] SK, UInt32 XX, UInt32 YY, UInt32 TT, int i)
    {
        X = XX;
        Y = YY;
        T = TT;

        T = SK[i] ^ X;
        Y ^= SB8[(T) & 0x3F] ^ SB6[(T >> 8) & 0x3F] ^ SB4[(T >> 16) & 0x3F] ^ SB2[(T >> 24) & 0x3F];

        T = SK[i+1] ^ ((X << 28) | (X >> 4));
        Y ^= SB7[(T) & 0x3F] ^ SB5[(T >> 8) & 0x3F] ^ SB3[(T >> 16) & 0x3F] ^ SB1[(T >> 24) & 0x3F];
    }


    /* DES key schedule */
    static void des_main_ks(UInt32[] SK, Byte[] key) 
    {
	    int i;
        UInt32 X, Y, T;

        GET_UINT32(out X, key, 0);
        GET_UINT32(out Y, key, 4);

	    /* Permuted Choice 1 */

	    T = ((Y >> 4) ^ X) & 0x0F0F0F0F;  X ^= T; Y ^= (T << 4);
	    T = ((Y) ^ X) & 0x10101010;  X ^= T; Y ^= (T);

	    X = (LHs[(X)& 0xF] << 3) | (LHs[(X >> 8) & 0xF] << 2)
		    | (LHs[(X >> 16) & 0xF] << 1) | (LHs[(X >> 24) & 0xF])
		    | (LHs[(X >> 5) & 0xF] << 7) | (LHs[(X >> 13) & 0xF] << 6)
		    | (LHs[(X >> 21) & 0xF] << 5) | (LHs[(X >> 29) & 0xF] << 4);

	    Y = (RHs[(Y >> 1) & 0xF] << 3) | (RHs[(Y >> 9) & 0xF] << 2)
		    | (RHs[(Y >> 17) & 0xF] << 1) | (RHs[(Y >> 25) & 0xF])
		    | (RHs[(Y >> 4) & 0xF] << 7) | (RHs[(Y >> 12) & 0xF] << 6)
		    | (RHs[(Y >> 20) & 0xF] << 5) | (RHs[(Y >> 28) & 0xF] << 4);

	    X &= 0x0FFFFFFF;
	    Y &= 0x0FFFFFFF;

	    /* calculate subkeys */

	    for (i = 0; i < 16; i++)
	    {
		    if (i < 2 || i == 8 || i == 15)
		    {
			    X = ((X << 1) | (X >> 27)) & 0x0FFFFFFF;
			    Y = ((Y << 1) | (Y >> 27)) & 0x0FFFFFFF;
		    }
		    else
		    {
			    X = ((X << 2) | (X >> 26)) & 0x0FFFFFFF;
			    Y = ((Y << 2) | (Y >> 26)) & 0x0FFFFFFF;
		    }

		    SK[i*2] = ((X << 4) & 0x24000000) | ((X << 28) & 0x10000000)
			    | ((X << 14) & 0x08000000) | ((X << 18) & 0x02080000)
			    | ((X << 6) & 0x01000000) | ((X << 9) & 0x00200000)
			    | ((X >> 1) & 0x00100000) | ((X << 10) & 0x00040000)
			    | ((X << 2) & 0x00020000) | ((X >> 10) & 0x00010000)
			    | ((Y >> 13) & 0x00002000) | ((Y >> 4) & 0x00001000)
			    | ((Y << 6) & 0x00000800) | ((Y >> 1) & 0x00000400)
			    | ((Y >> 14) & 0x00000200) | ((Y)& 0x00000100)
			    | ((Y >> 5) & 0x00000020) | ((Y >> 10) & 0x00000010)
			    | ((Y >> 3) & 0x00000008) | ((Y >> 18) & 0x00000004)
			    | ((Y >> 26) & 0x00000002) | ((Y >> 24) & 0x00000001);

		    SK[i*2 + 1] = ((X << 15) & 0x20000000) | ((X << 17) & 0x10000000)
			    | ((X << 10) & 0x08000000) | ((X << 22) & 0x04000000)
			    | ((X >> 2) & 0x02000000) | ((X << 1) & 0x01000000)
			    | ((X << 16) & 0x00200000) | ((X << 11) & 0x00100000)
			    | ((X << 3) & 0x00080000) | ((X >> 6) & 0x00040000)
			    | ((X << 15) & 0x00020000) | ((X >> 4) & 0x00010000)
			    | ((Y >> 2) & 0x00002000) | ((Y << 8) & 0x00001000)
			    | ((Y >> 14) & 0x00000808) | ((Y >> 9) & 0x00000400)
			    | ((Y)& 0x00000200) | ((Y << 7) & 0x00000100)
			    | ((Y >> 7) & 0x00000020) | ((Y >> 3) & 0x00000011)
			    | ((Y << 2) & 0x00000004) | ((Y >> 21) & 0x00000002);
	    }
    }


    static void des_key(Byte[] key, int keysz, UInt32[] SK) 
    {
	    if (keysz != 8) {
		    return;
	    }
	    des_main_ks(SK, key);
    }

    /* DES 64-bit block encryption/decryption */
    static void des_crypt(UInt32[] SK, Byte[] input, Byte[] output, int inputindex, int outputindex) 
    {
        UInt32 X, Y, T;

        GET_UINT32(out X, input, 0 + inputindex);
        GET_UINT32(out Y, input, 4 + inputindex);

        DES_IP(out X, out Y, out T, X, Y);

        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 0); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 2);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 4); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 6);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 8); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 10);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 12); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 14);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 16); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 18);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 20); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 22);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 24); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 26);
        DES_ROUND(out Y, out X, out T, SK, Y, X, T, 28); DES_ROUND(out X, out Y, out T, SK, X, Y, T, 30);

        DES_FP(out Y, out X, out T, Y, X);

        PUT_UINT32(Y, output, 0 + outputindex);
        PUT_UINT32(X, output, 4 + outputindex);
    }


    static void ldesencode(Byte[] key, int keysz, Byte[] text, int textsz, Byte[] buffer) 
    {
        UInt32[] SK = new UInt32[32];

	    //int chunksz = (textsz + 8) & ~7;
	    //uint8_t tmp[SMALL_CHUNK];
	    //uint8_t *buffer = tmp;
	    //if (chunksz > SMALL_CHUNK) {
	    //buffer = lua_newuserdata(L, chunksz);
	    //}
	    int i;
	    int bytes;
        Byte[] tail = new Byte[8];
	    int j;

	    des_key(key, keysz, SK);
	    for (i = 0; i < (int)textsz - 7; i += 8) {
            des_crypt(SK, text, buffer, i, i);
	    }
	    bytes = textsz - i;

	    for (j = 0; j < 8; j++) {
		    if (j < bytes) {
			    tail[j] = text[i + j];
		    }
		    else if (j == bytes) {
			    tail[j] = 0x80;
		    }
		    else {
			    tail[j] = 0;
		    }
	    }
	    des_crypt(SK, tail, buffer, 0, i);
    }

    private static void lhashkey(Byte[] fromKey, int sz, Byte[] realKey)
    {
        Hash(fromKey, (int)sz, realKey);
    }


    static int ldesdecode(Byte[] key, int keysz, Byte[] text, int textsz, Byte[] buffer) 
    {

        UInt32[] ESK = new UInt32[32];
        UInt32[] SK = new UInt32[32];

	    int i;
	    int padding = 1;

	    des_key(key, keysz, ESK);

	    for (i = 0; i < 32; i += 2) {
		    SK[i] = ESK[30 - i];
		    SK[i + 1] = ESK[31 - i];
	    }
	    //size_t textsz = 0;//const uint8_t *text = (const uint8_t *)luaL_checklstring(L, 2, &textsz);
	    if ((textsz & 7) > 0 || textsz == 0) {
		    return 0;
		    //return luaL_error(L, "Invalid des crypt text length %d", (int)textsz);
	    }
	    //uint8_t tmp[SMALL_CHUNK];//uint8_t *buffer = tmp;
	    //if (textsz > SMALL_CHUNK) {//buffer = lua_newuserdata(L, textsz);//}
	    for (i = 0; i < textsz; i += 8) {
            des_crypt(SK, text, buffer, i, i);
	    }

	    for (i = textsz - 1; i >= textsz - 8; i--) {
		    if (buffer[i] == 0) {
			    padding++;
		    }
		    else if (buffer[i] == 0x80) {
			    break;
		    }
		    else {
			    return 0;
			    //return luaL_error(L, "Invalid des crypt text");
		    }
	    }
	    if (padding > 8) {
		    return 0;
		    //return luaL_error(L, "Invalid des crypt text");
	    }
	    //lua_pushlstring(L, (const char *)buffer, textsz - padding);
        return padding;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------


    public static byte[] randomkey()
    {
        Byte[] dest = new Byte[8];

        int i;
        Byte x = 0;
        System.Random rd = new System.Random();
        for (i = 0; i < 8; i++)
        {
            dest[i] = (Byte)(rd.Next(0, 255) & 0xff);
            x ^= dest[i];
        }
        if (x == 0)
        {
            dest[0] |= 1;	// avoid 0
        }

        return dest;
    }


    public static byte[] dhexchange(byte[] x)
    {
        byte[] buffer = new byte[8];
        ldhexchange(x, x.Length, buffer);
        return buffer;
    }

    public static byte[] dhsecret(Byte[] one_text, Byte[] two_text)
    {
        Byte[] buffer = new byte[8];
        ldhsecret(one_text, one_text.Length, two_text, two_text.Length, buffer);
        return buffer;
    }

    public static byte[] hmac64(Byte[] one_text, Byte[] two_text)
    {
        byte[] buffer = new byte[8];
        lhmac64(one_text, one_text.Length, two_text, two_text.Length, buffer);
        return buffer;
    }

    public static byte[] desencode(Byte[] key, Byte[] text)
    {
        byte[] dest = new byte[(text.Length + 8) & ~7];
        ldesencode(key, key.Length, text, text.Length, dest);
        return dest;
    }

    public static byte[] desdecode(Byte[] key, Byte[] text)
    {
        byte[] dest = new byte[text.Length];
        int padding = ldesdecode(key, key.Length, text, text.Length, dest);
        Array.Resize<byte>(ref dest, dest.Length - padding);
        return dest;
    }

    public static byte[] hashkey(Byte[] fromKey)
    {
        Byte[] realKey = new byte[8];
        lhashkey(fromKey, fromKey.Length, realKey);
        return realKey;
    }


    public static string base64encode(string text)
    {
        string result = null;
        try
        {
            byte[] textByte = System.Text.Encoding.UTF8.GetBytes(text);
            result = Convert.ToBase64String(textByte);
        }
        catch
        {
            result = null;
        }
        return result;
    }

    public static byte[] base64encode(byte[] bytes)
    {
        byte[] result = null;
        try
        {
            string line = Convert.ToBase64String(bytes);
            result = System.Text.Encoding.UTF8.GetBytes(line);
        }
        catch
        {
            result = null;
        }
        return result;
    }

    public static string base64decode(string text)
    {
        string result = null;
        try
        {
            byte[] line = Convert.FromBase64String(text);
            result = System.Text.Encoding.UTF8.GetString(line);
        }
        catch
        {
            result = null;
        }
        return result;
    }

    public static byte[] base64decode(byte[] result)
    {
        byte[] bytes = null;
        try
        {
            string line = System.Text.Encoding.UTF8.GetString(result);
            bytes = Convert.FromBase64String(line);
        }
        catch
        {
            bytes = null;
        }
        return bytes;
    }


}
