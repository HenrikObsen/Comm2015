using System;
using System.Data.SqlClient;

namespace CommRepo
{


    public class ForumFac : AutoFac<Forum>
    {

        public string AntalSvar(int EmneID)
        {
            string count = "0";

            using (var cmd = new SqlCommand("SELECT COUNT(ID) AS Antal FROM Forum WHERE SubID=@ID;", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@ID", EmneID);
                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    count = r["Antal"].ToString();
                }
                r.Close();
                cmd.Connection.Close();
            }
            return count;
        }
    }

}
