using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct.Authentication
{
    public interface IAuthProviderSettings
    {
        string this[string key]
        {
            get;
        }
    }
}
