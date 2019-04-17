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
sudo docker run
```


