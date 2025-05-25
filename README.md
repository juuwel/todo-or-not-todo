Secrets can be generated like:

```bash
kubectl create secret generic task-db-secret \
  --namespace=todo-app \
  --from-env-file=taskms.env
```