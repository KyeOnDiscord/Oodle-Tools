using System.Runtime.InteropServices;

namespace OodleTools;

// Imports are documented in https://htmlpreview.github.io/?https://github.com/WorkingRobot/OodleUE/blob/main/Engine/Source/Runtime/OodleDataCompression/Sdks/2.9.10/help/oodle2.html

enum OodleLZ_CompressionLevel
{
    None = 0, //    don't compress, just copy raw bytes
    SuperFast = 1, //	super fast mode, lower compression ratio
    VeryFast = 2,// fastest LZ mode with still decent compression ratio
    Fast = 3,// fast - good for daily use
    Normal = 4,//   standard medium speed LZ mode
    Optimal1 = 5,//     optimal parse level 1 (faster optimal encoder)
    Optimal2 = 6,// 	optimal parse level 2 (recommended baseline optimal encoder)
    Optimal3 = 7,//     optimal parse level 3 (slower optimal encoder)
    Optimal4 = 8,//     optimal parse level 4 (very slow optimal encoder)
    Optimal5 = 9,//     optimal parse level 5 (don't care about encode speed, maximum compression)
    HyperFast1 = -1,//  	faster than SuperFast, less compression
    HyperFast2 = -2,//      faster than HyperFast1, less compression
    HyperFast3 = -3,//      	faster than HyperFast2, less compression
    HyperFast4 = -4,//          	fastest, less compression
    HyperFast = HyperFast1,//   	alias hyperfast base level
    Optimal = Optimal2,//   alias optimal standard level
    Max = Optimal5,//   maximum compression level
    Min = HyperFast4,// fastest compression level
    //Force32 = 0x40000000,
    //Invalid = Force32
}

enum OodleLZ_Compressor
{
    Invalid = -1, //    Invalid compression
    None = 3, //    None = memcpy, pass through uncompressed bytes
    Kraken = 8,//   Fast decompression and high compression ratios, amazing!
    Leviathan = 13,//   	Leviathan = Kraken's big brother with higher compression, slightly slower decompression.
    Mermaid = 9,//  Mermaid is between Kraken & Selkie - crazy fast, still decent compression.
    Selkie = 11,//  	Selkie is a super-fast relative of Mermaid. For maximum decode speed.
    Hydra = 12,//   Hydra, the many-headed beast = Leviathan, Kraken, Mermaid, or Selkie 
    //BitKnit = 10,// no longer supported as of Oodle 2.9.0
    LZB16 = 4,//    DEPRECATED but still supported
    //LZNA = 7,// no longer supported as of Oodle 2.9.0
    //LZH = 0,//  no longer supported as of Oodle 2.9.0
    //LZHLW = 1,//    no longer supported as of Oodle 2.9.0
    //LZNIB = 2,
    //LZBLW = 5,
    //LZA = 6,
    //Count = 14, Unused
   // Force32 = 0x40000000 Unused
}

public class Imports
{
    private const string OodlePath = "oo2core_9_win64.dll";

    [DllImport(OodlePath, CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint OodleLZ_GetCompressedBufferSizeNeeded(OodleLZ_Compressor compressor, uint rawSize);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="compressor">which OodleLZ variant to use in compression </param>
    /// <param name="rawBuf">raw data to compress</param>
    /// <param name="rawLen">number of bytes in rawBuf to compress</param>
    /// <param name="compBuf">pointer to write compressed data to. MUST be at least OodleLZ_GetCompressedBufferSizeNeeded bytes</param>
    /// <param name="level">OodleLZ_CompressionLevel controls how much CPU effort is put into maximizing compression </param>
    /// <param name="pOptions">(optional) options; if NULL, OodleLZ_CompressOptions_GetDefault is used </param>
    /// <param name="dictionaryBase">(optional) if not NULL, provides preceding data to prime the dictionary; must be contiguous with rawBuf, the data between the pointers dictionaryBase and rawBuf is used as the preconditioning data. The exact same precondition must be passed to encoder and decoder. </param>
    /// <param name="lrm">(optional) long range matcher</param>
    /// <param name="scratchMem">(optional) pointer to scratch memory</param>
    /// <param name="scratchSize">(optional) size of scratch memory (see OodleLZ_GetCompressScratchMemBound) </param>
    /// <returns>size of compressed data written, or OODLELZ_FAILED for failure </returns>
    [DllImport(OodlePath, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int OodleLZ_Compress(OodleLZ_Compressor compressor, byte[] rawBuf, long rawLen, byte[] compBuf, OodleLZ_CompressionLevel level, uint pOptions = 0, uint dictionaryBase = 0, uint lrm = 0, uint scratchMem = 0, uint scratchSize = 0);
    
    [DllImport(OodlePath, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int OodleLZ_Decompress(byte[] Buffer, long BufferSize, byte[] OutputBuffer, long OutputBufferSize, uint a, uint b, uint c, uint d, uint e, uint f, uint g, uint h, uint i, int ThreadModule);
}