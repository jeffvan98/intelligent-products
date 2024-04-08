from promptflow import tool
import json, os, requests

def get_product_by_id(id: str) -> object:
    service_url = os.environ.get("PRODUCT_SERVICE_URL", "http://localhost:5144/api/Product")
    request_url = service_url + "/" + id
    response = requests.get(request_url)

    if response.status_code >= 200 and response.status_code < 300:
        product = response.json()
    else:
        product = {}

    return product

def get_product_by_alternate_id(alt_id_type: str, alt_id: str) -> object:
    service_url = os.environ.get("PRODUCT_SERVICE_URL", "http://localhost:5144/api/Product")
    request_url = service_url + "/" + alt_id_type + "/" + alt_id
    response = requests.get(request_url)

    if response.status_code >= 200 and response.status_code < 300:
        product = response.json()
    else:
        product = {}

    return product

# The inputs section will change based on the arguments of the tool function, after you save the code
# Adding type to arguments and return value will help the system show the types properly
# Please update the function name/signature per need
@tool
def call_function(input: dict) -> object:
    result = {}

    if "function_call" in input:
        if not input["function_call"] is None:
            if not input["function_call"]["name"] is None:
                function_name = input["function_call"]["name"]
                if not input["function_call"]["arguments"] is None:
                    function_args = json.loads(input["function_call"]["arguments"])
                    result = globals()[function_name](**function_args)

    return result