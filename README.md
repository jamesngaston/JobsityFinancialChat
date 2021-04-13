# JobsityFinancialChat

## Steps to run the application
- Open the *JobsityFinancialChat.sln* file
- Run the command *Update-Database* in the **Package Manager Console**
- Run the solution

## Features
- .NET identity login (passwords must be at least 6 characters long and contain uppercase, lowercase, number and special characters)
- Async Chatroom
- FinacialBot (Enter '/stock=' followed by the stock code and the bot will check it at stooq.com)
- Api for checking the stock at stooq.com (~/Api/Stooq/Stock?code=*stockcode*)
