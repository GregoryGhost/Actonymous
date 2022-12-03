from dataclasses import dataclass
from typing import final
from jira_dtos import UserWorklogItemDto


@final
@dataclass
class ActRecord:
    column_number: int
    service_name: str
    time_spent_in_hours: float


ActRecords = list[ActRecord]


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


@final
@dataclass
class ActReport:
    act: ContractInfo
    contract: ContractInfo
    supplementary_agreement: ContractInfo
    work_info: WorkInfo
    act_records: ActRecords
    contract_parties_info: ContractPartiesInfo


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