from dataclasses import dataclass
from datetime import datetime
from typing import Optional
from dataclasses_json import dataclass_json


@dataclass_json
@dataclass
class UserWorklogItemDto:
    task_code: str
    task_name: str
    start_period_date: datetime
    end_period_date: datetime
    time_spent_seconds: int

MappedUserWorklog = Optional[UserWorklogItemDto]
MappedUserWorklogs = list[MappedUserWorklog]