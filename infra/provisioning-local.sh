echo "Provisioning Kind Clusters"
kind create cluster --name employees-cluster

echo "Pull Images"
docker pull quay.io/metallb/controller:v0.13.7
docker pull quay.io/metallb/speaker:v0.13.7
docker pull k8s.gcr.io/metrics-server/metrics-server:v0.6.2
docker pull mcr.microsoft.com/mssql/server:2019-CU16-ubuntu-20.04
docker pull richiebono/bono-employees-employeeapi:latest
docker pull richiebono/employees-frontend:latest

echo "Load Images into Kind"
kind load docker-image quay.io/metallb/controller:v0.13.7 --name employees-cluster
kind load docker-image quay.io/metallb/speaker:v0.13.7 --name employees-cluster
kind load docker-image k8s.gcr.io/metrics-server/metrics-server:v0.6.2 --name employees-cluster
kind load docker-image mcr.microsoft.com/mssql/server:2019-CU16-ubuntu-20.04 --name employees-cluster
kind load docker-image richiebono/bono-employees-employeeapi:latest --name employees-cluster
kind load docker-image richiebono/employees-frontend:latest --name employees-cluster

echo "Provisioning MetalLB"
sh ./k8s/metallb/metallb.sh

echo "Provisioning Metrics Server"
sh ./k8s/metrics-server/metrics-server.sh

echo "Provisioning Nginx"
sh ./k8s/nginx/nginx.sh

echo "Provisioning MSSQL"
sh ./k8s/mssql/mssql.sh

# echo "Create Namespaces"
# kubectl apply -f ./k8s/apps/namespaces/

echo "Create All ConfigMaps"
kubectl apply -f ./k8s/apps/configmaps/

echo "Create All deployments"
kubectl apply -f ./k8s/apps/deployments/

echo "Create All Services"
kubectl apply -f ./k8s/apps/services/

echo "Create All HPA"
kubectl apply -f ./k8s/apps/hpa/

# echo "Create All Ingress"
# kubectl apply -f ./k8s/apps/ingress/