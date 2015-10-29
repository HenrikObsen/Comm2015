using System;

namespace CommRepo
{

    public class Forum
    {
        public int ID { get; set; }

        public string Emne { get; set; }

        public int BrugerID { get; set; }

        public DateTime Dato { get; set; }

        public string Tekst { get; set; }

        public int SubID { get; set; }



    }

}
