To generate task-db-config:

```bash
kubectl create configmap task-db-config --from-env-file=.env.task -n todo-app
```

To generate user-db-config:
```bash
kubectl create configmap user-db-config --from-env-file=.env.user -n todo-app
```