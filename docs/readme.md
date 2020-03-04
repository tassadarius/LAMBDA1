[![Build Status](https://travis-ci.com/tassadarius/LAMBDA1.svg?branch=master)](https://travis-ci.com/tassadarius/LAMBDA1)

# LAMBDA1 Encryption Algorithm

LAMBDA1 is a block cipher developed in mid to late 1980s in Eastern Germany (German Democratic Republic - GDR).
The algorithm is a modified DES (Data Encryption Standard) and therefore shares many properties with it.


|  Property     |  Value/Status    |
| ------------- |-----------|
| Block size    | 64 bit           |
| Key size      | 256 bit          |
| Structure     | Feistel cipher   |
| Cryptanalysis | unknown          |

The algorithm differs slightly from DES - mainly the key and key derication function. You find more information how the algorithm works below.

## Build with developer command line / Mono under Linux

```bash
msbuild LAMBDA1.sln -restore
```

## Build with Visual Studio

`Open the solution`

then hit

`Build` &#8594; `Build Solution (Ctrl+Shift+B)`

# About this software

This is a scientific implementation of the specified algorithm in C#. It was developed as part of a bachelor's thesis. The implementation is correct according
to the original documents and test cases provided by the BStU (archive for documents of the "Stasi" - document no. MfS-Abt-XI-601).

This repository contains the algorithm ([LAMBDA1](../src/LAMBDA1)) and a basic terminal program ([LAMBDA1Tool](../src/LAMBDA1Tool)). The tool allows the creation of keys and encryption/decryption in Cipher Feedback Mode.
This mode was chosen for historic reasons, as it was intended to be used in Eastern Germany as well.

Note that this implementation works correctly but does not utilize any tricks to speed it up. In fact it is very slow. The algorithm is also part of [CrypTool 2](https://cryptool.org/de/cryptool2) check it out.

This software is licensed under the [GPL-3.0](https://www.gnu.org/licenses/gpl-3.0-standalone.html).

## Usage Examples

The program LAMBDA1Tool can be used to create keys and encrypt/decrypt messages:

```bash
LAMBDA1Tool --create-key keyfile                        // Creates a key
LAMBDA1Tool -k keyfile -e input.txt ciphertext.bin      // Encrypts input.txt and outputs to ciphertext.bin
LAMBDA1Tool -k keyfile -d ciphertext.bin                // Decrypts the file again and prints to stdout
```

It supports input/output files as well as reading/writing to stdout/stdin.

# About the Algorithm

LAMBDA1 uses the same Feistel structure as DES. It consists of 16 rounds with a left and right block.
There are 3 differences in the scheme compared to DES. These are the following:
    
 * The input permutation was removed
 * The output permutation was removed
 * In the 8th round, an additional operation is added. Both blocks are modulo added with additional keys (<img src="/docs/tex/1840e35d3ef76c206537733066262d28.svg?invert_in_darkmode&sanitize=true" align=middle width=27.06630629999999pt height=22.465723500000017pt/> and <img src="/docs/tex/9ba489ada35428f0a168b0ea0f2bf0d4.svg?invert_in_darkmode&sanitize=true" align=middle width=27.06630629999999pt height=22.465723500000017pt/>). The operation is denoted with the symbol <img src="/docs/tex/5f5572097ecc0bb70ff01850e2d27567.svg?invert_in_darkmode&sanitize=true" align=middle width=12.785434199999989pt height=22.19178720000002pt/> - the divisor is <img src="/docs/tex/fc1ad1e8be2d915908e89ee69245646d.svg?invert_in_darkmode&sanitize=true" align=middle width=21.324302999999993pt height=26.76175259999998pt/> (0x100000000)

Following diagram shows the scheme. <img src="/docs/tex/190083ef7a1625fbc75f243cffb9c96d.svg?invert_in_darkmode&sanitize=true" align=middle width=9.81741584999999pt height=22.831056599999986pt/> is the round function (described below). The symbol <img src="/docs/tex/45848451c711deba755da6422f9e68c6.svg?invert_in_darkmode&sanitize=true" align=middle width=12.785434199999989pt height=19.1781018pt/> is a bitwise XOR:

<p align="center">
    <img src="images/lambda1_overview.png" height="600pt" scheme of lambda1, in every round the right block goes into the round function and is then xor with the left block. In round 8 there are two additional modulo operations></br></br>
    Scheme of LAMBDA1
</p>

### Decrypting
As in DES reverse the order of the round keys for decryption, as well as swap the keys 17 and 18.

# Round function 

In LAMBDA1 all functions, permutations and S-Boxes are the same as in DES. However, their arrangement differs slightly.
In a normal round of DES the permutation <img src="/docs/tex/df5a289587a2f0247a5b97c1e8ac58ca.svg?invert_in_darkmode&sanitize=true" align=middle width=12.83677559999999pt height=22.465723500000017pt/> is applied in the beginning before the expansion function. In LAMBDA1 it has been
moved to the end after the S-Boxes.

A single round is depicted here. <img src="/docs/tex/96b697078d351b7b43bd5b5dce0254cd.svg?invert_in_darkmode&sanitize=true" align=middle width=22.08723494999999pt height=22.465723500000017pt/> is the 48 bit round key:

<p align="center">
    <img src="images/round_overview.png" height="400pt" round function of LAMBDA1; first comes round permutation P, then expansion function E, then xor with round key, finally the S-boxes></br></br>
    Scheme of the round function 𝒇
</p>


# Key derivation

The key derivation is the main difference between LAMBDA1 and DES. As the developers knew that the key length of DES was its greatest weakness, the key length was drastically increased
from 56 to **256 bits**. As a trade-off the key derivation function is way simpler. Note that LAMBDA1 has to derive 18 instead of the usual 16 subkeys, as two 32 bit keys are required for
the modulo addition in round 8.

The first round keys (<img src="/docs/tex/6d6968ce16fa7a6d8af6261f0d09b3b9.svg?invert_in_darkmode&sanitize=true" align=middle width=20.513758649999993pt height=22.465723500000017pt/> to <img src="/docs/tex/4c4c9f46415f8ecb4e1bf15cc7a7f79c.svg?invert_in_darkmode&sanitize=true" align=middle width=20.513758649999993pt height=22.465723500000017pt/>) and the additional keys (<img src="/docs/tex/1840e35d3ef76c206537733066262d28.svg?invert_in_darkmode&sanitize=true" align=middle width=27.06630629999999pt height=22.465723500000017pt/> and <img src="/docs/tex/9ba489ada35428f0a168b0ea0f2bf0d4.svg?invert_in_darkmode&sanitize=true" align=middle width=27.06630629999999pt height=22.465723500000017pt/>) are directly taken from the key.

<p align="center"><img src="/docs/tex/0d025fa4b1a41da4dbb12c6cecdca524.svg?invert_in_darkmode&sanitize=true" align=middle width=210.89056394999997pt height=139.72602765pt/></p>

 The rest of the keys are generated in a very simple LFSR (Linear-feedback shift register) by shifting the bits 11 times.
 Following image shows the Shifting Register function. It is very simple and only appends the last bit at the front again.

<p align="center">
    <img src="images/lfsr.png" width="325pt" Shifting function T, shift most significant bit to lowest significant></br>
    Shifting register 𝑇
</p>

The rest of the key derivation can mathematically be expressed as this:

<p align="center"><img src="/docs/tex/3e55c0c1373eee01007ff9c6c009bde6.svg?invert_in_darkmode&sanitize=true" align=middle width=255.5480433pt height=45.77175735pt/></p>

Where <img src="/docs/tex/55a049b8f161ae7cfeb0197d75aff967.svg?invert_in_darkmode&sanitize=true" align=middle width=9.86687624999999pt height=14.15524440000002pt/> denotes the round, <img src="/docs/tex/96b697078d351b7b43bd5b5dce0254cd.svg?invert_in_darkmode&sanitize=true" align=middle width=22.08723494999999pt height=22.465723500000017pt/> the key for that round and <img src="/docs/tex/2f118ee06d05f3c2d98361d9c30e38ce.svg?invert_in_darkmode&sanitize=true" align=middle width=11.889314249999991pt height=22.465723500000017pt/> the shifting function.
<img src="/docs/tex/41ab8f29d5f9ce89c24c70a1bf92b30c.svg?invert_in_darkmode&sanitize=true" align=middle width=17.670148649999987pt height=27.91243950000002pt/> specifies the input shall be shifted <img src="/docs/tex/4bdc8d9bcfb35e1c9bfb51fc69687dfc.svg?invert_in_darkmode&sanitize=true" align=middle width=7.054796099999991pt height=22.831056599999986pt/> times, which is in our case always 11. 

#### For people not so familiar with mathematical representation, the keys are generated the following way:
 - **round 1 to 4** are the first 192 bits of the main key
 - **round 5 to 8** by shifting the keys 1 to 4 in the shifting function (shift is always 11 bits in the key derivation)
 - **round 9 to 12** by shifting the keys 5 to 8
 - **round 13 to 16** by shifting the previous key. That means that for round 13 the key 12 is shifted. For round 14 the key from 13 and so on
 - **bonus keys 17 and 18** are the bits 192 to 256 (only 32 bits per key)

# Additional information

Some more details and examples about some operations used above 

### Modulo addition (Symbol <img src="/docs/tex/b1257823e324c87f6a9ef527f3be0b28.svg?invert_in_darkmode&sanitize=true" align=middle width=12.785434199999989pt height=22.19178720000002pt/>)

This is a basic addition which is then taken modulo. This is equivalent when you add hours to a day, which is a modulo addition by 24:
```
20 +  8 = 28 % 24 = 4
16 + 16 = 32 % 24 = 8
23 +  1 = 24 % 24 = 0
10 +  5 = 13 % 24 = 15
```

In the algorithm the divisor is <img src="/docs/tex/fc1ad1e8be2d915908e89ee69245646d.svg?invert_in_darkmode&sanitize=true" align=middle width=21.324302999999993pt height=26.76175259999998pt/> (0x100000000). That means:

`half_block` <img src="/docs/tex/5f5572097ecc0bb70ff01850e2d27567.svg?invert_in_darkmode&sanitize=true" align=middle width=12.785434199999989pt height=22.19178720000002pt/> `key` % `0x100000000`

for example:

```
3181677172 + 2434904559 = 5616581731 % 0x100000000 = 1321614435
```


### Examples for Shifting function <img src="/docs/tex/79a03da4195d79ebd799bb7dffbf2660.svg?invert_in_darkmode&sanitize=true" align=middle width=11.889314249999991pt height=22.465723500000017pt/> on 48 bit keys

`111111111111111111111111000000000000000000000000` &#8594; <img src="/docs/tex/93c9de495cf7711c819b565d35f325af.svg?invert_in_darkmode&sanitize=true" align=middle width=24.994404599999992pt height=26.76175259999998pt/> &#8594; `000000000001111111111111111111111110000000000000`

`000000000000000000000000111111111111111111111111` &#8594; <img src="/docs/tex/93c9de495cf7711c819b565d35f325af.svg?invert_in_darkmode&sanitize=true" align=middle width=24.994404599999992pt height=26.76175259999998pt/> &#8594; `111111111110000000000000000000000001111111111111`

`000000000000000000000000000000000000011111111111` &#8594; <img src="/docs/tex/93c9de495cf7711c819b565d35f325af.svg?invert_in_darkmode&sanitize=true" align=middle width=24.994404599999992pt height=26.76175259999998pt/> &#8594; `111111111100000000000000000000000000000000000000`

`000011110000111100001111000011110000111100001111` &#8594; <img src="/docs/tex/93c9de495cf7711c819b565d35f325af.svg?invert_in_darkmode&sanitize=true" align=middle width=24.994404599999992pt height=26.76175259999998pt/> &#8594; `111000011110000111100001111000011110000111100001`
