using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HeadHunter2.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User { }
    public class UserMetaData
    {
        [Remote("IsEmailAvailable", "Account", ErrorMessage = "User Already Available")]
        public string Email
        {
            get;
            set;
        }
    }
}