# Assignment11Database
This is the 11th. assignment for PBA Database soft2019spring

# what it is
This is 2 C# projects;
One of them, called 'generator', is the project resposible for generating the class-source file and the sql file.<br>
The other, called 'ORM' makes use of the generated classes and sql to query a MySql database.<br>
<br>
Both of the project are set up to run in docker containers and both of them have the dotnet core SDK in the docker (in case you want to fiddle around with the code).

# Setup
*Before we set up the generator, I should mention that the generator is building output based on Kaspers JSON [jsonfile](https://github.com/cph-js284/Assignment11Database/blob/master/generator/specfile.txt) from the assignment text. I have included this in the docker file, so it gets build into the image*<br>
<br>
## Generator setup
1) enter the generator folder
2) build the docker image
```
sudo docker build -t generator .
```
3) Run the container
```
sudo docker run --name mygen01 generator
```
*At this point the source file and sql have been created, but are still inside the generator(mygen01) container*<br>
4) Extract the files from the container

NB you might get a permission denied when doing this copy, if that is the case you can grant permission 
```
sudo chmod  a+rw .
```
Extracting
```
sudo docker cp mygen01:/app/Generated.sql ./Generated.sql
sudo docker cp mygen01:/app/Generated.cs ./Generated.cs
```
*Alternatively you can run the generator container in detached mode (-d) and enter it using bash, and then copy out the 2 files to your host - or even better you can just check out the files here [Generated.sql](https://github.com/cph-js284/Assignment11Database/blob/master/Outputs/Generated.sql) and [Generated.cs](https://github.com/cph-js284/Assignment11Database/blob/master/Outputs/Generated.cs)* <br>
<br>
The source code for that generates these 2 files can be found here [Creator.cs](https://github.com/cph-js284/Assignment11Database/blob/master/generator/Creator.cs)<br>
<br>
## MySql Setup
To use the ORM we need to set up a MySql database first:
1) Start MySql in a docker container
```
sudo docker run -d --rm --name mysql01 -p3306:3306 -e MYSQL_ROOT_PASSWORD=test1234 mysql
```
*Now we need to create the database and the tables by using the generated sql file.<br>
Assuming you are still in the 'generator' folder, and assuming this is where you copied the generated files to <br>
<br>
For convenience I have created some [inserts.sql](https://github.com/cph-js284/Assignment11Database/blob/master/generator/inserts.sql) (this file is placed in the root of the 'generator' folder.)* <br>
2) Copy the files into the MySql container
```
sudo docker cp ./inserts.sql mysql01:/inserts.sql
sudo docker cp ./Generated.sql mysql01:/Generated.sql
```
3) Enter the MySql container 
```
sudo docker exec -it mysql01 bash
```
4) Enter MySql
```
mysql -uroot -ptest1234
```
5) Source the files
```
source ./Generated.sql;
source ./inserts.sql;
```
<br>
That takes care of the MySql setup, you can exit out of the shell and container(leave it running) now
## ORM Setup <br>

1) Navigate to the ORM folder (placed in the root) and build the image
```
sudo docker build -t orm01 .
```
2) Run the image
```
sudo docker run --name ohohorm01 --link mysql01:mysql orm01
```

The output from the ORM have been captured in the file [output.txt](https://github.com/cph-js284/Assignment11Database/blob/master/Outputs/output.txt) found in the 'output' folder

# Cleanup
To remove the container 
```
sudo docker rm -f mysql01
```
*Consider resetting any read/write permission set*
