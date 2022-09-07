from datetime import datetime
import json
from typing import List, Tuple, final


@final
class UserWorklogInfo:
    comment: str = ""
    created_date: datetime = None  # type: ignore
    time_spent_seconds: int = 0

    def __init__(self, comment: str, created_date: datetime, time_spent_seconds: int):
        self.comment = comment
        self.created_date = created_date
        self.time_spent_seconds = time_spent_seconds


@final
class UserWorklogInfoEncoder(json.JSONEncoder):
    def default(self, o: UserWorklogInfo):
        return o.__dict__


JiraWorklogs = List[UserWorklogInfo]


@final
class JiraIssue:
    code: str = ""
    name: str = ""
    worklogs: JiraWorklogs = []

    def __init__(self, code: str, name: str, worklogs: JiraWorklogs):
        self.code = code
        self.name = name
        self.worklogs = worklogs


@final
class JiraIssueEncoder(json.JSONEncoder):
    def default(self, o: JiraIssue):
        return o.__dict__


JiraBasicAuth = Tuple[str, str]


@final
class JiraInfo:
    jira_server_address: str = ""
    basic_auth: JiraBasicAuth = ("login", "password")
    user_login: str = ""

    def __init__(
        self, user_login: str, jira_server_address: str, basic_auth: JiraBasicAuth
    ):
        self.user_login = user_login
        self.jira_server_address = jira_server_address
        self.basic_auth = basic_auth


@final
class UserWorklogItemDto:
    task_code: str = ""
    task_name: str = ""
    start_period_date: datetime = None  # type: ignore
    end_period_date: datetime = None  # type: ignore
    time_spent_seconds: int = 0

    def __init__(
        self,
        task_code: str,
        task_name: str,
        start_period_date: datetime,
        end_period_date: datetime,
        time_spent_seconds: int,
    ):
        self.task_code = task_code
        self.task_name = task_name
        self.start_period_date = start_period_date
        self.end_period_date = end_period_date
        self.time_spent_seconds = time_spent_seconds


@final
class UserWorklogItemDtoEncoder(json.JSONEncoder):
    def default(self, o: UserWorklogItemDto):
        return o.__dict__
