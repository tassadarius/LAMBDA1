using System.Linq;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Lambda
{
    /// <summary>
    /// Provides a Mode of Operation to use LAMBDA1 with multiple blocks.
    /// </summary>
    /// 
    /// As was intended with the original algorithm Cipher Feedback Mode (CFB)
    /// with Zero-Padding is used.
    public class CipherFeedbackMode
    {
        private readonly byte[] key;
        private readonly int bSize = Lambda1.BlockSize;

        public CipherFeedbackMode(byte[] key)
        {
            this.key = key;
        }

        /// <summary>
        /// Encrypt multiple blocks of data in CFB (Cipher Feedback Mode) and Zero-Padding.
        /// </summary>
        /// <param name="input"> plaintext bytes </param>
        /// <param name="output"> Encrypted bytes whereas first 8 bytes are the initialization vector</param>
        public void EncryptData(byte[] input, out byte[] output)
        {
            PrepareData(ref input, out var inputBuffer, out output);
            byte[] encryptIn = new byte[bSize];

            var algorithm = new Lambda1(key, OperationMode.Encrypt);
            for (int i = 0, j = 1; j < output.Length / bSize; ++i, ++j)
            {
                CopyChunk(ref encryptIn, 0, ref output, i * bSize, bSize);
                algorithm.ProcessBlock(encryptIn, out var encryptOut);
                CopyChunk(ref output, j * bSize, ref encryptOut, 0, bSize);
                XorChunk(ref output, j * bSize, ref inputBuffer, j * bSize, bSize);
            }
        }

        /// <summary>
        /// Decrypt multiple blocks of data in CFB (Cipher Feedback Mode) and Zero-Padding.
        /// The padding is NOT stripped.
        /// </summary>
        /// <param name="input"> Encrypted buffer whereas first 8 bytes are the initialization vector </param>
        /// <param name="output"> Decrypted bytes </param>
        public void DecryptData(byte[] input, out byte[] output)
        {
            byte[] outputBuffer = new byte[input.Length - bSize];
            byte[] decryptIn = new byte[bSize];

            var algorithm = new Lambda1(key, OperationMode.Encrypt);
            for (int i = 0, j = 1; j < input.Length / bSize; ++i, ++j)
            {
                CopyChunk(ref decryptIn, 0, ref input, i * bSize, bSize);
                algorithm.ProcessBlock(decryptIn, out var decryptOut);
                CopyChunk(ref outputBuffer, i * bSize, ref decryptOut, 0, bSize);
                XorChunk(ref outputBuffer, i * bSize, ref input, j * bSize, bSize);
            }
            output = outputBuffer;

        }

        /// <summary>
        /// For encryption: Prepare the buffers for encryption - input buffer (plaintext) and output (encrypted)
        /// </summary>
        /// 
        /// Appends the initialization vector in the front (for both) and adds padding at the input buffer.
        /// <param name="data"> the to-encrypt data </param>
        /// <param name="preparedInputBuffer"> The input buffer gets padded, data filled in and iV put </param>
        /// <param name="preparedOutputBuffer"> The output buffer has the iV set and same length as padded input</param>
        private void PrepareData(ref byte[] data, out byte[] preparedInputBuffer, out byte[] preparedOutputBuffer)
        {
            // Calculate the Length of the new Block
            // The lengths consist of 3 parts:
            //   initializationVector    +    data      +        padding
            //   \__________________/         \__/               \_____/
            //      Block size (8)          n bytes        0 bytes for full block
            //         bSize              originalLength       missingBytes             
            int exceedingBytes = data.Length % bSize;
            int originalLength = data.Length;
            int missingBytes = 0;
            if (exceedingBytes > 0)
                missingBytes = bSize - exceedingBytes;
            int newLength = bSize + originalLength + missingBytes;

            // The array should be initialized with 0, so no need
            // to manually set the padding. We just concat the arrays
            byte[] padding = new byte[missingBytes];
            preparedInputBuffer = Enumerable.Concat(data, padding).ToArray();
            preparedOutputBuffer = new byte[preparedInputBuffer.Length];


            // Create initialization vector
            byte[] initVectorBuffer = new byte[bSize];
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            rng.GetBytes(initVectorBuffer);

            preparedInputBuffer = Enumerable.Concat(initVectorBuffer, preparedInputBuffer).ToArray();
            preparedOutputBuffer = Enumerable.Concat(initVectorBuffer, preparedOutputBuffer).ToArray();
        }


        /// <summary>
        /// Copy a part of an array into another array. Offsets can be specified respectively
        /// </summary>
        private void CopyChunk(ref byte[] destination, int destinationOffset,
            ref byte[] source, int sourceOffset, int length)
        {
            var cap = length + destinationOffset;
            for (int i = destinationOffset, j = sourceOffset; i < cap; ++i, ++j)
                destination[i] = source[j];
        }


        /// <summary>
        /// XOR an array onto another array. Result is in a, while b stays unchanged.
        /// Offsets can be specified respectively.
        /// </summary>
        private void XorChunk(ref byte[] a, int offset_a, ref byte[] b, int offset_b, int length)
        {
            Debug.Assert(offset_a + length <= a.Length);
            Debug.Assert(offset_b + length <= b.Length);
            for (int i = 0; i < length; ++i)
                a[offset_a + i] ^= b[offset_b + i];
        }
    }
}
