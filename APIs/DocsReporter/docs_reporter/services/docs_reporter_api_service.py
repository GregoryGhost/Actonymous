import generated.grpc.protos.API.docs_reporter_pb2 as docs_reporter_pb2
import generated.grpc.protos.API.docs_reporter_pb2_grpc as docs_reporter_pb2_grpc

class DocsReporterApiService(docs_reporter_pb2_grpc.DocsReporterServicer):
    def GetReportDocs(self, request: docs_reporter_pb2.UserReportingDataDto, context) -> docs_reporter_pb2.ReportDocsInfoDto:
        # TODO: implement
        return docs_reporter_pb2.ReportDocsInfoDto()