using System;
using PayPalCheckoutSdk.Core;
using PayPalHttp;

using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace INYTWebsite.Code
{
    public class PayPalClient
    {
        /**
            Set up PayPal environment with sandbox credentials.
            In production, use LiveEnvironment.
         */
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment("AYVD_uPWS6woa_5TRSFol92tn9jamHQrho5YFJletAtGdg6T7tkpAr-7IDEcNanFOJ3mqFyvYDcMu6Ns", "EBE48Ng5E8dy73kRKuv2rZZaKCgFkBLKP7sKh8a8cCIJTrx6PK-9K0qspWTkmLnb9jTrRz7fP89JBrpu");
        }

        /**
            Returns PayPalHttpClient instance to invoke PayPal APIs.
         */
        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            Use this method to serialize Object to a JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}
