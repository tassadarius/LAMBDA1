# Test-Cases for LAMBDA1

Test-Cases for the encryption algorithm LAMBDA1 taken from the dokument MfS-Abt-XI-601-Lambda1.pdf
It consists of 31 pages containing test cases with plain text, key, the internal states of the algorithm per round and the round key.

Note that the modes do not matter that much. They are there for the sake of completeness.

## Testcase 1

	Mode:	    Tauschverfahren
	Plain Text: 39vu3139vu3 (not needed - I don't know why this is specified in the documents)
    Key:        00000000 00000000 00000000 0000000 00000000 00000000 00000000 0000000

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 87878787 78787878 | |
| Ciphertext | 66604A27 FE8C0575 | |

## Testcase 2

	Mode:	    Tauschverfahren
	Plain Text: 39vu3139vu3 (not needed - I don't know why this is specified in the documents)
    Key:        0000FFFF 0000FFFF 0000FFFF 0000FFFF 0000FFFF 0000FFFF 00000000 00000000

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 87878787 78787878 | |
| Ciphertext | C389227B B055F6A4 | |

## Testcase 3

	Mode:	    Tauschverfahren
	Plain Text: 39vu3139vu3 (not needed - I don't know why this is specified in the documents)
    Key:        0000FFFF 0000FFFF 0000FFFF 0000FFFF 0000FFFF 0000FFFF 00000000 00000000 

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Key | FFFF0000 FFFF0000 FFFF0000 FFFF0000 FFFF0000 FFFF0000 00000000 FFFFFFFF | |
| Plain Text | 87878787 78787878 | |
| Ciphertext | 7A15F1CC 6A59C208 | |

## Testcase 4 

	Mode:	    Tauschverfahren
    Plain Text: aller anfang ist sehr schwer doch hinterher ist man schlau
    Key:        0F1F2F3F 4F5F6F7F 8F9FAFBF CFDFEFFF 01234567 89ABCDEF FEDCBA98 76543210

### Part 1

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 0D248128 40CC3433 | |
| Ciphertext | 1229315A 6B4D45DE | |

### Part 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 1A106150 10505428 | |
| Ciphertext | 58875804 DE270829 | |

### Part 3 

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 414E5130 4A10960E | |
| Ciphertext | 51C5A938 4D8ED58F | |

### Part 4

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 50450631 004A5012 | |
| Ciphertext | ADE94104 1A75C28F | |

### Part 5 

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 84185404 70330414 | |
| Ciphertext | A440B500 48275F87 | |

### Part 6

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | E5120C70 4A000000 | |
| Ciphertext | 589BB0A9 1B7EC441 | |

## Testcase 5

	Mode:	    Additionsverfahren
    Plain Text: mikroprozessor-technik
    IV:         87878787 87878787
    Key:        00000000 00000000 00000000 0000000 00000000 00000000 00000000 0000000

### Part 1

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 7063CA61 62984411 |  |
| Original Block | 87878787 87878787 |  |
| Encrypted Block | CEAD09F6 60494C97 | |
| Ciphertext | BECEC397 02D10886 | |

### Part 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 4560A8D0 04E50C18 |  |
| Original Block | CEAD09F6 60494C97 |  |
| Encrypted Block | 87878787 87878787 | |
| Ciphertext | C2E72F57 83628B9F | |

### Part 3

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | F0000000 00000000 | |
| Original Block | 87878787 87878787 | |
| Encrypted Block | CEAD09F6 60494C97 | |
| Ciphertext | 3EAD09F6 60494C97 | |

## Testcase 6

	Mode:	    Additionsverfahren
    Plain Text: mikroprozessor-technik
    IV:         87878787 87878787
    Key:        0000FFFF 0000FFFF 0000FFFF 0000FFFF 0000FFFF 0000FFFF 00000000 00000000

### Part 1

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 7063CA61 62984411 |  |
| Original Block | 87878787 87878787 | |
| Encrypted Block | F5222360 6C36D1BB | |
| Ciphertext | 8541E901 0EAE95AA | |

### Part 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 4560A8D0 04E50C18 | |
| Original Block | F5222360 6C36D1BB | |
| Encrypted Block | F1C80AFC 7157566C | |
| Ciphertext | B4A8A22 C75B25A74 | |

### Part 3

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | F0000000 00000000 | |
| Original Block | F1C80AFC 7157566C | |
| Encrypted Block | 56192EC7 82E053AE | |
| Ciphertext | A6192EC7 82E053AE | |

## Testcase 7

	Mode:	    Additionsverfahren
    Plain Text: mikroprozessor-technik
    IV:         87878787 87878787
    Key:        FFFF0000 FFFF0000 FFFF0000 FFFF0000 FFFF0000 FFFF0000 00000000 FFFFFFFF

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 7063CA61 62984411 | |
| Original Block | 87878787 87878787 | |
| Encrypted Block | E750BB2D C3ADFEE7 | |
| Ciphertext | 9733714C A135BAF6 | |

### Part 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 4560A8D0 04E50C18 | |
| Original Block | E750BB2D C3ADFEE7 | |
| Encrypted Block | 3331B146 9E4598FB | |
| Ciphertext | 76511996 9AA094E3 | |

### Part 3

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | F0000000 00000000 | |
| Original Block | 3331B146 9E4598FB | |
| Encrypted Block | 7B126C1F 338152BF | |
| Ciphertext | 8B126C1F 338152BF | |

## Testcase 8

	Mode:	    Additionsverfahren
    Plain Text: mit der megatek 944 (bild unten) stellte cis & bil 
    IV:         87878787 87878787
    Key:        01234567 89ABCDEF FEDCBA98 76543210 01234567 89ABCDEF FEDCBA98 76543210

### Block 1

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 70640424 12847016 | |
| Original Block | 87878787 87878787 | |
| Encrypted Block | 87D418F0 FC28307A | |
| Ciphertext | F7B01CD4 EEAC406C | |

### Block 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 834013C4 E2AA84BD | |
| Original Block | 87D418F0 FC28307A | |
| Encrypted Block | 620A96C1 1A82FE2A | |
| Ciphertext | E14A8505 F8287A97 | |

### Block 3

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 91922441 CC401332 | |
| Original Block | 620A96C1 1A82FE2A | |
| Encrypted Block | 645B19D2 AEA07C00 | |
| Ciphertext | F5C93D93 62E06F32 | |

### Block 4

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 10540149 240110E1 | |
| Original Block | 645B19D2 AEA07C00 | |
| Encrypted Block | FD3180DF 69BA717D | |
| Ciphertext | ED658196 4DBB619C | |

### Block 5

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 8511A283 59418F13 | |
| Original Block | FD3180DF 69BA717D | |
| Encrypted Block | F4206828 FAE6CC09 | |
| Ciphertext | 7131CAAB A3A7431A | |

### Block 6 

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | D1191922 5E04A0CA | |
| Original Block | F4206828 FAE6CC09 | |
| Encrypted Block | 16C5B52F 8ECD28F1 | |
| Ciphertext | C7DCAC0D D0C9883B | |

### Block 7

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 64404640 731A1031 | |
| Original Block | 16C5B52F 8ECD28F1 | |
| Encrypted Block | 7E5CB017 C2B429A9 | |
| Ciphertext | 1A1CF657 B1AE3998 | |

### Block 8

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | CD10904A 10554540 | |
| Original Block | 7E5CB017 C2B429A9 | |
| Encrypted Block | 36AB8170 0F3F3C73 | |
| Ciphertext | FBBB113A 1F6A7933 | |

### Block 9

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 1705126E 04046301 | |
| Original Block | 36AB8170 0F3F3C73 | |
| Encrypted Block | 546DA3C 0A589CA91 | |
| Ciphertext | 4368B1AE A18DA990 | |

### Block 10 

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 10C04704 44D828F1 | |
| Original Block | 546DA3C 0A589CA91 | |
| Encrypted Block | F33D1DEC F6F2A8DD | |
| Ciphertext | E3FD5AE8 B22A802C | |

### Block 11

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 500D0198 31A04C04 | |
| Original Block | F33D1DEC F6F2A8DD | |
| Encrypted Block | AA35DA0C 059FEE86 | |
| Ciphertext | FA38DB94 343FA282 | |

### Block 12

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | A0D01983 0434704A | |
| Original Block | AA35DA0C 059FEE86 | |
| Encrypted Block | D599A1D2 56639A29 | |
| Ciphertext | 7549B851 5257EA63 | |

### Block 13

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 10918110 13944110 | |
| Original Block | D599A1D2 56639A29 | |
| Encrypted Block | B19FFE21 E13FE2FC | |
| Ciphertext | A10E7F31 F2ABA3EC | |

### Block 14

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 46423146 70748340  | |
| Original Block | B19FFE21 E13FE2FC | |
| Encrypted Block | 9A210B22 6CA097A8 | |
| Ciphertext | DC633A64 1CD414E8 | |

### Block 15

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 660C11E6 0AF04701 | |
| Original Block | 9A210B22 6CA097A8 | |
| Encrypted Block | 917EC314 C60E77B9 | |
| Ciphertext | F772D2F2 CCFE30B8 | |

### Block 16

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 6834013C 41854040 | |
| Original Block | 917EC314 C60E77B9 | |
| Encrypted Block | 09E52965 97B3FA5A | |
| Ciphertext | 61D12859 D636BA1A | |

### Block 17

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 463041CC 40128C05 | |
| Original Block | 09E52965 97B3FA5A | |
| Encrypted Block | FA46883F C5AAE662 | |
| Ciphertext | BC76C9F3 85B86A67 | |

### Block 18

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 47013042 412841CC | |
| Original Block | FA46883F C5AAE662 | |
| Encrypted Block | 812C34F1 0BA65665 | |
| Ciphertext | C62D04B3 4A8E17A9 | |

### Block 19

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 19004911 005204E6 | |
| Original Block | 812C34F1 0BA65665 | |
| Encrypted Block | 722E2A87 B7C1DD10 | |
| Ciphertext | 6B2E6396 B793D9F6 | |

### Block 20

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 1C107309 11318A24 | |
| Original Block | 722E2A87 B7C1DD10 | |
| Encrypted Block | FD65443E 519BE6FC | |
| Ciphertext | E1753737 40AA6CD8 | |

### Block 21

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 47983043 8614405D |  |
| Original Block | FD65443E 519BE6FC | |
| Encrypted Block | 60EC8FD7 565E55F9 | |
| Ciphertext | 2774BF94 D04A15A4 | |

### Block 22

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 3D21C519 E11E04A4 |  |
| Original Block | 60EC8FD7 565E55F9 | |
| Encrypted Block | B9164F74 072E99A6 | |
| Ciphertext | 84378A6D E6309D02 | |

### Block 23

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 0A05004 CF0424728 |  |
| Original Block | B9164F74 072E99A6 | |
| Encrypted Block | C97A6D6D 3E8851D4 | |
| Ciphertext | C37F6D21 CECA16FC | |

### Block 24

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | E5042460 4418C401 |  |
| Original Block | C97A6D6D 3E8851D4 | |
| Encrypted Block | 575A4280 D1DB0C14 | |
| Ciphertext | B25E66E0 95C3C815 | |

### Block 25

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 68a0d019 83047983 |  |
| Original Block | 575A4280 D1DB0C14 | |
| Encrypted Block | 619414DB 3C54A1C2 | |
| Ciphertext | 0934C4C2 BF50D841 | |

### Block 26

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 04156051 18149204 |  |
| Original Block | 619414DB 3C54A1C2 | |
| Encrypted Block | 2CA36A0B 5489E6C1 | |
| Ciphertext | 28B60A5A 4C9D74C5 | |

### Block 27

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | C1162984 4114560A |  |
| Original Block | 2CA36A0B 5489E6C1 | |
| Encrypted Block | 92EEC8B3 4EE40E2D | |
| Ciphertext | 53F8E137 0FF05827 | |

### Block 28

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 04C11E60 C1130464 |  |
| Original Block | 92EEC8B3 4EE40E2D | |
| Encrypted Block | DFCC5177 4A3DDEC6 | |
| Ciphertext | DB0D4F17 8B2EDAA2 | |

### Block 29

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 013EC110 05D0C510 |  |
| Original Block | DFCC5177 4A3DDEC6 | |
| Encrypted Block | D64ABDE2 B7104CE8 | |
| Ciphertext | D7747CF2 B2C089F8 | |

### Block 30

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 630540A1 DC04C405 |  |
| Original Block | D64ABDE2 B7104CE8 | |
| Encrypted Block | 77320612 66F66504 | |
| Ciphertext | 143746B3 BAF2A101 | |

### Block 31

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 10730911 C61060A6 |  |
| Original Block | 77320612 66F66504 | |
| Encrypted Block | A9A73FC8 DC25ED21 | |
| Ciphertext | B9D436D9 1A358D87 | |

### Block 32

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 120C418C 10118C05 |  |
| Original Block | A9A73FC8 DC25ED21 | |
| Encrypted Block | 30D789A 2359CFC76 | |
| Ciphertext | 22DBC82E 258D7073 | |

### Block 33

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | C1054033 090CA259 |  |
| Original Block | 30D789A 2359CFC76 | |
| Encrypted Block | 0D6F1119 C2D53702 | |
| Ciphertext | CC6A512A CBD9955B | |

### Block 34

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 1C500000 00000000 |  |
| Original Block | 0D6F1119 C2D53702 | |
| Encrypted Block | 9948F748 DC41CFAC | |
| Ciphertext | 8518F748 DC41CFAC | |

## Testcase 9

	Mode:	    Additionsverfahren
    Plain Text: $%>*R%, 4R)>*
    IV:         87878787 78787878
    Key:        0123456789ABCDEF FEDCBA9876543210 0123456789ABCDEF FEDCBA9876543210

### Block 1

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 022A2B2A 2B2A2B2A | |
| Original Block | 87878787 78787878 | |
| Encrypted Block | 00DCEA9C E9C8463D | |
| Ciphertext | 02F6C1B6 C2E26D17 | |

### Block 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 2B000000 00000000 | |
| Original Block | 00DCEA9C E9C8463D | |
| Encrypted Block | 8016F7CA 2D4F42BA | |
| Ciphertext | AB16F7CA 2D4F42BA | |

## Testcase 10

	Mode:		Selbstregeneration
	Plain Text: mit der megatek 944 (bild unten) stellte cis & bil 
    IV:         87878787 87878787
    Key:        01234567 89ABCDEF FEDCBA98 76543210 01234567 89ABCDEF FEDCBA98 76543210

### Block 1

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 70640424 12847016 |  |
| Encrypted Block | 87D418F0 FC28307A | |
| Ciphertext | F7B01CD4 EEAC406C | |

### Block 2

| Element | Value in hex | Additional Encoding |
|----|----|----|
| Plain Text | 834013C4 E2AA84BD |  |
| Encrypted Block | E1A6C141 93DB754A | |
| Ciphertext | 62E6D285 7171F1F7 | |

### Block 3

| Element | Value in hex
|----|----|
| Plain Text      | 91922441 CC401332 |
| Encrypted Block | 13A264BD E0C4D81D |
| Ciphertext      | 823040FC 2C84CB2F |

### Block 4

| Element | Value in hex
|----|----|
| Plain Text      | 10540149 240110E1 |
| Encrypted Block | D13010DB 4F723575 |
| Ciphertext      |  |

### Block 5

| Element | Value in hex
|----|----|
| Plain Text      | 8511A283 59418F13 |
| Encrypted Block | 26630AFC EED33505 |
| Ciphertext      | A372A87F B792BA16 |

### Block 6

| Element | Value in hex
|----|----|
| Plain Text      | D1191922 5E04A0CA |
| Encrypted Block | 8DEA0BFC 1F9CD4B8 |
| Ciphertext      | 5CF312DE 41987472 |

### Block 7

| Element | Value in hex
|----|----|
| Plain Text      | 64404640 731A1031 |
| Encrypted Block | BD935985 F238345C |
| Ciphertext      | D9D31FC5 8122246D |

### Block 8

| Element | Value in hex
|----|----|
| Plain Text      | CD10904A 10554540 |
| Encrypted Block | BDF78415 869F8363 |
| Ciphertext      | 70E7145F 96CAC623 |

### Block 9

| Element | Value in hex
|----|----|
| Plain Text      | 1705126E 04046301 |
| Encrypted Block | 3DF0A983 F28B6896 |
| Ciphertext      | 2AF5BBED F68F0B97 |

### Block 10                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 10C04704 44D828F1 |
| Encrypted Block | 0B8826B3 337DA95E |
| Ciphertext      | 1B4861B7 77A581AF |

### Block 11                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 500D0198 31A04C04 |
| Encrypted Block | 403C0CD2 6581CB40 |
| Ciphertext      | 10310D4A 54218744 |

### Block 12                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | A0D01983 0434704A |
| Encrypted Block | FAADB47A 99C732EA |
| Ciphertext      | 5A7DADF9 9DF342A0 |

### Block 13                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 10918110 13944110 |
| Encrypted Block | 8989EFE7 5A2B1646 |
| Ciphertext      | 99186EF7 49BF5756 |

### Block 14                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 46423146 70748340 |
| Encrypted Block | 73B6EAFE 63B4436C |
| Ciphertext      | 35F4DBB8 13C0C02C |

### Block 15                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 660C11E6 0AF04701 |
| Encrypted Block | 2ABAE206 9ED13C00 |
| Ciphertext      | 4CB6F3E0 94217B01 |

### Block 16                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 6834013C 41854040 |
| Encrypted Block | 7B9964A6 B03A3F81 |
| Ciphertext      | 13AD659A F1BF7FC1 |

### Block 17                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 463041CC 40128C05 |
| Encrypted Block | 1F4C7B87 6DFCE38A |
| Ciphertext      | 597C3A4B 2DEE6F8F |

### Block 18                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 47013042 412841CC |
| Encrypted Block | C3151D26 F30ED174 |
| Ciphertext      | 84142D64 B22690B8 |

### Block 19                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 19004911 005204e6 |
| Encrypted Block | C73C8332 C5320018 |
| Ciphertext      | DE3CCA23 C56004FE |

### Block 20                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 1C107309 11318A24 |
| Encrypted Block | 095B4194 8E422623 |
| Ciphertext      | 154B329D 9F73AC07 |

### Block 21                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 47983043 8614405D |
| Encrypted Block | EEBD5CD0 30C80BF4 |
| Ciphertext      | A9256C93 B6DC4BA9 |

### Block 22                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 3D21C519 E11E04A4 |
| Encrypted Block | DD9A829F 023C16C8 |
| Ciphertext      | E0BB4786 E322126C |

### Block 23                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 0A05004C F0424728 |
| Encrypted Block | BDABED1E F197EC81 |
| Ciphertext      | B7AEED52 01D5ABA9 |

### Block 24                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | E5042460 4418C401 |
| Encrypted Block | 40896EE8 3F236011 |
| Ciphertext      | A58D4A88 7B3BA410 |

### Block 25                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 68A0D019 83047983 |
| Encrypted Block | 2C008248 C85D811E |
| Ciphertext      | 44A05251 4B59F89D |

### Block 26                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 04156051 18149204 |
| Encrypted Block | 7D7A5234 4722A5AF |
| Ciphertext      | 796F3265 5F3637AB |

### Block 27                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | C1162984 4114560A |
| Encrypted Block | 180E25A8 9E8CAF9D |
| Ciphertext      | D9180C2C DF98F997 |

### Block 28                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 04C11E60 C1130464 |
| Encrypted Block | 7C5E65DF E128061A |
| Ciphertext      | 789F7BBF 203B027E |

### Block 29                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 013EC110 05D0C510 |
| Encrypted Block | 67288E04 A1C6F69A |
| Ciphertext      | 66164F14 A416338A |

### Block 30                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 630540A1 DC04C405 |
| Encrypted Block | ED3179DC 483116F5 |
| Ciphertext      | 8E34397D 9435D2F0 |

### Block 31                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 10730911 C61060A6 |
| Encrypted Block | 6624CCF3 9150F7AF |
| Ciphertext      | 7657C5E2 57409709 |

### Block 32                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 120C418C 10118C05 |
| Encrypted Block | 799E4746 E70C8EBD |
| Ciphertext      | 6b9206ca f71d02b8 |

### Block 33                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | C1054033 090CA259 |
| Encrypted Block | EECCB429 DDEC4A7D |
| Ciphertext      | 2FC9F41A D4E0E824 |

### Block 34                 

| Element | Value in hex     
|----|----|                  
| Plain Text      | 1C500000 00000000 |
| Encrypted Block | FDFD57DC 575F558B |
| Ciphertext      | E1AD57DC 575F558B |

## Testcase 11

	Mode:		Selbstregeneration
	Plain Text: $%>*R%,4R)>*
    IV:         87878787 87878787
    Key:        01234567 89ABCDEF FEDCBA98 76543210 01234567 89ABCDEF FEDCBA98 76543210

### Block 1

| Element | Value in hex
|----|----|
| Plain Text      | 022A2B2A 2B2A2B2A |
| Encrypted Block | 00DCEA9C E9C8463D |
| Ciphertext      | 02F6C1B6 C2E26D17 |

### Block 2                   

| Element | Value in hex      
|----|----|                   
| Plain Text      | 2B000000 00000000 |
| Encrypted Block | C3134563 445B07CB |
| Ciphertext      | E8134563 445B07CB |
