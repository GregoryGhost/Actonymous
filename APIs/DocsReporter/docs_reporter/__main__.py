from concurrent import futures
import logging

import grpc
from services import (docs_reporter_api_service, mock_report_data_service)
import generated.grpc.protos.API.docs_reporter_pb2_grpc as docs_reporter_pb2_grpc


def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    docs_reporter_pb2_grpc.add_JiraWorklogManagerServicer_to_server(docs_reporter_api_service.DocsReporterApiService(), server)
    server.add_insecure_port("[::]:80")
    server.start()
    server.wait_for_termination()


if __name__ == "__main__":
    logging.basicConfig()
    serve()
