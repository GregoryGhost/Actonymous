FROM ubuntu:bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install python3 python3-pip -y
RUN pip3 install grpcio
RUN pip3 install grpcio-tools
RUN pip3 install hatch
RUN pip3 install --upgrade setuptools

WORKDIR /src
COPY ["APIs/JiraWorklogManager/jira-worklog-manager-domain/", "jira-worklog-manager-domain/"]
COPY ["APIs/JiraWorklogManager/jira_worklog_manager/", "jira_worklog_manager/"]
COPY ["APIs/JiraWorklogManager/pyproject.toml", "pyproject.toml"]
COPY ["protos/", "protos/"]

ENV LC_ALL C.UTF-8
ENV LANG C.UTF-8
RUN hatch build -e --user ./jira-worklog-manager-domain

RUN pip3 install ./jira-worklog-manager-domain

RUN python3 -m grpc_tools.protoc -I . \
    --python_out=./jira_worklog_manager/generated/grpc \
    --grpc_python_out=./jira_worklog_manager/generated/grpc \
    ./protos/API/jira_worklog_manager.proto

CMD ["python3", "-u", "jira_worklog_manager/__main__.py"]