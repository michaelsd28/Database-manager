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

        private string status; // field

        private string userName { get; set; }

        private string userPassword { get; set; }

        private string userClientName { get; set; }

        private bool serverSSL  { get; set; }

       private bool allowAdmin { get; set; }

        private int serverPort { get; set; }

        public string Status   // property
        {
            get { return status; }   // get method
            set { status = value; }  // set method
        }





    }
}
