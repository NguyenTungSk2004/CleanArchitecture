using System.ComponentModel;

namespace Domain.AuthModule.Enum
{
    public enum ProviderType
    {
        [Description("Tài khoản nội bộ")]
        Local = 0,
        [Description("Tài khoản Google")]
        Google = 1,
        [Description("Tài khoản Facebook")]
        Facebook = 2,
        [Description("Tài khoản Apple")]
        Apple = 3
    }

    public enum OtpType
    {
        [Description("Xác thực đăng nhập")]
        Login = 0,
        [Description("Đăng ký tài khoản")]
        Register = 1,
        [Description("Đặt lại mật khẩu")]
        ResetPassword = 2
    }

    public enum UserStatus
    {
        [Description("Bị cấm")]
        Banned = 0,
        [Description("Đang hoạt động")]
        Actived = 1,
        [Description("Chưa kích hoạt")]
        NotActived = 2,

    }
}