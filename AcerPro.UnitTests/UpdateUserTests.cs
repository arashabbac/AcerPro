using AcerPro.Domain.Aggregates;
using AcerPro.Domain.Contracts;
using AcerPro.Domain.ValueObjects;
using AcerPro.UnitTests.Doubles;

namespace AcerPro.UnitTests;

public class UpdateUserTests
{
    private Name _firstname;
    private Name _lastname;
    private Email _email;
    private Password _password;
    private IUserRepository _customerRepository;
    private readonly User _user;

    public UpdateUserTests()
    {
        _firstname = Name.Create("Arash").Value;
        _lastname = Name.Create("Abbasi").Value;
        _email = Email.Create("arash@gmail.com").Value;
        _password = Password.Create("Arash@12345").Value;
        _customerRepository = new FakeUserRepository();

        _user = User.Create(_firstname, _lastname, _email,_password, _customerRepository).Value;
    }

    [Fact]
    public void CanNot_Update_User_With_Null_Firstname()
    {
        //Arrange
        _firstname = null;

        //Act
        Action action = () => _user.Update(_firstname, _lastname,
            _email,
            _password,
            _customerRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Update_User_With_Null_Lastname()
    {
        //Arrange
        _lastname = null;

        //Act
        Action action = () => _user.Update(_firstname, _lastname,
            _email,
            _password,
            _customerRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Update_User_With_Null_Email()
    {
        //Arrange
        _email = null;

        //Act
        Action action = () => _user.Update(_firstname, _lastname,
            _email,
            _password,
            _customerRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Update_User_With_Null_Password()
    {
        //Arrange
        _password = null;

        //Act
        Action action = () => _user.Update(_firstname, _lastname,
            _email,
            _password,
            _customerRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Update_User_With_Null_UserRepository()
    {
        //Arrange
        _customerRepository = null;

        //Act
        Action action = () => _user.Update(_firstname, _lastname,
            _email,
            _password,
            _customerRepository);

        //Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void CanNot_Update_User_If_His_Email_Is_Already_Existed()
    {
        //Arrange
        var customer1 = User.Create(Name.Create("John").Value,
            Name.Create("Doe").Value,
            Email.Create("johndoe@gmail.com").Value,
            _password,
            _customerRepository).Value;

        _customerRepository.AddAsync(customer1);

        //Act
        var result = _user.Update(_firstname,
            _lastname,
            Email.Create("Johndoe@gmail.com").Value,
            _password,
            _customerRepository);

        //Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Select(c => c.Message).Should().Contain("Email has already been used");
    }
}
