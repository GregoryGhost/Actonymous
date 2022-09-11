# How to work with JiraWorklogManager
## Install gRPC tools
```
> python -m pip install grpcio
> python -m pip install grpcio-tools
```
[For more details](https://grpc.io/docs/languages/python/quickstart/)

## Generate gRPC code
Change current folder to the root project folder. After enter the next command:
```
> python -m grpc_tools.protoc --proto_path=./protos/API/ jira_worklog_manager.proto --python_out=./APIs/JiraWorklogManager/generated/grpc --grpc_python_out=./APIs/JiraWorklogManager/generated/grpc --mypy_out=./APIs/JiraWorklogManager/generated/proto_typing
```