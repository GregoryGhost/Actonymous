from dataclasses import dataclass
from typing import final

from ...DTOs import ContractInfo, ContractPartiesInfo, WorkInfo


@final
@dataclass
class ActRecord:
    column_number: int
    service_name: str
    time_spent_in_hours: float


ActRecords = list[ActRecord]

@final
@dataclass
class ActReport:
    act: ContractInfo
    contract: ContractInfo
    supplementary_agreement: ContractInfo
    work_info: WorkInfo
    act_records: ActRecords
    contract_parties_info: ContractPartiesInfo
