using AcerPro.Domain.Aggregates;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.UnitTests.Doubles;

namespace AcerPro.UnitTests;

public class CreateUserTests
{
    private Name _firstname;
    private Name _lastname;
    private Email _email;
    private Password _password;
    private IUserRepository _userRepository;

    public CreateUserTests()
    {
        _firstname = Name.Create("Arash").Value;
        _lastname = Name.Create("Abbasi").Value;
        _email = Email.Create("arash@gmail.com").Value;
        _password = Password.Create("Arash@12345").Value;
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public void CanNot_Create_User_With_Null_Firstname()
    {
        //Arrange
        _firstname = null;

        //Act
        Action action = () => User.Create(_firstname, _lastname,
            _email,
            _password,
            _userRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Create_User_With_Null_Lastname()
    {
        //Arrange
        _lastname = null;

        //Act
        Action action = () => User.Create(_firstname, _lastname,
            _email,
            _password,
            _userRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Create_User_With_Null_Email()
    {
        //Arrange
        _email = null;

        //Act
        Action action = () => User.Create(_firstname, _lastname,
            _email,
            _password,
            _userRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Create_User_With_Null_Password()
    {
        //Arrange
        _password = null;

        //Act
        Action action = () => User.Create(_firstname, _lastname,
            _email,
            _password,
            _userRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Create_User_With_Null_UserRepository()
    {
        //Arrange
        _userRepository = null;

        //Act
        Action action = () => User.Create(_firstname, _lastname,
            _email,
            _password,
            _userRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Create_User_With_Duplicate_Email()
    {
        //Arrange
        var customer1 = User.Create(_firstname, _lastname,
            _email,
            _password,
            _userRepository).Value;

        _userRepository.AddAsync(customer1);

        //Act
        var customer2 = User.Create(Name.Create("Arash").Value, Name.Create("Abbac").Value,
            Email.Create("Arash@gmail.com").Value,
            _password,
            _userRepository);

        //Assert
        customer2.IsFailed.Should().BeTrue();
        customer2.Errors.Select(c => c.Message).Should().Contain("Email has already been used");
    }
}