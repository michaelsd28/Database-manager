namespace Database_Manager.Model
{
    internal class DatabaseCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StorageSize { get; set; }
        //  public string Status { get; set; }
        public string Type { get; set; }
        public string URI { get; set; }

        public string status; // field

        public string userName { get; set; }

        public string userPassword { get; set; }

        public string userClientName { get; set; }

        public bool serverSSL  { get; set; }

        public bool allowAdmin { get; set; }

        public int serverPort { get; set; }

        public string Status   // property
        {
            get { return status; }   // get method
            set { status = value; }  // set method
        }





    }
}
