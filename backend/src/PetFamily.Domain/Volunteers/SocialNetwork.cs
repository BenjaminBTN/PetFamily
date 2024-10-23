using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public class SocialNetwork
    {
        private SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; }
        public string Url { get; }

        public static Result<SocialNetwork> Create(string name, string url)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result.Failure<SocialNetwork>("Name can not be empty");

            if(string.IsNullOrWhiteSpace(url))
                return Result.Failure<SocialNetwork>("Url can not be empty");

            return Result.Success(new SocialNetwork(name, url));
        }
    }
}
