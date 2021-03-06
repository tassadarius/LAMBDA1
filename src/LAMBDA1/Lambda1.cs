﻿using System;

namespace Lambda
{

    public enum OperationMode
    {
        Encrypt,
        Decrypt
    }

    /// <summary>
    /// The block cipher LAMBDA1 from East Germany
    /// </summary>
    /// Provides functions to encrypt and decrypt with the LAMBDA1 algorithm. A modified version of DES.
    /// 
    public class Lambda1
    {
        public const int BlockSize = 8;
        public const int KeySize = 32;
        public const int IVSize = 8;
        public const int BitsPerByte = 8;

        private UInt64[] _roundKeys;
        private readonly OperationMode _mode;

        private const UInt64 Mask = 0xFFFFFFFF;
        private const UInt64 One = 1;

        #region LAMBDA1-Tables

        // Expansion Function (E)
        private static readonly byte[] TableE = {
            32,  1,  2,  3,  4,  5,
            4,   5,  6,  7,  8,  9,
            8,   9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32,  1
        };

        // Permutation Function (P)
        private static readonly byte[] TableP = {
            16,  7, 20, 21,
            29, 12, 28, 17,
            1,  15, 23, 26,
            5,  18, 31, 10,
            2,   8, 24, 14,
            32, 27,  3,  9,
            19, 13, 30,  6,
            22, 11,  4, 25
        };


        // S-Boxes
        private static readonly byte[,] TableSBoxes = new byte[,] {
            {14,  4, 13,  1,     2, 15, 11,  8, 3, 10,  6, 12,   5,  9,  0,  7},
            { 0, 15,  7,  4,    14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8},
            { 4,  1, 14,  8,    13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0},
            {15, 12,  8,  2, 4,  9,  1,  7,   5, 11,  3, 14,    10,  0,  6, 13},

            {15, 1,  8, 14,  6, 11,  3,  4,  9, 7,   2, 13, 12,  0,  5, 10},
            {3, 13,  4,  7, 15,  2,  8, 14, 12, 0,   1, 10,  6,  9, 11,  5},
            {0, 14,  7, 11, 10,  4, 13,  1, 5, 8,   12,  6,  9,  3,  2, 15},
            {13, 8, 10,  1,  3, 15,  4,  2, 11, 6,   7, 12,  0,  5, 14,  9},

            {10,     0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8},
            {13,     7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1},
            {13,     6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7},
            {1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12},

            {7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15},
            {13,     8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9},
            {10,     6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4},
            {3, 15,  0,  6, 10,  1, 13,  8, 9,   4,  5, 11, 12,  7,  2, 14},

            {2, 12,  4,  1,  7, 10, 11, 6,  8,   5,  3, 15, 13,  0, 14,  9},
            {14,    11,  2, 12,  4,  7, 13,  1, 5,   0, 15, 10,  3,  9,  8,  6},
            {4,  2,  1, 11, 10, 13,  7,  8,15,   9, 12,  5,  6,  3,  0, 14},
            {11,     8, 12,  7,  1, 14,  2, 13, 6,  15,  0,  9, 10,  4,  5,  3},

            {12,     1, 10, 15,  9,  2,  6,  8,0,   13,  3,  4, 14,  7,  5, 11},
            {10,    15,  4,  2,  7, 12,  9,  5,6,    1, 13, 14,  0, 11,  3,  8},
            {9, 14, 15,  5,  2,  8, 12,  3,7,    0,  4, 10,  1, 13, 11,  6},
            {4,  3,  2, 12,  9,  5, 15, 10,11,  14,  1,  7,  6,  0,  8, 13},

            {4, 11,  2, 14, 15,  0,  8, 13, 3,  12,  9,  7,  5, 10,  6,  1},
            {13,     0, 11,  7,  4,  9,  1, 10,14,   3,  5, 12,  2, 15,  8,  6},
            {1,  4, 11, 13, 12,  3,  7, 14,10,  15,  6,  8,  0,  5,  9,  2},
            {6, 11, 13,  8,  1,  4, 10,  7,9,    5,  0, 15, 14,  2,  3, 12},

            {13,     2,  8,  4,  6, 15, 11,  1,10,   9,  3, 14,  5,  0, 12,  7},
            {1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2},
            {7, 11,  4,  1,  9, 12, 14,  2,0,    6, 10, 13, 15,  3,  5,  8},
            {2,  1, 14,  7,  4, 10,  8, 13,15,  12,  9,  0,  3,  5,  6, 11}
        };

