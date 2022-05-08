# IBook backend

# Run server

## Run with `docker` and `docker-compose`

### Run in local machine

1. Clone this repo:

```bash
git clone https://github.com/DaniinXorchenabo/social_network_backend.git 
```

2. go over to a target folder:

```bach
cd social_network_backend/socialNetwork_backend
```

3. Create a environ variables file:
   1. Copied a `social_network_backend/socialNetwork_backend/example.env` file and paste as a `social_network_backend/socialNetwork_backend/.env` file.
   2. Open `social_network_backend/socialNetwork_backend/.env`
   3. Edit environ variables in this file, add values for not initializing variables. As example:
        
       ```
       DB_DATABASE_NAME=my_database_name
       PGADMIN_DEFAULT_EMAIL=MyEmail@domen.com
       EXTERNAL_BACKEND_PORT=8000
       ```
         end ect.
4. (*Optional) Build server image.

     If you wont to get latest server version, run it:
     ```bash
     docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml -f .\build.docker-compose.yml build
     ```
5. Run server:
       
      If you wont to run containers in the background and don't see logs, would:
      ```bash
      docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml up -d db backend
      ```
      If you watch to logs in real time.
      ```bash
      docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml up db backend
      ```
               
      Run and build in ones command
      ```bash
      docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml -f .\build.docker-compose.yml up --build db backend
      ```
6. Check it: [http://localhost:<port from `EXTERNAL_BACKEND_PORT` in `.env` file>/swagger/index.html]() or [http://<Your `IP` in local network>:<port from `EXTERNAL_BACKEND_PORT` in `.env` file>/swagger/index.html]()
7. If you wont to stop the server, you would:
   
     ```bash
     docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml down
     ```
    
# Update the server

## Update the server, which running with a `docker`

### Update in local machine
1. Update you the local repo:
    ```bash
   cd <path/to/root/project/dir>/social_network_backend
   git pull origin master
    ```
2. Build server:
   ```bash
   docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml -f .\build.docker-compose.yml build
     ```
3. Stop  running last version server:
    ```bash
    docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml down
    ```
4. Run latest version server:
      ```bash
      docker-compose -f .\docker-compose.yml -f .\proxy.docker-compose.yml up -d db backend
      ```

# Tutorials

## How to use a `Swagger`

A simple usage

https://user-images.githubusercontent.com/45897837/167266853-1314e0a9-2158-47f2-8e70-f36e99a121ab.mp4

-----------------

Create a user from `Swagger`

https://user-images.githubusercontent.com/45897837/167267072-3d38d485-8bed-479a-b5c9-1256aa232822.mp4

-----------------

Autorize in system

https://user-images.githubusercontent.com/45897837/167267167-f10736e9-647c-43f3-88db-e55124a91507.mp4

-----------------

Autorization with roles

https://user-images.githubusercontent.com/45897837/167267344-703f3fcf-43de-44c7-8f6e-30c02104da4b.mp4

-----------------

Autorization token in http headers

https://user-images.githubusercontent.com/45897837/167267516-a2fdbdd5-1a24-4517-b971-5b7621309eea.mp4

-----------------
