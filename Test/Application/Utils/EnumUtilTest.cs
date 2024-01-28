using Application.Utils;
using Domain.Enums;

namespace Test.Application.Utils
{
    public class EnumUtilTest
    {
        [Fact]
        public void GetDescriptionFromEnumValue_ReturnsExpectedResult()
        {
            // Act
            var result = EnumUtil.GetDescriptionFromEnumValue(StatusPagamento.Aprovado);

            // Assert
            Assert.Equal("Aprovado", result);
        }
    }
}