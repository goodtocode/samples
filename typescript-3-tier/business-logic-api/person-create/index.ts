import { AzureFunction, Context, HttpRequest } from "@azure/functions"

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');
    const name = (req.query.name || (req.body && req.body.name));
    const email = (req.query.email || (req.body && req.body.email));

    if (name && email) {
        // Send a "hello" response.
        context.res = {
            status: 200, /* Defaults to 200 */
            body: "Hello " + name + " - " + email
        };
    }
    else {
        context.res = {
            status: 400,
            body: "Please pass name and email on the query string or in the request body"
        };
    }
};

export default httpTrigger; 