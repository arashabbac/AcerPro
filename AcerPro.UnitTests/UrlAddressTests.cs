using AcerPro.Domain.ValueObjects;
using AcerPro.UnitTests.Helpers;

namespace AcerPro.UnitTests;

public class UrlAddressTests
{
    [Theory]
    [MemberData(nameof(TestData.NullOrEmpty), MemberType = typeof(TestData))]
    public void UrlAddress_Must_Have_Value(string value)
    {
        //Act
        var result = UrlAddress.Create(value);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData.IncorrectValuesForUrlAddress), MemberType = typeof(TestData))]
    public void UrlAddress_CanNot_Created_With_Incorrect_Value(string value)
    {
        //Act
        var result = UrlAddress.Create(value);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void UrlAddress_Created_Successfully()
    {
        //Arrange
        var input = "https://something.com";

        //Act
        var result = UrlAddress.Create(input);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(input);
    }
}