        #endregion LAMBDA1-Tables

        /// <summary>
        /// Initialize LAMBDA1 by setting the keys and mode (encrypt or decrypt)
        /// </summary>
        /// <param name="key">a 32 byte array with the key</param>
        /// <param name="mode">an enum whether you want to encrypt or decrypt</param>
        public Lambda1(byte[] key, OperationMode mode)
        {
            if (!CheckKeys(key))
                throw new ArgumentException();

            this._mode = mode;
            SetKeys(key);
        }

        /// <summary>
        /// Check the key for validity
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool CheckKeys(byte[] key)
        {
            if (key == null)
                return false;
            if (key.Length != 32)
                return false;
            return true;
        }

        /// <summary>
        /// The key initialization function
        /// </summary>
        /// Splits the raw key into 18 round keys
        /// <param name="key">a raw 32 byte key</param>
        private void SetKeys(byte[] key)
        {
            _roundKeys = new UInt64[18];
            int keyCounter = 0;
            UInt64 tmp;

            // Keys 1 to 4
            for (int i = 0; i < 24; i += 6)
            {
                ByteToInt(key, i, 6, out tmp);
                _roundKeys[keyCounter++] = tmp;
            }

            // Keys 5 to 12
            for (; keyCounter < 12; ++keyCounter)
                rotateKey(_roundKeys[keyCounter - 4], out _roundKeys[keyCounter], 11);

            // Keys 13 to 16
            for (; keyCounter < 16; ++keyCounter)
                rotateKey(_roundKeys[(24 - keyCounter) - 1], out _roundKeys[keyCounter], 11);

            // Bonus keys 17 and 18
            ByteToInt(key, 24, 4, out tmp);
            _roundKeys[16] = tmp;
            ByteToInt(key, 28, 4, out tmp);
            _roundKeys[17] = tmp;

            /*
             * If we decrypt we have to invert the keys
             */
            if (_mode == OperationMode.Decrypt)
            {
                // First we invert the order of the normal keys 1 to 16
                for (int i = 0, k = 15; i < k; ++i, --k)
                {
                    tmp = _roundKeys[i];
                    _roundKeys[i] = _roundKeys[k];
                    _roundKeys[k] = tmp;
                }

                // then we invert the keys 17 and 18
                tmp = _roundKeys[16];
                _roundKeys[16] = ModularAddition(One, _roundKeys[17] ^ Mask);
                _roundKeys[17] = ModularAddition(One, tmp ^ Mask);
            }
        }


