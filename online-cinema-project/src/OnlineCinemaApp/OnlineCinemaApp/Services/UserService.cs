public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Register(string name, string email, string password)
    {
        _userRepository.Register(name, email, password);
    }

    public bool Login(string email, string password)
    {
        return _userRepository.Login(email, password);
    }
}