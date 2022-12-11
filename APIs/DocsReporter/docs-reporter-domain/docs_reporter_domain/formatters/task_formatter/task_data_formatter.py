from dataclasses import dataclass
from typing import Any, final

from ..tex_helpers import ITemplateInfo

from .task_dtos import TaskRecord, TaskRecords, TaskReport
from jira_dtos import MappedUserWorklogs, UserWorklogItemDto


def map_task_record(worklog: UserWorklogItemDto, index: int) -> TaskRecord:
    service_name = f"{worklog.task_code}. {worklog.task_name}"
    start_period = worklog.start_period_date.strftime("%d.%m.%y")
    end_period = worklog.end_period_date.strftime("%d.%m.%y")
    validity = f"{start_period} – {end_period}"
    index = index + 1
    record = TaskRecord(
        column_number=index, service_name=service_name, validity=validity
    )

    return record


def format_task_records(worklogs: MappedUserWorklogs) -> TaskRecords:
    filtered = filter(None, worklogs)
    enumerated = enumerate(filtered)
    task_records = map(lambda x: map_task_record(x[1], x[0]), enumerated)
    task_records = list(task_records)

    return task_records


def format_task_report(data: ITemplateInfo) -> TaskReport:
    task_records = format_task_records(data.worklogs)
    report = TaskReport(
        task=data.contracts_info.supplementary_agreement,
        contract=data.contracts_info.contract,
        supplementary_agreement=data.contracts_info.supplementary_agreement,
        work_info=data.contracts_info.work_info,
        task_records=task_records,
        contract_parties_info=data.contract_parties_info,
    )

    return report


@final
@dataclass
class TaskData(ITemplateInfo):
    def get_template_data(self) -> object:
        return format_task_report(self)