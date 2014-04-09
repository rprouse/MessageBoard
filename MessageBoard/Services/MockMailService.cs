using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MessageBoard.Services
{
    public class MockMailService : IMailService
    {
        #region Implementation of IMailService

        public bool SendMail( string @from, string to, string subject, string body )
        {
            Debug.WriteLine( "Sending mail to " + to );
            return true;
        }

        #endregion
    }
}