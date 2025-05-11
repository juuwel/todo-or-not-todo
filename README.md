Secrets can be generated like:

```bash
kubectl create secret generic task-db-secret \
  --namespace=todo-app \
  --from-literal=POSTGRES_PASSWORD=<your-password> \
  --from-literal=POSTGRES_USER=<your-username> \
  --from-literal=POSTGRES_DB=<your-database>
```