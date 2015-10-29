using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CommRepo
{


    public class ChatFac : AutoFac<Chat>
    {
        public List<Chat> GetChatList(int Antal)
        {
            using (var cmd = new SqlCommand("SELECT TOP " + Antal + " * FROM Chat ORDER BY ID DESC", Conn.CreateConnection()))
            {
                var mapper = new Mapper<Chat>();
                var list = mapper.MapList(cmd.ExecuteReader());

                cmd.Connection.Close();

                return list;
            }
       }

        public string GetSmilys(string tekst)
        {
            string retur = tekst.Replace(":-)", "<img src=\"Smilys/Smile.gif\" />");
            retur = retur.Replace(":)", "<img src=\"Smilys/Smile.gif\" />");
            retur = retur.Replace(":-(", "<img src=\"Smilys/Sad.gif\" />");
            retur = retur.Replace(":(", "<img src=\"Smilys/Sad.gif\" />");

            //retur = retur.Replace("[[hest]]", "<script>window.location.assign('https://www.youtube.com/watch?v=H8G6O9Zpo64');</script>");

            return retur;
        }

    }

}
