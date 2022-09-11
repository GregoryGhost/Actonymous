"""The Python implementation of the GRPC helloworld.Greeter server."""

from concurrent import futures
import logging

import grpc
import services.jira_worklog_manager as jira_worklog_manager
import generated.grpc.jira_worklog_manager_pb2_grpc as jira_worklog_manager_pb2_grpc


def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    jira_worklog_manager_pb2_grpc.add_JiraWorklogManagerServicer_to_server(jira_worklog_manager.JiraWorklogManager(), server)
    server.add_insecure_port("[::]:50051")
    server.start()
    server.wait_for_termination()


if __name__ == "__main__":
    logging.basicConfig()
    serve()
