using Shared.DataTransferObjects;
using Shared.Exceptions;

namespace Shared.Helpers
{
    public static class GymDtoChecker
    {
        public static bool IsValidGymDto(GymForCreationDto gymDto)
        {
            if (gymDto.Name!.Length > 60)
                throw new CreationListParameterException($"Name length in gymDto for creation is {gymDto.Name!.Length}, but should not be longer than 60.");

            if (gymDto.Address!.Length > 100)
                throw new CreationListParameterException($"Address length in gymDto for creation is {gymDto.Address!.Length}, but should not be longer than 100.");

            return true;
        }

        public static bool IsValidGymDto(GymForUpdateDto gymDto)
        {
            if (gymDto.Name!.Length > 60)
                throw new CreationListParameterException($"Name length in gymDto for creation is {gymDto.Name!.Length}, but should not be longer than 60.");

            if (gymDto.Address!.Length > 100)
                throw new CreationListParameterException($"Address length in gymDto for creation is {gymDto.Address!.Length}, but should not be longer than 100.");

            return true;
        }
    }
}
