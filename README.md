# Introduction 
Employee Management System Project using .NetCore on backend APIs, React and React Admin on frontend, validation using SonarQube.

## Docker

Install docker on windows.

`$ choco install docker-cli`

Install docker on Ubuntu.

apt get update

`$ sudo apt-get update`

install

`$ sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin`

Start it

`$ sudo systemctl start docker`

Install docker on Mac with Brew

`$ brew install --cask docker`

Install docker on windows.

`$ choco install docker-cli`

## Running the app

There is a `docker-compose.yml` file for starting SQLSERVER, API, and Frontend.

`$ docker compose up`

You can stop the Docker container, after your tests, using:

`$ docker compose down`

IMPORTANT: If you change something on the project you need to execute the following command before "docker compose up".

`$ docker compose build` 


## Url Swagger for API Documentation

```
http://localhost:8080/api/swagger
```

## Frontend URL

```
http://localhost
```

## Test K8S local Deploy

Access the infra folder:

`$ cd infra`

Install Kind To Create the Cluster

On macOS via Homebrew:

`$ brew install kind`

On Windows via Chocolatey

`$ choco install kind`

On Linux (Installing From Release Binaries):

`$ curl -Lo ./kind https://kind.sigs.k8s.io/dl/v0.17.0/kind-linux-amd64`
`$ chmod +x ./kind`
`$ sudo mv ./kind /usr/local/bin/kind`


Now Install Helm:

From Homebrew (macOS)
Members of the Helm community have contributed a Helm formula build to Homebrew. This formula is generally up to date.

`$ brew install helm`

From Chocolatey (Windows)
Members of the Helm community have contributed a Helm package build to Chocolatey. This package is generally up to date.

`$ choco install kubernetes-helm`

From Apt (Debian/Ubuntu)

`$ curl https://baltocdn.com/helm/signing.asc | gpg --dearmor | sudo tee /usr/share/keyrings/helm.gpg > /dev/null`

`$ sudo apt-get install apt-transport-https --yes`

`$ echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/helm.gpg] https://baltocdn.com/helm/stable/debian/ all main" | sudo tee /etc/apt/sources.list.d/helm-stable-debian.list`

`$ sudo apt-get update`

`$ sudo apt-get install helm`

Execute the command to provision the environment:

`$ sh ./provisioning-local.sh`

Wait for the pods to be ready:

`$ kubectl get pods -w`

Check the services:

`$ kubectl get svc`

if external ip is pending, execute the follow command:

`$ docker network inspect -f '{{.IPAM.Config}}' kind`

then execute the following command to change the IP address on metallb-configmap.yaml file using this IP that you got from the previous command, and execute the following command:

`$ kubectl apply -f metallb-configmap.yaml`

Forward the ports to access the frontend and backend:

`$ kubectl port-forward svc/employees-backend 8080:80`

Open a new terminal and execute the following command:

`$ kubectl port-forward svc/employees-frontend 80:80`

Access the API URL:

```

http://localhost:8080/api/swagger/index.html

```

Use the endpoint "authenticate" to get the token
Important: it will create the database and tables automatically.

```
/api/Users/authenticate

```

Use the following credentials to authenticate for the first time on the backend API.

email: richiebono@gmail.com
password: admin@123

Access the frontend URL:

```

http://localhost

```

You can use the same credentials from the backend to login on frontend.


## Destroy the kind cluster, and all of its resources:

`$ kind delete cluster --name employees-cluster`

## Exmplanation of the project video

```
https://www.youtube.com/watch?v=B1GKypCpouE
```


## Contact:

```
https://www.linkedin.com/in/richard-bono-75418818/
```