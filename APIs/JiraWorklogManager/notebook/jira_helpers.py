from dataclasses import dataclass
from datetime import datetime
from datetime import datetime
from typing import Any, Optional, Tuple, final
import jira_dtos

from jira.resources import Issue

import dateutil.parser

def is_intersected(worklog: Any, date_range: jira_dtos.DateRange) -> bool:
    date = dateutil.parser.isoparse(worklog.created)

    return date_range.start_date.timestamp() <= date.timestamp() <= date_range.end_date.timestamp()


def get_user_worklogs(
    issue: Issue, user_login: str, date_range: jira_dtos.DateRange
) -> jira_dtos.JiraWorklogs:
    worklogs = issue.fields.worklog.worklogs
    worklogs = filter(
        lambda x: x.author.name == user_login and is_intersected(x, date_range),
        worklogs,
    )
    worklogs = map(
        lambda x: jira_dtos.UserWorklogInfo(#TODO: split on different function
            comment=x.comment,
            created_date=dateutil.parser.isoparse(x.created),
            time_spent_seconds=int(x.timeSpentSeconds),
        ),
        worklogs,
    )
    worklogs = list(worklogs)

    return worklogs


def map_issues(
    issue: Issue, user_login: str, data_range: jira_dtos.DateRange
) -> jira_dtos.JiraIssue:
    issue_code = issue.key
    issue_name = issue.fields.summary
    worklogs = get_user_worklogs(issue, user_login, data_range)
    # json_worklogs = json.dumps(worklogs, cls = UserWorklogInfoEncoder)
    mapped = jira_dtos.JiraIssue(code=issue_code, name=issue_name, worklogs=worklogs)

    return mapped


WorklogPeriod = Optional[Tuple[datetime, datetime]]


def get_worklogs_period(worklogs: jira_dtos.JiraWorklogs) -> WorklogPeriod:
    if not worklogs:
        return None

    select_dates = lambda x: x.created_date
    start_period = min(worklogs, key=select_dates).created_date
    end_period = max(worklogs, key=select_dates).created_date

    return (start_period, end_period)


def get_time_spent(worklogs: jira_dtos.JiraWorklogs) -> int:
    times = map(lambda x: x.time_spent_seconds, worklogs)
    time_spent = sum(times)

    return time_spent


MappedUserWorklog = Optional[jira_dtos.UserWorklogItemDto]


def map_user_worklog(user_worklog: jira_dtos.JiraIssue) -> MappedUserWorklog:
    period = get_worklogs_period(user_worklog.worklogs)

    if period is None:
        return None

    start_period_date, end_period_date = period
    time_spent_seconds = get_time_spent(user_worklog.worklogs)
    user_worklog_item = jira_dtos.UserWorklogItemDto(
        task_code=user_worklog.code,
        task_name=user_worklog.name,
        start_period_date=start_period_date,
        end_period_date=end_period_date,
        time_spent_seconds=time_spent_seconds,
    )

    return user_worklog_item
