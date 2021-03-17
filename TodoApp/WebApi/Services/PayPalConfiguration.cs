using Fistix.Training.Core.Config;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Services
{
  public class PayPalConfiguration
  {
    public static string ClientId = "AQm921FElnvm67C0zEH33eRkI7H2i1XM5r7MEyp49Gz_vuXT0D-aFbML5jTj2mKKbtOCKB7uyIagKwG4";
    public static string ClientSecret = "ELvlAIfdcDBRM7lC61hOe7C79tHCVGm9vLm3780vzJLU2270UugPNWQYzKS_8yk8xRWjPom_I6d1PlBt";
    private readonly MasterConfig _masterConfig = null;

    // Static constructor for setting the readonly static members.
    public PayPalConfiguration(MasterConfig masterConfig)
    {
      _masterConfig = masterConfig;
      //var config = GetConfig();
      //ClientId = config["clientId"];
      ClientId = _masterConfig.PayPalConfig.ClientId;
      //ClientSecret = config["clientSecret"];
      ClientSecret = _masterConfig.PayPalConfig.ClientSecret;
    }

    // Create the configuration map that contains mode and other optional configuration details.
    public static Dictionary<string, string> GetConfig()
    {
      var dict = new Dictionary<string, string>();
      dict["mode"] = "sandbox";
      dict["connectionTimeout"] = "360000";
      dict["requestRetries"] = "1";
      dict["clientId"] = ClientId;// "AQm921FElnvm67C0zEH33eRkI7H2i1XM5r7MEyp49Gz_vuXT0D-aFbML5jTj2mKKbtOCKB7uyIagKwG4";
      dict["clientSecret"] = ClientSecret;// "ELvlAIfdcDBRM7lC61hOe7C79tHCVGm9vLm3780vzJLU2270UugPNWQYzKS_8yk8xRWjPom_I6d1PlBt";
      return dict;//ConfigManager.Instance.GetProperties();
    }

    // Create accessToken
    private static string GetAccessToken()
    {
      // ###AccessToken
      // Retrieve the access token from
      // OAuthTokenCredential by passing in
      // ClientID and ClientSecret
      // It is not mandatory to generate Access Token on a per call basis.
      // Typically the access token can be generated once and
      // reused within the expiry window                
      string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
      //string accessToken = new OAuthTokenCredential(ClientId, ClientSecret);
      //string accessToken = "A21AALjODzbusucMa0GBW3MqJxEfEtydY-SHJ4di1wgUlS9LdiDZ0909uXIaFbBGBnDeFo8AoghOmHWMVmdGUZqucSIZQiBaA";
      return accessToken;
    }

    // Returns APIContext object
    public static APIContext GetAPIContext(string accessToken = "")
    {
      // ### Api Context
      // Pass in a `APIContext` object to authenticate 
      // the call and to send a unique request id 
      // (that ensures idempotency). The SDK generates
      // a request id if you do not pass one explicitly. 
      var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ? GetAccessToken() : accessToken);
      apiContext.Config = GetConfig();

      // Use this variant if you want to pass in a request id  
      // that is meaningful in your application, ideally 
      // a order id.
      // String requestId = Long.toString(System.nanoTime();
      // APIContext apiContext = new APIContext(GetAccessToken(), requestId ));

      return apiContext;
    }

  }
}
