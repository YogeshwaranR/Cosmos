using System;
using CPUx86 = Cosmos.IL2CPU.X86;
using CPU = Cosmos.IL2CPU.X86;
namespace Cosmos.IL2CPU.X86.IL
{
    [Cosmos.IL2CPU.OpCode( ILOpCode.Code.Cgt_Un )]
    public class Cgt_Un : ILOp
    {
        public Cgt_Un( Cosmos.IL2CPU.Assembler aAsmblr )
            : base( aAsmblr )
        {
        }

        public override void Execute( MethodInfo aMethod, ILOpCode aOpCode )
        {
            var xStackItem = Assembler.Stack.Pop();
            if( xStackItem.Size > 8 )
            {
                //EmitNotImplementedException( Assembler, GetServiceProvider(), "Cgt_Un: StackSizes>8 not supported", CurInstructionLabel, mMethodInfo, mCurrentOffset, NextInstructionLabel );
                throw new NotImplementedException("StackSizes>8 not supported");
            }
            Assembler.Stack.Pop();
            Assembler.Stack.Push( new StackContents.Item( 4, typeof( bool ) ) );
            string BaseLabel = GetLabel( aMethod, aOpCode ) + "__";
            string LabelTrue = BaseLabel + "True";
            string LabelFalse = BaseLabel + "False";
            if( xStackItem.Size > 4 )
            {
                if (xStackItem.IsFloat)
                {
                    new CPUx86.SSE.XorPS { DestinationReg = CPUx86.Registers.XMM0, SourceReg = CPUx86.Registers.XMM0 };
                    new CPUx86.Move { DestinationReg = CPUx86.Registers.ESI, SourceValue = 1 };
                    new CPUx86.MoveD { DestinationReg = CPUx86.Registers.XMM1, SourceReg = CPUx86.Registers.ESI};
                    new CPUx86.SSE.AddSS { DestinationReg = CPUx86.Registers.XMM0, SourceReg = CPUx86.Registers.XMM1};
                    new CPUx86.SSE.XorPS { DestinationReg = CPUx86.Registers.XMM1, SourceReg = CPUx86.Registers.XMM1 };
                    new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM2, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM3, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM4, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM5, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.SSE.SubSS { DestinationReg = CPUx86.Registers.XMM4, SourceReg = CPUx86.Registers.XMM2 };
                    new CPUx86.SSE.SubSS { DestinationReg = CPUx86.Registers.XMM5, SourceReg = CPUx86.Registers.XMM3 };
                    new CPUx86.LoadStatusFlags { };
                    new CPUx86.Move { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.AH };
                    new CPUx86.And { SourceValue = 0x1, DestinationReg = CPUx86.Registers.EBX };
                    new CPUx86.MoveD { SourceReg = Registers.EBX, DestinationReg = Registers.XMM0 };
                    new CPUx86.SSE.SubSS { DestinationReg = CPUx86.Registers.XMM5, SourceReg = CPUx86.Registers.XMM2 };
                }
                else
                {
                    new CPUx86.Xor { DestinationReg = CPUx86.Registers.ESI, SourceReg = CPUx86.Registers.ESI };
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESI, SourceValue = 1 };
                    new CPUx86.Xor { DestinationReg = CPUx86.Registers.EDI, SourceReg = CPUx86.Registers.EDI };
                    //esi = 1
                    new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
                    new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
                    //value2: EDX:EAX
                    new CPUx86.Pop { DestinationReg = CPUx86.Registers.EBX };
                    new CPUx86.Pop { DestinationReg = CPUx86.Registers.ECX };
                    //value1: ECX:EBX
                    new CPUx86.Sub { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.EAX };
                    new CPUx86.SubWithCarry { DestinationReg = CPUx86.Registers.ECX, SourceReg = CPUx86.Registers.EDX };
                    //result = value1 - value2
                    //new CPUx86.ConditionalMove(Condition.Above, "edi", "esi");
                    //new CPUx86.Push { DestinationReg = Registers.EDI };
                }
                new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.GreaterThan, DestinationLabel = LabelTrue };
                new CPUx86.Push { DestinationValue = 0 };
                new CPUx86.Jump { DestinationLabel = GetLabel(aMethod, aOpCode.NextPosition) };
                new Label( LabelTrue );
                new CPUx86.Push { DestinationValue = 1 };

            }
            else
            {
                if (xStackItem.IsFloat)
                {

                    new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM0, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.SSE.MoveSS { DestinationReg = CPUx86.Registers.XMM1, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                    new CPUx86.SSE.CompareSS { DestinationReg = CPUx86.Registers.XMM1, SourceReg = CPUx86.Registers.XMM0, pseudoOpcode = (byte)CPUx86.SSE.ComparePseudoOpcodes.NotLessThanOrEqualTo };
                    new CPUx86.MoveD { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.XMM1 };
                    new CPUx86.And { DestinationReg = CPUx86.Registers.EBX, SourceValue = 1 };
                    new CPUx86.Move { SourceReg = CPUx86.Registers.EBX, DestinationReg = CPUx86.Registers.ESP, DestinationIsIndirect = true };
                }
                else
                {
                    new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
                    new CPUx86.Compare { DestinationReg = CPUx86.Registers.EAX, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
                
                    new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.Below, DestinationLabel = LabelTrue };
                    new CPUx86.Jump { DestinationLabel = LabelFalse };
                    new Label( LabelTrue );
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.Push { DestinationValue = 1 };
                    new CPUx86.Jump { DestinationLabel = GetLabel(aMethod, aOpCode.NextPosition) };
                    new Label( LabelFalse );
                    new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
                    new CPUx86.Push { DestinationValue = 0 };
                }
            }
        }


