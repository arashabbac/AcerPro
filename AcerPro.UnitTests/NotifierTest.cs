using AcerPro.Domain.Aggregates;
using AcerPro.Domain.Enums;
using AcerPro.Domain.ValueObjects;
using AcerPro.UnitTests.Doubles;
using AcerPro.UnitTests.Helpers;

namespace AcerPro.UnitTests;

public class NotifierTest
{
    private NotifierType _notifierType;
    private string _notifierAddress;
    private readonly User _user;
    private readonly int _targetAppId;

    public NotifierTest()
    {
        _user = User.Create(Name.Create("Arash").Value,
            Name.Create("Abbasi").Value, 
            Email.Create("arash@gmail.com").Value,
            Password.Create("Arash@12345").Value,
            new FakeUserRepository()).Value;

        _targetAppId = (_user.AddTargetApp(Name.Create("TargetApp").Value, UrlAddress.Create("https://something.com").Value, 60)).Value.Id;
    }

    [Theory]
    [MemberData(nameof(TestData.IncorrectValuesForName), MemberType = typeof(TestData))]
    public void CanNot_Add_Notifier_With_EmailNotifierType_With_Incorrect_Email_Address(string value)
    {
        //Arrange
        _notifierType = NotifierType.Email;

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId,value,_notifierType);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData.NullOrEmpty), MemberType = typeof(TestData))]
    public void CanNot_Add_Notifier_With_NullOrEmpty_Address(string value)
    {
        //Arrange
        _notifierType = NotifierType.Email;

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId, value, _notifierType);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData.IncorrectValuesForMobileNumber), MemberType = typeof(TestData))]
    public void CanNot_Add_Notifier_With_SMSNotifierType_With_Incorrect_SMS_Address(string value)
    {
        //Arrange
        _notifierType = NotifierType.SMS;

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId, value, _notifierType);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData.IncorrectValuesForMobileNumber), MemberType = typeof(TestData))]
    public void CanNot_Add_Notifier_With_SMSNotifierType_With_Incorrect_Call_Address(string value)
    {
        //Arrange
        _notifierType = NotifierType.Call;

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId, value, _notifierType);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Add_Notifier_With_EmailType_Successfully()
    {
        //Arrange
        _notifierType = NotifierType.Email;
        _notifierAddress = "arash@gmail.com";

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId, _notifierAddress, _notifierType);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Add_Notifier_With_SMSType_Successfully()
    {
        //Arrange
        _notifierType = NotifierType.SMS;
        _notifierAddress = "09122222222";

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId, _notifierAddress, _notifierType);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Add_Notifier_With_CallType_Successfully()
    {
        //Arrange
        _notifierType = NotifierType.Call;
        _notifierAddress = "09122222222";

        //Act
        var result = _user.AddNotifierToTargetApp(_targetAppId, _notifierAddress, _notifierType);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}


