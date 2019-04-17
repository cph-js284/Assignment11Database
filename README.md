# Assignment11Database
This is the 11th. assignment for PBA Database soft2019spring

# what it is
This is 2 C# projects;
One of them, called 'generator', is the project resposible for generating the class-sources file and the sql file.<br>
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
```
sudo docker cp mygen01:/app/Generated.sql ./Generated.sql
sudo docker cp mygen01:/app/Generated.cs ./Generated.cs
```
NB you might get a permission denied when doing this copy, if that is the case you can grant permission 
```
sudo chmod  a+rw
```
*Alternatively you can run the generator container in detached mode (-d) and enter it using bash, and then copy out the 2 files to your host - or even better you can just check out the files here [Generated.sql](https://github.com/cph-js284/Assignment11Database/blob/master/Outputs/Generated.sql) and [Generated.cs](https://github.com/cph-js284/Assignment11Database/blob/master/Outputs/Generated.cs)* <br>