        /// <summary>
        /// Encrypt or decrypt a block
        /// </summary>
        /// <param name="block">input block</param>
        /// <param name="processedBlock">output block</param>
        public void ProcessBlock(byte[] block, out byte[] processedBlock)
        {
            processedBlock = new byte[8];

            UInt64 complBlock = 0, tmpBlock, tmpBit, rBlock, lBlock, rOld;
            int tableValue;

            // Convert byte[] to int
            // First byte (block[0]) should be most significant block in the int
            for (int i = 0, shiftFactor = 7; i < Lambda1.BlockSize; ++i, --shiftFactor)
            {
                complBlock |= ((UInt64)block[i]) << (8 * shiftFactor);   
            }

            lBlock = (complBlock >> 32) & 0xFFFFFFFF;
            rBlock = complBlock & 0xFFFFFFFF;
            
            for (int round = 0; round < 16; ++round)
            {

                rOld = rBlock;
                /*
                 * round permutation P
                 */
                tmpBlock = 0;
                for (int i = 0; i < TableP.Length; ++i)
                {
                    tableValue = TableP[i];
                    tmpBit = (rBlock >> (32 - tableValue)) & 1;
                    tmpBlock |= tmpBit << i;
                }

                rBlock = tmpBlock;

                /*
                 * expansion function E
                 */
                tmpBlock = 0;
                for (int i = 0; i < TableE.Length; ++i)
                {
                    tableValue = TableE[i];
                    tmpBit = (rBlock >> (32 - tableValue)) & 1;
                    tmpBlock |= tmpBit << i;
                }

                rBlock = tmpBlock;
                tmpBlock = 0;
                /*
                 * XOR result from expansion function with the key 
                 */
                rBlock ^= _roundKeys[round];

                /*
                 * s-boxes
                 */

                // i is for looping through the s-boxes
                // k is for calculating the offsets
                int row, col;
                for (int i = 0, k = 8; k > 0; --k, i += 4)
                {
                    // Calculate the row index
                    row = (int)(((rBlock >> ((k * 6) - 1)) << 1) | (rBlock >> ((k - 1) * 6) & 0x1));
                    row &= 0x3;

                    // Calculate the col (column) index
                    col = (int)((rBlock >> ((k * 6) - 5)) & 0xF);

                    // Get and set value from the s-boxes
                    // tmpBit is in this case more than One bit
                    tmpBit = TableSBoxes[i + row, col];
                    tmpBlock |= tmpBit << ((k - 1) * 4);
                }

                rBlock = tmpBlock ^ lBlock;
                lBlock = rOld;
                tmpBlock = 0;

                // In the 8th round (half of the rounds) we additionally add the bonus round keys
                // operation is a modular addition
                if (round == 7)
                {
                    lBlock = ModularAddition(lBlock, _roundKeys[16]);
                    rBlock = ModularAddition(rBlock, _roundKeys[17]);
                }

            }

            // Stitch together the block
            complBlock = rBlock << 32;
            complBlock |= lBlock;

            // Convert it to a byte[]
            for (int i = 0; i < Lambda1.BlockSize; ++i)
            {
                processedBlock[i] = (byte)((complBlock >> ((7 - i) * 8)) & 0xFF);
            }
        }

        /// <summary>
            /// Shift a key in a LFSR (most significant appended as least significant)
            /// </summary>
            /// <param name="key">a 48 bit round key</param>
            /// <param name="outputKey">the shifted 48 bit round key</param>
            /// <param name="rounds">number of rounds to shift</param>
            private void rotateKey(UInt64 key, out UInt64 outputKey, int rounds)
            {
                outputKey = 0;
                UInt64 tmp;
                for (int i = 0; i < rounds; ++i)
                {
                    tmp = key & 1;
                    key = (key >> 1) & 0xFFFFFFFFFFFF;
                    key |= (tmp << 47);
                }
                outputKey = key;
            }

            /// <summary>
            /// Converts a byte[] to an 64 bit unsigned integer. Maximum length allowed is 8.
            /// The most significant bit is the most significant bit of the first byte (byte[0])
            /// </summary>
            /// <param name="buffer">a byte[] which is converted</param>
            /// <param name="offset">the offset in the buffer</param>
            /// <param name="length">How many bytes should be converted from buffer</param>
            /// <param name="result">the result integer</param>
            /// <returns>true on success, false otherwise</returns>
            private void ByteToInt(byte[] buffer, int offset, int length, out UInt64 result)
        {
            result = 0;

            if (offset + length > buffer.Length)
                throw new ArgumentException("Offset + Length is greater than the actual buffer");

            if (length == 0)
                throw new ArgumentException("Length can't be 0");

            for (int i = offset, shiftFactor = 7 - (8 - length); i < offset + length; ++i, --shiftFactor)
            {
                result |= ((UInt64)buffer[i]) << (8 * shiftFactor);
            }
        }


        /// <summary>
        /// Modular addition of to 32 bit vectors
        /// </summary>
        /// <param name="a">filled with 32 bits </param>
        /// <param name="b"> filled with 32 bits</param>
        /// <returns>a and b added up together mod 2^32</returns>
        private UInt64 ModularAddition(UInt64 a, UInt64 b)
        {
            return (a + b) % 0x100000000;
        }
    }
}
