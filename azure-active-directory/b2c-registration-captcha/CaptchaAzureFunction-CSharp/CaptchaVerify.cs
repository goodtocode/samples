using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

namespace b2c_registration_captcha_function
{
    public static class Function1
    {
        [FunctionName("Captcha")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            // Body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var gRecaptchaResponse = data("extension_" + Environment.GetEnvironmentVariable("B2C_EXTENSIONS_APP_ID") + "_CaptchaUserResponseToken");

            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={Environment.GetEnvironmentVariable("CaptchaSecretKey")}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                log.LogError("Error while sending request to ReCaptcha");
                return new BadRequestResult();
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success.ToLower() != "true")
            {
                return new UnauthorizedResult();
            }

            return new OkResult();
        }

        public static void JSVersion()
        {
            // parse Basic Auth username and password
            //var header = req.headers["authorization"] || "", // get the header
            //    token = header.split(/\s+/).pop() || "", // and the encoded auth token
            //    auth = new Buffer.from(token, "base64").toString(), // convert from base64
            //    parts = auth.split(/:/), // split on colon
            //    username = parts[0],
            //    password = parts[1];

            //// Check for HTTP Basic Authentication, return HTTP 401 error if invalid credentials.
            //if (false
            //    //username !== process.env["BASIC_AUTH_USERNAME"] ||
            //    //password !== process.env["BASIC_AUTH_PASSWORD"]
            //) {
            //    context.res = {
            //        status: 401,
            //    };
            //    context.log("Invalid Authentication");
            //    return;
            //}

            //context.log('JavaScript HTTP trigger function processed a request.');

            //let data = req.body;


            //const extensionAttributeKey = "extension_" + process.env["B2C_EXTENSIONS_APP_ID"] + "_CaptchaUserResponseToken";

            //let captchaToken = data && data[extensionAttributeKey]; //extension app-id

            //// Calls Captcha API check for server-side validation of the generated token
            //let captchaApiCheck = captchaToken && await axios.post("https://www.google.com/recaptcha/api/siteverify", qs.stringify({
            //    "secret": process.env["CAPTCHA_SECRET_KEY"],
            //    "response": captchaToken
            //})).then(function (response) {
            //    const success = response.data.success;
            //    if (!success) {
            //        context.log("Captcha verification unsuccessful: " + JSON.stringify(response));
            //    }
            //    return success;
            //}).catch(function (err) {
            //    context.log.error("Some other issue with Captcha API call: " + JSON.stringify(err));
            //    return false;
            //});

            //context.log("value of captcha check");
            //context.log(captchaApiCheck);


            //var body = {
            //    "version": "1.0.0",
            //    "status": 400,
            //    "action": "ValidationError",
            //};
            //var status = 400;

            //if (!captchaToken) {
            //    context.log("No captcha token verification code was sent to the API.");
            //    body["userMessage"] = "Please complete the Captcha."
            //} else if (!captchaApiCheck) {
            //    body["userMessage"] = "Invalid Captcha. Please try again."
            //} else {
            //    status = 200;
            //    body = {
            //        "version": "1.0.0",
            //        "action": "Continue",
            //        [extensionAttributeKey]: "" //overwrites extension attribute to "" in order to not store it in the directory
            //    };
            //}

            //context.res = {
            //    status: status,
            //    body: body
            //};
        }
    }
}
