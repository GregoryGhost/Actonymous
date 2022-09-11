import generated.grpc.jira_worklog_manager_pb2 as jira_worklog_manager_pb2
import generated.grpc.jira_worklog_manager_pb2_grpc as jira_worklog_manager_pb2_grpc


class JiraWorklogManager(jira_worklog_manager_pb2_grpc.JiraWorklogManagerServicer):
    def ListProducts(
        self, request: jira_worklog_manager_pb2.UserWorklogDto, context
    ) -> jira_worklog_manager_pb2.UserWorklogsDto:
        # TODO: implement
        return jira_worklog_manager_pb2.UserWorklogsDto()
