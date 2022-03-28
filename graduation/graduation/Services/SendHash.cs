using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace graduation.Services
{
    internal class SendHash
    {
        public HttpClient Client { get;}
        public SendHash()
        {
            Client = new HttpClient();
        }
    }
}
