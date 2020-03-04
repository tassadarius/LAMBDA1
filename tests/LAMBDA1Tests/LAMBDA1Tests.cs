using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lambda;


namespace BlockLevelTests

{

    [TestClass]
    public class Tauschverfahren
    {
        enum Tests { Case1, Case2, Case3, Case4, Case5 };
        static Dictionary<Tests, byte[]> keystore = new Dictionary<Tests, byte[]>();


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            keystore[Tests.Case1] = KeyProvider.key1;
            keystore[Tests.Case2] = KeyProvider.key2;
            keystore[Tests.Case3] = KeyProvider.key3;
            keystore[Tests.Case4] = KeyProvider.key4;
            keystore[Tests.Case5] = KeyProvider.key1;

        }


        [TestMethod]
        public void Case1()
        {
            byte[] key, plainTextBlock, encryptedBlock, expectedBlock, decryptedBlock;
            key = keystore[Tests.Case1];
            plainTextBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x78, 0x78, 0x78, 0x78 };
            expectedBlock = new byte[] { 0x66, 0x60, 0x4A, 0x27, 0xFE, 0x8C, 0x05, 0x75 };

            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);

            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);
        }


        [TestMethod]
        public void Case2()
        {
            byte[] key, plainTextBlock, encryptedBlock, expectedBlock, decryptedBlock;
            key = keystore[Tests.Case2];
            plainTextBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x78, 0x78, 0x78, 0x78 };
            expectedBlock = new byte[] { 0xC3, 0x89, 0x22, 0x7B, 0xB0, 0x55, 0xF6, 0xA4 };

            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);

            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);
        }

        [TestMethod]
        public void Case3()
        {
            byte[] key, plainTextBlock, encryptedBlock, expectedBlock, decryptedBlock;
            key = keystore[Tests.Case3];
            plainTextBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x78, 0x78, 0x78, 0x78 };
            expectedBlock = new byte[] { 0x7A, 0x15, 0xF1, 0xCC, 0x6A, 0x59, 0xC2, 0x08 };

            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);

            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);
        }

        [TestMethod]
        public void Case4()
        {
            /*
             * Original Plain Text: "aller anfang ist sehr schwer doch hinterher ist man schlau";
             */
            byte[] key, plainTextBlock, encryptedBlock, expectedBlock, decryptedBlock;
            key = keystore[Tests.Case4];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x0D, 0x24, 0x81, 0x28, 0x40, 0xCC, 0x34, 0x33 };
            expectedBlock = new byte[] { 0x12, 0x29, 0x31, 0x5A, 0x6B, 0x4D, 0x45, 0xDE };
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x1A, 0x10, 0x61, 0x50, 0x10, 0x50, 0x54, 0x28 };
            expectedBlock = new byte[] { 0x58, 0x87, 0x58, 0x04, 0xDE, 0x27, 0x08, 0x29 };
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);

            /*
             * Block 3
             */
            plainTextBlock = new byte[] { 0x41, 0x4E, 0x51, 0x30, 0x4A, 0x10, 0x96, 0x0E };
            expectedBlock = new byte[] { 0x51, 0xC5, 0xA9, 0x38, 0x4D, 0x8E, 0xD5, 0x8F };
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);

            /*
            * Block 4
            */
            plainTextBlock = new byte[] { 0x50, 0x45, 0x06, 0x31, 0x00, 0x4A, 0x50, 0x12 };
            expectedBlock = new byte[] { 0xAD, 0xE9, 0x41, 0x04, 0x1A, 0x75, 0xC2, 0x8F };
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);

            /*
            * Block 5
            */
            plainTextBlock = new byte[] { 0x84, 0x18, 0x54, 0x04, 0x70, 0x33, 0x04, 0x14 };
            expectedBlock = new byte[] { 0xA4, 0x40, 0xB5, 0x00, 0x48, 0x27, 0x5F, 0x87 };
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);

            /*
            * Block 6
            */
            plainTextBlock = new byte[] { 0xE5, 0x12, 0x0C, 0x70, 0x4A, 0x00, 0x00, 0x00 };
            expectedBlock = new byte[] { 0x58, 0x9B, 0xB0, 0xA9, 0x1B, 0x7E, 0xC4, 0x41 };
            encryption_engine.ProcessBlock(plainTextBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, plainTextBlock);

        }
    }


    [TestClass]
    public class Additionsverfahren
    {
        enum Tests { Case5, Case6, Case7, Case8, Case9, Case10 };
        static Dictionary<Tests, byte[]> keystore = new Dictionary<Tests, byte[]>();


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

            keystore[Tests.Case5] = KeyProvider.key1;
            keystore[Tests.Case6] = KeyProvider.key2;
            keystore[Tests.Case7] = KeyProvider.key3;
            keystore[Tests.Case8] = KeyProvider.key5;
            keystore[Tests.Case9] = KeyProvider.key5;
        }

        [TestMethod]
        public void Case5()
        {
            /*
             * Original Plain Text: "mikroprozessor-technik";
             * Initialization Vector: 87878787 87878787
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case5];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x70, 0x63, 0xCA, 0x61, 0x62, 0x98, 0x44, 0x11 };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedEncryptedBlock = new byte[] { 0xCE, 0xAD, 0x09, 0xF6, 0x60, 0x49, 0x4C, 0x97 };
            expectedResultBlock = new byte[] { 0xBE, 0xCE, 0xC3, 0x97, 0x02, 0xD1, 0x08, 0x86 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x45, 0x60, 0xA8, 0xD0, 0x04, 0xE5, 0x0C, 0x18 };
            originalBlock = new byte[] { 0xCE, 0xAD, 0x09, 0xF6, 0x60, 0x49, 0x4C, 0x97 };
            expectedEncryptedBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedResultBlock = new byte[] { 0xC2, 0xE7, 0x2F, 0x57, 0x83, 0x62, 0x8B, 0x9F };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 3
             */
            plainTextBlock = new byte[] { 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedEncryptedBlock = new byte[] { 0xCE, 0xAD, 0x09, 0xF6, 0x60, 0x49, 0x4C, 0x97 };
            expectedResultBlock = new byte[] { 0x3E, 0xAD, 0x09, 0xF6, 0x60, 0x49, 0x4C, 0x97 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }



        [TestMethod]
        public void Case6()
        {
            /*
             * Original Plain Text: "mikroprozessor-technik"
             * Initialization Vector: 87878787 87878787
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case6];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x70, 0x63, 0xCA, 0x61, 0x62, 0x98, 0x44, 0x11 };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedEncryptedBlock = new byte[] { 0xF5, 0x22, 0x23, 0x60, 0x6C, 0x36, 0xD1, 0xBB };
            expectedResultBlock = new byte[] { 0x85, 0x41, 0xE9, 0x01, 0x0E, 0xAE, 0x95, 0xAA };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x45, 0x60, 0xA8, 0xD0, 0x04, 0xE5, 0x0C, 0x18 };
            originalBlock = new byte[] { 0xF5, 0x22, 0x23, 0x60, 0x6C, 0x36, 0xD1, 0xBB };
            expectedEncryptedBlock = new byte[] { 0xF1, 0xC8, 0x0A, 0xFC, 0x71, 0x57, 0x56, 0x6C };
            expectedResultBlock = new byte[] { 0xB4, 0xA8, 0xA2, 0x2C, 0x75, 0xB2, 0x5A, 0x74 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 3
             */
            plainTextBlock = new byte[] { 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0xF1, 0xC8, 0x0A, 0xFC, 0x71, 0x57, 0x56, 0x6C };
            expectedEncryptedBlock = new byte[] { 0x56, 0x19, 0x2E, 0xC7, 0x82, 0xE0, 0x53, 0xAE };
            expectedResultBlock = new byte[] { 0xA6, 0x19, 0x2E, 0xC7, 0x82, 0xE0, 0x53, 0xAE };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }

        [TestMethod]
        public void Case7()
        {
            /*
             * Original Plain Text: "mikroprozessor-technik"
             * Initialization Vector: 87878787 87878787
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case7];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x70, 0x63, 0xCA, 0x61, 0x62, 0x98, 0x44, 0x11 };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedEncryptedBlock = new byte[] { 0xE7, 0x50, 0xBB, 0x2D, 0xC3, 0xAD, 0xFE, 0xE7 };
            expectedResultBlock = new byte[] { 0x97, 0x33, 0x71, 0x4C, 0xA1, 0x35, 0xBA, 0xF6 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x45, 0x60, 0xA8, 0xD0, 0x04, 0xE5, 0x0C, 0x18 };
            originalBlock = new byte[] { 0xE7, 0x50, 0xBB, 0x2D, 0xC3, 0xAD, 0xFE, 0xE7 };
            expectedEncryptedBlock = new byte[] { 0x33, 0x31, 0xB1, 0x46, 0x9E, 0x45, 0x98, 0xFB };
            expectedResultBlock = new byte[] { 0x76, 0x51, 0x19, 0x96, 0x9A, 0xA0, 0x94, 0xE3 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 3
             */
            plainTextBlock = new byte[] { 0xF0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0x33, 0x31, 0xB1, 0x46, 0x9E, 0x45, 0x98, 0xFB };
            expectedEncryptedBlock = new byte[] { 0x7B, 0x12, 0x6C, 0x1F, 0x33, 0x81, 0x52, 0xBF };
            expectedResultBlock = new byte[] { 0x8B, 0x12, 0x6C, 0x1F, 0x33, 0x81, 0x52, 0xBF };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }

        [TestMethod]
        public void Case8()
        {
            /*
             * Original Plain Text: "mit der megatek 944 (bild unten) stellte cis & bil"
             * Initialization Vector: 87878787 87878787
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case8];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x70, 0x64, 0x04, 0x24, 0x12, 0x84, 0x70, 0x16 };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedEncryptedBlock = new byte[] { 0x87, 0xD4, 0x18, 0xF0, 0xFC, 0x28, 0x30, 0x7A };
            expectedResultBlock = new byte[] { 0xF7, 0xB0, 0x1C, 0xD4, 0xEE, 0xAC, 0x40, 0x6C };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x83, 0x40, 0x13, 0xC4, 0xE2, 0xAA, 0x84, 0xBD };
            originalBlock = new byte[] { 0x87, 0xD4, 0x18, 0xF0, 0xFC, 0x28, 0x30, 0x7A };
            expectedEncryptedBlock = new byte[] { 0x62, 0x0A, 0x96, 0xC1, 0x1A, 0x82, 0xFE, 0x2A };
            expectedResultBlock = new byte[] { 0xE1, 0x4A, 0x85, 0x05, 0xF8, 0x28, 0x7A, 0x97 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 3
             */
            plainTextBlock = new byte[] { 0x91, 0x92, 0x24, 0x41, 0xCC, 0x40, 0x13, 0x32 };
            originalBlock = new byte[] { 0x62, 0x0A, 0x96, 0xC1, 0x1A, 0x82, 0xFE, 0x2A };
            expectedEncryptedBlock = new byte[] { 0x64, 0x5B, 0x19, 0xD2, 0xAE, 0xA0, 0x7C, 0x00 };
            expectedResultBlock = new byte[] { 0xF5, 0xC9, 0x3D, 0x93, 0x62, 0xE0, 0x6F, 0x32 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 4
            */
            plainTextBlock = new byte[] { 0x10, 0x54, 0x01, 0x49, 0x24, 0x01, 0x10, 0xE1 };
            originalBlock = new byte[] { 0x64, 0x5B, 0x19, 0xD2, 0xAE, 0xA0, 0x7C, 0x00 };
            expectedEncryptedBlock = new byte[] { 0xFD, 0x31, 0x80, 0xDF, 0x69, 0xBA, 0x71, 0x7D };
            expectedResultBlock = new byte[] { 0xED, 0x65, 0x81, 0x96, 0x4D, 0xBB, 0x61, 0x9C };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 5
            */
            plainTextBlock = new byte[] { 0x85, 0x11, 0xA2, 0x83, 0x59, 0x41, 0x8F, 0x13 };
            originalBlock = new byte[] { 0xFD, 0x31, 0x80, 0xDF, 0x69, 0xBA, 0x71, 0x7D };
            expectedEncryptedBlock = new byte[] { 0xF4, 0x20, 0x68, 0x28, 0xFA, 0xE6, 0xCC, 0x09 };
            expectedResultBlock = new byte[] { 0x71, 0x31, 0xCA, 0xAB, 0xA3, 0xA7, 0x43, 0x1A };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 6
            */
            plainTextBlock = new byte[] { 0xD1, 0x19, 0x19, 0x22, 0x5E, 0x04, 0xA0, 0xCA };
            originalBlock = new byte[] { 0xF4, 0x20, 0x68, 0x28, 0xFA, 0xE6, 0xCC, 0x09 };
            expectedEncryptedBlock = new byte[] { 0x16, 0xC5, 0xB5, 0x2F, 0x8E, 0xCD, 0x28, 0xF1 };
            expectedResultBlock = new byte[] { 0xC7, 0xDC, 0xAC, 0x0D, 0xD0, 0xC9, 0x88, 0x3B };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 7
            */
            plainTextBlock = new byte[] { 0x64, 0x40, 0x46, 0x40, 0x73, 0x1A, 0x10, 0x31 };
            originalBlock = new byte[] { 0x16, 0xC5, 0xB5, 0x2F, 0x8E, 0xCD, 0x28, 0xF1 };
            expectedEncryptedBlock = new byte[] { 0x7E, 0x5C, 0xB0, 0x17, 0xC2, 0xB4, 0x29, 0xA9 };
            expectedResultBlock = new byte[] { 0x1A, 0x1C, 0xF6, 0x57, 0xB1, 0xAE, 0x39, 0x98 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 8
            */
            plainTextBlock = new byte[] { 0xCD, 0x10, 0x90, 0x4A, 0x10, 0x55, 0x45, 0x40 };
            originalBlock = new byte[] { 0x7E, 0x5C, 0xB0, 0x17, 0xC2, 0xB4, 0x29, 0xA9 };
            expectedEncryptedBlock = new byte[] { 0x36, 0xAB, 0x81, 0x70, 0x0F, 0x3F, 0x3C, 0x73 };
            expectedResultBlock = new byte[] { 0xFB, 0xBB, 0x11, 0x3A, 0x1F, 0x6A, 0x79, 0x33 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 9
            */
            plainTextBlock = new byte[] { 0x17, 0x05, 0x12, 0x6E, 0x04, 0x04, 0x63, 0x01 };
            originalBlock = new byte[] { 0x36, 0xAB, 0x81, 0x70, 0x0F, 0x3F, 0x3C, 0x73 };
            expectedEncryptedBlock = new byte[] { 0x54, 0x6D, 0xA3, 0xC0, 0xA5, 0x89, 0xCA, 0x91 };
            expectedResultBlock = new byte[] { 0x43, 0x68, 0xB1, 0xAE, 0xA1, 0x8D, 0xA9, 0x90 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 10
            */
            plainTextBlock = new byte[] { 0x10, 0xC0, 0x47, 0x04, 0x44, 0xD8, 0x28, 0xF1 };
            originalBlock = new byte[] { 0x54, 0x6D, 0xA3, 0xC0, 0xA5, 0x89, 0xCA, 0x91 };
            expectedEncryptedBlock = new byte[] { 0xF3, 0x3D, 0x1D, 0xEC, 0xF6, 0xF2, 0xA8, 0xDD };
            expectedResultBlock = new byte[] { 0xE3, 0xFD, 0x5A, 0xE8, 0xB2, 0x2A, 0x80, 0x2C };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 11
            */
            plainTextBlock = new byte[] { 0x50, 0x0D, 0x01, 0x98, 0x31, 0xA0, 0x4C, 0x04 };
            originalBlock = new byte[] { 0xF3, 0x3D, 0x1D, 0xEC, 0xF6, 0xF2, 0xA8, 0xDD };
            expectedEncryptedBlock = new byte[] { 0xAA, 0x35, 0xDA, 0x0C, 0x05, 0x9F, 0xEE, 0x86 };
            expectedResultBlock = new byte[] { 0xFA, 0x38, 0xDB, 0x94, 0x34, 0x3F, 0xA2, 0x82 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 12
            */
            plainTextBlock = new byte[] { 0xA0, 0xD0, 0x19, 0x83, 0x04, 0x34, 0x70, 0x4A };
            originalBlock = new byte[] { 0xAA, 0x35, 0xDA, 0x0C, 0x05, 0x9F, 0xEE, 0x86 };
            expectedEncryptedBlock = new byte[] { 0xD5, 0x99, 0xA1, 0xD2, 0x56, 0x63, 0x9A, 0x29 };
            expectedResultBlock = new byte[] { 0x75, 0x49, 0xB8, 0x51, 0x52, 0x57, 0xEA, 0x63 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 13
            */
            plainTextBlock = new byte[] { 0x10, 0x91, 0x81, 0x10, 0x13, 0x94, 0x41, 0x10 };
            originalBlock = new byte[] { 0xD5, 0x99, 0xA1, 0xD2, 0x56, 0x63, 0x9A, 0x29 };
            expectedEncryptedBlock = new byte[] { 0xB1, 0x9F, 0xFE, 0x21, 0xE1, 0x3F, 0xE2, 0xFC };
            expectedResultBlock = new byte[] { 0xA1, 0x0E, 0x7F, 0x31, 0xF2, 0xAB, 0xA3, 0xEC };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 14
            */
            plainTextBlock = new byte[] { 0x46, 0x42, 0x31, 0x46, 0x70, 0x74, 0x83, 0x40 };
            originalBlock = new byte[] { 0xB1, 0x9F, 0xFE, 0x21, 0xE1, 0x3F, 0xE2, 0xFC };
            expectedEncryptedBlock = new byte[] { 0x9A, 0x21, 0x0B, 0x22, 0x6C, 0xA0, 0x97, 0xA8 };
            expectedResultBlock = new byte[] { 0xDC, 0x63, 0x3A, 0x64, 0x1C, 0xD4, 0x14, 0xE8 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 15
            */
            plainTextBlock = new byte[] { 0x66, 0x0C, 0x11, 0xE6, 0x0A, 0xF0, 0x47, 0x01 };
            originalBlock = new byte[] { 0x9A, 0x21, 0x0B, 0x22, 0x6C, 0xA0, 0x97, 0xA8 };
            expectedEncryptedBlock = new byte[] { 0x91, 0x7E, 0xC3, 0x14, 0xC6, 0x0E, 0x77, 0xB9 };
            expectedResultBlock = new byte[] { 0xF7, 0x72, 0xD2, 0xF2, 0xCC, 0xFE, 0x30, 0xB8 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 16
            */
            plainTextBlock = new byte[] { 0x68, 0x34, 0x01, 0x3C, 0x41, 0x85, 0x40, 0x40 };
            originalBlock = new byte[] { 0x91, 0x7E, 0xC3, 0x14, 0xC6, 0x0E, 0x77, 0xB9 };
            expectedEncryptedBlock = new byte[] { 0x09, 0xE5, 0x29, 0x65, 0x97, 0xB3, 0xFA, 0x5A };
            expectedResultBlock = new byte[] { 0x61, 0xD1, 0x28, 0x59, 0xD6, 0x36, 0xBA, 0x1A };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 17
            */
            plainTextBlock = new byte[] { 0x46, 0x30, 0x41, 0xCC, 0x40, 0x12, 0x8C, 0x05 };
            originalBlock = new byte[] { 0x09, 0xE5, 0x29, 0x65, 0x97, 0xB3, 0xFA, 0x5A };
            expectedEncryptedBlock = new byte[] { 0xFA, 0x46, 0x88, 0x3F, 0xC5, 0xAA, 0xE6, 0x62 };
            expectedResultBlock = new byte[] { 0xBC, 0x76, 0xC9, 0xF3, 0x85, 0xB8, 0x6A, 0x67 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 18
            */
            plainTextBlock = new byte[] { 0x47, 0x01, 0x30, 0x42, 0x41, 0x28, 0x41, 0xCC };
            originalBlock = new byte[] { 0xFA, 0x46, 0x88, 0x3F, 0xC5, 0xAA, 0xE6, 0x62 };
            expectedEncryptedBlock = new byte[] { 0x81, 0x2C, 0x34, 0xF1, 0x0B, 0xA6, 0x56, 0x65 };
            expectedResultBlock = new byte[] { 0xC6, 0x2D, 0x04, 0xB3, 0x4A, 0x8E, 0x17, 0xA9 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 19
            */
            plainTextBlock = new byte[] { 0x19, 0x00, 0x49, 0x11, 0x00, 0x52, 0x04, 0xE6 };
            originalBlock = new byte[] { 0x81, 0x2C, 0x34, 0xF1, 0x0B, 0xA6, 0x56, 0x65 };
            expectedEncryptedBlock = new byte[] { 0x72, 0x2E, 0x2A, 0x87, 0xB7, 0xC1, 0xDD, 0x10 };
            expectedResultBlock = new byte[] { 0x6B, 0x2E, 0x63, 0x96, 0xB7, 0x93, 0xD9, 0xF6 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 20
            */
            plainTextBlock = new byte[] { 0x1C, 0x10, 0x73, 0x09, 0x11, 0x31, 0x8A, 0x24 };
            originalBlock = new byte[] { 0x72, 0x2E, 0x2A, 0x87, 0xB7, 0xC1, 0xDD, 0x10 };
            expectedEncryptedBlock = new byte[] { 0xFD, 0x65, 0x44, 0x3E, 0x51, 0x9B, 0xE6, 0xFC };
            expectedResultBlock = new byte[] { 0xE1, 0x75, 0x37, 0x37, 0x40, 0xAA, 0x6C, 0xD8 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 21
            */
            plainTextBlock = new byte[] { 0x47, 0x98, 0x30, 0x43, 0x86, 0x14, 0x40, 0x5D };
            originalBlock = new byte[] { 0xFD, 0x65, 0x44, 0x3E, 0x51, 0x9B, 0xE6, 0xFC };
            expectedEncryptedBlock = new byte[] { 0x60, 0xEC, 0x8F, 0xD7, 0x56, 0x5E, 0x55, 0xF9 };
            expectedResultBlock = new byte[] { 0x27, 0x74, 0xBF, 0x94, 0xD0, 0x4A, 0x15, 0xA4 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 22
            */
            plainTextBlock = new byte[] { 0x3D, 0x21, 0xC5, 0x19, 0xE1, 0x1E, 0x04, 0xA4 };
            originalBlock = new byte[] { 0x60, 0xEC, 0x8F, 0xD7, 0x56, 0x5E, 0x55, 0xF9 };
            expectedEncryptedBlock = new byte[] { 0xB9, 0x16, 0x4F, 0x74, 0x07, 0x2E, 0x99, 0xA6 };
            expectedResultBlock = new byte[] { 0x84, 0x37, 0x8A, 0x6D, 0xE6, 0x30, 0x9D, 0x02 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 23
            */
            plainTextBlock = new byte[] { 0x0A, 0x05, 0x00, 0x4C, 0xF0, 0x42, 0x47, 0x28 };
            originalBlock = new byte[] { 0xB9, 0x16, 0x4F, 0x74, 0x07, 0x2E, 0x99, 0xA6 };
            expectedEncryptedBlock = new byte[] { 0xC9, 0x7A, 0x6D, 0x6D, 0x3E, 0x88, 0x51, 0xD4 };
            expectedResultBlock = new byte[] { 0xC3, 0x7F, 0x6D, 0x21, 0xCE, 0xCA, 0x16, 0xFC };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 24
            */
            plainTextBlock = new byte[] { 0xE5, 0x04, 0x24, 0x60, 0x44, 0x18, 0xC4, 0x01 };
            originalBlock = new byte[] { 0xC9, 0x7A, 0x6D, 0x6D, 0x3E, 0x88, 0x51, 0xD4 };
            expectedEncryptedBlock = new byte[] { 0x57, 0x5A, 0x42, 0x80, 0xD1, 0xDB, 0x0C, 0x14 };
            expectedResultBlock = new byte[] { 0xB2, 0x5E, 0x66, 0xE0, 0x95, 0xC3, 0xC8, 0x15 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 25
            */
            plainTextBlock = new byte[] { 0x68, 0xa0, 0xd0, 0x19, 0x83, 0x04, 0x79, 0x83 };
            originalBlock = new byte[] { 0x57, 0x5A, 0x42, 0x80, 0xD1, 0xDB, 0x0C, 0x14 };
            expectedEncryptedBlock = new byte[] { 0x61, 0x94, 0x14, 0xDB, 0x3C, 0x54, 0xA1, 0xC2 };
            expectedResultBlock = new byte[] { 0x09, 0x34, 0xC4, 0xC2, 0xBF, 0x50, 0xD8, 0x41 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 26
            */
            plainTextBlock = new byte[] { 0x04, 0x15, 0x60, 0x51, 0x18, 0x14, 0x92, 0x04 };
            originalBlock = new byte[] { 0x61, 0x94, 0x14, 0xDB, 0x3C, 0x54, 0xA1, 0xC2 };
            expectedEncryptedBlock = new byte[] { 0x2C, 0xA3, 0x6A, 0x0B, 0x54, 0x89, 0xE6, 0xC1 };
            expectedResultBlock = new byte[] { 0x28, 0xB6, 0x0A, 0x5A, 0x4C, 0x9D, 0x74, 0xC5 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 27
            */
            plainTextBlock = new byte[] { 0xC1, 0x16, 0x29, 0x84, 0x41, 0x14, 0x56, 0x0A };
            originalBlock = new byte[] { 0x2C, 0xA3, 0x6A, 0x0B, 0x54, 0x89, 0xE6, 0xC1 };
            expectedEncryptedBlock = new byte[] { 0x92, 0xEE, 0xC8, 0xB3, 0x4E, 0xE4, 0x0E, 0x2D };
            expectedResultBlock = new byte[] { 0x53, 0xF8, 0xE1, 0x37, 0x0F, 0xF0, 0x58, 0x27 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 28
            */
            plainTextBlock = new byte[] { 0x04, 0xC1, 0x1E, 0x60, 0xC1, 0x13, 0x04, 0x64 };
            originalBlock = new byte[] { 0x92, 0xEE, 0xC8, 0xB3, 0x4E, 0xE4, 0x0E, 0x2D };
            expectedEncryptedBlock = new byte[] { 0xDF, 0xCC, 0x51, 0x77, 0x4A, 0x3D, 0xDE, 0xC6 };
            expectedResultBlock = new byte[] { 0xDB, 0x0D, 0x4F, 0x17, 0x8B, 0x2E, 0xDA, 0xA2 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 29
            */
            plainTextBlock = new byte[] { 0x01, 0x3E, 0xC1, 0x10, 0x05, 0xD0, 0xC5, 0x10 };
            originalBlock = new byte[] { 0xDF, 0xCC, 0x51, 0x77, 0x4A, 0x3D, 0xDE, 0xC6 };
            expectedEncryptedBlock = new byte[] { 0xD6, 0x4A, 0xBD, 0xE2, 0xB7, 0x10, 0x4C, 0xE8 };
            expectedResultBlock = new byte[] { 0xD7, 0x74, 0x7C, 0xF2, 0xB2, 0xC0, 0x89, 0xF8 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 30
            */
            plainTextBlock = new byte[] { 0x63, 0x05, 0x40, 0xA1, 0xDC, 0x04, 0xC4, 0x05 };
            originalBlock = new byte[] { 0xD6, 0x4A, 0xBD, 0xE2, 0xB7, 0x10, 0x4C, 0xE8 };
            expectedEncryptedBlock = new byte[] { 0x77, 0x32, 0x06, 0x12, 0x66, 0xF6, 0x65, 0x04 };
            expectedResultBlock = new byte[] { 0x14, 0x37, 0x46, 0xB3, 0xBA, 0xF2, 0xA1, 0x01 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 31
            */
            plainTextBlock = new byte[] { 0x10, 0x73, 0x09, 0x11, 0xC6, 0x10, 0x60, 0xA6 };
            originalBlock = new byte[] { 0x77, 0x32, 0x06, 0x12, 0x66, 0xF6, 0x65, 0x04 };
            expectedEncryptedBlock = new byte[] { 0xA9, 0xA7, 0x3F, 0xC8, 0xDC, 0x25, 0xED, 0x21 };
            expectedResultBlock = new byte[] { 0xB9, 0xD4, 0x36, 0xD9, 0x1A, 0x35, 0x8D, 0x87 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 32
            */
            plainTextBlock = new byte[] { 0x12, 0x0C, 0x41, 0x8C, 0x10, 0x11, 0x8C, 0x05 };
            originalBlock = new byte[] { 0xA9, 0xA7, 0x3F, 0xC8, 0xDC, 0x25, 0xED, 0x21 };
            expectedEncryptedBlock = new byte[] { 0x30, 0xD7, 0x89, 0xA2, 0x35, 0x9C, 0xFC, 0x76 };
            expectedResultBlock = new byte[] { 0x22, 0xDB, 0xC8, 0x2E, 0x25, 0x8D, 0x70, 0x73 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 33
            */
            plainTextBlock = new byte[] { 0xC1, 0x05, 0x40, 0x33, 0x09, 0x0C, 0xA2, 0x59 };
            originalBlock = new byte[] { 0x30, 0xD7, 0x89, 0xA2, 0x35, 0x9C, 0xFC, 0x76 };
            expectedEncryptedBlock = new byte[] { 0x0D, 0x6F, 0x11, 0x19, 0xC2, 0xD5, 0x37, 0x02 };
            expectedResultBlock = new byte[] { 0xCC, 0x6A, 0x51, 0x2A, 0xCB, 0xD9, 0x95, 0x5B };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 34
            */
            plainTextBlock = new byte[] { 0x1C, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0x0D, 0x6F, 0x11, 0x19, 0xC2, 0xD5, 0x37, 0x02 };
            expectedEncryptedBlock = new byte[] { 0x99, 0x48, 0xF7, 0x48, 0xDC, 0x41, 0xCF, 0xAC };
            expectedResultBlock = new byte[] { 0x85, 0x18, 0xF7, 0x48, 0xDC, 0x41, 0xCF, 0xAC };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }

        [TestMethod]
        public void Case9()
        {
            /*
             * Original Plain Text: "$%>*R%, 4R)>*";
             * Initialization Vector: 87878787 78787878
             *                                 \______/
             *                    careful here, it switches
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case9];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x02, 0x2A, 0x2B, 0x2A, 0x2B, 0x2A, 0x2B, 0x2A };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x78, 0x78, 0x78, 0x78 };
            expectedEncryptedBlock = new byte[] { 0x00, 0xDC, 0xEA, 0x9C, 0xE9, 0xC8, 0x46, 0x3D };
            expectedResultBlock = new byte[] { 0x02, 0xF6, 0xC1, 0xB6, 0xC2, 0xE2, 0x6D, 0x17 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0x00, 0xDC, 0xEA, 0x9C, 0xE9, 0xC8, 0x46, 0x3D };
            expectedEncryptedBlock = new byte[] { 0x80, 0x16, 0xF7, 0xCA, 0x2D, 0x4F, 0x42, 0xBA };
            expectedResultBlock = new byte[] { 0xAB, 0x16, 0xF7, 0xCA, 0x2D, 0x4F, 0x42, 0xBA };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }

    }

    [TestClass]
    public class Selbstregeneration
    {

        enum Tests { Case10, Case11 };
        static Dictionary<Tests, byte[]> keystore = new Dictionary<Tests, byte[]>();


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            keystore[Tests.Case10] = KeyProvider.key5;
            keystore[Tests.Case11] = KeyProvider.key5;
        }


        [TestMethod]
        public void Case10()
        {
            /*
             * Original Plain Text: "mit der megatek 944 (bild unten) stellte cis & bil";
             * Initialization Vector: 87878787 87878787
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case10];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x70, 0x64, 0x04, 0x24, 0x12, 0x84, 0x70, 0x16 };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87, 0x87 };
            expectedEncryptedBlock = new byte[] { 0x87, 0xD4, 0x18, 0xF0, 0xFC, 0x28, 0x30, 0x7A };
            expectedResultBlock = new byte[] { 0xF7, 0xB0, 0x1C, 0xD4, 0xEE, 0xAC, 0x40, 0x6C };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x83, 0x40, 0x13, 0xC4, 0xE2, 0xAA, 0x84, 0xBD };
            originalBlock = new byte[] { 0xF7, 0xB0, 0x1C, 0xD4, 0xEE, 0xAC, 0x40, 0x6C };
            expectedEncryptedBlock = new byte[] { 0xE1, 0xA6, 0xC1, 0x41, 0x93, 0xDB, 0x75, 0x4A };
            expectedResultBlock = new byte[] { 0x62, 0xE6, 0xD2, 0x85, 0x71, 0x71, 0xF1, 0xF7 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 3
             */
            plainTextBlock = new byte[] { 0x91, 0x92, 0x24, 0x41, 0xCC, 0x40, 0x13, 0x32 };
            originalBlock = new byte[] { 0x62, 0xE6, 0xD2, 0x85, 0x71, 0x71, 0xF1, 0xF7 };
            expectedEncryptedBlock = new byte[] { 0x13, 0xA2, 0x64, 0xBD, 0xE0, 0xC4, 0xD8, 0x1D };
            expectedResultBlock = new byte[] { 0x82, 0x30, 0x40, 0xFC, 0x2C, 0x84, 0xCB, 0x2F };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 4
            */
            plainTextBlock = new byte[] { 0x10, 0x54, 0x01, 0x49, 0x24, 0x01, 0x10, 0xE1 };
            originalBlock = new byte[] { 0x82, 0x30, 0x40, 0xFC, 0x2C, 0x84, 0xCB, 0x2F };
            expectedEncryptedBlock = new byte[] { 0xD1, 0x30, 0x10, 0xDB, 0x4F, 0x72, 0x35, 0x75 };
            expectedResultBlock = new byte[] { 0xC1, 0x64, 0x11, 0x92, 0x6B, 0x73, 0x25, 0x94 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);


            /*
            * Block 5
            */
            plainTextBlock = new byte[] { 0x85, 0x11, 0xA2, 0x83, 0x59, 0x41, 0x8F, 0x13 };
            originalBlock = new byte[] { 0xC1, 0x64, 0x11, 0x92, 0x6B, 0x73, 0x25, 0x94 };
            expectedEncryptedBlock = new byte[] { 0x26, 0x63, 0x0A, 0xFC, 0xEE, 0xD3, 0x35, 0x05 };
            expectedResultBlock = new byte[] { 0xA3, 0x72, 0xA8, 0x7F, 0xB7, 0x92, 0xBA, 0x16 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 6
            */
            plainTextBlock = new byte[] { 0xD1, 0x19, 0x19, 0x22, 0x5E, 0x04, 0xA0, 0xCA };
            originalBlock = new byte[] { 0xA3, 0x72, 0xA8, 0x7F, 0xB7, 0x92, 0xBA, 0x16 };
            expectedEncryptedBlock = new byte[] { 0x8D, 0xEA, 0x0B, 0xFC, 0x1F, 0x9C, 0xD4, 0xB8 };
            expectedResultBlock = new byte[] { 0x5C, 0xF3, 0x12, 0xDE, 0x41, 0x98, 0x74, 0x72 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 7
            */
            plainTextBlock = new byte[] { 0x64, 0x40, 0x46, 0x40, 0x73, 0x1A, 0x10, 0x31 };
            originalBlock = new byte[] { 0x5C, 0xF3, 0x12, 0xDE, 0x41, 0x98, 0x74, 0x72 };
            expectedEncryptedBlock = new byte[] { 0xBD, 0x93, 0x59, 0x85, 0xF2, 0x38, 0x34, 0x5C };
            expectedResultBlock = new byte[] { 0xD9, 0xD3, 0x1F, 0xC5, 0x81, 0x22, 0x24, 0x6D };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 8
            */
            plainTextBlock = new byte[] { 0xCD, 0x10, 0x90, 0x4A, 0x10, 0x55, 0x45, 0x40 };
            originalBlock = new byte[] { 0xD9, 0xD3, 0x1F, 0xC5, 0x81, 0x22, 0x24, 0x6D };
            expectedEncryptedBlock = new byte[] { 0xBD, 0xF7, 0x84, 0x15, 0x86, 0x9F, 0x83, 0x63 };
            expectedResultBlock = new byte[] { 0x70, 0xE7, 0x14, 0x5F, 0x96, 0xCA, 0xC6, 0x23 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 9
            */
            plainTextBlock = new byte[] { 0x17, 0x05, 0x12, 0x6E, 0x04, 0x04, 0x63, 0x01 };
            originalBlock = new byte[] { 0x70, 0xE7, 0x14, 0x5F, 0x96, 0xCA, 0xC6, 0x23 };
            expectedEncryptedBlock = new byte[] { 0x3D, 0xF0, 0xA9, 0x83, 0xF2, 0x8B, 0x68, 0x96 };
            expectedResultBlock = new byte[] { 0x2A, 0xF5, 0xBB, 0xED, 0xF6, 0x8F, 0x0B, 0x97 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 10
            */
            plainTextBlock = new byte[] { 0x10, 0xC0, 0x47, 0x04, 0x44, 0xD8, 0x28, 0xF1 };
            originalBlock = new byte[] { 0x2A, 0xF5, 0xBB, 0xED, 0xF6, 0x8F, 0x0B, 0x97 };
            expectedEncryptedBlock = new byte[] { 0x0B, 0x88, 0x26, 0xB3, 0x33, 0x7D, 0xA9, 0x5E };
            expectedResultBlock = new byte[] { 0x1B, 0x48, 0x61, 0xB7, 0x77, 0xA5, 0x81, 0xAF };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 11
            */
            plainTextBlock = new byte[] { 0x50, 0x0D, 0x01, 0x98, 0x31, 0xA0, 0x4C, 0x04 };
            originalBlock = new byte[] { 0x1B, 0x48, 0x61, 0xB7, 0x77, 0xA5, 0x81, 0xAF };
            expectedEncryptedBlock = new byte[] { 0x40, 0x3C, 0x0C, 0xD2, 0x65, 0x81, 0xCB, 0x40 };
            expectedResultBlock = new byte[] { 0x10, 0x31, 0x0D, 0x4A, 0x54, 0x21, 0x87, 0x44 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 12
             */
            plainTextBlock = new byte[] { 0xA0, 0xD0, 0x19, 0x83, 0x04, 0x34, 0x70, 0x4A };
            originalBlock = new byte[] { 0x10, 0x31, 0x0D, 0x4A, 0x54, 0x21, 0x87, 0x44 };
            expectedEncryptedBlock = new byte[] { 0xFA, 0xAD, 0xB4, 0x7A, 0x99, 0xC7, 0x32, 0xEA };
            expectedResultBlock = new byte[] { 0x5A, 0x7D, 0xAD, 0xF9, 0x9D, 0xF3, 0x42, 0xA0 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 13
             */
            plainTextBlock = new byte[] { 0x10, 0x91, 0x81, 0x10, 0x13, 0x94, 0x41, 0x10 };
            originalBlock = new byte[] { 0x5A, 0x7D, 0xAD, 0xF9, 0x9D, 0xF3, 0x42, 0xA0 };
            expectedEncryptedBlock = new byte[] { 0x89, 0x89, 0xEF, 0xE7, 0x5A, 0x2B, 0x16, 0x46 };
            expectedResultBlock = new byte[] { 0x99, 0x18, 0x6E, 0xF7, 0x49, 0xBF, 0x57, 0x56 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 14
            */
            plainTextBlock = new byte[] { 0x46, 0x42, 0x31, 0x46, 0x70, 0x74, 0x83, 0x40 };
            originalBlock = new byte[] { 0x99, 0x18, 0x6E, 0xF7, 0x49, 0xBF, 0x57, 0x56 };
            expectedEncryptedBlock = new byte[] { 0x73, 0xB6, 0xEA, 0xFE, 0x63, 0xB4, 0x43, 0x6C };
            expectedResultBlock = new byte[] { 0x35, 0xF4, 0xDB, 0xB8, 0x13, 0xC0, 0xC0, 0x2C };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);


            /*
            * Block 15
            */
            plainTextBlock = new byte[] { 0x66, 0x0C, 0x11, 0xE6, 0x0A, 0xF0, 0x47, 0x01 };
            originalBlock = new byte[] { 0x35, 0xF4, 0xDB, 0xB8, 0x13, 0xC0, 0xC0, 0x2C };
            expectedEncryptedBlock = new byte[] { 0x2A, 0xBA, 0xE2, 0x06, 0x9E, 0xD1, 0x3C, 0x00 };
            expectedResultBlock = new byte[] { 0x4C, 0xB6, 0xF3, 0xE0, 0x94, 0x21, 0x7B, 0x01 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 16
            */
            plainTextBlock = new byte[] { 0x68, 0x34, 0x01, 0x3C, 0x41, 0x85, 0x40, 0x40 };
            originalBlock = new byte[] { 0x4C, 0xB6, 0xF3, 0xE0, 0x94, 0x21, 0x7B, 0x01 };
            expectedEncryptedBlock = new byte[] { 0x7B, 0x99, 0x64, 0xA6, 0xB0, 0x3A, 0x3F, 0x81 };
            expectedResultBlock = new byte[] { 0x13, 0xAD, 0x65, 0x9A, 0xF1, 0xBF, 0x7F, 0xC1 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 17
            */
            plainTextBlock = new byte[] { 0x46, 0x30, 0x41, 0xCC, 0x40, 0x12, 0x8C, 0x05 };
            originalBlock = new byte[] { 0x13, 0xAD, 0x65, 0x9A, 0xF1, 0xBF, 0x7F, 0xC1 };
            expectedEncryptedBlock = new byte[] { 0x1F, 0x4C, 0x7B, 0x87, 0x6D, 0xFC, 0xE3, 0x8A };
            expectedResultBlock = new byte[] { 0x59, 0x7C, 0x3A, 0x4B, 0x2D, 0xEE, 0x6F, 0x8F };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 18
            */
            plainTextBlock = new byte[] { 0x47, 0x01, 0x30, 0x42, 0x41, 0x28, 0x41, 0xCC };
            originalBlock = new byte[] { 0x59, 0x7C, 0x3A, 0x4B, 0x2D, 0xEE, 0x6F, 0x8F };
            expectedEncryptedBlock = new byte[] { 0xC3, 0x15, 0x1D, 0x26, 0xF3, 0x0E, 0xD1, 0x74 };
            expectedResultBlock = new byte[] { 0x84, 0x14, 0x2D, 0x64, 0xB2, 0x26, 0x90, 0xB8 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 19
            */
            plainTextBlock = new byte[] { 0x19, 0x00, 0x49, 0x11, 0x00, 0x52, 0x04, 0xE6 };
            originalBlock = new byte[] { 0x84, 0x14, 0x2D, 0x64, 0xB2, 0x26, 0x90, 0xB8 };
            expectedEncryptedBlock = new byte[] { 0xC7, 0x3C, 0x83, 0x32, 0xC5, 0x32, 0x00, 0x18 };
            expectedResultBlock = new byte[] { 0xDE, 0x3C, 0xCA, 0x23, 0xC5, 0x60, 0x04, 0xFE };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 20
            */
            plainTextBlock = new byte[] { 0x1C, 0x10, 0x73, 0x09, 0x11, 0x31, 0x8A, 0x24 };
            originalBlock = new byte[] { 0xDE, 0x3C, 0xCA, 0x23, 0xC5, 0x60, 0x04, 0xFE };
            expectedEncryptedBlock = new byte[] { 0x09, 0x5B, 0x41, 0x94, 0x8E, 0x42, 0x26, 0x23 };
            expectedResultBlock = new byte[] { 0x15, 0x4B, 0x32, 0x9D, 0x9F, 0x73, 0xAC, 0x07 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 21
            */
            plainTextBlock = new byte[] { 0x47, 0x98, 0x30, 0x43, 0x86, 0x14, 0x40, 0x5D };
            originalBlock = new byte[] { 0x15, 0x4B, 0x32, 0x9D, 0x9F, 0x73, 0xAC, 0x07 };
            expectedEncryptedBlock = new byte[] { 0xEE, 0xBD, 0x5C, 0xD0, 0x30, 0xC8, 0x0B, 0xF4 };
            expectedResultBlock = new byte[] { 0xA9, 0x25, 0x6C, 0x93, 0xB6, 0xDC, 0x4B, 0xA9 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 22
             */
            plainTextBlock = new byte[] { 0x3D, 0x21, 0xC5, 0x19, 0xE1, 0x1E, 0x04, 0xA4 };
            originalBlock = new byte[] { 0xA9, 0x25, 0x6C, 0x93, 0xB6, 0xDC, 0x4B, 0xA9 };
            expectedEncryptedBlock = new byte[] { 0xDD, 0x9A, 0x82, 0x9F, 0x02, 0x3C, 0x16, 0xC8 };
            expectedResultBlock = new byte[] { 0xE0, 0xBB, 0x47, 0x86, 0xE3, 0x22, 0x12, 0x6C };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 23
             */
            plainTextBlock = new byte[] { 0x0A, 0x05, 0x00, 0x4C, 0xF0, 0x42, 0x47, 0x28 };
            originalBlock = new byte[] { 0xE0, 0xBB, 0x47, 0x86, 0xE3, 0x22, 0x12, 0x6C };
            expectedEncryptedBlock = new byte[] { 0xBD, 0xAB, 0xED, 0x1E, 0xF1, 0x97, 0xEC, 0x81 };
            expectedResultBlock = new byte[] { 0xB7, 0xAE, 0xED, 0x52, 0x01, 0xD5, 0xAB, 0xA9 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 24
            */
            plainTextBlock = new byte[] { 0xE5, 0x04, 0x24, 0x60, 0x44, 0x18, 0xC4, 0x01 };
            originalBlock = new byte[] { 0xB7, 0xAE, 0xED, 0x52, 0x01, 0xD5, 0xAB, 0xA9 };
            expectedEncryptedBlock = new byte[] { 0x40, 0x89, 0x6E, 0xE8, 0x3F, 0x23, 0x60, 0x11 };
            expectedResultBlock = new byte[] { 0xA5, 0x8D, 0x4A, 0x88, 0x7B, 0x3B, 0xA4, 0x10 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);


            /*
            * Block 25
            */
            plainTextBlock = new byte[] { 0x68, 0xA0, 0xD0, 0x19, 0x83, 0x04, 0x79, 0x83 };
            originalBlock = new byte[] { 0xA5, 0x8D, 0x4A, 0x88, 0x7B, 0x3B, 0xA4, 0x10 };
            expectedEncryptedBlock = new byte[] { 0x2C, 0x00, 0x82, 0x48, 0xC8, 0x5D, 0x81, 0x1E };
            expectedResultBlock = new byte[] { 0x44, 0xA0, 0x52, 0x51, 0x4B, 0x59, 0xF8, 0x9D };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 26
            */
            plainTextBlock = new byte[] { 0x04, 0x15, 0x60, 0x51, 0x18, 0x14, 0x92, 0x04 };
            originalBlock = new byte[] { 0x44, 0xA0, 0x52, 0x51, 0x4B, 0x59, 0xF8, 0x9D };
            expectedEncryptedBlock = new byte[] { 0x7D, 0x7A, 0x52, 0x34, 0x47, 0x22, 0xA5, 0xAF };
            expectedResultBlock = new byte[] { 0x79, 0x6F, 0x32, 0x65, 0x5F, 0x36, 0x37, 0xAB };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 27
            */
            plainTextBlock = new byte[] { 0xC1, 0x16, 0x29, 0x84, 0x41, 0x14, 0x56, 0x0A };
            originalBlock = new byte[] { 0x79, 0x6F, 0x32, 0x65, 0x5F, 0x36, 0x37, 0xAB };
            expectedEncryptedBlock = new byte[] { 0x18, 0x0E, 0x25, 0xA8, 0x9E, 0x8C, 0xAF, 0x9D };
            expectedResultBlock = new byte[] { 0xD9, 0x18, 0x0C, 0x2C, 0xDF, 0x98, 0xF9, 0x97 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 28
            */
            plainTextBlock = new byte[] { 0x04, 0xC1, 0x1E, 0x60, 0xC1, 0x13, 0x04, 0x64 };
            originalBlock = new byte[] { 0xD9, 0x18, 0x0C, 0x2C, 0xDF, 0x98, 0xF9, 0x97 };
            expectedEncryptedBlock = new byte[] { 0x7C, 0x5E, 0x65, 0xDF, 0xE1, 0x28, 0x06, 0x1A };
            expectedResultBlock = new byte[] { 0x78, 0x9F, 0x7B, 0xBF, 0x20, 0x3B, 0x02, 0x7E };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 29
            */
            plainTextBlock = new byte[] { 0x01, 0x3E, 0xC1, 0x10, 0x05, 0xD0, 0xC5, 0x10 };
            originalBlock = new byte[] { 0x78, 0x9F, 0x7B, 0xBF, 0x20, 0x3B, 0x02, 0x7E };
            expectedEncryptedBlock = new byte[] { 0x67, 0x28, 0x8E, 0x04, 0xA1, 0xC6, 0xF6, 0x9A };
            expectedResultBlock = new byte[] { 0x66, 0x16, 0x4F, 0x14, 0xA4, 0x16, 0x33, 0x8A };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 30
            */
            plainTextBlock = new byte[] { 0x63, 0x05, 0x40, 0xA1, 0xDC, 0x04, 0xC4, 0x05 };
            originalBlock = new byte[] { 0x66, 0x16, 0x4F, 0x14, 0xA4, 0x16, 0x33, 0x8A };
            expectedEncryptedBlock = new byte[] { 0xED, 0x31, 0x79, 0xDC, 0x48, 0x31, 0x16, 0xF5 };
            expectedResultBlock = new byte[] { 0x8E, 0x34, 0x39, 0x7D, 0x94, 0x35, 0xD2, 0xF0 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 31
            */
            plainTextBlock = new byte[] { 0x10, 0x73, 0x09, 0x11, 0xC6, 0x10, 0x60, 0xA6 };
            originalBlock = new byte[] { 0x8E, 0x34, 0x39, 0x7D, 0x94, 0x35, 0xD2, 0xF0 };
            expectedEncryptedBlock = new byte[] { 0x66, 0x24, 0xCC, 0xF3, 0x91, 0x50, 0xF7, 0xAF };
            expectedResultBlock = new byte[] { 0x76, 0x57, 0xC5, 0xE2, 0x57, 0x40, 0x97, 0x09 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 32
             */
            plainTextBlock = new byte[] { 0x12, 0x0C, 0x41, 0x8C, 0x10, 0x11, 0x8C, 0x05 };
            originalBlock = new byte[] { 0x76, 0x57, 0xC5, 0xE2, 0x57, 0x40, 0x97, 0x09 };
            expectedEncryptedBlock = new byte[] { 0x79, 0x9E, 0x47, 0x46, 0xE7, 0x0C, 0x8E, 0xBD };
            expectedResultBlock = new byte[] { 0x6B, 0x92, 0x06, 0xCA, 0xF7, 0x1D, 0x02, 0xB8 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 33
             */
            plainTextBlock = new byte[] { 0xC1, 0x05, 0x40, 0x33, 0x09, 0x0C, 0xA2, 0x59 };
            originalBlock = new byte[] { 0x6B, 0x92, 0x06, 0xCA, 0xF7, 0x1D, 0x02, 0xB8 };
            expectedEncryptedBlock = new byte[] { 0xEE, 0xCC, 0xB4, 0x29, 0xDD, 0xEC, 0x4A, 0x7D };
            expectedResultBlock = new byte[] { 0x2F, 0xC9, 0xF4, 0x1A, 0xD4, 0xE0, 0xE8, 0x24 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
            * Block 34
            */
            plainTextBlock = new byte[] { 0x1C, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0x2F, 0xC9, 0xF4, 0x1A, 0xD4, 0xE0, 0xE8, 0x24 };
            expectedEncryptedBlock = new byte[] { 0xFD, 0xFD, 0x57, 0xDC, 0x57, 0x5F, 0x55, 0x8B };
            expectedResultBlock = new byte[] { 0xE1, 0xAD, 0x57, 0xDC, 0x57, 0x5F, 0x55, 0x8B };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }

        [TestMethod]
        public void Case11()
        {
            /*
             * Original Plain Text: "$%>*R%4R)>";
             * Initialization Vector: 87878787 78787878
             *                                 \______/
             *                    careful here, it switches
             */
            byte[] key, plainTextBlock, originalBlock, expectedEncryptedBlock, expectedResultBlock,
                encryptedBlock, decryptedBlock, resultBlock;
            key = keystore[Tests.Case11];
            Lambda1 encryption_engine = new Lambda1(key, OperationMode.Encrypt);
            Lambda1 decryption_engine = new Lambda1(key, OperationMode.Decrypt);

            /*
             * Block 1
             */
            plainTextBlock = new byte[] { 0x02, 0x2A, 0x2B, 0x2A, 0x2B, 0x2A, 0x2B, 0x2A };
            originalBlock = new byte[] { 0x87, 0x87, 0x87, 0x87, 0x78, 0x78, 0x78, 0x78 };
            expectedEncryptedBlock = new byte[] { 0x00, 0xDC, 0xEA, 0x9C, 0xE9, 0xC8, 0x46, 0x3D };
            expectedResultBlock = new byte[] { 0x02, 0xF6, 0xC1, 0xB6, 0xC2, 0xE2, 0x6D, 0x17 };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);

            /*
             * Block 2
             */
            plainTextBlock = new byte[] { 0x2B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            originalBlock = new byte[] { 0x02, 0xF6, 0xC1, 0xB6, 0xC2, 0xE2, 0x6D, 0x17 };
            expectedEncryptedBlock = new byte[] { 0xC3, 0x13, 0x45, 0x63, 0x44, 0x5B, 0x07, 0xCB };
            expectedResultBlock = new byte[] { 0xE8, 0x13, 0x45, 0x63, 0x44, 0x5B, 0x07, 0xCB };
            encryption_engine.ProcessBlock(originalBlock, out encryptedBlock);
            decryption_engine.ProcessBlock(encryptedBlock, out decryptedBlock);
            resultBlock = UtilityFunctions.XORBlock(encryptedBlock, plainTextBlock);
            UtilityFunctions.AssertByteEqual(encryptedBlock, expectedEncryptedBlock);
            UtilityFunctions.AssertByteEqual(decryptedBlock, originalBlock);
            UtilityFunctions.AssertByteEqual(resultBlock, expectedResultBlock);
        }
    }
}
