using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Build.Services.StaticFileBuilder
{
    public interface IBuilder
    {
        void BuildHead();
        void BuildMeta();
        void BuildBody();
    }
}
