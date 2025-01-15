namespace PetFamily.Infrastructure.Options
{
    public class MinioOptions
    {
        public const string MINIO = "Minio";

        public string Endpoint { get; init; } = String.Empty;
        public string AccessKey {  get; init; } = String.Empty; 
        public string SecretKey { get; init; } = String.Empty;
    }
}