        // using System;
        // 
        // using CPUx86 = Cosmos.IL2CPU.X86;
        // using CPU = Cosmos.IL2CPU.X86;
        // using Cosmos.IL2CPU.X86;
        // using Cosmos.IL2CPU.X86;
        // 
        // namespace Cosmos.IL2CPU.IL.X86 {
        // 	[OpCode(OpCodeEnum.Cgt_Un)]
        // 	public class Cgt_Un: Op {
        // 		private readonly string NextInstructionLabel;
        //         private readonly string CurInstructionLabel;
        //         private uint mCurrentOffset;
        //         private MethodInformation mMethodInfo;
        //         public Cgt_Un(ILReader aReader, MethodInformation aMethodInfo)
        //             : base(aReader, aMethodInfo)
        //         {
        //             NextInstructionLabel = GetInstructionLabel(aReader.NextPosition);
        //             CurInstructionLabel = GetInstructionLabel(aReader);
        //             mMethodInfo = aMethodInfo;
        //             mCurrentOffset = aReader.Position;
        //         }
        // 
        // 	    public override void DoAssemble()
        // 		{
        // 			var xStackItem = Assembler.Stack.Pop();
        //             if (xStackItem.IsFloat)
        //             {
        //                 EmitNotImplementedException(Assembler, GetServiceProvider(), "Cgt_Un: Floats not yet supported", CurInstructionLabel, mMethodInfo, mCurrentOffset, NextInstructionLabel);
        //                 return;
        //             }
        //             if (xStackItem.Size > 8)
        //             {
        //                 EmitNotImplementedException(Assembler, GetServiceProvider(), "Cgt_Un: StackSizes>8 not supported", CurInstructionLabel, mMethodInfo, mCurrentOffset, NextInstructionLabel);
        //                 return;
        //             }
        // 			Assembler.Stack.Push(new StackContent(4, typeof(bool)));
        // 			string BaseLabel = CurInstructionLabel + "__";
        // 			string LabelTrue = BaseLabel + "True";
        // 			string LabelFalse = BaseLabel + "False";
        // 			if (xStackItem.Size > 4)
        // 			{
        //                 new CPUx86.Xor { DestinationReg = CPUx86.Registers.ESI, SourceReg = CPUx86.Registers.ESI};
        //                 new CPUx86.Add { DestinationReg = CPUx86.Registers.ESI, SourceValue = 1 };
        //                 new CPUx86.Xor { DestinationReg = CPUx86.Registers.EDI, SourceReg = CPUx86.Registers.EDI};
        // 				//esi = 1
        //                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EAX };
        //                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EDX };
        //                 //value2: EDX:EAX
        //                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.EBX };
        //                 new CPUx86.Pop { DestinationReg = CPUx86.Registers.ECX };
        //                 //value1: ECX:EBX
        //                 new CPUx86.Sub { DestinationReg = CPUx86.Registers.EBX, SourceReg = CPUx86.Registers.EAX };
        //                 new CPUx86.SubWithCarry { DestinationReg = CPUx86.Registers.ECX, SourceReg = CPUx86.Registers.EDX };
        // 				//result = value1 - value2
        // 				//new CPUx86.ConditionalMove(Condition.Above, "edi", "esi");
        //                 //new CPUx86.Push { DestinationReg = Registers.EDI };
        // 
        //                 new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.Above, DestinationLabel = LabelTrue };
        //                 new CPUx86.Push { DestinationValue = 0 };
        //                 new CPUx86.Jump { DestinationLabel = NextInstructionLabel };
        // 
        // 				new Label(LabelTrue);
        // 				new CPUx86.Push{DestinationValue=1};
        // 
        // 			} else
        // 			{
        //                 new CPUx86.Pop{DestinationReg=CPUx86.Registers.EAX};
        //                 new CPUx86.Compare { DestinationReg = CPUx86.Registers.EAX, SourceReg = CPUx86.Registers.ESP, SourceIsIndirect = true };
        //                 new CPUx86.ConditionalJump { Condition = CPUx86.ConditionalTestEnum.Below, DestinationLabel = LabelTrue };
        //                 new CPUx86.Jump { DestinationLabel = LabelFalse };
        //                 new Label(LabelTrue);
        //                 new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
        //                 new CPUx86.Push { DestinationValue = 1 };
        //                 new CPUx86.Jump { DestinationLabel = NextInstructionLabel };
        //                 new Label(LabelFalse);
        //                 new CPUx86.Add { DestinationReg = CPUx86.Registers.ESP, SourceValue = 4 };
        //                 new CPUx86.Push { DestinationValue = 0 };
        //                 new CPUx86.Jump { DestinationLabel = NextInstructionLabel };
        // 			}
        // 		}
        // 	}
        // }

    }
}
