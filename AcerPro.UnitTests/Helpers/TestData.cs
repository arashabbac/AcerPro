namespace AcerPro.UnitTests.Helpers;

public static class TestData
{
    public static IEnumerable<object[]> NullOrEmpty =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { string.Empty },
            new object[] { "       " }
        };

    public static IEnumerable<object[]> IncorrectValuesForEmail =>
    new List<object[]>
    {
            new object[] { StringGenerator.Create(9,StringGeneratorType.NumberAndCharacter) },
            new object[] { StringGenerator.Create(151,StringGeneratorType.NumberAndCharacter) },
            new object[] { StringGenerator.Create(8,StringGeneratorType.Complex) },
    };

    public static IEnumerable<object[]> IncorrectValuesForPassword =>
        new List<object[]>
        {
                    new object[] { StringGenerator.Create(6,StringGeneratorType.NumberAndCharacter) },
                    new object[] { StringGenerator.Create(45,StringGeneratorType.NumberAndCharacter) },
                    new object[] { StringGenerator.Create(10,StringGeneratorType.Character) },
        };

    public static IEnumerable<object[]> IncorrectValuesForUrlAddress =>
        new List<object[]>
        {
            new object[] { StringGenerator.Create(9,StringGeneratorType.NumberAndCharacter) },
            new object[] { StringGenerator.Create(1001,StringGeneratorType.NumberAndCharacter) },
            new object[] { StringGenerator.Create(8,StringGeneratorType.Complex) },
            new object[] { "https://ssss" },
            new object[] { "//ssss" },
            new object[] { "http:/sss" },
            new object[] { "http//sss" },
            new object[] { "http/:/sss" },
            new object[] { "http://sss." },
            new object[] { "http://sss,com" },
        };

    public static IEnumerable<object[]> IncorrectValuesForName =>
        new List<object[]>
        {
            new object[] { StringGenerator.Create(2,StringGeneratorType.Character) },
            new object[] { StringGenerator.Create(101,StringGeneratorType.Character) },
        };

    public static IEnumerable<object[]> IncorrectValuesForMobileNumber =>
        new List<object[]>
        {
            new object[] { StringGenerator.Create(9,StringGeneratorType.Number) },
            new object[] { StringGenerator.Create(12,StringGeneratorType.Number) },
            new object[] { StringGenerator.Create(11,StringGeneratorType.NumberAndCharacter) },
            new object[] { StringGenerator.Create(11,StringGeneratorType.NumberAndCharacter) },
        };
}
