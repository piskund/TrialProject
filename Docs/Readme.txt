1. Instructions to install and configure prerequisites or dependencies:

The application requires SQLEXPRESS installed on the server. 
Make sure there is the instance .\SQLEXPRESS exists and the user with uid = sa and password = 1q2w3e presents there.

Considered that a backup client stores files from the local directory named sharedFolder to the server's 
directory named serverSharedFolder. Before start create such folders please and share it over network

On the client side: 
The user configurable in the App.config of Backup.Client.Console.App, section
      <register type="Backup.Common.Helpers.ClientRegistrationHelper, Backup.Common">
        <constructor>
          <param name="userName" value="ShareUser" />
          <param name="password" value="1q2w3e" />
          <param name="folderPath" value="sharedFolder" />
        </constructor>
      </register>

On the server side:
          <param name="userName" value="ServerUser" />
          <param name="password" value="1q2w3e" />
          <param name="folderPath" value="serverSharedFolder" />

Please make sure such users are exists on the appropriate systems and read/write access to the according folders are granted to them.

After that in order to start system it's necessary to host Backup.Web.API somewhere and set in the client's 
configs (App.config of Backup.Client.Console.App.App.config) parameter webServiceUrl to the appropriate value.

When all these prerequisites are met starting of the any number of clients should lead to starting backup of the files from 
sharedFolder to serverSharedFolder ( to the subfolders with client's ip, which will be create automatically on the first run ) on the 
repetitive manner.

2. Instructions to create and initialize the database: 

Make sure there are SQLEXPRESS presents on the server side ( uid = sa; password = 1q2w3e; instance .\SQLEXPRESS )
Database BackupDB will be created automatically by the code on the first access, under the credentials, described above


3. Assumptions I have made:

I assume that someone like network system administrator will always perform the steps described in my prerequisites. 
Most of them have to be automated in the real system, though. I just didn't have enough time to create 100% 
ready-to-production backup system.

4. Requirements that I have not covered in my submission:
The following parts are suffered becasue lack of time:
- MVC web application. I was able to create just very basic demo to demonstrate the ability of the parts of the system to reuse the code.
- Test's coverage. I've added a couple of tests on the most crucial parts of every subsystem, but of course it is not even close to the 
well-tested system.
- Documentation and demonstration video. Just didn't have enough time to complete everything.
- Fault tolerance. There are just a basic error handling, rather with demonstration of approach purpose.

5. Instructions to configure and prepare the source code to build and run properly:

Opening of Crossover.Trial.sln in  the Visual Studio 2015 and building it should crate fully working application. All necessary libraries
will be download from the nuget on the first run (the internet connection have to be present, of course).

There are 3 major points in the solution
1) Backup.Client.Console.App - simple client configurable by its App.config, as described above. All the client's logic located in the separate
Backup.Client.BL, so it would be very easy to create any different kind of clent with the same functionality. 

2) Backup.Web.API - Web API service to perform work as described in the assignment

3) Backup.Admin.WebApp - MVC web admint ( rather skeleton of it because I didn't have enough time to complete it as required).

All parts are based on the IoC principles ( with help of the unity framework ) and lousely coupled 
( I have never used direct access to a database or server, but wraps such requests to the facades/repositories etc with help of Dependency Injections )

6. Issues you have faced while completing the assignment:

Well, the hardest part was lack of time to done everything perfectly plus some kind of a flu I've caught meanwhile.

7. Constructive feedback for improving the assignment:

Probably a task could be smaller - it's rally hard to find a time to produce close to real production system, especially when you have some other work.
But it was challenging and interesting, thank you.

P.S. The code of this system also availabe through github:
https://github.com/piskund/TrialProject