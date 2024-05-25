import { AzureFunction, Context, HttpRequest } from "@azure/functions"
import * as personList from '../person-list.json'

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    if (personList) {
        context.res = {
            status: 200,
            body: personList
        };
    }
    else {
        context.res = {
            status: 500,
            body: "An error has occurred."
        };
    }
};

export default httpTrigger; 