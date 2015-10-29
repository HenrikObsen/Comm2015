using System.Data.SqlClient;


namespace CommRepo
{

    public class LikeFac : AutoFac<Like>
    {

        public int CountLikes(int ForumID)
        {
            int count = 0;

            using (var cmd = new SqlCommand("SELECT COUNT(ID) AS Antal FROM [Like] WHERE ForumID=@ForumID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@ForumID", ForumID);
                
                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    count = int.Parse(r["Antal"].ToString());
                }

                r.Close();
                cmd.Connection.Close();
            }

            return count;
        }

        public void AddLike(int ForumID, int UserID)
        {
            int ID = LikeExists(ForumID, UserID);

            if (ID > 0)
            {
                Delete(ID);
            }
            else
            {
                Like l = new Like();
                l.ForumID = ForumID;
                l.UserID = UserID;
                Insert(l);
            }
        }


        /// <summary>
        /// Denne metode bruges til at se om et like eksistere i databasen.
        /// </summary>
        /// <param name="ForumID">IDet på det indlæg der er liket</param>
        /// <param name="UserID">IDet på brugeren der har liket</param>
        /// <returns>0 hvis der ikke eksistere et lik. IDet på liket hvis det eksistere</returns>
        public int LikeExists(int ForumID, int UserID)
        {
            int id = 0;

            using (var cmd = new SqlCommand("SELECT ID FROM [Like] WHERE UserID=@UserID AND ForumID=@ForumID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ForumID", ForumID);

                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    id = int.Parse(r["ID"].ToString());
                }

                r.Close();
                cmd.Connection.Close();
            }
            return id;
        }
    }
}