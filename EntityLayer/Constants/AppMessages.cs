using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Constants
{
    public static class AppMessages
    {
        public const string MAINTENANCE_TIME = "Sistem Bakımda.";
        public const string AUTHORIZATION_DENIED = "Yetkiniz Yok";
        public const string ID_CANNOT_BE_NULL = "Id NULL olamaz!";
        public const string SEND_EMAIL = "Mail gönderildi";
        public const string SEND_EMAIL_ERROR = "Mail gönderilemedi";
        public const string ACCESS_DENIED = "Giriş Reddedildi!";
        public const string LOGIN_SUCCEEDED = "Giriş Başarılı!";
        public const string ACCOUNT_NOT_FOUND = "Girilen mail'e ait bir hesap bulunamamıştır";
    }
}
