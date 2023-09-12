FROM ubuntu:bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install python3 python3-pip -y
RUN pip install grpcio
RUN pip install grpcio-tools

COPY ["docs-reporter-domain/", "docs-reporter-domain/"]
COPY ["docs_reporter/", "docs_reporter/"]

RUN pip install ./docs-reporter-domain/
RUN py -m grpc_tools.protoc -I . \
    --python_out=./APIs/DocsReporter/docs_reporter/generated/grpc \
    --grpc_python_out=./APIs/DocsReporter/docs_reporter/generated/grpc \
    ./protos/API/docs_reporter.proto

ENTRYPOINT ["python", "docs_reporter/__main__.py"]