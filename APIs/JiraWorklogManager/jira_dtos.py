import json


class JiraIssueEncoder(json.JSONEncoder):
    def default(self, o):
        return o.__dict__ 


class JiraIssue:
    code = ''
    name = ''
    worklogs = []

    def __init__(self, code, name, worklogs):
        self.code = code
        self.name = name
        self.worklogs = worklogs


class JiraInfo:
    jira_server_address = ''
    basic_auth = ('login', 'password')
    user_login = ''

    def __init__(self, user_login, jira_server_address, basic_auth):
        self.user_login = user_login
        self.jira_server_address = jira_server_address
        self.basic_auth = basic_auth


class UserWorklogInfoEncoder(json.JSONEncoder):
    def default(self, o):
        return o.__dict__ 


class UserWorklogInfo:
    comment = ''
    created_date = None
    time_spent_seconds = 0

    def __init__(self, comment, created_date, time_spent_seconds):
        self.comment = comment
        self.created_date = created_date
        self.time_spent_seconds = time_spent_seconds


class UserWorklogItemDtoEncoder(json.JSONEncoder):
    def default(self, o):
        return o.__dict__ 


class UserWorklogItemDto:
    task_code = ''
    task_name = ''
    start_period_date = None
    end_period_date = None
    time_spent_seconds = 0

    def __init__(self, task_code, task_name, start_period_date, end_period_date, time_spent_seconds):
        self.task_code = task_code
        self.task_name = task_name
        self.start_period_date = start_period_date
        self.end_period_date = end_period_date
        self.time_spent_seconds = time_spent_seconds