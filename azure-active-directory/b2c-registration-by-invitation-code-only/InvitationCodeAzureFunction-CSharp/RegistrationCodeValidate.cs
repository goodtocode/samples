using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace b2c_reg_code_function
{
    public class RegistrationCodeValidate
    {
        private string[] validInvitationCodes = { "invitation-code-1", "invitation-code-2" };
        private ILogger log;

        [FunctionName("RegistrationCodeValidate")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger logger)
        {
            log = logger;
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                // Credentials
                if (!CheckCredentials(req))
                    return new BadRequestObjectResult(new ConflictResponse() { userMessage = "Invalid credentials." });

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogInformation(requestBody);
                if (string.IsNullOrWhiteSpace(requestBody))
                    return new ConflictObjectResult(new ConflictResponse() { userMessage = "No invitation code supplied." });

                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string invitationCodeAttributeKey = "extension_" + Environment.GetEnvironmentVariable("B2C_EXTENSIONS_APP_ID") + "_InvitationCode";
                string inviteCode = data[invitationCodeAttributeKey];
                log.LogInformation($"Code: {inviteCode}");
                if (string.IsNullOrWhiteSpace(inviteCode))
                    return new ConflictObjectResult(new ConflictResponse() { userMessage = "Please provide an invitation code." });
                else if (Array.IndexOf(validInvitationCodes, inviteCode) < 1)
                    return new ConflictObjectResult(new ConflictResponse() { userMessage = "Your invitation code is invalid. Please try again." });

                return new OkObjectResult(new SuccessResponse() { promoCode = "12345" });
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new BadRequestResult();
            }
        }

        private bool CheckCredentials(HttpRequest req)
        {
            bool returnValue;
            try
            {
                string authHeader = req.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    //the coding should be iso or you could use ASCII and UTF-8 decoder
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    int seperatorIndex = usernamePassword.IndexOf(':');
                    string username = usernamePassword.Substring(0, seperatorIndex);
                    string password = usernamePassword.Substring(seperatorIndex + 1);
                    // Compare stored vs. header user/pass
                    return username == Environment.GetEnvironmentVariable("BASIC_AUTH_USERNAME") && password == Environment.GetEnvironmentVariable("BASIC_AUTH_PASSWORD");
                }
                else
                {
                    returnValue = false;
                }
            }
            catch
            {
                returnValue = false;
            }
            log.LogInformation($"CheckCredentials: {returnValue}");

            return returnValue;
        }
    }
}
