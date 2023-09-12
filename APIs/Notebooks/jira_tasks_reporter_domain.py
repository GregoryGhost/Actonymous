from jira import JIRA
from jira_worklog_manager_domain import (
    JiraInfo,
    UserWorklogsInfo,
    get_worklog_date_range,
    jira_dtos,
    map_trimmered_issues_worklogs,
)
from dataclasses import dataclass
from typing import Optional, final
from dataclasses_json import dataclass_json
from jira_worklog_manager_domain import get_worklogs_period, map_issues
import dateutil.parser
from datetime import datetime, timedelta


@dataclass_json
@dataclass
class UserWorklog:
    task_code: str = ""
    comment: str = ""
    worklog_time: int = 0


MappedUserWorklog = Optional[UserWorklog]

UserWorklogs = list[UserWorklog]


@final
@dataclass
class DateRange:
    start_date: str
    end_date: str


def get_user_worklogs_info(
    user_worklog_info: jira_dtos.UserWorklogInfo,
) -> UserWorklogs:
    map_issue = lambda issue: map_issues(
        issue, user_worklog_info.user_login, user_worklog_info.worklogs_data_range
    )
    user_worklogs = map(map_issue, user_worklog_info.issues)
    user_worklogs = map(map_user_worklog, user_worklogs)
    user_worklogs = filter(lambda x: x is not None, user_worklogs)
    user_worklogs = list(user_worklogs)

    return user_worklogs


def map_user_worklog(user_worklog: jira_dtos.JiraIssue) -> MappedUserWorklog:
    period = get_worklogs_period(user_worklog.worklogs)

    if period is None:
        return None

    comments = map(lambda x: x.comment, user_worklog.worklogs)
    comment = ", ".join(comments)
    worklog_time = map(lambda x: x.time_spent_seconds, user_worklog.worklogs)  # type: ignore
    worklog_time = sum(worklog_time)
    user_worklog_item = UserWorklog(user_worklog.code, comment, worklog_time)

    return user_worklog_item


def format_task_link(user_worklog: UserWorklog, jira_info: JiraInfo) -> str:
    task_jira_link = f"{jira_info.jira_server_address}browse/{user_worklog.task_code}"
    task_link = f"[{user_worklog.task_code}]({task_jira_link})"

    return task_link


def format_work_msg(user_worklog: UserWorklog, jira_info: JiraInfo) -> str:
    task_link = format_task_link(user_worklog, jira_info)
    msg = f"{user_worklog.comment} ({task_link})"

    return msg


def format_total_time_for_period_by_tasks(
    user_worklogs: UserWorklogs, jira_info: JiraInfo
) -> str:
    info = "Worklogs:\n"
    for user_worklog in user_worklogs:
        worklog_time = user_worklog.worklog_time / 3600
        task_link = format_task_link(user_worklog, jira_info)
        info += f"- {task_link} ({worklog_time}h);\n"

    total_work_time = map(lambda x: x.worklog_time, user_worklogs)
    total_work_time = sum(total_work_time) / 3600
    info += f"Total work time for period: {total_work_time}h\n"

    return info


def get_work_report(user_worklogs: UserWorklogs, jira_info: JiraInfo) -> str:
    report_msg = "вчера "

    len_user_worklogs = len(user_worklogs)

    if len_user_worklogs == 0:
        return ""

    total_time_for_period = format_total_time_for_period_by_tasks(user_worklogs, jira_info) + "\n"
    report_msg = total_time_for_period + report_msg

    if len_user_worklogs == 1:
        report_msg += format_work_msg(user_worklogs[0], jira_info) + "."

        return report_msg

    total_time_for_period = format_total_time_for_period_by_tasks(user_worklogs, jira_info) + "\n"
    report_msg = total_time_for_period + "вчера:\n"
    for user_worklog in user_worklogs[:-1]:
        formatted_msg = format_work_msg(user_worklog, jira_info)
        report_msg += f"- {formatted_msg};\n"

    formatted_msg = format_work_msg(user_worklogs[-1], jira_info)
    report_msg += f"- {formatted_msg}."

    return report_msg


def get_today() -> DateRange:
    today_date = datetime.today().strftime("%Y-%m-%d")
    worklogs_data_range = DateRange(start_date=today_date, end_date=today_date)
    # worklogs_data_range = DateRange(start_date='2023-07-06', end_date='2023-07-06')

    return worklogs_data_range

def get_yesterday() -> DateRange:
    yesterday = datetime.today() - timedelta(days = 1)
    yesterday_date = yesterday.strftime("%Y-%m-%d")
    worklogs_data_range = DateRange(start_date=yesterday_date, end_date=yesterday_date)

    return worklogs_data_range


def get_full_work_report_for_today() -> str:
    worklog_range = get_today()
    report = get_full_work_report(worklog_range)

    return report

def get_full_work_report_for_yesterday() -> str:
    worklog_range = get_yesterday()
    report = get_full_work_report(worklog_range)

    return report

def get_date_range() -> DateRange:
    start_date = '2023-07-13'
    end_date = '2023-07-15'
    worklogs_data_range = DateRange(start_date=start_date, end_date=end_date)

    return worklogs_data_range

def get_full_work_report_for_date() -> str:
    worklog_range = get_date_range()
    report = get_full_work_report(worklog_range)

    return report

def get_full_work_report(worklog_date_range: DateRange) -> str:
    # NOTE: sensitive data
    user_login = "login here"
    user_jira_password = "secret password"
    jira_info = JiraInfo(
        jira_server_address="https://jira.ru/",
        basic_auth=(user_login, user_jira_password),
        user_login=user_login,
    )
    # -----
    auth_jira = JIRA(
        basic_auth=jira_info.basic_auth,
        options={"server": jira_info.jira_server_address},
    )
    start_worklog_date = worklog_date_range.start_date
    end_worklog_date = worklog_date_range.end_date
    jql = f'worklogAuthor in ("{jira_info.user_login}") and worklogDate >= {start_worklog_date} and worklogDate <= {end_worklog_date}'
    found_issues = auth_jira.search_issues(jql)
    found_issues = map_trimmered_issues_worklogs(found_issues, auth_jira)

    worklogs_data_range = get_worklog_date_range(start_worklog_date, end_worklog_date)
    user_worklog_info = UserWorklogsInfo(
        issues=found_issues,
        worklogs_data_range=worklogs_data_range,
        user_login=user_login,
    )
    user_worklogs = get_user_worklogs_info(user_worklog_info)

    report_msg = get_work_report(user_worklogs, jira_info)

    return report_msg
