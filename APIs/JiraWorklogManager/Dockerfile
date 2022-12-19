FROM ubuntu:bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install python3 python3-pip -y
RUN pip install grpcio
RUN pip install grpcio-tools

COPY ["jira-worklog-manager-domain/", "jira-worklog-manager-domain/"]
COPY ["jira-worklog-manager/", "jira-worklog-manager/"]



RUN pip install ./jira-worklog-manager-domain/
RUN py -m grpc_tools.protoc -I . \
    --python_out=./APIs/JiraWorklogManager/jira_worklog_manager/generated/grpc \
    --grpc_python_out=./APIs/JiraWorklogManager/jira_worklog_manager/generated/grpc \
    ./protos/API/jira_worklog_manager.proto

ENTRYPOINT ["python", "jira_worklog_manager/__main__.py"]