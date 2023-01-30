using DemoApplication.Contracts.Email;

namespace DemoApplication.Contracts.Email
{
    public class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Activation account";
            public const string NOTIFICATION_MESSAGE = $"Sifarishinizin Statusu Yenilendi Xais Edirik Yoxlayin";

        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Your activation url : {EmailMessageKeyword.ACTIVATION_URL}";
        }
    }
}
