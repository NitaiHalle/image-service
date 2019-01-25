using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shered;

namespace WebApplication2.Commuincation
{
    interface IWebClient
    {

        event EventHandler<TypeEventArgs> OnDataRecived;
        //for close gui and for remove dir
        bool connected();
        void send(object sender, TypeEventArgs e);
        void start();
        void stop();

    }
}


