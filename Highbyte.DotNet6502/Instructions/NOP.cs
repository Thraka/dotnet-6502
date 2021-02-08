using System.Collections.Generic;

namespace Highbyte.DotNet6502.Instructions
{
    /// <summary>
    /// No Operation.
    /// The NOP instruction causes no changes to the processor other than the normal incrementing of the program counter to the next instruction.
    /// </summary>
    public class NOP : Instruction
    {
        private readonly List<OpCode> _opCodes;
        public override List<OpCode> OpCodes => _opCodes;

        public override bool Execute(CPU cpu, Memory mem, AddrModeCalcResult addrModeCalcResult)
        {
            // TODO: What is the extra cycle for?
            cpu.ExecState.CyclesConsumed++;
            return true;
        }
        
        public NOP()
        {
            _opCodes = new List<OpCode>
                {
                    new OpCode
                    {
                        Code = Ins.NOP,
                        AddressingMode = AddrMode.Implied,
                        Size = 1,
                        Cycles = 2,
                    }
            };
        }
    }
}
