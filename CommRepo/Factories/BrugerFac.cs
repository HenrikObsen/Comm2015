using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CommRepo
{

    public class BrugerFac : AutoFac<Bruger>
    {

        public void AddPoint(int BrugerID, int Antal)
        {
            using (var cmd = new SqlCommand("UPDATE Bruger SET Point = Point + " + Antal + " WHERE ID=@ID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@ID", BrugerID);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
        }

        public string GetUserName(int UserID)
        {
            string userName = "";

            using (var cmd = new SqlCommand("SELECT Brugernavn AS B FROM Bruger WHERE ID=@ID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@ID", UserID);

                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    userName = r["B"].ToString();
                }
                r.Close();
                cmd.Connection.Close();
            }

            
            return userName;
        }


        public Bruger Login(string brugernavn, string adgangskode)
        {
            Bruger b = new Bruger();
            Mapper<Bruger> mapper = new Mapper<Bruger>();

            using (var cmd = new SqlCommand("SELECT * FROM Bruger WHERE Brugernavn=@Brugernavn AND Adgangskode=@Adgangskode", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@Brugernavn", brugernavn.Trim());
                cmd.Parameters.AddWithValue("@Adgangskode", adgangskode.Trim());

                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    b = mapper.Map(r);
                }

                r.Close();
                cmd.Connection.Close();
            }
            return b;
        }


        public void UpdateOnlineStatus(int userID)
        {
            using (var cmd = new SqlCommand("UPDATE Bruger SET Online=@Online WHERE ID=@ID", Conn.CreateConnection()) )
            {
                cmd.Parameters.AddWithValue("@Online", DateTime.Now);
                cmd.Parameters.AddWithValue("@ID", userID);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public List<Bruger> GetOnlineUsers()
        {
            using (var cmd = new SqlCommand("SELECT * FROM Bruger WHERE Online>@Online",Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@Online", DateTime.Now.AddSeconds(-10));

                var mapper = new Mapper<Bruger>();
                var list = mapper.MapList(cmd.ExecuteReader());

                cmd.Connection.Close();

                return list;
            }
        }


    }



}
