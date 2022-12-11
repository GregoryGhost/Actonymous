from dataclasses import dataclass
from typing import final

from ..tex_helpers import ITemplateInfo

from .act_dtos import ActRecord, ActRecords, ActReport

from ...DTOs import UserWorklogItemDto, MappedUserWorklogs

def map_act_record(worklog: UserWorklogItemDto, index: int) -> ActRecord:
    service_name = f"{worklog.task_code}. {worklog.task_name}"
    in_hours = worklog.time_spent_seconds / 3600
    column_number = index + 1
    record = ActRecord(
        column_number=column_number,
        service_name=service_name,
        time_spent_in_hours=in_hours,
    )

    return record


def format_act_records(worklogs: MappedUserWorklogs) -> ActRecords:
    filtered = filter(None, worklogs)
    enumerated = enumerate(filtered)
    act_records = map(lambda x: map_act_record(x[1], x[0]), enumerated)
    act_records = list(act_records)

    return act_records


def format_act_report(data: ITemplateInfo) -> ActReport:
    act_records = format_act_records(data.worklogs)
    report = ActReport(
        act=data.contracts_info.act,
        contract=data.contracts_info.contract,
        supplementary_agreement=data.contracts_info.supplementary_agreement,
        work_info=data.contracts_info.work_info,
        act_records=act_records,
        contract_parties_info=data.contract_parties_info,
    )

    return report

@final
@dataclass
class ActData(ITemplateInfo):
    def get_template_data(self) -> object:
        return format_act_report(self)