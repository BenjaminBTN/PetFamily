using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.SpeciesManagement.VO;
using PetFamily.Domain.VolunteersManagement;
using PetFamily.Domain.VolunteersManagement.Entities;
using PetFamily.Domain.VolunteersManagement.Enums;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Domain.UnitTests
{
    public class VolunteerTests
    {
        [Fact]
        public void AddPet_FirstAddition_ReturnSuccessResult()
        {
            // arrange
            var firstPet = CreatePet();
            var volunteer = CreateVolunteerWithPets(0);

            // act
            var result = volunteer.AddPet(firstPet);

            // assert
            Assert.True(result.IsSuccess);
            Assert.True(volunteer.Pets[0].OrdinalNumber.Value == 1);
            Assert.Equal(firstPet, volunteer.Pets[0]);
        }

        [Fact]
        public void AddPet_WithOtherPets_ReturnSuccessResult()
        {
            int petsCount = 5;
            var volunteer = CreateVolunteerWithPets(petsCount);
            var newPet = CreatePet();

            var result = volunteer.AddPet(newPet);
            var addedPet = volunteer.GetPetById(newPet.Id).Value;

            Assert.True(result.IsSuccess);
            Assert.Equal(newPet.Id, addedPet.Id);
            Assert.True(newPet.OrdinalNumber.Value == petsCount + 1);
            Assert.True(addedPet.OrdinalNumber.Value == petsCount + 1);
        }

        [Fact]
        public void MovePet_WhenPetIsAlreadyAtNewPosition_ShouldNotMove()
        {
            const int petsCount = 5;
            var volunteer = CreateVolunteerWithPets(petsCount);
            var secondPosition = OrdinalNumber.Create(2).Value;

            var petOnFirst = volunteer.Pets[0];
            var petOnSecond = volunteer.Pets[1];
            var petOnThird = volunteer.Pets[2];
            var petOnFourth = volunteer.Pets[3];
            var petOnFifth = volunteer.Pets[4];

            var result = volunteer.MovePet(petOnSecond, secondPosition);

            Assert.True(result.IsSuccess);
            Assert.True(petOnFirst.OrdinalNumber.Value == 1);
            Assert.True(petOnSecond.OrdinalNumber.Value == 2);
            Assert.True(petOnThird.OrdinalNumber.Value == 3);
            Assert.True(petOnFourth.OrdinalNumber.Value == 4);
            Assert.True(petOnFifth.OrdinalNumber.Value == 5);
        }

        [Fact]
        public void MovePet_WhenNewPositionIsGreater_ShouldMoveOtherPetsBack()
        {
            const int petsCount = 6;
            var volunteer = CreateVolunteerWithPets(petsCount);
            var fithPosition = OrdinalNumber.Create(5).Value;

            var petOnFirst = volunteer.Pets[0];
            var petOnSecond = volunteer.Pets[1];
            var petOnThird = volunteer.Pets[2];
            var petOnFourth = volunteer.Pets[3];
            var petOnFifth = volunteer.Pets[4];
            var petOnSixth = volunteer.Pets[5];

            var result = volunteer.MovePet(petOnSecond, fithPosition);

            Assert.True(result.IsSuccess);
            Assert.True(petOnFirst.OrdinalNumber.Value == 1);
            Assert.True(petOnSecond.OrdinalNumber.Value == 5);
            Assert.True(petOnThird.OrdinalNumber.Value == 2);
            Assert.True(petOnFourth.OrdinalNumber.Value == 3);
            Assert.True(petOnFifth.OrdinalNumber.Value == 4);
            Assert.True(petOnSixth.OrdinalNumber.Value == 6);
        }

        [Fact]
        public void MovePet_WhenNewPositionIsLower_ShouldMoveOtherPetsForward()
        {
            const int petsCount = 6;
            var volunteer = CreateVolunteerWithPets(petsCount);
            var secondPosition = OrdinalNumber.Create(2).Value;

            var petOnFirst = volunteer.Pets[0];
            var petOnSecond = volunteer.Pets[1];
            var petOnThird = volunteer.Pets[2];
            var petOnFourth = volunteer.Pets[3];
            var petOnFifth = volunteer.Pets[4];
            var petOnSixth = volunteer.Pets[5];

            var result = volunteer.MovePet(petOnFourth, secondPosition);

            Assert.True(result.IsSuccess);
            Assert.True(petOnFirst.OrdinalNumber.Value == 1);
            Assert.True(petOnSecond.OrdinalNumber.Value == 3);
            Assert.True(petOnThird.OrdinalNumber.Value == 4);
            Assert.True(petOnFourth.OrdinalNumber.Value == 2);
            Assert.True(petOnFifth.OrdinalNumber.Value == 5);
            Assert.True(petOnSixth.OrdinalNumber.Value == 6);
        }

        [Fact]
        public void MovePet_WhenNewPositionIsFirst_ShouldMoveOtherPetsForward()
        {
            const int petsCount = 5;
            var volunteer = CreateVolunteerWithPets(petsCount);
            var firstPosition = OrdinalNumber.Create(1).Value;

            var petOnFirst = volunteer.Pets[0];
            var petOnSecond = volunteer.Pets[1];
            var petOnThird = volunteer.Pets[2];
            var petOnFourth = volunteer.Pets[3];
            var petOnFifth = volunteer.Pets[4];

            var result = volunteer.MovePet(petOnFifth, firstPosition);

            Assert.True(result.IsSuccess);
            Assert.True(petOnFirst.OrdinalNumber.Value == 2);
            Assert.True(petOnSecond.OrdinalNumber.Value == 3);
            Assert.True(petOnThird.OrdinalNumber.Value == 4);
            Assert.True(petOnFourth.OrdinalNumber.Value == 5);
            Assert.True(petOnFifth.OrdinalNumber.Value == 1);
        }

        [Fact]
        public void MovePet_WhenNewPositionIsLast_ShouldMoveOtherPetsBack()
        {
            const int petsCount = 5;
            var volunteer = CreateVolunteerWithPets(petsCount);
            var fifthPosition = OrdinalNumber.Create(5).Value;

            var petOnFirst = volunteer.Pets[0];
            var petOnSecond = volunteer.Pets[1];
            var petOnThird = volunteer.Pets[2];
            var petOnFourth = volunteer.Pets[3];
            var petOnFifth = volunteer.Pets[4];

            var result = volunteer.MovePet(petOnFirst, fifthPosition);

            Assert.True(result.IsSuccess);
            Assert.True(petOnFirst.OrdinalNumber.Value == 5);
            Assert.True(petOnSecond.OrdinalNumber.Value == 1);
            Assert.True(petOnThird.OrdinalNumber.Value == 2);
            Assert.True(petOnFourth.OrdinalNumber.Value == 3);
            Assert.True(petOnFifth.OrdinalNumber.Value == 4);
        }


        private static Volunteer CreateVolunteerWithPets(int petsCount)
        {
            var fullName = FullName.Create("A", "S", "D").Value;
            var description = Description.Create("Description").Value;
            var email = Email.Create("qq@qq.qq").Value;
            var phoneNumber = PhoneNumber.Create("76665554433").Value;

            var volunteer = Volunteer.Create(
                VolunteerId.NewId(),
                fullName,
                description,
                email,
                1.0,
                phoneNumber).Value;

            for(int i = 0; i < petsCount; i++)
                volunteer.AddPet(CreatePet());

            return volunteer;
        }

        private static Pet CreatePet()
        {
            var name = PetName.Create("A").Value;
            var typeInfo = PetType.Create(SpeciesId.NewId(), BreedId.NewId().Value).Value;
            var petColor = PetColor.Create("Red").Value;
            var phoneNumber = PhoneNumber.Create("76665554433").Value;

            var pet = Pet.Create(
                PetId.NewId(),
                name,
                typeInfo,
                petColor,
                1.0,
                1,
                phoneNumber,
                false,
                false,
                null,
                AssistanceStatus.NeedsHelp).Value;

            return pet;
        }
    }
}
