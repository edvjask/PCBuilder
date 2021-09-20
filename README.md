# PCBuilder
In order to it out yourself, you will need MySQL server running on port 3306, .NET environment, and Node installed on your machine.

## 1. Import the database file

In order to have some basic data, **pcbuilder.sql** database file is provided at the root of this repo, which should be imported into your MySQL DBMS (on Windows [XAMPP](https://www.apachefriends.org/download.html) can be used).
The backend API is configured to connect to MySQL databse running on *localhost* port 3306 with default credentials for testing purposes, however, you can change the parameters to your liking in *appsettings.json* file, located in PCBuilder_API/PCBuilder folder.


## 2. Starting the backend

The backend API is written using [.NET Core](https://dotnet.microsoft.com/download) libraries, therefore its runtimes need to be installed on your machine to be able to run *dotnet* commands.

To start the server, simply navigate to *PCBuilder_API* folder in your terminal and run *dotnet run* command:
```
cd "your path to the files\PCBuilder_API\PCBuilder
dotnet run
```
If the commands were successful, you will see the server started:
![image](https://user-images.githubusercontent.com/44035175/134076363-dc9fe3c2-0a6e-4af6-8eae-f815a5556b32.png)

## 3. Starting the client

The client is written using React.js and its packages and needs [Node.js](https://nodejs.org/en/download/) in order to run it.
Simply navigate to *PCBuilder_Client* folder in your terminal and run *npm start* command:
```
cd "your path to the client\PCBuilder_Client"
npm start
```
If the above commands were successful, you will see the development server deployed:
![image](https://user-images.githubusercontent.com/44035175/134077809-74b94fd3-7733-424f-ad8c-b4b94cef52e7.png)

## Using the system

To access the client, navigate to https://localhost:3000 in your browser and you will be greeted with this window:

![image](https://user-images.githubusercontent.com/44035175/134077989-9f056026-96ea-49e5-a76c-31536644a336.png)

To login to *admin* user and its actions, use credentials:
>Email: admin@admin.com  
>Password: admin

Normal users can be registered, or you can use:
> Email: edvinas@gmail.com  
> Password: edvinas

### Screenshots

![image](https://user-images.githubusercontent.com/44035175/134079022-6d15ad07-2623-44b9-ae59-06d14e9df254.png)
![image](https://user-images.githubusercontent.com/44035175/134079046-bf1b14c1-8cb0-4442-9ce3-8dd1ef4a9f05.png)
![image](https://user-images.githubusercontent.com/44035175/134079065-0a6540ed-551f-41a3-860e-840b100f6bf6.png)
![image](https://user-images.githubusercontent.com/44035175/134079088-b65ff930-d223-4b35-80f2-b93d340b4818.png)


