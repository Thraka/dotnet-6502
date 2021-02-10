using System.Collections.Generic;

namespace Highbyte.DotNet6502.Instructions
{
    /// <summary>
    /// Branch if Overflow Clear.
    /// If the overflow flag is clear then add the relative displacement to the program counter to cause a branch to a new location.
    /// </summary>
    public class BVC : Instruction
    {
        private readonly List<OpCode> _opCodes;
        public override List<OpCode> OpCodes => _opCodes;

        public override bool Execute(CPU cpu, Memory mem, AddrModeCalcResult addrModeCalcResult)
        {
            byte insValue = addrModeCalcResult.InsValue.Value;

            if(!cpu.ProcessorStatus.Overflow)
            {
                // The instruction value is signed byte with the relative address (positive or negative)
                cpu.PC = BranchHelper.CalculateNewAbsoluteBranchAddress(cpu.PC, (sbyte)insValue, out ulong cyclesConsumed);
                cpu.ExecState.CyclesConsumed += cyclesConsumed;
            }
            return true;
        }

        public BVC()
        {
            _opCodes = new List<OpCode>
            {
                new OpCode
                {

                    Code = OpCodeId.BVC,
                    AddressingMode = AddrMode.Relative,
                    Size = 1,
                    MinimumCycles = 2, // +1 if branch succeeds +2 if to a new page
                },
            };
        }
    }
}
