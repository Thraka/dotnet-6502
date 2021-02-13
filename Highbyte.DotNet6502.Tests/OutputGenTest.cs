using Xunit;

namespace Highbyte.DotNet6502.Tests.Instructions
{
    public class OutputGenTest
    {
        [Fact]
        public void OutputGen_Returns_Correctly_Formatted_Disassembly_If_OpCode_Is_Known()
        {
            // Arrange
            var cpu = new CPU();
            var mem = new Memory();
            mem[0x1000] = OpCodeId.LDX_I.ToByte();
            mem[0x1001] = 0xee;

            // Act
            var outputString = OutputGen.GetInstructionDisassembly(cpu, mem, 0x1000);

            // Assert
            Assert.Equal("1000  A2 EE     LDX #$EE   ", outputString);
        }

        [Theory]
        [InlineData(AddrMode.Accumulator,   new byte[]{},           "A")]
        [InlineData(AddrMode.I,             new byte[]{0xee},       "#$EE")]
        [InlineData(AddrMode.ZP,            new byte[]{0x01},       "$01")]
        [InlineData(AddrMode.ZP_X,          new byte[]{0x02},       "$02,X")]
        [InlineData(AddrMode.ZP_Y,          new byte[]{0x03},       "$03,Y")]
        [InlineData(AddrMode.Relative,      new byte[]{0x00},       "*+0")]
        [InlineData(AddrMode.Relative,      new byte[]{0x04},       "*+4")]
        [InlineData(AddrMode.Relative,      new byte[]{0x80},       "*-128")]
        [InlineData(AddrMode.ABS,           new byte[]{0x10,0xc0},  "$C010")]
        [InlineData(AddrMode.ABS_X,         new byte[]{0xf0,0x80},  "$80F0,X")]
        [InlineData(AddrMode.ABS_Y,         new byte[]{0x42,0x21},  "$2142,Y")]
        [InlineData(AddrMode.Indirect,      new byte[]{0x37,0x13},  "($1337)")]
        [InlineData(AddrMode.IX_IND,        new byte[]{0x42},       "($42,X)")]
        [InlineData(AddrMode.IND_IX,        new byte[]{0x21},       "($21),Y")]
        public void OutputGen_Returns_Correctly_Formatted_Operand_String_For_AddrMode(AddrMode addrMode, byte[] operand, string expectedOutput)
        {
            // Act
            var outputString = OutputGen.BuildOperandString(addrMode, operand);

            // Assert
            Assert.Equal(expectedOutput, outputString);
        }

    }
}
