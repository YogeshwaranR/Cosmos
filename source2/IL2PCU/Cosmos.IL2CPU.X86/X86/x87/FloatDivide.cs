﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosmos.IL2CPU.X86.x87
{
    [OpCode("fdiv")]
    public class FloatDivide : InstructionWithDestinationAndSourceAndSize
    {
        public static void InitializeEncodingData(Instruction.InstructionData aData)
        {
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption
            {
                OpCode = new byte[] { 0xD8 },
                NeedsModRMByte = true,
                InitialModRMByteValue = 6,
                DestinationImmediate = false,
                DestinationMemory = true,
                DestinationReg = null,
                SourceEmpty=true,
                AllowedSizes = InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord
            });
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption
            {
                OpCode = new byte[] { 0xDC },
                NeedsModRMByte = true,
                InitialModRMByteValue = 6,
                DestinationEmpty=true,
                SourceEmpty=true,
                AllowedSizes = InstructionSizes.QWord,
                DefaultSize = InstructionSize.QWord
            });
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption
            {
                OpCode = new byte[] { 0xD8, 0xF0 },
                DestinationReg = RegistersEnum.ST0,
                SourceReg = RegistersEnum.ST0 | RegistersEnum.ST1 | RegistersEnum.ST2 | RegistersEnum.ST3 | RegistersEnum.ST4 | RegistersEnum.ST5 | RegistersEnum.ST6 | RegistersEnum.ST7,
                SourceImmediate = false,
                SourceMemory = false,
                DestinationMemory = false,
                DestinationImmediate = false
            });
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption
            {
                OpCode = new byte[] { 0xDC, 0xF8 },
                SourceReg = RegistersEnum.ST0,
                DestinationReg = RegistersEnum.ST0 | RegistersEnum.ST1 | RegistersEnum.ST2 | RegistersEnum.ST3 | RegistersEnum.ST4 | RegistersEnum.ST5 | RegistersEnum.ST6 | RegistersEnum.ST7,
                SourceImmediate = false,
                SourceMemory = false,
                DestinationMemory = false,
                DestinationImmediate = false
            });
        }
        public override void WriteText(Assembler aAssembler, System.IO.TextWriter aOutput)
        {

        }
    }
}