using AcerPro.Domain.Aggregates;
using AcerPro.Domain.ValueObjects;
using AcerPro.UnitTests.Doubles;
using System.Xml.Linq;

namespace AcerPro.UnitTests;

public class TargetAppTests
{
    private Name _name;
    private UrlAddress _urladdress;
    private int _monitoringIntervalInSeconds;
    private readonly User _user;

    public TargetAppTests()
    {
        _monitoringIntervalInSeconds = 60;
        _name = Name.Create("Endpoint1").Value;
        _urladdress = UrlAddress.Create("https://something.com").Value;
        _user = User.Create(Name.Create("Arash").Value,
                Name.Create("Abbasi").Value,
                Email.Create("arash@gmail.com").Value,
                Password.Create("Arash@12345").Value,
                new FakeUserRepository()).Value;
    }

    [Fact]
    public void CanNot_Add_TargetApp_With_Null_UrlAddress()
    {
        //Arrange
        _urladdress = null;

        //Act
        Action action = () => _user.AddTargetApp(_name, _urladdress,_monitoringIntervalInSeconds);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Add_TargetApp_With_Null_Name()
    {
        //Arrange
        _name = null;

        //Act
        Action action = () => _user.AddTargetApp(_name, _urladdress, _monitoringIntervalInSeconds);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void CanNot_Add_TargetApp_With_UnSpecified_MonitoringIntervalInSeconds(int value)
    {
        //Arrange
        _monitoringIntervalInSeconds = value;

        //Act
        var result = _user.AddTargetApp(_name,_urladdress, _monitoringIntervalInSeconds);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Add_TargetApp_Successfully()
    {
        //Act
        var result = _user.AddTargetApp(_name,_urladdress,_monitoringIntervalInSeconds);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void CanNot_Update_TargetApp_With_Null_UrlAddress()
    {
        //Arrange
        UrlAddress _newUrladdress = null;
        var targetAppResult = _user.AddTargetApp(_name, _urladdress, _monitoringIntervalInSeconds);

        //Act
        Action action = () => _user.UpdateTargetApp(targetAppResult.Value.Id, _name, _newUrladdress, _monitoringIntervalInSeconds);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Update_TargetApp_With_Null_Name()
    {
        //Arrange
        Name _newName = null;
        var targetAppResult = _user.AddTargetApp(_name, _urladdress, _monitoringIntervalInSeconds);

        //Act
        Action action = () => _user.UpdateTargetApp(targetAppResult.Value.Id,_newName, _urladdress, _monitoringIntervalInSeconds);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void CanNot_Update_TargetApp_With_UnSpecified_MonitoringIntervalInSeconds(int value)
    {
        //Arrange
        int newMonitoringIntervalInSeconds = value;
        var targetAppResult = _user.AddTargetApp(_name, _urladdress, _monitoringIntervalInSeconds);

        //Act
        var result = _user.UpdateTargetApp(targetAppResult.Value.Id, _name, _urladdress, newMonitoringIntervalInSeconds);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Update_TargetApp_Successfully()
    {
        //Arrange
        var newMonitoringIntervalInSeconds = 120;
        var newName = Name.Create("NewTargetApp").Value;
        var newUrlAddress = UrlAddress.Create("https://sometimes.com").Value;
        var targetAppResult = _user.AddTargetApp(_name, _urladdress, _monitoringIntervalInSeconds);

        //Act
        var result = _user.UpdateTargetApp(targetAppResult.Value.Id,newName, newUrlAddress, newMonitoringIntervalInSeconds);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be(newName); 
        result.Value.UrlAddress.Should().Be(newUrlAddress);
        result.Value.MonitoringIntervalInSeconds.Should().Be(newMonitoringIntervalInSeconds);
    }
}
