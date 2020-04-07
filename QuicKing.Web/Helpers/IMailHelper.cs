using QuicKing.Common.Models;

namespace QuicKing.Web.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }

}
