using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Web.Script.Serialization;

namespace GraphApiSamples
{
    public static class Utility
    {
        public static string ExtractErrorMessage(this Exception exception)
        {
            List<string> errorMessages = new List<string>();
            
            string tabs = "\n";
            while (exception != null)
            {
                string requestIdLabel = "requestId";
                if (exception is DataServiceClientException &&
                    exception.Message.Contains(requestIdLabel))
                {
                    Dictionary<string, object> odataError =
                        new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(exception.Message);
                    odataError = (Dictionary<string, object>)odataError["odata.error"];
                    errorMessages.Insert(0, "\nRequest ID: " + odataError[requestIdLabel]);
                    errorMessages.Insert(1, "Date: " + odataError["date"]);
                }
                tabs += "    ";
                errorMessages.Add(tabs + exception.Message);
                exception = exception.InnerException;
            }

            return string.Join("\n", errorMessages);
        }
    }
}
