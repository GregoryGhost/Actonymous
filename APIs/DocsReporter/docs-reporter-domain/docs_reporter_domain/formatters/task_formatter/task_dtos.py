from dataclasses import dataclass
from typing import final

from template_dtos import ContractInfo, ContractPartiesInfo, WorkInfo


@final
@dataclass
class TaskRecord:
    column_number: int
    service_name: str
    validity: str


TaskRecords = list[TaskRecord]

@final
@dataclass
class TaskReport:
    task: ContractInfo
    contract: ContractInfo
    supplementary_agreement: ContractInfo
    work_info: WorkInfo
    task_records: TaskRecords
    contract_parties_info: ContractPartiesInfo
