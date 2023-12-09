using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Tranning.Validations;

namespace Tranning.Models
{
    public class UserModel
    {
        public List<UserDetail> UserDetailLists { get; set; }
    }

    public class UserDetail
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Enter name, please")]
        public string username { get; set; }
        public int role_id { get; set; }
        public string extra_code { get; set; }
        
        public string password { get; set; }
        public string email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string gender { get; set; }
        public DateTime? birthday { get; set; }
        public string? avatar { get; set; }
        public string full_name { get; set; }
        public string? education { get; set; }
        public string? programming_language { get; set; }
        public int? toeic_score { get; set; }
        public string? experience { get; set; }
        public string? department { get; set; }
           
        [Required(ErrorMessage = "Choose Status, please")]
        public string status { get; set; }

        [Required(ErrorMessage = "Choose file, please")]
        [AllowedExtensionFile(new string[] { ".png", ".jpg", ".jpeg" })]
        [AllowedSizeFile(3 * 1024 * 1024)]
        public IFormFile? Photo { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}