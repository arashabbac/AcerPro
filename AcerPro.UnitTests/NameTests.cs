using AcerPro.Domain.ValueObjects;
using AcerPro.UnitTests.Helpers;

namespace AcerPro.UnitTests;

public class NameTests
{
    [Theory]
    [MemberData(nameof(TestData.NullOrEmpty), MemberType = typeof(TestData))]
    public void Name_Must_Have_Value(string value)
    {
        //Act
        var result = Name.Create(value);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData.IncorrectValuesForName), MemberType = typeof(TestData))]
    public void Name_CanNot_BeCreated_With_Incorrect_Value(string value)
    {
        //Act
        var result = Name.Create(value);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Name_Created_Successfully()
    {
        //Arrange
        var input = "Arash";

        //Act
        var result = Name.Create(input);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(input.ToLower());
    }
}
