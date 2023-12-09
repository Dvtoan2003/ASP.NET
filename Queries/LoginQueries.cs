using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Tranning.Models;

namespace Tranning.Queries
{
    public class LoginQueries
    {
        //chua cac logic sql xu ly voi database

        public LoginModel CheckLoginUser(string username, string password)
        {
            LoginModel dataUser = new LoginModel();
            using (SqlConnection conn = DatabaseConnection.GetSqlConnection())
            {
                string querySql = "SELECT * FROM users WHERE username = @username AND password =@password";
                //@uesrname va @ password: tham so truyen vao cua cau lenh sql voi gia tri duoc nhan tu 2 bien string username va password 
                // tao 1 doi tuong command de thuc thi cau lenh sql
                SqlCommand cmd = new SqlCommand(querySql, conn);
                cmd.Parameters.AddWithValue("@username", username ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@password", password ?? (object)DBNull.Value);
                //mo ket noi den databse
                conn.Open();
                //thucthi cau lenh sql
                using (SqlDataReader reader = cmd.ExecuteReader())
                { 
                    while (reader.Read())
                    {
                        //do du lieu tu table trong database vao model minh da dinh nghia
                        dataUser.UserId = reader["id"].ToString();
                        dataUser.UserName = reader["username"].ToString();
                        dataUser.EmailUser = reader["email"].ToString();
                        dataUser.RoleId = reader["role_id"].ToString();
                        dataUser.PhoneUser = reader["phone"].ToString();
                        dataUser.FullName = reader["full_name"].ToString();
                        dataUser.ExtraCode = reader["extra_code"].ToString();
                    }
                    //ngat ket noi
                    conn.Close();
                }


            } 
            return dataUser;
        }
    }
}
