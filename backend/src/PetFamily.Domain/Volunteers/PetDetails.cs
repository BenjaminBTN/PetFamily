using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers
{
    public record PetDetails
    {
        //private readonly List<PetRequisites> _requisites = [];
        //private readonly List<PetPhoto> _petPhotos = [];


        //private PetDetails(List<PetRequisites> requisites, List<PetPhoto> petPhotos)
        //{
        //    _requisites = requisites;
        //    _petPhotos = petPhotos;
        //}


        public List<PetRequisites> RequisitesForHelp { get; private set; } = [];

        public List<PetPhoto> PetPhotos { get; private set; } = [];


        //public static Result<PetDetails> Create(List<PetRequisites> requisites, List<PetPhoto> petPhotos)
        //{
        //    return new PetDetails(requisites, petPhotos);
        //}
    }
}