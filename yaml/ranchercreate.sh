kubectl config use-context local
kubectl delete -f dotnetwebapi-deploy.yaml || true
kubectl create -f dotnetwebapi-deploy.yaml --validate=false
kubectl apply -f dotnetwebapi-service.yaml --validate=false