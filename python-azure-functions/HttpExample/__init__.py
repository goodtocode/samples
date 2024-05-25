import logging
import azure.functions as func

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    try:
        # Get the two named parameters from the query string.
        num1 = req.params.get('num1')
        num2 = req.params.get('num2')
        
        # Convert the two strings to int and return the sum. 
        sum = int(num1) + int(num2)
        return func.HttpResponse(str(sum))
    except:
        return func.HttpResponse("Something went wrong!",status_code=500)