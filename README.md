# Best-Ledger-Solution
Solution for the bank ledger coding challenge

This is a very basic solution to the banking ledger challenge; implemented as a console application using C#.

### Features
* Users can create new accounts
* Users can login with their username and password
* Users can make deposits and withdrawals to and from their account
* When a deposit or withdrawal occurs, a transaction is created representing what was done
* Users can view their transaction history
* Users can logout
* Information is persisted across sessions using the file system

### Storage
I decided to use the local file system for storing user and transaction information. 
* Information is written to external files in JSON format since this is a format that is widely used by many applications. 
* JSON data is also easy to work with.
* When creating users and transactions a directory is created in the project structure to store this information so that you can look at the information easily. Generally all this information is better stored in a database away from prying eyes.

### Nuget Packages
I chose to use Newtonsoft.Json and BCrypt packages
* Newtonsoft.Json makes working with JSON data simple
* BCrypt is a nice hashing algorithm for hashing user passwords so that it isn't easy for attackers to gain access to user passwords.

### Further Considerations
This project is a fairly basic implementation of the problem at hand. As such there are things that could be changed. These considerations include:
* Refactoring the project in order to adhere to SOLID principles of Object Oriented Design
* Refactor code to ensure that things are being done in the appropriate place
* Implementing a database to store user and transaction information
* Use a salt on top of hashing user passwords to further strengthen the application against attackers looking to get user passwords

### Other Mentions
Please feel free to look through my other public repositories to see some of the other projects that I have worked on. In particular:

[a link](https://github.com/cs-capstone-team-c/synchrolean-server)
Implemented UnitOfWork and Repository pattern in the application as well as worked on many of the various Controllers and Repositories used.

[a link](https://github.com/cs-capstone-team-c/synchrolean-web-app)
Designed and developed most of the front end in accordance with what the sponser wanted.
