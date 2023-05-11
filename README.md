# Assignment-WebBank

Preview Project at this link: https://bankofscandinavia.azurewebsites.net/

Project built in RazorPages with good structure. Relevant folder names, Services, Class Library, VMs.

Database is hosted on Azure. You will have access to data from a dummyDB.

Feel free to test the application to the fullest.

*Users;
Role: Admin, Username: richard.chalk@systementor.se, Password: Hejsan123#
Role: Cashier, Username: richard.erdos.chalk@gmail.com, Password: Hejsan123#

*About 
Web based Bank App. Used by Admins and Cashiers. 
Admin have the authority to CRUD users and cashiers.
Cashiers have the authority to handle Transactions for customers.
Landing page is public.
Client-side validation implemented where needed with jquery scripts.
Server-side validation implmented where needed with ModelState.IsValid().

*Features:
"show less" button in transaction list: render all transactions. select a number (x) of how many transactions to be shown. (10, 20, 30, ... all)
Pagination implemented on Customer List page.
CRUD menu as dropdown list in header.
Deposit/ Withdraw/ Transfer located in AccountView page.


*This Solution contains 3 projects and a Class Library. 
Starting Project: Web Bank.
Second Project: API that gets Customers data and Account data.
Third Project: Checks all transactions if money laundry is ongoing and writes the result to a folder in the users :C drive called Reports.

*Bugs / Inormalities in the DataBase

1. Norway has no NationalId- Column data completly wiped from the table or not added from start.
2. Accounts are able to have more than one customer. ("OWNER" "DISPONENT") this may not be a bug but not what i expected.
3. A customer transaction is bugged. One transaction is -3000 but the balance gets added with 3000 instead.
