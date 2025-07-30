using Domain.AuthModule.Entities;
using Domain.AuthModule.Enum;
using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace Domain.AuthModule.AggregateRoot
{
    public class User : Entity, ICreationTrackable, IAggregateRoot
    {
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? ShortName { get; private set; }
        public string? FullName { get; private set; }
        public string? Avatar { get; private set; }
        public string? Address { get; private set; }
        public string? Gender { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public UserStatus Status { get; private set; }

        public Account? Account { get; set; }

        public User(
            string? email,
            string? phoneNumber,
            string? fullName,
            string? address,
            string? gender,
            DateTime? birthDate
        )
        {
            UpdateProfile(email, phoneNumber, fullName, address, gender, birthDate);
        }
        public void UpdateProfile(
            string? email,
            string? phoneNumber,
            string? fullName,
            string? address,
            string? gender,
            DateTime? birthDate
        )
        {
            Email = email;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            Address = address;
            Gender = gender;
            BirthDate = birthDate;
        }
        public void SetAvatar(string? avatar)
        {
            Avatar = avatar;
        }
        public void SetShortName(string? shortName)
        {
            ShortName = shortName;
        }
        public void SetStatus(UserStatus status)
        {
            Status = status;
        }
    }
} 