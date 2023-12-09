using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Training.DataDBContext
{
    public class User
    {
        // tao cac cot nam trong table
        // mh dang quy uoc tabale bo ten class
        [Key]
        public int id { get; set; }

        [Column("role_id", TypeName = "Integer"), Required]
        public int role_id { get; set; }
        [Column("extra_code", TypeName = "Integer"), Required]
        public string extra_code { get; set; }
        [Column("username", TypeName = "Varchar(50)"), Required]
        public string username { get; set; }
        [Column("password", TypeName = "Varchar(50)"), Required]
        public string password { get; set; }
        [Column("email", TypeName = "Varchar(50)"), Required]
        public string email { get; set; }

        [Column("phone", TypeName = "Varchar(20)"), Required]
        public string? phone { get; set; }
        [Column("address", TypeName = "Varchar(100)"), AllowNull]
        public string? address { get; set; }
        [Column("gender", TypeName = "Varchar(50)"), Required]
        public string gender { get; set; }
        [Column("birthday", TypeName = "Varchar(50)"), AllowNull]
        public DateTime? birthday { get; set; }
        [Column("avatar", TypeName = "Varchar(200)"), AllowNull]
        public string? avatar { get; set; }
        [Column("full_name", TypeName = "Varchar(50)"), Required]
        public string full_name { get; set; }
        [Column("education", TypeName = "Varchar(50)"), AllowNull]
        public string? education { get; set; }
        [Column("programming_language", TypeName = "Varchar(50)"), AllowNull]
        public string? programming_language { get; set; }
        [Column("toeic_score", TypeName = "Varchar(50)"), AllowNull]
        public int? toeic_score { get; set; }
        [Column("experience", TypeName = "Integer"), AllowNull]
        public string? experience { get; set; }
        [Column("department", TypeName = "Varchar(50)"), AllowNull]
        public string? department { get; set; }

        [Column("status", TypeName = "Varchar(50)"), Required]
        public string status { get; set; }
        [AllowNull]
        public DateTime? created_at { get; set; }
        [AllowNull]
        public DateTime? updated_at { get; set; }
        [AllowNull]
        public DateTime? deleted_at { get; set; }

    }
}






