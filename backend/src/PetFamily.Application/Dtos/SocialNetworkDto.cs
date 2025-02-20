namespace PetFamily.Application.Dtos;

public class SocialNetworkDto(string name, string url)
{
    public string Name { get; init; } = name;
    public string Url { get; init;} = url;
}
