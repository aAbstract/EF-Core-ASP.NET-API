import pyodbc

# config
server_addr = '127.0.0.1'
database_name = 'db_api_test'
user_name = 'SA'
password = 'p@55word'

# state
cursor = None

# utils
def map_sql_params(sql_params):
    return {
        'sql_keys': ','.join([key for key in sql_params.keys()]),
        'sql_vals': [sql_params[key] for key in sql_params.keys()],
        'sql_str': ','.join(['?' for _ in sql_params.keys()])
    }

# routines
def init_customers():
    data = [
        {
            'FirstName': 'Ahemd',
            'LastName': 'Hassan',
            'Address': 'Test_addr',
            'Phone': '0123456789'
        },
        {
            'FirstName': 'Ali',
            'LastName': 'Ashraf',
            'Address': 'Test_addr',
            'Phone': '0123456789'
        },
        {
            'FirstName': 'Ramy',
            'LastName': 'Galal',
            'Address': 'Test_addr',
            'Phone': '0123456789'
        },
    ]
    for obj in data:
        sql_map = map_sql_params(obj)
        sql_cmd = f"""
                INSERT INTO Customers({sql_map['sql_keys']})
                VALUES ({sql_map['sql_str']})
                """
        print('executing cmd:')
        print(sql_cmd)
        try:
            cursor.execute(sql_cmd, tuple(sql_map['sql_vals']))
            conn.commit()
        except Exception as err:
            print(err)

def init_users():
    data = [
        {
            'Username': 'Ahmed',
            'Userrole': 'admin'
        },
        {
            'Username': 'Ali',
            'userrole': 'User'
        },
        {
            'USername': 'Ramy',
            'Userrole': 'User'
        },
    ]
    for obj in data:
        sql_map = map_sql_params(obj)
        sql_cmd = f"""
                INSERT INTO Users({sql_map['sql_keys']})
                VALUES ({sql_map['sql_str']})
                """
        print('executing cmd:')
        print(sql_cmd)
        try:
            cursor.execute(sql_cmd, tuple(sql_map['sql_vals']))
            conn.commit()
        except Exception as err:
            print(err)

# main script
print('using connection string:')
print(f"""
    DRIVER={{ODBC Driver 17 for SQL Server}};
    SERVER={server_addr};
    DATABASE={database_name};
    UID={user_name};
    PWD={password};
    """)

try:
    conn = pyodbc.connect(f"""
    DRIVER={{ODBC Driver 17 for SQL Server}};
    SERVER={server_addr};
    DATABASE={database_name};
    UID={user_name};
    PWD={password};
    """)
    cursor = conn.cursor()
except Exception as err:
    print(err)

if cursor != None:
    init_customers()
    # init_users()
else:
    print('can not connect to a database')
