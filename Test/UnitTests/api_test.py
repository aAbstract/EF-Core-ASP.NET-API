import requests
import json
import urllib3
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

api_url = 'https://127.0.0.1:7240/api'

# utils
def log_json(data):
    json_obj = json.loads(data)
    print(json.dumps(json_obj, indent=2))

# test routines
def test_login_api():
    url = f"{api_url}/login"
    json_data = {
        'username': 'Ali',
        'password': 'TestPass'
    }
    http_res = requests.post(url, json=json_data, verify=False)
    log_json(http_res.content.decode())
    json_obj = json.loads(http_res.content.decode())
    return json_obj['token']

def test_customers_api():
    auth_token = test_login_api()
    fake_headers = {
        'Authorization': f"Bearer {auth_token}"
    }
    url = f"{api_url}/customers"
    http_res = requests.get(url, headers=fake_headers, verify=False)
    log_json(http_res.content.decode())

def test_add_user_api():
    # add user to database
    reg_json_data = {
        'FirstName': 'test',
        'LastName': 'user6',
        'Password': 'testpass',
        'RePassword': 'testpass'
    }
    reg_url = f"{api_url}/signup"
    reg_http_res = requests.post(reg_url, json=reg_json_data, verify=False)
    log_json(reg_http_res.content.decode())
    # login using this data
    login_json_data = {
        'username': f"{reg_json_data['FirstName'].lower()}-{reg_json_data['LastName'].lower()}",
        'password': 'testpass'
    }
    login_url = f"{api_url}/login"
    login_http_res = requests.post(login_url, json=login_json_data, verify=False)
    log_json(login_http_res.content.decode())

# main routine
test_add_user_api()
