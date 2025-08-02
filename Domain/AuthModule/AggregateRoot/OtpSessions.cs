using Domain.AuthModule.Enum;
using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace Domain.AuthModule.AggregateRoot
{
    public class OtpSession : Entity, IAggregateRoot
    {
        public int? UserId { get; private set; }
        public string PhoneOrEmail { get; private set; } = string.Empty;
        public string OtpCode { get; private set; } = string.Empty;
        public OtpType Type { get; private set; }
        public int AttemptCount { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiredAt { get; private set; }
        public DateTime? VerifiedAt { get; private set; }
        public OtpStatus Status { get; private set; }

        private const int MaxAttempts = 5;

        // Constructor khởi tạo gửi OTP
        public OtpSession(string phoneOrEmail, OtpType type, string otpCode, TimeSpan validDuration)
        {
            PhoneOrEmail = phoneOrEmail;
            Type = type;
            OtpCode = otpCode;
            CreatedAt = DateTime.UtcNow;
            ExpiredAt = CreatedAt.Add(validDuration);
            Status = OtpStatus.Pending;
            AttemptCount = 0;
        }

        public void SetUser(int? userId)
        {
            UserId = userId;
        }

        public void Verify(string inputCode)
        {
            if (Status == OtpStatus.Verified)
                throw new InvalidOperationException("OTP đã được xác minh.");

            if (Status == OtpStatus.Expired)
                throw new InvalidOperationException("OTP đã hết hạn.");

            if (DateTime.UtcNow > ExpiredAt)
            {
                Status = OtpStatus.Expired;
                throw new InvalidOperationException("OTP đã hết hạn.");
            }

            if (inputCode != OtpCode)
            {
                AttemptCount++;

                if (AttemptCount >= MaxAttempts)
                { 
                    Status = OtpStatus.Failed;
                    throw new InvalidOperationException("Vượt quá số lần nhập sai OTP.");
                }

                throw new InvalidOperationException("Mã OTP không đúng.");
            }

            // Đúng OTP
            VerifiedAt = DateTime.UtcNow;
            Status = OtpStatus.Verified;
        }

        public bool IsVerified => Status == OtpStatus.Verified;

        public bool IsExpired => Status == OtpStatus.Expired || DateTime.UtcNow > ExpiredAt;
    }
}
