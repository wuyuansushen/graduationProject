using System;
using System.Collections.Generic;
using System.Text;

namespace graduation.Services
{
    public interface ILocalDownloadService
    {
        void WriteTorrent(string hash);
    }
}
