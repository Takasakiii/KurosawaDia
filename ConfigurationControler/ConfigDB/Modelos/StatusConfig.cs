namespace ConfigurationControler.ConfigDB.Modelos
{
    public class StatusConfig
    {
        public int id { get; private set; }
        public string status { get; private set; }
        public string url { get; private set; }
        public int tipo { get; private set; }

        public void SetStatus(int id, string status, string url, int tipo)
        {
            this.id = id;
            this.status = status;
            this.url = url;
            this.tipo = tipo;
        }
    }
}
