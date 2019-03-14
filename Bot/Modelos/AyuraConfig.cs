﻿namespace Bot.Modelos
{
    public class AyuraConfig
    {
        public string token { private set; get; }
        public string prefix { private set; get; }
        public string ownerId { private set; get; }
        public uint id { private set; get; }
        public AyuraConfig(string token, string prefix, string ownerId, uint id)
        {
            this.token = token;
            this.prefix = prefix;
            this.ownerId = ownerId;
            this.id = id;
        }

        public AyuraConfig(uint id)
        {
            this.id = id;
        }
        public void SetBotConfig(string token, string prefix, string ownerId)
        {
            this.token = token;
            this.prefix = prefix;
            this.ownerId = ownerId;
        }
    }
}