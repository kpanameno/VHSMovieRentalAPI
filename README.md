# VHSMovieRentalAPI
API for Movie Buy and Rentals
Introduction

By default, the application is ready to be used using Postman for endpoint testing and Google Chrome for the front end test that lists the movies available from the VHS Movie Rental store.
Steps to generate the database

1.	In Visual Studio, open the VHSMovieRentalAPI solution
2.	Open Package Manager Console and execute the Update-Database command
3.	Insert the sample records using the InsertSQL.sql script, test records will be inserted in all the Database tables
a.	Using SSMS: Locate the VHSMovieRentalDB database in the SQLEXPRESS instance, and execute the query from there.
b.	Using Server Explorer: Add the connection to the database using as SQLEXPRESS server and VHSMovieRentalDB database. Once the connection was added, the record insertion script would be executed.
At this point the application and the database are assembled and ready to be used.
Add Postman Collection and Environment
Steps to add the request collection to evaluate each point
1.	Open Postman
2.	Use the Import -> Import From Link option, and copy the following link
https://www.getpostman.com/collections/3799589e29eef180aa71
3.	Import the VHS Environment, use the option Manage Environments -> Import and then add the file VHS.postman_globals.json. In case, the value for the Administrator and Customer tokens are not imported correctly, then try adding one for each role with the values at the end of this document.
At this point the application's Endpoints can be consulted using postman and the JWT authentication tokens are generated for the administrator and client roles.
Endpoint operation

At each point is the explanation of the operation and the necessary steps to verify the requirement. For requests that require authentication it is necessary to use JWT tokens which can be generated using the Login method with the following credentials:
-	User ADMIN: 		kevinadmin	Password: 	password
-	User CUSTOMER: 	kevin		Password: 	password2

Table of API usage

Requirement	Authorization	Endpoint 	Notes
*********************************************************************
- Requirement: Add a Movie
- Authorization: Administrator
- Endpoint: Add Movie
- Notes:
*********************************************************************
- Requirement: Modify a Movie
- Authorization: Administrator
- Endpoint: Update Movie & Add Log
- Notes: Only admins can modify Availabilty
*********************************************************************
- Requirement: Delete a Movie
- Authorization: Administrator
- Endpoint: Delete Movie
- Notes:
*********************************************************************
- Requirement: Save a Log of Tile, Rental/Sale Price
- Authorization: Customer
- Endpoint:Update Movie & Add Log
- Notes: These logs are created during a Movie Update. The Movie entity will remain with the latest values, while the Log table will keep track of the historic values
*********************************************************************
- Requirement: Movie Rent and Purchase
- Authorization: Customer
- Endpoint: Save Transaction & Details
- Notes: For rent or purchase, the stock quantity of the movie will be evaluated before any further action
*********************************************************************
- Requirement: Keep a log of Movie Rentals and Purchases
- Authorization: Customer
- Endpoint: Save Transaction & Details
- Notes: These details are stored while making a Transaction. The price comes from the Movie entity 
*********************************************************************
- Requirement: Rental return
- Authorization: Administrator
- Endpoint: Movie Rental return
- Notes: If the return date exceeds the maximum amount of days for renting a movie without a penalty, the price of the detail will be affected, and then the Return status of the Detail will be updated to True.
*********************************************************************
- Requirement: Like a Movie
- Authorization: Customer
- Endpoint: Movie Like
- Notes: One user can only like one movie once. Like / unlike functionality
*********************************************************************
- Requirement: List all Movies
- Authorization: All
- Endpoint: List movies by Availabilty, Title
- Notes:  *Sort by Title (default)
          *Sort by Like
          *Pagination
          *Search by Name
*********************************************************************
- Requirement: User Login
- Authorization: All
- Endpoint: Login
- Notes: Use provided credentials
*********************************************************************
- Requirement: Get Movie Details
- Authorization: All
- Endpoint: Get Movie Details
- Notes: 
*********************************************************************
- Requirement: Forgot Password
- Authorization: Administrator/Customer
- Endpoint: Forgot Password
- Notes: The email will be sent to the email stored in the Users table. If you want to receive the email the easiest way is to alter an existing user record and update the mail with your email account.
*********************************************************************
- Requirement: Password Reset
- Authorization: Administrator/Customer
- Endpoint: Password Reset
- Notes: This link will only work once before changing the Password, this is because a Token is generated using a secret string combined with the old password. Take the token from the URL in the email you received by using the ‘Forgot Password’ endpoint and paste it in the body of this request along with the new password.
*********************************************************************
- Requirement: Confirm User account
- Authorization: Administrator/Customer
- Endpoint: Activate Account
- Notes: When you create an User using the endpoint in the UserController, the User entity is stored in the DB with  Active status false, then you will receive an email to the email account you just provided and by clicking the link inside the email, your user will be activated
*********************************************************************
- Requirement: Change other User's role
- Authorization: Administrator
- Endpoint: Update user Role
- Notes: The Endpoint updates a User including their role. But only Admins can update their role or other people's Role

Front End Application

In the following url there is a table that lists all the available movies.
https://localhost:44306/Default/Index
You can sort by title, or by number of likes. There is also the option to search by title. In addition, it supports pagination and number of records per page.

JWT for Admins and Customers

-	Admin
o	JWTTOKEN eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwicm9sZSI6IkFkbWluaXN0cmF0b3IiLCJuYmYiOjE1ODI0NDU1NTIsImV4cCI6MTU4MzA1MDM1MiwiaWF0IjoxNTgyNDQ1NTUyfQ.z7LvObkaXt2R-_fCT2P4mmt4LOA5HVOL_rYWKimXfv4
-	Customer
o	JWTTOKEN_CUSTOMER 
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwicm9sZSI6IkN1c3RvbWVyIiwibmJmIjoxNTgyNDQ2OTkzLCJleHAiOjE1ODMwNTE3OTMsImlhdCI6MTU4MjQ0Njk5M30.d3H-ZJcVA8dsAGu2r7o8segFcew-S4JXRBVX9_q4KMM

