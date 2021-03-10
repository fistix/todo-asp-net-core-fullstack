using PayPalCheckoutSdk.Core;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Service
{
  public class PayPalClientService
  {
    /**
        Setting up PayPal environment with credentials with sandbox cerdentails. 
        For Live, this should be LiveEnvironment Instance. 
     */
    //public static PayPalEnvironment environment()
    //{
    //  return new SandboxEnvironment(
    //       System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID") != null ?
    //       //System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID") : "<<PAYPAL-CLIENT-ID>>",
    //       System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID") : "AQm921FElnvm67C0zEH33eRkI7H2i1XM5r7MEyp49Gz_vuXT0D-aFbML5jTj2mKKbtOCKB7uyIagKwG4",
    //      System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET") != null ?
    //       //System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET") : "<<PAYPAL-CLIENT-SECRET>>");
    //       System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET") : "ELvlAIfdcDBRM7lC61hOe7C79tHCVGm9vLm3780vzJLU2270UugPNWQYzKS_8yk8xRWjPom_I6d1PlBt");
    //}

    public static PayPalEnvironment Environment()
    {
      return new SandboxEnvironment("AQm921FElnvm67C0zEH33eRkI7H2i1XM5r7MEyp49Gz_vuXT0D-aFbML5jTj2mKKbtOCKB7uyIagKwG4",
        "ELvlAIfdcDBRM7lC61hOe7C79tHCVGm9vLm3780vzJLU2270UugPNWQYzKS_8yk8xRWjPom_I6d1PlBt");
    }

    /**
        Returns PayPalHttpClient instance which can be used to invoke PayPal API's.
     */
    public static HttpClient Client()
    {
      return new PayPalHttpClient(Environment());
    }

    public static HttpClient Client(string refreshToken)
    {
      return new PayPalHttpClient(Environment(), refreshToken);
    }

    /**
        This method can be used to Serialize Object to JSON string.
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
