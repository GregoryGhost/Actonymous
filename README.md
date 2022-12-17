# How to work with JiraWorklogManager
## Install gRPC tools
```
> python -m pip install grpcio
> python -m pip install grpcio-tools
> pip3 install mypy-protobuf
```
[For more details](https://grpc.io/docs/languages/python/quickstart/)
[For mor details mypy protobuff](https://github.com/nipunn1313/mypy-protobuf)

## Generate gRPC code
Change current folder to the root project folder. After enter the next command:
```
> py -m grpc_tools.protoc -I . --python_out=./APIs/JiraWorklogManager/jira_worklog_manager/generated/grpc --grpc_python_out=./APIs/JiraWorklogManager/jira_worklog_manager/generated/grpc ./protos/API/jira_worklog_manager.proto
> py -m grpc_tools.protoc -I . --python_out=./APIs/DocsReporter/docs_reporter/generated/grpc --grpc_python_out=./APIs/DocsReporter/docs_reporter/generated/grpc ./protos/API/docs_reporter.proto
```
It's to generate Mypy typing for gRPC code:
```
> protoc --python_out=./APIs/JiraWorklogManager/jira_worklog_manager/generated/grpc --mypy_out=./APIs/JiraWorklogManager/jira_worklog_manager/generated/grpc ./protos/API/jira_worklog_manager.proto
> protoc --python_out=./APIs/DocsReporter/docs_reporter/generated/grpc --mypy_out=./APIs/DocsReporter/docs_reporter/generated/grpc ./protos/API/docs_reporter.proto
```