namespace Week3Objects
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }

        string GetFullName();
    }
}