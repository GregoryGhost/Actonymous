import datetime

from jira.resources import Issue, Worklog
from jira import JIRA

import dateutil.parser
from jira_worklog_manager_domain import jira_dtos


def get_user_worklogs_info(
    user_worklog_info: jira_dtos.UserWorklogInfo,
) -> jira_dtos.UserWorklogs:
    map_issue = lambda issue: map_issues(
        issue, user_worklog_info.user_login, user_worklog_info.worklogs_data_range
    )
    user_worklogs = map(map_issue, user_worklog_info.issues)
    user_worklogs = map(map_user_worklog, user_worklogs)
    user_worklogs = list(user_worklogs)

    return user_worklogs


def get_total_task_hours(user_worklogs: jira_dtos.UserWorklogs) -> float:
    total_task_hours: list[jira_dtos.UserWorklogItemDto] = filter(
        lambda x: x is not None, user_worklogs
    )
    total_task_hours = map(lambda x: x.time_spent_seconds, total_task_hours)
    total_task_hours = sum(total_task_hours) / 3600

    return total_task_hours


def map_trimmered_issue_worklogs(issue: Issue, auth_jira: JIRA) -> Issue:
    have_no_trimmered_worklog = (
        issue.fields.worklog.maxResults > issue.fields.worklog.total
    )
    if have_no_trimmered_worklog:
        return issue

    worklogs = auth_jira.worklogs(issue.key)
    issue.fields.worklog.worklogs = worklogs

    return issue


def map_trimmered_issues_worklogs(
    issues: jira_dtos.FoundJiraIssues, auth_jira: JIRA
) -> jira_dtos.FoundJiraIssues:
    found_issues = map(
        lambda issue: map_trimmered_issue_worklogs(issue, auth_jira), issues
    )
    found_issues = list(found_issues)

    return found_issues


def get_worklog_date_range(
    start_worklog_date: str, end_worklog_date: str
) -> jira_dtos.DateRange:
    start_date = dateutil.parser.isoparse(start_worklog_date)

    one_full_day = datetime.timedelta(days=1) - datetime.timedelta(microseconds=1)
    end_date = dateutil.parser.isoparse(end_worklog_date) + one_full_day

    worklogs_date_range = jira_dtos.DateRange(start_date=start_date, end_date=end_date)

    return worklogs_date_range


def is_intersected(worklog: Worklog, date_range: jira_dtos.DateRange) -> bool:
    date = dateutil.parser.isoparse(worklog.created)

    return (
        date_range.start_date.timestamp()
        <= date.timestamp()
        <= date_range.end_date.timestamp()
    )


def get_user_worklogs(
    issue: Issue, user_login: str, date_range: jira_dtos.DateRange
) -> jira_dtos.JiraWorklogs:
    worklogs = issue.fields.worklog.worklogs
    worklogs = filter(
        lambda x: x.author.name == user_login and is_intersected(x, date_range),
        worklogs,
    )
    worklogs = map(
        lambda x: jira_dtos.UserWorklogInfo(  # TODO: split on different function
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


def get_worklogs_period(worklogs: jira_dtos.JiraWorklogs) -> jira_dtos.WorklogPeriod:
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


def map_user_worklog(user_worklog: jira_dtos.JiraIssue) -> jira_dtos.MappedUserWorklog:
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
