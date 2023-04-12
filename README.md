# Assignment-WebBank


! IMPORTANT WHEN CONNECTING TO DATABASE !
defaultconnectionstring is connecting to { localhost\\SQLEXPRESS }.
if not running SQL EXPRESS on your SSMSM please change the connection string in (appsettings.json) by removing \\SQLEXPRESS and connect to localhost explicitly.
change appsettings.json defaultconnection from {  localhost\\SQLEXPRESS;  } ====> {  localhost;  } if not running SQLEXPRESS.


Bugs / Inormalities in the DataBase

1. Norway has no NationalId- Column data completly wiped from the table.
2. Accounts are able to have more than one customer. ("OWNER" "DISPONENT") this may not be a bug but not what i expected.
3. A customer transaction is bugged. one transaction is -3000 but the balance gets added with 3000 instead
