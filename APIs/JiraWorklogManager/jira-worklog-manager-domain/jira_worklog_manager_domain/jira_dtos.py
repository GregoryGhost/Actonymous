from dataclasses import dataclass, field
from datetime import datetime
from typing import Any, Dict, List, Optional, Tuple, final
from dataclasses_json import dataclass_json
from jira.resources import Issue
from jira.client import ResultList


@final
@dataclass
class UserWorklogInfo:
    comment: str = ""
    created_date: datetime = None  # type: ignore
    time_spent_seconds: int = 0


JiraWorklogs = List[UserWorklogInfo]


@final
@dataclass
class JiraIssue:
    code: str = ""
    name: str = ""
    worklogs: JiraWorklogs = field(default_factory=list) 


JiraBasicAuth = Tuple[str, str]


@final
@dataclass
class JiraInfo:
    jira_server_address: str = ""
    basic_auth: JiraBasicAuth = ("login", "password")
    user_login: str = ""


@dataclass_json
@dataclass
class UserWorklogItemDto:
    task_code: str
    task_name: str
    start_period_date: datetime
    end_period_date: datetime
    time_spent_seconds: int


@final
@dataclass
class DateRange:
    start_date: datetime
    end_date: datetime


FoundJiraIssues = Dict[str, Any] | ResultList[Issue]

WorklogPeriod = Optional[Tuple[datetime, datetime]]

MappedUserWorklog = Optional[UserWorklogItemDto]

UserWorklogs = list[MappedUserWorklog]


@final
@dataclass
class UserWorklogsInfo:
    issues: FoundJiraIssues
    worklogs_data_range: DateRange
    user_login: str
